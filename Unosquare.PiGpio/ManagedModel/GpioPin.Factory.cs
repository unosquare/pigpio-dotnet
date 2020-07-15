namespace Unosquare.PiGpio.ManagedModel
{
    using NativeEnums;
    using RaspberryIO.Abstractions;
    using System;

    public partial class GpioPin
    {
        internal static readonly Lazy<GpioPin> Pin00 = new Lazy<GpioPin>(() => new GpioPin(BcmPin.Gpio00)
        {
            Capabilities = PinCapabilities.GP | PinCapabilities.PWM | PinCapabilities.I2CSDA,
            Name = $"BCM 0 {(Board.BoardType == BoardType.Type1 ? "(SDA)" : "(ID_SD)")}",
        });

        internal static readonly Lazy<GpioPin> Pin01 = new Lazy<GpioPin>(() => new GpioPin(BcmPin.Gpio01)
        {
            Capabilities = PinCapabilities.GP | PinCapabilities.PWM | PinCapabilities.I2CSCL,
            Name = $"BCM 1  {(Board.BoardType == BoardType.Type1 ? "(SCL)" : "(ID_SC)")}",
        });

        internal static readonly Lazy<GpioPin> Pin02 = new Lazy<GpioPin>(() => new GpioPin(BcmPin.Gpio02)
        {
            Capabilities = PinCapabilities.GP | PinCapabilities.PWM | PinCapabilities.I2CSDA,
            Name = "BCM 2 (SDA)",
        });

        internal static readonly Lazy<GpioPin> Pin03 = new Lazy<GpioPin>(() => new GpioPin(BcmPin.Gpio03)
        {
            Capabilities = PinCapabilities.GP | PinCapabilities.PWM | PinCapabilities.I2CSCL,
            Name = "BCM 3 (SCL)",
        });

        internal static readonly Lazy<GpioPin> Pin04 = new Lazy<GpioPin>(() => new GpioPin(BcmPin.Gpio04)
        {
            Capabilities = PinCapabilities.GP | PinCapabilities.PWM | PinCapabilities.GPCLK,
            Name = "BCM 4 (GPCLK0)",
        });

        internal static readonly Lazy<GpioPin> Pin05 = new Lazy<GpioPin>(() => new GpioPin(BcmPin.Gpio05)
        {
            Capabilities = PinCapabilities.GP | PinCapabilities.PWM,
            Name = "BCM 5",
        });

        internal static readonly Lazy<GpioPin> Pin06 = new Lazy<GpioPin>(() => new GpioPin(BcmPin.Gpio06)
        {
            Capabilities = PinCapabilities.GP | PinCapabilities.PWM,
            Name = "BCM 6",
        });

        internal static readonly Lazy<GpioPin> Pin07 = new Lazy<GpioPin>(() => new GpioPin(BcmPin.Gpio07)
        {
            Capabilities = PinCapabilities.GP | PinCapabilities.PWM | PinCapabilities.SPICS,
            Name = "BCM 7 (CE1)",
        });

        internal static readonly Lazy<GpioPin> Pin08 = new Lazy<GpioPin>(() => new GpioPin(BcmPin.Gpio08)
        {
            Capabilities = PinCapabilities.GP | PinCapabilities.PWM | PinCapabilities.SPICS,
            Name = "BCM 8 (CE0)",
        });

        internal static readonly Lazy<GpioPin> Pin09 = new Lazy<GpioPin>(() => new GpioPin(BcmPin.Gpio09)
        {
            Capabilities = PinCapabilities.GP | PinCapabilities.PWM | PinCapabilities.SPIMISO,
            Name = "BCM 9 (MISO)",
        });

        internal static readonly Lazy<GpioPin> Pin10 = new Lazy<GpioPin>(() => new GpioPin(BcmPin.Gpio10)
        {
            Capabilities = PinCapabilities.GP | PinCapabilities.PWM | PinCapabilities.SPIMOSI,
            Name = "BCM 10 (MOSI)",
        });

        internal static readonly Lazy<GpioPin> Pin11 = new Lazy<GpioPin>(() => new GpioPin(BcmPin.Gpio11)
        {
            Capabilities = PinCapabilities.GP | PinCapabilities.PWM | PinCapabilities.SPICLK,
            Name = "BCM 11 (SCLCK)",
        });

        internal static readonly Lazy<GpioPin> Pin12 = new Lazy<GpioPin>(() => new GpioPin(BcmPin.Gpio12)
        {
            Capabilities = PinCapabilities.GP | PinCapabilities.PWM,
            Name = "BCM 12 (PWM0)",
        });

        internal static readonly Lazy<GpioPin> Pin13 = new Lazy<GpioPin>(() => new GpioPin(BcmPin.Gpio13)
        {
            Capabilities = PinCapabilities.GP | PinCapabilities.PWM,
            Name = "BCM 13 (PWM1)",
        });

        internal static readonly Lazy<GpioPin> Pin14 = new Lazy<GpioPin>(() => new GpioPin(BcmPin.Gpio14)
        {
            Capabilities = PinCapabilities.UARTTXD | PinCapabilities.PWM,
            Name = "BCM 14 (TXD)",
        });

        internal static readonly Lazy<GpioPin> Pin15 = new Lazy<GpioPin>(() => new GpioPin(BcmPin.Gpio15)
        {
            Capabilities = PinCapabilities.UARTRXD | PinCapabilities.PWM,
            Name = "BCM 15 (RXD)",
        });

        internal static readonly Lazy<GpioPin> Pin16 = new Lazy<GpioPin>(() => new GpioPin(BcmPin.Gpio16)
        {
            Capabilities = PinCapabilities.GP | PinCapabilities.PWM,
            Name = "BCM 16",
        });

        internal static readonly Lazy<GpioPin> Pin17 = new Lazy<GpioPin>(() => new GpioPin(BcmPin.Gpio17)
        {
            Capabilities = PinCapabilities.GP | PinCapabilities.PWM | PinCapabilities.UARTRTS,
            Name = "BCM 17",
        });

        internal static readonly Lazy<GpioPin> Pin18 = new Lazy<GpioPin>(() => new GpioPin(BcmPin.Gpio18)
        {
            Capabilities = PinCapabilities.GP | PinCapabilities.PWM | PinCapabilities.PWM,
            Name = "BCM 18 (PWM0)",
        });

        internal static readonly Lazy<GpioPin> Pin19 = new Lazy<GpioPin>(() => new GpioPin(BcmPin.Gpio19)
        {
            Capabilities = PinCapabilities.GP | PinCapabilities.PWM | PinCapabilities.PWM | PinCapabilities.SPIMISO,
            Name = "BCM 19 (MISO)",
        });

        internal static readonly Lazy<GpioPin> Pin20 = new Lazy<GpioPin>(() => new GpioPin(BcmPin.Gpio20)
        {
            Capabilities = PinCapabilities.GP | PinCapabilities.PWM | PinCapabilities.SPIMOSI,
            Name = "BCM 20 (MOSI)",
        });

        internal static readonly Lazy<GpioPin> Pin21 = new Lazy<GpioPin>(() => new GpioPin(BcmPin.Gpio21)
        {
            Capabilities = PinCapabilities.GP | PinCapabilities.PWM | PinCapabilities.SPICLK,
            Name = $"BCM 21{(Board.BoardType == BoardType.Type1 ? string.Empty : " (SCLK)")}",
        });

        internal static readonly Lazy<GpioPin> Pin22 = new Lazy<GpioPin>(() => new GpioPin(BcmPin.Gpio22)
        {
            Capabilities = PinCapabilities.GP | PinCapabilities.PWM,
            Name = "BCM 22",
        });

        internal static readonly Lazy<GpioPin> Pin23 = new Lazy<GpioPin>(() => new GpioPin(BcmPin.Gpio23)
        {
            Capabilities = PinCapabilities.GP | PinCapabilities.PWM,
            Name = "BCM 23",
        });

        internal static readonly Lazy<GpioPin> Pin24 = new Lazy<GpioPin>(() => new GpioPin(BcmPin.Gpio24)
        {
            Capabilities = PinCapabilities.GP | PinCapabilities.PWM,
            Name = "BCM 24",
        });

        internal static readonly Lazy<GpioPin> Pin25 = new Lazy<GpioPin>(() => new GpioPin(BcmPin.Gpio25)
        {
            Capabilities = PinCapabilities.GP | PinCapabilities.PWM,
            Name = "BCM 25",
        });

        internal static readonly Lazy<GpioPin> Pin26 = new Lazy<GpioPin>(() => new GpioPin(BcmPin.Gpio26)
        {
            Capabilities = PinCapabilities.GP | PinCapabilities.PWM,
            Name = "BCM 26",
        });

        internal static readonly Lazy<GpioPin> Pin27 = new Lazy<GpioPin>(() => new GpioPin(BcmPin.Gpio27)
        {
            Capabilities = PinCapabilities.GP | PinCapabilities.PWM,
            Name = "BCM 27",
        });

        internal static readonly Lazy<GpioPin> Pin28 = new Lazy<GpioPin>(() => new GpioPin(BcmPin.Gpio28)
        {
            Capabilities = PinCapabilities.GP | PinCapabilities.PWM | PinCapabilities.I2CSDA,
            Name = "BCM 28 (SDA)",
        });

        internal static readonly Lazy<GpioPin> Pin29 = new Lazy<GpioPin>(() => new GpioPin(BcmPin.Gpio29)
        {
            Capabilities = PinCapabilities.GP | PinCapabilities.PWM | PinCapabilities.I2CSCL,
            Name = "BCM 29 (SCL)",
        });

        internal static readonly Lazy<GpioPin> Pin30 = new Lazy<GpioPin>(() => new GpioPin(BcmPin.Gpio30)
        {
            Capabilities = PinCapabilities.GP | PinCapabilities.PWM,
            Name = "BCM 30",
        });

        internal static readonly Lazy<GpioPin> Pin31 = new Lazy<GpioPin>(() => new GpioPin(BcmPin.Gpio31)
        {
            Capabilities = PinCapabilities.GP | PinCapabilities.PWM,
            Name = "BCM 31",
        });
    }
}