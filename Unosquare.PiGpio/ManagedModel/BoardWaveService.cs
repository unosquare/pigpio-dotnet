namespace Unosquare.PiGpio.ManagedModel
{
    using Swan.DependencyInjection;
    using Unosquare.PiGpio.NativeMethods.Interfaces;

    /// <summary>
    /// Provides a a pin service to generate pulses with microsecond precision.
    /// </summary>
    /// <seealso cref="GpioPinServiceBase" />
    public sealed class BoardWaveService
    {
        private readonly IWavesService _wavesService;

        /// <summary>
        /// Initializes a new instance of the <see cref="BoardWaveService"/> class.
        /// </summary>
        internal BoardWaveService()
        {
            // placeholder
            // TODO: implementation is incomplete
            // see https://github.com/bschwind/ir-slinger/blob/master/irslinger.h
            // for usage

            _wavesService = DependencyContainer.Current.Resolve<IWavesService>();

            MaxPulses = _wavesService.GpioWaveGetMaxPulses();
            MaxDmaControlBlocks = _wavesService.GpioWaveGetMaxCbs();
            MaxDurationMicroSecs = _wavesService.GpioWaveGetMaxMicros();
        }

        /// <summary>
        /// Gets the maximum pulses allowable per wave.
        /// </summary>
        public int MaxPulses { get; }

        /// <summary>
        /// Gets the maximum DMA control blocks per wave.
        /// </summary>
        public int MaxDmaControlBlocks { get; }

        /// <summary>
        /// Gets the maximum duration of a wave in micro seconds.
        /// </summary>
        public int MaxDurationMicroSecs { get; }

        /// <summary>
        /// Gets a value indicating whether a waveform is being transmitted.
        /// </summary>
        public bool IsBusy => _wavesService.GpioWaveTxBusy() > 0;

        /// <summary>
        /// Gets the current wave identifier.
        /// </summary>
        public int CurrentWaveId
        {
            get
            {
                var waveId = _wavesService.GpioWaveTxAt();
                return waveId >= 0 && waveId < 9998 ? waveId : -1;
            }
        }

        /// <summary>
        /// Stops the current wave being transmitted.
        /// This is intended to stop waves that are generated with a cycling mode.
        /// </summary>
        public void StopCurrent() => _wavesService.GpioWaveTxStop();

        /// <summary>
        /// Creates the wave.
        /// </summary>
        /// <returns>A wave builder object to create and transmit PWM waves.</returns>
        public WaveBuilder CreateWave() => new WaveBuilder();
    }
}
