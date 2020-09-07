namespace Unosquare.PiGpio.ManagedModel
{
    using NativeEnums;
    using NativeMethods;
    using NativeTypes;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    /// <summary>
    /// Provides methods to build and render waveforms.
    /// </summary>
    public sealed class WaveBuilder : IDisposable
    {
        private const string WaveAlreadyPreparedErrorMessage = "Wave was already prepared and cannot be modified.";
        private const string DisposedErrorMessage = "Wave was already disposed.";
        private static readonly object SyncLock = new object();
        private readonly List<GpioPulse> m_Pulses;
        private bool _isDisposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="WaveBuilder"/> class.
        /// </summary>
        internal WaveBuilder()
        {
            m_Pulses = new List<GpioPulse>(Board.Waves.MaxPulses);
        }

        /// <summary>
        /// Gets the wave identifier. Returns a negative number if the wave has not been prepared.
        /// </summary>
        public int WaveId { get; private set; } = -1;

        /// <summary>
        /// Gets a value indicating whether this wave is prepared in the DMA registers.
        /// </summary>
        public bool IsPrepared => WaveId >= 0;

        /// <summary>
        /// Gets a read-only collection of pulses.
        /// </summary>
        public ReadOnlyCollection<GpioPulse> Pulses => new ReadOnlyCollection<GpioPulse>(m_Pulses);

        /// <summary>
        /// Adds a pulse.
        /// </summary>
        /// <param name="pulse">The pulse.</param>
        /// <exception cref="InvalidOperationException">When the wave has been prepared.</exception>
        public void AddPulse(GpioPulse pulse)
        {
            if (IsPrepared)
                throw new InvalidOperationException(WaveAlreadyPreparedErrorMessage);

            m_Pulses.Add(pulse);
        }

        /// <summary>
        /// Adds a pulse.
        /// </summary>
        /// <param name="durationMicroSecs">The duration micro secs.</param>
        /// <param name="onPins">The on pins.</param>
        /// <param name="offPins">The off pins.</param>
        /// <exception cref="InvalidOperationException">When the wave has been prepared.</exception>
        public void AddPulse(int durationMicroSecs, IEnumerable<UserGpio> onPins, IEnumerable<UserGpio> offPins)
        {
            if (IsPrepared)
                throw new InvalidOperationException(WaveAlreadyPreparedErrorMessage);

            var onPinFlags = PinsToBitMask(onPins);
            var offPinFlags = PinsToBitMask(offPins);

            var pulse = new GpioPulse
            {
                DurationMicroSecs = Convert.ToUInt32(durationMicroSecs),
                GpioOn = onPinFlags,
                GpioOff = offPinFlags,
            };

            m_Pulses.Add(pulse);
        }

        /// <summary>
        /// Adds a pulse.
        /// </summary>
        /// <param name="durationMicroSecs">The duration micro secs.</param>
        /// <param name="onPins">The on pins.</param>
        /// <param name="offPins">The off pins.</param>
        public void AddPulse(int durationMicroSecs, IEnumerable<GpioPin> onPins, IEnumerable<GpioPin> offPins)
            => AddPulse(durationMicroSecs, GpioPinsToUserGpios(onPins), GpioPinsToUserGpios(offPins));

        /// <summary>
        /// Adds a pulse.
        /// </summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        /// <param name="durationMicroSecs">The duration micro secs.</param>
        /// <param name="pins">The pins.</param>
        /// <exception cref="InvalidOperationException">When the wave has been prepared.</exception>
        public void AddPulse(bool value, int durationMicroSecs, params UserGpio[] pins)
        {
            if (IsPrepared)
                throw new InvalidOperationException(WaveAlreadyPreparedErrorMessage);

            AddPulse(durationMicroSecs,
                value ? pins : null,
                value ? null : pins);
        }

        /// <summary>
        /// Adds a pulse.
        /// </summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        /// <param name="durationMicroSecs">The duration micro secs.</param>
        /// <param name="pins">The pins.</param>
        public void AddPulse(bool value, int durationMicroSecs, params GpioPin[] pins) =>
            AddPulse(value, durationMicroSecs, GpioPinsToUserGpios(pins).ToArray());

        /// <summary>
        /// Adds carrier pulses to the wave (useful for stuff like Infrared pulses).
        /// </summary>
        /// <param name="frequency">The frequency.</param>
        /// <param name="durationMicroSecs">The duration micro secs.</param>
        /// <param name="pins">The pins.</param>
        public void AddCarrierPulses(double frequency, double durationMicroSecs, params UserGpio[] pins) =>
            AddCarrierPulses(frequency, durationMicroSecs, 0.5, pins);

        /// <summary>
        /// Adds carrier pulses to the wave (useful for stuff like Infrared pulses).
        /// </summary>
        /// <param name="frequency">The frequency.</param>
        /// <param name="durationMicroSecs">The duration micro secs.</param>
        /// <param name="pins">The pins.</param>
        public void AddCarrierPulses(double frequency, double durationMicroSecs, params GpioPin[] pins) =>
            AddCarrierPulses(frequency, durationMicroSecs, GpioPinsToUserGpios(pins).ToArray());

        /// <summary>
        /// Adds carrier pulses to the wave (useful for stuff like Infrared pulses).
        /// </summary>
        /// <param name="frequency">The frequency.</param>
        /// <param name="durationMicroSecs">The duration micro secs.</param>
        /// <param name="dutyCycle">The duty cycle.</param>
        /// <param name="pins">The pins.</param>
        public void AddCarrierPulses(double frequency, double durationMicroSecs, double dutyCycle, params UserGpio[] pins)
        {
            var oneCycleTime = 1000000.0 / frequency; // 1000000 microseconds in a second
            var onDuration = (int)Math.Round(oneCycleTime * dutyCycle);
            var offDuration = (int)Math.Round(oneCycleTime * (1.0 - dutyCycle));

            var totalCycles = (int)Math.Round(durationMicroSecs / oneCycleTime);
            var totalPulses = totalCycles * 2;

            for (var i = 0; i < totalPulses; i++)
            {
                if (i % 2 == 0)
                    AddPulse(true, onDuration, pins); // High pulse
                else
                    AddPulse(false, offDuration, pins); // Low pulse
            }
        }

        /// <summary>
        /// Adds carrier pulses to the wave (useful for stuff like Infrared pulses).
        /// </summary>
        /// <param name="frequency">The frequency.</param>
        /// <param name="durationMicroSecs">The duration micro secs.</param>
        /// <param name="dutyCycle">The duty cycle.</param>
        /// <param name="pins">The pins.</param>
        public void AddCarrierPulses(double frequency, double durationMicroSecs, double dutyCycle, params GpioPin[] pins) =>
            AddCarrierPulses(frequency, durationMicroSecs, dutyCycle, GpioPinsToUserGpios(pins).ToArray());

        /// <summary>
        /// Clears all previously added pulses.
        /// </summary>
        /// <exception cref="InvalidOperationException">When the wave has been prepared.</exception>
        public void ClearPulses()
        {
            if (IsPrepared)
                throw new InvalidOperationException(WaveAlreadyPreparedErrorMessage);

            m_Pulses.Clear();
        }

        /// <summary>
        /// Prepares the waveform to be rendered by DMA.
        /// </summary>
        /// <exception cref="ObjectDisposedException">When the wave has been disposed.</exception>
        public void Prepare()
        {
            lock (SyncLock)
            {
                if (IsPrepared) return;
                if (_isDisposed) throw new ObjectDisposedException(DisposedErrorMessage);

                Waves.GpioWaveClear();
                BoardException.ValidateResult(
                    Waves.GpioWaveAddGeneric(Convert.ToUInt32(m_Pulses.Count), m_Pulses.ToArray()));
                WaveId = BoardException.ValidateResult(
                    Waves.GpioWaveCreate());
            }
        }

        /// <summary>
        /// Begins rendering the waveform pulses.
        /// Do not forget to set the pin direction/mode as an output pin.
        /// The wave is automatically prepared if it has not been prepared before.
        /// </summary>
        /// <param name="mode">The mode.</param>
        public void Send(WaveMode mode)
        {
            lock (SyncLock)
            {
                if (_isDisposed) throw new ObjectDisposedException(DisposedErrorMessage);
            }

            if (IsPrepared == false)
                Prepare();

            if (IsPrepared)
                BoardException.ValidateResult(Waves.GpioWaveTxSend(Convert.ToUInt32(WaveId), mode));
        }

        /// <inheritdoc />
        public void Dispose() => Dispose(true);

        /// <summary>
        /// Converts a collection of User GPIO pins to a bitmask.
        /// </summary>
        /// <param name="pins">The pins.</param>
        /// <returns>A bitmask with each pin as a position.</returns>
        private static BitMask PinsToBitMask(IEnumerable<UserGpio> pins)
        {
            int bitMask = 0;
            
            if (pins != null)
            {
                foreach (var pin in pins)
                    bitMask = bitMask.SetBit((int)pin, true);
            }

            return (BitMask)bitMask;
        }

        /// <summary>
        /// Converts GPIO pins to their corresponding GpioEnumeration.
        /// </summary>
        /// <param name="pins">The pins.</param>
        /// <returns>An array of UserGpio pins.</returns>
        private static IEnumerable<UserGpio> GpioPinsToUserGpios(IEnumerable<GpioPin> pins) =>
            pins.Where(p => p.IsUserGpio).Select(p => (UserGpio)p.PinNumber).ToArray();

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="alsoManaged"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        private void Dispose(bool alsoManaged)
        {
            lock (SyncLock)
            {
                if (_isDisposed) return;
                _isDisposed = true;

                if (!alsoManaged || WaveId < 0) return;

                // Wait for the wave to finish a cycle
                while (true)
                {
                    var currentWaveId = Board.Waves.CurrentWaveId;
                    if (currentWaveId >= 0 && currentWaveId == WaveId)
                    {
                        Board.Waves.StopCurrent();
                        Board.Timing.Sleep(15);
                    }
                    else
                    {
                        break;
                    }
                }

                Waves.GpioWaveDelete(Convert.ToUInt32(WaveId));
                WaveId = -1;
            }
        }
    }
}
