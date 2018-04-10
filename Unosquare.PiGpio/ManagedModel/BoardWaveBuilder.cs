namespace Unosquare.PiGpio.ManagedModel
{
    using NativeEnums;
    using NativeMethods;
    using NativeTypes;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    /// <summary>
    /// Provides methods to build waveforms.
    /// </summary>
    public sealed class BoardWaveBuilder : IDisposable
    {
        private const string WaveAlreadyPreparedErrorMessage = "Wave was already prepared and cannot be modified.";
        private static readonly object SyncLock = new object();
        private readonly List<GpioPulse> m_Pulses;
        private bool IsDisposed = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="BoardWaveBuilder"/> class.
        /// </summary>
        internal BoardWaveBuilder()
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
        /// <exception cref="InvalidOperationException">When the wave has been prepared</exception>
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
        /// <exception cref="InvalidOperationException">When the wave has been prepared</exception>
        public void AddPulse(int durationMicroSecs, UserGpio[] onPins, UserGpio[] offPins)
        {
            if (IsPrepared)
                throw new InvalidOperationException(WaveAlreadyPreparedErrorMessage);

            var onPinFlags = PinsToBitMask(onPins);
            var offPinFlags = PinsToBitMask(offPins);

            var pulse = new GpioPulse
            {
                DurationMicroSecs = Convert.ToUInt32(durationMicroSecs),
                GpioOn = onPinFlags,
                GpioOff = offPinFlags
            };

            m_Pulses.Add(pulse);
        }

        /// <summary>
        /// Adds a pulse.
        /// </summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        /// <param name="durationMicroSecs">The duration micro secs.</param>
        /// <param name="pins">The pins.</param>
        /// <exception cref="InvalidOperationException">When the wave has been prepared</exception>
        public void AddPulse(bool value, int durationMicroSecs, params UserGpio[] pins)
        {
            if (IsPrepared)
                throw new InvalidOperationException(WaveAlreadyPreparedErrorMessage);

            AddPulse(durationMicroSecs,
                value ? pins : null,
                value ? null : pins);
        }

        /// <summary>
        /// Clears all previously added pulses.
        /// </summary>
        /// <exception cref="InvalidOperationException">When the wave has been prepared</exception>
        public void ClearPulses()
        {
            if (IsPrepared)
                throw new InvalidOperationException(WaveAlreadyPreparedErrorMessage);

            m_Pulses.Clear();
        }

        /// <summary>
        /// Prepares the waveform to be rendered by DMA
        /// </summary>
        /// <exception cref="ObjectDisposedException">When the wave has been disposed.</exception>
        public void Prepare()
        {
            lock (SyncLock)
            {
                if (IsPrepared) return;
                if (IsDisposed) throw new ObjectDisposedException();

                Waves.GpioWaveClear();
                BoardException.ValidateResult(
                    Waves.GpioWaveAddGeneric(Convert.ToUInt32(m_Pulses.Count), m_Pulses.ToArray()));
                WaveId = BoardException.ValidateResult(
                    Waves.GpioWaveCreate());
            }
        }

        /// <summary>
        /// Begins rendering the waveform pulses
        /// </summary>
        /// <param name="mode">The mode.</param>
        public void Send(WaveMode mode)
        {
            if (IsPrepared == false)
                Prepare();

            if (IsPrepared)
                BoardException.ValidateResult(Waves.GpioWaveTxSend(Convert.ToUInt32(WaveId), mode));
        }

        /// <summary>
        /// Stops and deletes the waveform if it is being tranferred. Releases the resources.
        /// </summary>
        public void Dispose() => Dispose(true);

        /// <summary>
        /// Converts a collection of User GPIO pins to a bitmask.
        /// </summary>
        /// <param name="pins">The pins.</param>
        /// <returns>A bitmask with each pin as a position</returns>
        private static BitMask PinsToBitMask(UserGpio[] pins)
        {
            var bitMask = 0;
            if (pins != null)
            {
                foreach (var pin in pins)
                    bitMask.SetBit((int)pin, true);
            }

            return (BitMask)bitMask;
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="alsoManaged"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        private void Dispose(bool alsoManaged)
        {
            lock (SyncLock)
            {
                if (IsDisposed) return;
                IsDisposed = true;

                if (alsoManaged)
                {
                    if (WaveId < 0) return;

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
}
