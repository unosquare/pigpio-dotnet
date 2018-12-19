namespace Unosquare.PiGpio
{
    using System.Collections.ObjectModel;
    using RaspberryIO.Abstractions;

    /// <summary>
    /// A simple wrapper for the I2c bus on the Raspberry Pi.
    /// </summary>
    public class I2CBus : II2CBus
    {
        /// <inheritdoc />
        public II2CDevice GetDeviceById(int deviceId)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public II2CDevice AddDevice(int deviceId)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public ReadOnlyCollection<II2CDevice> Devices { get; }

        /// <inheritdoc />
        public II2CDevice this[int deviceId] => throw new System.NotImplementedException();
    }
}
