namespace Unosquare.PiGpio
{
    using NativeEnums;
    using RaspberryIO.Abstractions;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the constants used by the libpigpio library.
    /// </summary>
    internal static class Constants
    {
        internal const string PiGpioLibrary = "libpigpio.so";

        internal static readonly int[] HardwareClockPins0 = new[] { 4, 20, 32, 34 };

        internal static readonly int[] HardwareClockPins2 = new[] { 6, 43 };

        internal static readonly Dictionary<ResultCode, string> ResultCodeMessages = new Dictionary<ResultCode, string>()
        {
            { ResultCode.Ok, "OK result code" },
            { ResultCode.InitFailed, "gpioInitialise failed" },
            { ResultCode.BadUserGpio, "GPIO not 0-31" },
            { ResultCode.BadGpio, "GPIO not 0-53" },
            { ResultCode.BadMode, "mode not 0-7" },
            { ResultCode.BadLevel, "level not 0-1" },
            { ResultCode.BadPud, "pud not 0-2" },
            { ResultCode.BadPulsewidth, "pulsewidth not 0 or 500-2500" },
            { ResultCode.BadDutycycle, "dutycycle outside set range" },
            { ResultCode.BadTimer, "timer not 0-9" },
            { ResultCode.BadMs, "ms not 10-60000" },
            { ResultCode.BadTimetype, "timetype not 0-1" },
            { ResultCode.BadSeconds, "seconds &lt; 0" },
            { ResultCode.BadMicros, "micros not 0-999999" },
            { ResultCode.TimerFailed, "gpioSetTimerFunc failed" },
            { ResultCode.BadWdogTimeout, "timeout not 0-60000" },
            { ResultCode.NoAlertFunc, "DEPRECATED" },
            { ResultCode.BadClkPeriph, "clock peripheral not 0-1" },
            { ResultCode.BadClkSource, "DEPRECATED" },
            { ResultCode.BadClkMicros, "clock micros not 1, 2, 4, 5, 8, or 10" },
            { ResultCode.BadBufMillis, "buf millis not 100-10000" },
            { ResultCode.BadDutyrange, "dutycycle range not 25-40000" },
            { ResultCode.BadSignum, "signum not 0-63" },
            { ResultCode.BadPathname, "can't open pathname" },
            { ResultCode.NoHandle, "no handle available" },
            { ResultCode.BadHandle, "unknown handle" },
            { ResultCode.BadIfFlags, "ifFlags &gt; 4" },
            { ResultCode.BadChannel, "DMA channel not 0-14" },
            { ResultCode.BadSocketPort, "socket port not 1024-32000" },
            { ResultCode.BadFifoCommand, "unrecognized fifo command" },
            { ResultCode.BadSecoChannel, "DMA secondary channel not 0-6" },
            { ResultCode.NotInitialised, "function called before gpioInitialise" },
            { ResultCode.Initialised, "function called after gpioInitialise" },
            { ResultCode.BadWaveMode, "waveform mode not 0-3" },
            { ResultCode.BadCfgInternal, "bad parameter in gpioCfgInternals call" },
            { ResultCode.BadWaveBaud, "baud rate not 50-250K(RX)/50-1M(TX)" },
            { ResultCode.TooManyPulses, "waveform has too many pulses" },
            { ResultCode.TooManyChars, "waveform has too many chars" },
            { ResultCode.NotSerialGpio, "no bit bang serial read on GPIO" },
            { ResultCode.BadSerialStruc, "bad (null) serial structure parameter" },
            { ResultCode.BadSerialBuf, "bad (null) serial buf parameter" },
            { ResultCode.NotPermitted, "GPIO operation not permitted" },
            { ResultCode.SomePermitted, "one or more GPIO not permitted" },
            { ResultCode.BadWvscCommnd, "bad WVSC subcommand" },
            { ResultCode.BadWvsmCommnd, "bad WVSM subcommand" },
            { ResultCode.BadWvspCommnd, "bad WVSP subcommand" },
            { ResultCode.BadPulselen, "trigger pulse length not 1-100" },
            { ResultCode.BadScript, "invalid script" },
            { ResultCode.BadScriptId, "unknown script id" },
            { ResultCode.BadSerOffset, "add serial data offset &gt; 30 minutes" },
            { ResultCode.GpioInUse, "GPIO already in use" },
            { ResultCode.BadSerialCount, "must read at least a byte at a time" },
            { ResultCode.BadParamNum, "script parameter id not 0-9" },
            { ResultCode.DupTag, "script has duplicate tag" },
            { ResultCode.TooManyTags, "script has too many tags" },
            { ResultCode.BadScriptCmd, "illegal script command" },
            { ResultCode.BadVarNum, "script variable id not 0-149" },
            { ResultCode.NoScriptRoom, "no more room for scripts" },
            { ResultCode.NoMemory, "can't allocate temporary memory" },
            { ResultCode.SockReadFailed, "socket read failed" },
            { ResultCode.SockWritFailed, "socket write failed" },
            { ResultCode.TooManyParam, "too many script parameters (&gt; 10)" },
            { ResultCode.ScriptNotReady, "script initialising" },
            { ResultCode.BadTag, "script has unresolved tag" },
            { ResultCode.BadMicsDelay, "bad MICS delay (too large)" },
            { ResultCode.BadMilsDelay, "bad MILS delay (too large)" },
            { ResultCode.BadWaveId, "non existent wave id" },
            { ResultCode.TooManyCbs, "No more CBs for waveform" },
            { ResultCode.TooManyOol, "No more OOL for waveform" },
            { ResultCode.EmptyWaveform, "attempt to create an empty waveform" },
            { ResultCode.NoWaveformId, "no more waveforms" },
            { ResultCode.I2cOpenFailed, "can't open I2C device" },
            { ResultCode.SerOpenFailed, "can't open serial device" },
            { ResultCode.SpiOpenFailed, "can't open SPI device" },
            { ResultCode.BadI2cBus, "bad I2C bus" },
            { ResultCode.BadI2cAddr, "bad I2C address" },
            { ResultCode.BadSpiChannel, "bad SPI channel" },
            { ResultCode.BadFlags, "bad i2c/spi/ser open flags" },
            { ResultCode.BadSpiSpeed, "bad SPI speed" },
            { ResultCode.BadSerDevice, "bad serial device name" },
            { ResultCode.BadSerSpeed, "bad serial baud rate" },
            { ResultCode.BadParam, "bad i2c/spi/ser parameter" },
            { ResultCode.I2cWriteFailed, "i2c write failed" },
            { ResultCode.I2cReadFailed, "i2c read failed" },
            { ResultCode.BadSpiCount, "bad SPI count" },
            { ResultCode.SerWriteFailed, "ser write failed" },
            { ResultCode.SerReadFailed, "ser read failed" },
            { ResultCode.SerReadNoData, "ser read no data available" },
            { ResultCode.UnknownCommand, "unknown command" },
            { ResultCode.SpiXferFailed, "spi xfer/read/write failed" },
            { ResultCode.BadPointer, "bad (NULL) pointer" },
            { ResultCode.NoAuxSpi, "no auxiliary SPI on Pi A or B" },
            { ResultCode.NotPwmGpio, "GPIO is not in use for PWM" },
            { ResultCode.NotServoGpio, "GPIO is not in use for servo pulses" },
            { ResultCode.NotHclkGpio, "GPIO has no hardware clock" },
            { ResultCode.NotHpwmGpio, "GPIO has no hardware PWM" },
            { ResultCode.BadHpwmFreq, "hardware PWM frequency not 1-125M" },
            { ResultCode.BadHpwmDuty, "hardware PWM dutycycle not 0-1M" },
            { ResultCode.BadHclkFreq, "hardware clock frequency not 4689-250M" },
            { ResultCode.BadHclkPass, "need password to use hardware clock 1" },
            { ResultCode.HpwmIllegal, "illegal, PWM in use for main clock" },
            { ResultCode.BadDatabits, "serial data bits not 1-32" },
            { ResultCode.BadStopbits, "serial (half) stop bits not 2-8" },
            { ResultCode.MsgToobig, "socket/pipe message too big" },
            { ResultCode.BadMallocMode, "bad memory allocation mode" },
            { ResultCode.TooManySegs, "too many I2C transaction segments" },
            { ResultCode.BadI2cSeg, "an I2C transaction segment failed" },
            { ResultCode.BadSmbusCmd, "SMBus command not supported by driver" },
            { ResultCode.NotI2cGpio, "no bit bang I2C in progress on GPIO" },
            { ResultCode.BadI2cWlen, "bad I2C write length" },
            { ResultCode.BadI2cRlen, "bad I2C read length" },
            { ResultCode.BadI2cCmd, "bad I2C command" },
            { ResultCode.BadI2cBaud, "bad I2C baud rate, not 50-500k" },
            { ResultCode.ChainLoopCnt, "bad chain loop count" },
            { ResultCode.BadChainLoop, "empty chain loop" },
            { ResultCode.ChainCounter, "too many chain counters" },
            { ResultCode.BadChainCmd, "bad chain command" },
            { ResultCode.BadChainDelay, "bad chain delay micros" },
            { ResultCode.ChainNesting, "chain counters nested too deeply" },
            { ResultCode.ChainTooBig, "chain is too long" },
            { ResultCode.Deprecated, "deprecated function removed" },
            { ResultCode.BadSerInvert, "bit bang serial invert not 0 or 1" },
            { ResultCode.BadEdge, "bad ISR edge value, not 0-2" },
            { ResultCode.BadIsrInit, "bad ISR initialisation" },
            { ResultCode.BadForever, "loop forever must be last command" },
            { ResultCode.BadFilter, "bad filter parameter" },
            { ResultCode.BadPad, "bad pad number" },
            { ResultCode.BadStrength, "bad pad drive strength" },
            { ResultCode.FilOpenFailed, "file open failed" },
            { ResultCode.BadFileMode, "bad file mode" },
            { ResultCode.BadFileFlag, "bad file flag" },
            { ResultCode.BadFileRead, "bad file read" },
            { ResultCode.BadFileWrite, "bad file write" },
            { ResultCode.FileNotRopen, "file not open for read" },
            { ResultCode.FileNotWopen, "file not open for write" },
            { ResultCode.BadFileSeek, "bad file seek" },
            { ResultCode.NoFileMatch, "no files match pattern" },
            { ResultCode.NoFileAccess, "no permission to access file" },
            { ResultCode.FileIsADir, "file is a directory" },
            { ResultCode.BadShellStatus, "bad shell return status" },
            { ResultCode.BadScriptName, "bad script name" },
            { ResultCode.BadSpiBaud, "bad SPI baud rate, not 50-500k" },
            { ResultCode.NotSpiGpio, "no bit bang SPI in progress on GPIO" },
            { ResultCode.BadEventId, "bad event id" },
            { ResultCode.CmdInterrupted, "Used by Python" },
            { ResultCode.PigifErr0, "Documentation not available" },
            { ResultCode.PigifErr99, "Documentation not available" },
            { ResultCode.CustomErr0, "Documentation not available" },
            { ResultCode.CustomErr999, "Documentation not available" },
        };

        internal static string CommandPipeName => "/dev/pigpio";
        internal static string ResultPipeName => "/dev/pigout";
        internal static string ErrorPipeName => "/dev/pigerr";

        internal static string GetResultCodeMessage(int resultCode)
        {
            if (resultCode >= 0) return ResultCodeMessages[ResultCode.Ok];

            return ResultCodeMessages.ContainsKey((ResultCode)resultCode) ? ResultCodeMessages[(ResultCode)resultCode] : "(Unknown error code)";
        }

        /// <summary>
        /// Gets the type of the board.
        /// see: https://www.raspberrypi.org/documentation/hardware/raspberrypi/revision-codes/README.md.
        /// </summary>
        /// <param name="hardwareRevision">The hardware revision.</param>
        /// <returns>The board type.</returns>
        internal static BoardType GetBoardType(long hardwareRevision)
        {
            if (hardwareRevision.IsBetween(2, 3))
                return BoardType.Type1;
            if (hardwareRevision.IsBetween(4, 6) || hardwareRevision == 15)
                return BoardType.Type2;

            return hardwareRevision >= 16 ? BoardType.Type3 : BoardType.Unknown;
        }

        internal static BoardRevision GetBoardRevision(long hardwareRevision)
        {
            if (hardwareRevision.IsBetween(2, 3))
                return BoardRevision.Rev1;

            return BoardRevision.Rev2;
        }

        internal static GpioPadId GetPad(SystemGpio gpio)
        {
            var gpioNumber = (int)gpio;
            if (gpioNumber.IsBetween(0, 27))
                return GpioPadId.Pad00To27;
            if (gpioNumber.IsBetween(28, 45))
                return GpioPadId.Pad28To45;
            if (gpioNumber.IsBetween(46, 53))
                return GpioPadId.Pad46To53;

            throw new ArgumentException($"Unable to get Pad identifier. Argument '{nameof(gpio)}' is invalid.");
        }

        internal static GpioPullMode GetDefaultPullMode(SystemGpio gpio)
        {
            var gpioNumber = (int)gpio;
            if (gpioNumber.IsBetween(0, 8))
                return GpioPullMode.Up;
            if (gpioNumber.IsBetween(9, 27))
                return GpioPullMode.Down;
            if (gpioNumber.IsBetween(28, 29))
                return GpioPullMode.Off;
            if (gpioNumber.IsBetween(30, 33))
                return GpioPullMode.Down;

            if (gpioNumber.IsBetween(34, 36))
                return GpioPullMode.Up;
            if (gpioNumber.IsBetween(37, 43))
                return GpioPullMode.Down;
            if (gpioNumber.IsBetween(44, 45))
                return GpioPullMode.Off;

            if (gpioNumber.IsBetween(46, 53))
                return GpioPullMode.Up;

            throw new ArgumentException($"Unable to get pull mode for {nameof(gpio)}. Invalid {nameof(GpioPullMode)} for GPIO '{gpioNumber}'");
        }

        internal static bool GetIsUserGpio(this SystemGpio gpio, BoardType boardType)
        {
            var pinNumber = (int)gpio;
            if (pinNumber < 0 || pinNumber >= 32) return false;

            switch (boardType)
            {
                case BoardType.Type1:
                    {
                        return pinNumber.IsBetween(0, 1)
                            || pinNumber == 4
                            || pinNumber.IsBetween(7, 11)
                            || pinNumber.IsBetween(14, 15)
                            || pinNumber.IsBetween(17, 18)
                            || pinNumber.IsBetween(21, 25);
                    }

                case BoardType.Type2:
                    {
                        return pinNumber.IsBetween(2, 4)
                            || pinNumber.IsBetween(7, 11)
                            || pinNumber.IsBetween(14, 15)
                            || pinNumber.IsBetween(17, 18)
                            || pinNumber.IsBetween(22, 25)
                            || pinNumber.IsBetween(27, 31);
                    }

                case BoardType.Type3:
                    {
                        return pinNumber.IsBetween(2, 27);
                    }

                default:
                    {
                        return true;
                    }
            }
        }

        internal static bool IsBetween(this int number, int min, int max) => number >= min && number <= max;

        internal static bool IsBetween(this long number, long min, long max) => number >= min && number <= max;

        internal static NativeEnums.EdgeDetection GetEdgeDetection(RaspberryIO.Abstractions.EdgeDetection edgeDetection)
        {
            switch (edgeDetection)
            {
                case RaspberryIO.Abstractions.EdgeDetection.RisingEdge:
                    return NativeEnums.EdgeDetection.RisingEdge;
                case RaspberryIO.Abstractions.EdgeDetection.FallingEdge:
                    return NativeEnums.EdgeDetection.FallingEdge;
                default:
                    return NativeEnums.EdgeDetection.EitherEdge;
            }
        }
    }
}
