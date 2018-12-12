namespace Unosquare.PiGpio.NativeTypes
{
    using System.Runtime.InteropServices;

    /// <summary>
    /// Represents Raw SPI channel data.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class RawSpiData
    {
        #region Fields

        private int m_ClockPin;
        private int m_MosiPin;
        private int m_MisoPin;
        private int m_SlaveSelectOffState;
        private int m_SlaveSelectDelayMicroseconds;
        private int m_ClockOffState;
        private int m_ClockPhase;
        private int m_ClockMicroseconds;

        #endregion

        /// <summary>
        /// GPIO for clock.
        /// </summary>
        public int ClockPin { get => m_ClockPin; set => m_ClockPin = value; }

        /// <summary>
        /// GPIO for MOSI.
        /// </summary>
        public int MosiPin { get => m_MosiPin; set => m_MosiPin = value; }

        /// <summary>
        /// GPIO for MISO.
        /// </summary>
        public int MisoPin { get => m_MisoPin; set => m_MisoPin = value; }

        /// <summary>
        /// slave select off state.
        /// </summary>
        public int SlaveSelectOffState { get => m_SlaveSelectOffState; set => m_SlaveSelectOffState = value; }

        /// <summary>
        /// delay after slave select.
        /// </summary>
        public int SlaveSelectDelayMicroseconds { get => m_SlaveSelectDelayMicroseconds; set => m_SlaveSelectDelayMicroseconds = value; }

        /// <summary>
        /// clock off state.
        /// </summary>
        public int ClockOffState { get => m_ClockOffState; set => m_ClockOffState = value; }

        /// <summary>
        /// clock phase.
        /// </summary>
        public int ClockPhase { get => m_ClockPhase; set => m_ClockPhase = value; }

        /// <summary>
        /// clock micros.
        /// </summary>
        public int ClockMicroseconds { get => m_ClockMicroseconds; set => m_ClockMicroseconds = value; }
    }
}
