namespace Unosquare.PiGpio
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using ManagedModel;
    using RaspberryIO.Abstractions;

    /// <summary>
    /// A simple wrapper for the I2c bus on the Raspberry Pi.
    /// </summary>
    public class I2CBus : II2CBus
    {
        private static readonly object SyncRoot = new object();
        private readonly BoardPeripheralsService _service = Board.Peripherals;
        private readonly Dictionary<int, II2CDevice> _devices = new Dictionary<int, II2CDevice>();

        /// <inheritdoc />
        public ReadOnlyCollection<II2CDevice> Devices
        {
            get
            {
                lock (SyncRoot)
                {
                    return new ReadOnlyCollection<II2CDevice>(_devices.Values.ToArray());
                }
            }
        }

        /// <inheritdoc />
        public II2CDevice this[int deviceId] => GetDeviceById(deviceId);

        /// <inheritdoc />
        public II2CDevice GetDeviceById(int deviceId)
        {
            lock (SyncRoot)
            {
                return _devices[deviceId];
            }
        }

        /// <inheritdoc />
        public II2CDevice AddDevice(int deviceId)
        {
            lock (SyncRoot)
            {
                if (_devices.TryGetValue(deviceId, out var device))
                {
                    return device;
                }

                var d = _service.OpenI2cDevice((byte)deviceId);
                device = new I2CDevice(d);
                _devices[deviceId] = device;
                return device;
            }
        }
    }
}
