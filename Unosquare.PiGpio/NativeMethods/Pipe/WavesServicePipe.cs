namespace Unosquare.PiGpio.NativeMethods.Pipe
{
    using System;
    using System.Linq;
    using System.Text;
    using Unosquare.PiGpio.NativeEnums;
    using Unosquare.PiGpio.NativeMethods.Interfaces;
    using Unosquare.PiGpio.NativeMethods.Pipe.Infrastructure;
    using Unosquare.PiGpio.NativeTypes;

    /// <summary>
    /// Waves Service Pipe strategy pattern.
    /// </summary>
    internal class WavesServicePipe : IWavesService
    {
        private PigpioPipe _pigpioPipe;

        public WavesServicePipe(PigpioPipe pipe)
        {
            _pigpioPipe = pipe;
        }

        public int GpioWaveClear()
        {
            _pigpioPipe.SendCommand($"wvclr");
            return 0;
        }

        public int GpioWaveAddNew()
        {
            _pigpioPipe.SendCommand($"wvnew");
            return 0;
        }

        public int GpioWaveAddGeneric(uint numPulses, GpioPulse[] pulses)
        {
            var builder = new StringBuilder();
            foreach (var pulse in pulses)
            {
                builder.Append(" 0x");
                builder.Append(Convert.ToString((int)pulse.GpioOn, 16));
                builder.Append(" 0x");
                builder.Append(Convert.ToString((int)pulse.GpioOff, 16));
                builder.Append(" ");
                builder.Append(pulse.DurationMicroSecs);
            }

            return _pigpioPipe.SendCommandWithIntResult($"wvag{builder}");
        }

        public int GpioWaveAddSerial(UserGpio userGpio, uint baudRate, uint dataBits, uint stopBits, uint offset, uint numBytes, byte[] str)
        {
            return _pigpioPipe.SendCommandWithIntResult($"wvas {(uint)userGpio} {baudRate} {dataBits} {stopBits} {offset}{str.Select(b => " 0x" + Convert.ToString(b, 16))}");
        }

        public int GpioWaveCreate()
        {
            return _pigpioPipe.SendCommandWithIntResult("wvcre");
        }

        public ResultCode GpioWaveDelete(uint waveId)
        {
            return _pigpioPipe.SendCommandWithResultCode($"wvdel {waveId}");
        }

        public int GpioWaveTxSend(uint waveId, WaveMode waveMode)
        {
            return _pigpioPipe.SendCommandWithIntResult($"wvtxm {waveId} {(int)waveMode}");
        }

        public ResultCode GpioWaveChain(byte[] buffer, uint bufferSize)
        {
            return _pigpioPipe.SendCommandWithResultCode($"wvcha{buffer.Select(b => " " + b)}");
        }

        public int GpioWaveTxAt()
        {
            return _pigpioPipe.SendCommandWithIntResult("wvtat");
        }

        public int GpioWaveTxBusy()
        {
            return _pigpioPipe.SendCommandWithIntResult("wvbsy");
        }

        public int GpioWaveTxStop()
        {
            _pigpioPipe.SendCommand("wvhlt");
            return 0;
        }

        public int GpioWaveGetMicros()
        {
            return _pigpioPipe.SendCommandWithIntResult("wvsm 0");
        }

        public int GpioWaveGetHighMicros()
        {
            return _pigpioPipe.SendCommandWithIntResult("wvsm 1");
        }

        public int GpioWaveGetMaxMicros()
        {
            return _pigpioPipe.SendCommandWithIntResult("wvsm 2");
        }

        public int GpioWaveGetPulses()
        {
            return _pigpioPipe.SendCommandWithIntResult("wvsp 0");
        }

        public int GpioWaveGetHighPulses()
        {
            return _pigpioPipe.SendCommandWithIntResult("wvsp 1");
        }

        public int GpioWaveGetMaxPulses()
        {
            return _pigpioPipe.SendCommandWithIntResult("wvsp 2");
        }

        public int GpioWaveGetCbs()
        {
            return _pigpioPipe.SendCommandWithIntResult("wvsc 0");
        }

        public int GpioWaveGetHighCbs()
        {
            return _pigpioPipe.SendCommandWithIntResult("wvsc 1");
        }

        public int GpioWaveGetMaxCbs()
        {
            return _pigpioPipe.SendCommandWithIntResult("wvsc 2");
        }
    }
}