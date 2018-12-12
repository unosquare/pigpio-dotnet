namespace Unosquare.PiGpio.NativeEnums
{
    /// <summary>
    /// Defines the different operation result codes from calling pigpio API.
    /// 0 is OK. Anything negative is an error.
    /// </summary>
    public enum ResultCode : int
    {
        /// <summary>OK result code</summary>
        Ok = 0,

        /// <summary>gpioInitialise failed</summary>
        InitFailed = -1,

        /// <summary>GPIO not 0-31</summary>
        BadUserGpio = -2,

        /// <summary>GPIO not 0-53</summary>
        BadGpio = -3,

        /// <summary>mode not 0-7</summary>
        BadMode = -4,

        /// <summary>level not 0-1</summary>
        BadLevel = -5,

        /// <summary>pud not 0-2</summary>
        BadPud = -6,

        /// <summary>pulsewidth not 0 or 500-2500</summary>
        BadPulsewidth = -7,

        /// <summary>dutycycle outside set range</summary>
        BadDutycycle = -8,

        /// <summary>timer not 0-9</summary>
        BadTimer = -9,

        /// <summary>ms not 10-60000</summary>
        BadMs = -10,

        /// <summary>timetype not 0-1</summary>
        BadTimetype = -11,

        /// <summary>seconds &lt; 0</summary>
        BadSeconds = -12,

        /// <summary>micros not 0-999999</summary>
        BadMicros = -13,

        /// <summary>gpioSetTimerFunc failed</summary>
        TimerFailed = -14,

        /// <summary>timeout not 0-60000</summary>
        BadWdogTimeout = -15,

        /// <summary>DEPRECATED</summary>
        NoAlertFunc = -16,

        /// <summary>clock peripheral not 0-1</summary>
        BadClkPeriph = -17,

        /// <summary>DEPRECATED</summary>
        BadClkSource = -18,

        /// <summary>clock micros not 1, 2, 4, 5, 8, or 10</summary>
        BadClkMicros = -19,

        /// <summary>buf millis not 100-10000</summary>
        BadBufMillis = -20,

        /// <summary>dutycycle range not 25-40000</summary>
        BadDutyrange = -21,

        /// <summary>signum not 0-63</summary>
        BadSignum = -22,

        /// <summary>can't open pathname</summary>
        BadPathname = -23,

        /// <summary>no handle available</summary>
        NoHandle = -24,

        /// <summary>unknown handle</summary>
        BadHandle = -25,

        /// <summary>ifFlags &gt; 4</summary>
        BadIfFlags = -26,

        /// <summary>DMA channel not 0-14</summary>
        BadChannel = -27,

        /// <summary>socket port not 1024-32000</summary>
        BadSocketPort = -28,

        /// <summary>unrecognized fifo command</summary>
        BadFifoCommand = -29,

        /// <summary>DMA secondary channel not 0-6</summary>
        BadSecoChannel = -30,

        /// <summary>function called before gpioInitialise</summary>
        NotInitialised = -31,

        /// <summary>function called after gpioInitialise</summary>
        Initialised = -32,

        /// <summary>waveform mode not 0-3</summary>
        BadWaveMode = -33,

        /// <summary>bad parameter in gpioCfgInternals call</summary>
        BadCfgInternal = -34,

        /// <summary>baud rate not 50-250K(RX)/50-1M(TX)</summary>
        BadWaveBaud = -35,

        /// <summary>waveform has too many pulses</summary>
        TooManyPulses = -36,

        /// <summary>waveform has too many chars</summary>
        TooManyChars = -37,

        /// <summary>no bit bang serial read on GPIO</summary>
        NotSerialGpio = -38,

        /// <summary>bad (null) serial structure parameter</summary>
        BadSerialStruc = -39,

        /// <summary>bad (null) serial buf parameter</summary>
        BadSerialBuf = -40,

        /// <summary>GPIO operation not permitted</summary>
        NotPermitted = -41,

        /// <summary>one or more GPIO not permitted</summary>
        SomePermitted = -42,

        /// <summary>bad WVSC subcommand</summary>
        BadWvscCommnd = -43,

        /// <summary>bad WVSM subcommand</summary>
        BadWvsmCommnd = -44,

        /// <summary>bad WVSP subcommand</summary>
        BadWvspCommnd = -45,

        /// <summary>trigger pulse length not 1-100</summary>
        BadPulselen = -46,

        /// <summary>invalid script</summary>
        BadScript = -47,

        /// <summary>unknown script id</summary>
        BadScriptId = -48,

        /// <summary>add serial data offset &gt; 30 minutes</summary>
        BadSerOffset = -49,

        /// <summary>GPIO already in use</summary>
        GpioInUse = -50,

        /// <summary>must read at least a byte at a time</summary>
        BadSerialCount = -51,

        /// <summary>script parameter id not 0-9</summary>
        BadParamNum = -52,

        /// <summary>script has duplicate tag</summary>
        DupTag = -53,

        /// <summary>script has too many tags</summary>
        TooManyTags = -54,

        /// <summary>illegal script command</summary>
        BadScriptCmd = -55,

        /// <summary>script variable id not 0-149</summary>
        BadVarNum = -56,

        /// <summary>no more room for scripts</summary>
        NoScriptRoom = -57,

        /// <summary>can't allocate temporary memory</summary>
        NoMemory = -58,

        /// <summary>socket read failed</summary>
        SockReadFailed = -59,

        /// <summary>socket write failed</summary>
        SockWritFailed = -60,

        /// <summary>too many script parameters (&gt; 10)</summary>
        TooManyParam = -61,

        /// <summary>script initialising</summary>
        ScriptNotReady = -62,

        /// <summary>script has unresolved tag</summary>
        BadTag = -63,

        /// <summary>bad MICS delay (too large)</summary>
        BadMicsDelay = -64,

        /// <summary>bad MILS delay (too large)</summary>
        BadMilsDelay = -65,

        /// <summary>non existent wave id</summary>
        BadWaveId = -66,

        /// <summary>No more CBs for waveform</summary>
        TooManyCbs = -67,

        /// <summary>No more OOL for waveform</summary>
        TooManyOol = -68,

        /// <summary>attempt to create an empty waveform</summary>
        EmptyWaveform = -69,

        /// <summary>no more waveforms</summary>
        NoWaveformId = -70,

        /// <summary>can't open I2C device</summary>
        I2cOpenFailed = -71,

        /// <summary>can't open serial device</summary>
        SerOpenFailed = -72,

        /// <summary>can't open SPI device</summary>
        SpiOpenFailed = -73,

        /// <summary>bad I2C bus</summary>
        BadI2cBus = -74,

        /// <summary>bad I2C address</summary>
        BadI2cAddr = -75,

        /// <summary>bad SPI channel</summary>
        BadSpiChannel = -76,

        /// <summary>bad i2c/spi/ser open flags</summary>
        BadFlags = -77,

        /// <summary>bad SPI speed</summary>
        BadSpiSpeed = -78,

        /// <summary>bad serial device name</summary>
        BadSerDevice = -79,

        /// <summary>bad serial baud rate</summary>
        BadSerSpeed = -80,

        /// <summary>bad i2c/spi/ser parameter</summary>
        BadParam = -81,

        /// <summary>i2c write failed</summary>
        I2cWriteFailed = -82,

        /// <summary>i2c read failed</summary>
        I2cReadFailed = -83,

        /// <summary>bad SPI count</summary>
        BadSpiCount = -84,

        /// <summary>ser write failed</summary>
        SerWriteFailed = -85,

        /// <summary>ser read failed</summary>
        SerReadFailed = -86,

        /// <summary>ser read no data available</summary>
        SerReadNoData = -87,

        /// <summary>unknown command</summary>
        UnknownCommand = -88,

        /// <summary>spi xfer/read/write failed</summary>
        SpiXferFailed = -89,

        /// <summary>bad (NULL) pointer</summary>
        BadPointer = -90,

        /// <summary>no auxiliary SPI on Pi A or B</summary>
        NoAuxSpi = -91,

        /// <summary>GPIO is not in use for PWM</summary>
        NotPwmGpio = -92,

        /// <summary>GPIO is not in use for servo pulses</summary>
        NotServoGpio = -93,

        /// <summary>GPIO has no hardware clock</summary>
        NotHclkGpio = -94,

        /// <summary>GPIO has no hardware PWM</summary>
        NotHpwmGpio = -95,

        /// <summary>hardware PWM frequency not 1-125M</summary>
        BadHpwmFreq = -96,

        /// <summary>hardware PWM dutycycle not 0-1M</summary>
        BadHpwmDuty = -97,

        /// <summary>hardware clock frequency not 4689-250M</summary>
        BadHclkFreq = -98,

        /// <summary>need password to use hardware clock 1</summary>
        BadHclkPass = -99,

        /// <summary>illegal, PWM in use for main clock</summary>
        HpwmIllegal = -100,

        /// <summary>serial data bits not 1-32</summary>
        BadDatabits = -101,

        /// <summary>serial (half) stop bits not 2-8</summary>
        BadStopbits = -102,

        /// <summary>socket/pipe message too big</summary>
        MsgToobig = -103,

        /// <summary>bad memory allocation mode</summary>
        BadMallocMode = -104,

        /// <summary>too many I2C transaction segments</summary>
        TooManySegs = -105,

        /// <summary>an I2C transaction segment failed</summary>
        BadI2cSeg = -106,

        /// <summary>SMBus command not supported by driver</summary>
        BadSmbusCmd = -107,

        /// <summary>no bit bang I2C in progress on GPIO</summary>
        NotI2cGpio = -108,

        /// <summary>bad I2C write length</summary>
        BadI2cWlen = -109,

        /// <summary>bad I2C read length</summary>
        BadI2cRlen = -110,

        /// <summary>bad I2C command</summary>
        BadI2cCmd = -111,

        /// <summary>bad I2C baud rate, not 50-500k</summary>
        BadI2cBaud = -112,

        /// <summary>bad chain loop count</summary>
        ChainLoopCnt = -113,

        /// <summary>empty chain loop</summary>
        BadChainLoop = -114,

        /// <summary>too many chain counters</summary>
        ChainCounter = -115,

        /// <summary>bad chain command</summary>
        BadChainCmd = -116,

        /// <summary>bad chain delay micros</summary>
        BadChainDelay = -117,

        /// <summary>chain counters nested too deeply</summary>
        ChainNesting = -118,

        /// <summary>chain is too long</summary>
        ChainTooBig = -119,

        /// <summary>deprecated function removed</summary>
        Deprecated = -120,

        /// <summary>bit bang serial invert not 0 or 1</summary>
        BadSerInvert = -121,

        /// <summary>bad ISR edge value, not 0-2</summary>
        BadEdge = -122,

        /// <summary>bad ISR initialisation</summary>
        BadIsrInit = -123,

        /// <summary>loop forever must be last command</summary>
        BadForever = -124,

        /// <summary>bad filter parameter</summary>
        BadFilter = -125,

        /// <summary>bad pad number</summary>
        BadPad = -126,

        /// <summary>bad pad drive strength</summary>
        BadStrength = -127,

        /// <summary>file open failed</summary>
        FilOpenFailed = -128,

        /// <summary>bad file mode</summary>
        BadFileMode = -129,

        /// <summary>bad file flag</summary>
        BadFileFlag = -130,

        /// <summary>bad file read</summary>
        BadFileRead = -131,

        /// <summary>bad file write</summary>
        BadFileWrite = -132,

        /// <summary>file not open for read</summary>
        FileNotRopen = -133,

        /// <summary>file not open for write</summary>
        FileNotWopen = -134,

        /// <summary>bad file seek</summary>
        BadFileSeek = -135,

        /// <summary>no files match pattern</summary>
        NoFileMatch = -136,

        /// <summary>no permission to access file</summary>
        NoFileAccess = -137,

        /// <summary>file is a directory</summary>
        FileIsADir = -138,

        /// <summary>bad shell return status</summary>
        BadShellStatus = -139,

        /// <summary>bad script name</summary>
        BadScriptName = -140,

        /// <summary>bad SPI baud rate, not 50-500k</summary>
        BadSpiBaud = -141,

        /// <summary>no bit bang SPI in progress on GPIO</summary>
        NotSpiGpio = -142,

        /// <summary>bad event id</summary>
        BadEventId = -143,

        /// <summary>Used by Python</summary>
        CmdInterrupted = -144,

        /// <summary>Documentation not available</summary>
        PigifErr0 = -2000,

        /// <summary>Documentation not available</summary>
        PigifErr99 = -2099,

        /// <summary>Documentation not available</summary>
        CustomErr0 = -3000,

        /// <summary>Documentation not available</summary>
        CustomErr999 = -3999,
    }
}
