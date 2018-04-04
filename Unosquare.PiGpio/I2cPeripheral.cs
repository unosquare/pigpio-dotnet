namespace Unosquare.PiGpio
{
    using NativeEnums;
    using NativeMethods;
    using System;

    /// <summary>
    /// Represents a perpheral connected via the I2c bus.
    /// </summary>
    /// <seealso cref="IDisposable" />
    public sealed class I2cPeripheral : IDisposable
    {
        /// <summary>
        /// The default bus is I2C bus 1.
        /// </summary>
        public const I2cBusId DefaultBus = I2cBusId.Bus1;

        private bool IsDisposed = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="I2cPeripheral" /> class.
        /// </summary>
        /// <param name="bus">The bus.</param>
        /// <param name="address">The address.</param>
        private I2cPeripheral(I2cBusId bus, byte address)
        {
            DeviceHandle = I2c.I2cOpen(Convert.ToUInt32(bus), address);
            Address = address;
        }

        /// <summary>
        /// Gets the devide address on the bus.
        /// </summary>
        public byte Address { get; }

        /// <summary>
        /// Gets the device handle.
        /// </summary>
        public UIntPtr DeviceHandle { get; }

        /// <summary>
        /// Opens the specified address.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns>The open peripheral</returns>
        public static I2cPeripheral Open(byte address)
        {
            return new I2cPeripheral(DefaultBus, address);
        }

        /// <summary>
        /// Opens the specified address on the given bus.
        /// </summary>
        /// <param name="bus">The bus.</param>
        /// <param name="address">The address.</param>
        /// <returns>The open peripheral</returns>
        public static I2cPeripheral Open(I2cBusId bus, byte address)
        {
            return new I2cPeripheral(bus, address);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose() => Dispose(true);

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="alsoManaged"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        private void Dispose(bool alsoManaged)
        {
            if (IsDisposed) return;
            IsDisposed = true;

            if (alsoManaged)
            {
                if (DeviceHandle != UIntPtr.Zero)
                    I2c.I2cClose(DeviceHandle);
            }
        }
    }
}
