﻿namespace Unosquare.PiGpio.ManagedModel
{
    using NativeEnums;
    using Swan.DependencyInjection;
    using System;
    using Unosquare.PiGpio.NativeMethods.Interfaces;

    /// <summary>
    /// Provides a software based (bit-banged Serial Port).
    /// </summary>
    public sealed class SoftSerialPort : IDisposable
    {
        private readonly GpioPin _transmitPin;
        private readonly IWavesService _wavesService;
        private readonly ISerialService _serialService;
        private bool _isDisposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="SoftSerialPort"/> class.
        /// </summary>
        /// <param name="receivePin">The receive pin.</param>
        /// <param name="transmitPin">The transmit pin.</param>
        /// <param name="baudRate">The baud rate.</param>
        /// <param name="dataBits">The data bits.</param>
        /// <param name="invert">if set to <c>true</c> [invert].</param>
        internal SoftSerialPort(GpioPin receivePin, GpioPin transmitPin, UartRate baudRate, int dataBits, bool invert)
        {
            _wavesService = DependencyContainer.Current.Resolve<IWavesService>();
            _serialService = DependencyContainer.Current.Resolve<ISerialService>();
            BoardException.ValidateResult(
                _serialService.GpioSerialReadOpen((UserGpio)receivePin.BcmPinNumber, Convert.ToUInt32(baudRate), Convert.ToUInt32(dataBits)));

            Handle = (UserGpio)receivePin.BcmPinNumber;
            DataBits = dataBits;
            BaudRate = (int)baudRate;

            if (invert)
            {
                _serialService.GpioSerialReadInvert(Handle, true);
                Invert = true;
            }

            _transmitPin = transmitPin;
        }

        /// <summary>
        /// Gets or sets the stop bits. Defaults to 2 stop bits.
        /// </summary>
        public int StopBits { get; set; } = 2;

        /// <summary>
        /// Gets the baud rate.
        /// </summary>
        public int BaudRate { get; }

        /// <summary>
        /// Gets a value indicating whether the IO is inverted.
        /// </summary>
        public bool Invert { get; }

        /// <summary>
        /// Gets the data bits.
        /// </summary>
        public int DataBits { get; }

        /// <summary>
        /// Gets the handle.
        /// </summary>
        public UserGpio Handle { get; }

        /// <summary>
        /// Reads up to count bytes.
        /// </summary>
        /// <param name="count">The count.</param>
        /// <returns>The bytes that were read.</returns>
        public byte[] Read(int count) =>
            _serialService.GpioSerialRead(Handle, count);

        /// <summary>
        /// Writes the specified buffer to the transmit pin as a free-form wave.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        public void Write(byte[] buffer)
        {
            _wavesService.GpioWaveClear();
            BoardException.ValidateResult(
                _wavesService.GpioWaveAddSerial(
                    (UserGpio)_transmitPin.BcmPinNumber,
                    Convert.ToUInt32(BaudRate),
                    Convert.ToUInt32(DataBits),
                    Convert.ToUInt32(StopBits),
                    0,
                    Convert.ToUInt32(buffer.Length),
                    buffer));

            var waveId = BoardException.ValidateResult(
                _wavesService.GpioWaveCreate());
            _wavesService.GpioWaveTxSend(Convert.ToUInt32(waveId), WaveMode.OneShotSync);

            // Wait for the wave to finish sending
            while (_wavesService.GpioWaveTxBusy() > 0)
                Board.Timing.Sleep(1);

            _wavesService.GpioWaveDelete(Convert.ToUInt32(waveId));
        }

        /// <inheritdoc />
        public void Dispose() => Dispose(true);

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="alsoManaged"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        private void Dispose(bool alsoManaged)
        {
            if (_isDisposed) return;
            _isDisposed = true;

            if (alsoManaged)
            {
                _serialService.GpioSerialReadClose(Handle);
            }
        }
    }
}
