namespace Unosquare.PiGpio
{
    using NativeEnums;
    using NativeMethods;
    using System;

    public sealed class PiGpioPort
    {
        private SystemGpio SystemGpio = default(SystemGpio);
        private GpioPullMode m_PullMode = GpioPullMode.Off;

        internal PiGpioPort(SystemGpio gpio)
        {
            SystemGpio = gpio;
            PortId = (int)gpio;
            IsUserGpio = PortId < 32;
            Pad = Constants.GetPad(SystemGpio);
            m_PullMode = Constants.GetDefaultPullMode(SystemGpio);
        }

        public int PortId { get; }

        public bool IsUserGpio { get; }

        public GpioPad Pad { get; }

        public GpioPadStrength PadStrength
        {
            get => IO.GpioGetPad(Pad);
            set => PiGpioException.ValidateResult(IO.GpioSetPad(Pad, value));
        }

        public GpioPullMode PullMode
        {
            get
            {
                return m_PullMode;
            }
            set
            {
                PiGpioException.ValidateResult(IO.GpioSetPullUpDown(SystemGpio, value));
                m_PullMode = value;
            }

        }

        public bool Value
        {
            get => IO.GpioRead(SystemGpio);
            set => PiGpioException.ValidateResult(IO.GpioWrite(SystemGpio, value));
        }

        public void Pulsate(int microSecs, bool value)
        {
            PiGpioException.ValidateResult(
                IO.GpioTrigger((UserGpio)PortId, Convert.ToUInt32(microSecs), value));
        }
    }
}
