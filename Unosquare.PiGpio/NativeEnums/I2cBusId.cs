namespace Unosquare.PiGpio.NativeEnums
{
    /// <summary>
    /// The Pi has 2 hardware SPI buses. Bus 0 and Bus 1.
    /// Bus 1 is the default one, accessible through the main P1 header.
    /// </summary>
    public enum I2cBusId
    {
        /// <summary>
        /// The 0th I2c Bus
        /// </summary>
        Bus0 = 0,

        /// <summary>
        /// The 1st I2c Bus -- This is the default bus number on the Pi.
        /// </summary>
        Bus1 = 1,
    }
}
