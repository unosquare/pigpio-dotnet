namespace Unosquare.PiGpio.Samples.Workbench
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Swan;
    using ManagedModel;

    internal class Mpu6050 : WorkbenchItemBase
    {
        private const byte PowerManagementRegister = 0x6B;
        private I2cDevice Device;

        public Mpu6050(bool isEnabled) 
            : base(isEnabled)
        {
            // placeholder
        }

        protected override void Setup()
        {
            $"Scanning I2C bus . . .".Info(Name);
            var deviceAddresses = Board.Peripherals.ScanI2cBus();
            $"Found {deviceAddresses.Length} I2C devices.".Info(Name);
            foreach (var address in deviceAddresses)
                $"    Device on address 0x{address:X2}".Info(Name);

            if (deviceAddresses.Contains(0x68))
            {
                Device = Board.Peripherals.OpenI2cDevice(0x68);
                return;
            }

            if (deviceAddresses.Contains(0x69))
            {
                Device = Board.Peripherals.OpenI2cDevice(0x69);
                return;
            }
        }

        protected override void DoBackgroundWork(CancellationToken ct)
        {
            if (Device == null)
                return;

            Sleep = false;

            while (ct.IsCancellationRequested == false)
            {
                $"Temperature: {Temperature,6:0.000}".Info(Name);
                Board.Timing.Sleep(1000);
            }
        }

        private bool Sleep
        {
            get
            {
                var powerConfig = (int)Device.ReadByte(PowerManagementRegister);
                var sleepEnabledFlag = 1 << 6;
                return (powerConfig & sleepEnabledFlag) != 0;
            }
            set
            {
                var powerConfig = (int)Device.ReadByte(PowerManagementRegister);
                if (value)
                    powerConfig |= (1 << 6);
                else
                    powerConfig &= ~(1 << 6);

                Device.Write(PowerManagementRegister, (byte)powerConfig);
            }
        }

        private double Temperature
        {
            get
            {
                if (Device == null) return double.NaN;

                // recieves the high byte (MSB) first and then the low byte (LSB) as an int 16
                var tempBytes = BitConverter.GetBytes(Device.ReadWord(0x41));

                // Since we are in little endian, we need to reverse so that we parse LSB and MSB
                var rawReading = BitConverter.ToInt16(new byte[] { tempBytes[1], tempBytes[0] }, 0);
                return rawReading / 340d + 36.53d;
            }
        }

        protected override void Cleanup()
        {
            if (Device == null) return;

            Sleep = true;
            Device.Dispose();
            Device = null;
        }
    }
}
