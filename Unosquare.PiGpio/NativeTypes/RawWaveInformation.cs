namespace Unosquare.PiGpio.NativeTypes
{
    using System.Runtime.InteropServices;

    /// <summary>
    /// Represents raw waveform information
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class RawWaveInformation
    {
        #region Fields

        private ushort m_BottomCB;
        private ushort m_TopCB;
        private ushort m_BottomOOL;
        private ushort m_TopOOL;
        private ushort m_Deleted;
        private ushort m_NumberCB;
        private ushort m_NumberBOOL;
        private ushort m_NumberTOOL;

        #endregion

        /// <summary>
        /// first CB used by wave
        /// </summary>
        public ushort BottomCB { get => m_BottomCB; set => m_BottomCB = value; }

        /// <summary>
        /// last CB used by wave
        /// </summary>
        public ushort TopCB { get => m_TopCB; set => m_TopCB = value; }

        /// <summary>
        /// last OOL used by wave
        /// </summary>
        public ushort BottomOOL { get => m_BottomOOL; set => m_BottomOOL = value; }

        /// <summary>
        /// first OOL used by wave
        /// </summary>
        public ushort TopOOL { get => m_TopOOL; set => m_TopOOL = value; }

        /// <summary>
        /// Gets or sets the deleted.
        /// </summary>
        public ushort Deleted { get => m_Deleted; set => m_Deleted = value; }

        /// <summary>
        /// Gets or sets the number cb.
        /// </summary>
        public ushort NumberCB { get => m_NumberCB; set => m_NumberCB = value; }

        /// <summary>
        /// Gets or sets the number bool.
        /// </summary>
        public ushort NumberBOOL { get => m_NumberBOOL; set => m_NumberBOOL = value; }

        /// <summary>
        /// Gets or sets the number tool.
        /// </summary>
        public ushort NumberTOOL { get => m_NumberTOOL; set => m_NumberTOOL = value; }
    }
}
