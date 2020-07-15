namespace Unosquare.PiGpio.Workbench.Runners
{
    using ManagedModel;
    using NativeEnums;
    using Swan.Logging;
    using Swan.Threading;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading;

    internal class DhtSensor : RunnerBase
    {
        private readonly List<Tuple<bool, uint>> _pulses = new List<Tuple<bool, uint>>(1024);
        private uint _lastTick;
        private GpioPin _pin;

        public DhtSensor(bool isEnabled)
            : base(isEnabled) { }

        protected override void OnSetup()
        {
            _pin = Board.Pins[27];
            _pin.PullMode = GpioPullMode.Off;
            _pin.Alerts.TimeoutMilliseconds = 200;
            _lastTick = Board.Timing.TimestampTick;
            _pin.Alerts.Start((pin, level, tick) =>
            {
                // The DHT22 produces a total of 83 interrupts on the line which are interpreted as 5 bytes of data -
                // two bytes containing the humidity reading, two bytes containing the temperature reading, and a hash
                // which is the sum of the other four bytes.
                // The sensor starts by sending two preliminary signals, each ~80 us in length - it first pulls the line LOW,
                // then releases it back to HIGH.
                if (level == LevelChange.NoChange)
                    return;

                var elapsed = tick - _lastTick;
                var value = level != LevelChange.LowToHigh; // THE DHT signal is active-low
                var pulse = new Tuple<bool, uint>(value, elapsed);
                _pulses.Add(pulse);
                _lastTick = tick;
            });
        }

        protected override void DoBackgroundWork(CancellationToken ct)
        {
            while (ct.IsCancellationRequested == false)
            {
                // The DHT22 must be manually triggered with a specific signal as follows:
                // 1. Line is initially kept HIGH (it is active when it is LOW). 250 ms.
                _pin.Value = true;
                Board.Timing.Sleep(250);

                // 2. Pull the line LOW for at least 1 ms.In this driver, the triggering signal is set to 10 ms
                _pin.Value = false;
                Board.Timing.Sleep(18);

                // 3. Stop pulling LOW, allowing the line to return to HIGH and wait between 20 and 40 us.
                _pin.Value = true;

                // Board.Timing.SleepMicros(30);
                // time to read. The alrets should fire ok now.
                _pin.Direction = PinDirection.Input;

                // Reading the data
                _lastTick = Board.Timing.TimestampTick;

                // Wait for the data to be read.
                Board.Timing.Sleep(1000);

                DebugPulses(_pulses);
                var bits = DecodeBits(_pulses);
                if (bits.Count >= 40)
                {
                    DebugBits(bits);
                    var bitArray = new BitArray(bits.ToArray());
                    var bytes = new byte[bits.Count / 8];
                    bitArray.CopyTo(bytes, 0);

                    $"Bytes Received: {BitConverter.ToString(bytes).Replace("-", " ", StringComparison.InvariantCulture)}".Info(Name);

                    var humidity = BitConverter.ToUInt16(bytes, 0);
                    var temperature = BitConverter.ToUInt16(bytes, 2);
                    var checksum = (byte)(bytes[0] + bytes[1] + bytes[2] + bytes[3]);
                    $"Read Results - Humidity: {humidity / 10d:0.0} % | Temp: {temperature / 10d: 0.0} C | Computed CS: {checksum:X} vs RX: {bytes[4]:X}".Info(Name);
                }

                _pulses.Clear();

                // Wait 3 seconds for the next read.
                var startSleep = Board.Timing.TimestampSeconds;
                while (Board.Timing.TimestampSeconds - startSleep < 3.0d)
                {
                    if (ct.IsCancellationRequested)
                        break;

                    Board.Timing.Sleep(50);
                }
            }
        }

        protected override void Cleanup()
        {
            // TODO: Stopping the alerts for some reason hangs the thread...
            _pin.Alerts.Stop();
            Board.Timing.Sleep(500);
        }

        private static List<bool> DecodeBits(IReadOnlyList<Tuple<bool, uint>> pulses)
        {
            // For each bit, there are two signals - one preparatory (50 us LOW) and one containing the actual bit value.
            // The bit value is signalled by a HIGH signal, the length of the signal determines whether it is '0' or '1' - a '0'
            // is about 28 us and a '1' is about 70 us.
            if (pulses.Count < 82)
                return new List<bool>(0);

            var startPulseIndex = pulses.Count - 80 - 1;
            var dataBits = new List<bool>(5 * 8); // 40 bit is 8 bytes

            for (var pulseIndex = startPulseIndex; pulseIndex < pulses.Count; pulseIndex += 2)
            {
                var p0 = pulses[pulseIndex + 0];
                var p1 = pulses[pulseIndex + 1];
                dataBits.Add(p1.Item2 > 50);
                if (dataBits.Count >= 40)
                    break;
            }

            // dataBits.Reverse();
            return dataBits;
        }

        private void DebugPulses(IReadOnlyList<Tuple<bool, uint>> pulses)
        {
            var builder = new StringBuilder();

            for (var i = 0; i < pulses.Count; i++)
            {
                var p = pulses[i];
                builder.Append($"| {(p.Item1 ? "H" : "L")}: {p.Item2,12}   ");
                if ((i + 1) % 4 == 0)
                    builder.AppendLine("|");
            }

            $"Pulses Received: {pulses.Count}\r\n{builder}".Debug(Name);
        }

        private void DebugBits(IReadOnlyList<bool> bits)
        {
            var builder = new StringBuilder();

            for (var i = 0; i < bits.Count; i++)
            {
                builder.Append(bits[i] ? "1" : "0");
                if ((i + 1) % 4 == 0)
                    builder.Append(" ");
            }

            $"Bits: {bits.Count}\r\n{builder}".Debug(Name);
        }
    }
}
