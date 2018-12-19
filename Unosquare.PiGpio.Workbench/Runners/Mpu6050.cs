namespace Unosquare.PiGpio.Workbench.Runners
{
    using ManagedModel;
    using NativeMethods;
    using Swan;
    using Swan.Abstractions;
    using System;
    using System.Threading;

    internal class Mpu6050 : RunnerBase
    {
        private const byte PowerManagementRegister = 0x6B;
        private I2cDevice _device;

        public Mpu6050(bool isEnabled)
            : base(isEnabled)
        {
            // placeholder
        }

        private bool Sleep
        {
            get
            {
                var powerConfig = _device.ReadByte(PowerManagementRegister);
                return powerConfig.GetBit(6);
            }
            set
            {
                var powerConfig = _device.ReadByte(PowerManagementRegister);
                _device.Write(PowerManagementRegister, powerConfig.SetBit(6, value));
            }
        }

        private double Temperature
        {
            get
            {
                if (_device == null) return double.NaN;

                // receives the high byte (MSB) first and then the low byte (LSB) as an int 16
                var tempBytes = BitConverter.GetBytes(_device.ReadWord(0x41));

                // Since we are in little endian, we need to reverse so that we parse LSB and MSB
                var rawReading = BitConverter.ToInt16(new[] { tempBytes[1], tempBytes[0] }, 0);
                return (rawReading / 340d) + 36.53d;
            }
        }

        protected override void Setup()
        {
            "Scanning I2C bus . . .".Info(Name);
            var deviceAddresses = Board.Peripherals.ScanI2cBus();
            $"Found {deviceAddresses.Length} I2C device(s)".Info(Name);
            foreach (var address in deviceAddresses)
                $"    Device on address 0x{address:X2}".Info(Name);

            if (deviceAddresses.Contains(0x68))
            {
                _device = Board.Peripherals.OpenI2cDevice(0x68);
                return;
            }

            if (deviceAddresses.Contains(0x69))
            {
                _device = Board.Peripherals.OpenI2cDevice(0x69);
            }
        }

        protected override void DoBackgroundWork(CancellationToken ct)
        {
            if (_device == null)
                return;

            Sleep = false;

            while (ct.IsCancellationRequested == false)
            {
                Thread.Sleep(500);
                $"Temperature: {Temperature,6:0.000}".Info(Name);
            }
        }

        protected override void Cleanup()
        {
            if (_device == null) return;

            Sleep = true;
            _device.Dispose();
            _device = null;
        }
    }
}
