namespace Unosquare.PiGpio.NativeMethods.Pipe
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Text;
    using Interfaces;
    using NativeEnums;
    using NativeTypes;

    /// <summary>
    /// Pipe strategy pattern.
    /// </summary>
    public class IOServicePipe : IIOService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IOServicePipe"/> class.
        /// </summary>
        public IOServicePipe()
        {
            // Named Pipe classes would be easier but they don't work on Linux in .Net Core
            WritePipe = new FileStream(Constants.CommandPipeName, FileMode.Open, FileAccess.Write, FileShare.ReadWrite, 4096, FileOptions.WriteThrough);
            ReadPipe = new FileStream(Constants.ResultPipeName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, 4096, FileOptions.Asynchronous);
            ErrorPipe = new FileStream(Constants.ErrorPipeName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

            PipeWriter = new StreamWriter(WritePipe, Encoding.ASCII) { AutoFlush = true };
            PipeReader = new StreamReader(ReadPipe, Encoding.ASCII);
            ErrorReader = new StreamReader(ErrorPipe, Encoding.ASCII);

            IsConnected = true;
        }

        /// <inheritdoc />
        public ControllerMode Mode => ControllerMode.Pipe;

        /// <inheritdoc />
        public bool IsConnected { get; }

        #region Pipes
        private FileStream WritePipe { get; }
        private FileStream ReadPipe { get; }
        private FileStream ErrorPipe { get; }
        private StreamWriter PipeWriter { get; }
        private StreamReader PipeReader { get; }
        private StreamReader ErrorReader { get; }
        #endregion

        /// <inheritdoc />
        public ResultCode GpioSetMode(SystemGpio gpio, PigpioPinMode mode)
        {
            char code;
            switch (mode)
            {
                case PigpioPinMode.Input:
                    code = 'R';
                    break;
                case PigpioPinMode.Output:
                    code = 'W';
                    break;
                case PigpioPinMode.Alt0:
                    code = '0';
                    break;
                case PigpioPinMode.Alt1:
                    code = '1';
                    break;
                case PigpioPinMode.Alt2:
                    code = '2';
                    break;
                case PigpioPinMode.Alt3:
                    code = '3';
                    break;
                case PigpioPinMode.Alt4:
                    code = '4';
                    break;
                case PigpioPinMode.Alt5:
                    code = '5';
                    break;
                default: 
                    throw new ArgumentException("Unknown mode: " + mode);
            }

            return SendCommandWithResultCode($"m {(int)gpio} {code}");
        }

        /// <inheritdoc/>
        public PigpioPinMode GpioGetMode(SystemGpio gpio)
        {
            char modeCode = SendCommandWithResult($"mg {(int)gpio}").ToUpperInvariant()[0];
            switch (modeCode)
            {
                case 'R':
                    return PigpioPinMode.Input;
                case 'W':
                    return PigpioPinMode.Output;
                case '0':
                    return PigpioPinMode.Alt0;
                case '1':
                    return PigpioPinMode.Alt1;
                case '2':
                    return PigpioPinMode.Alt2;
                case '3':
                    return PigpioPinMode.Alt3;
                case '4':
                    return PigpioPinMode.Alt4;
                case '5':
                    return PigpioPinMode.Alt5;
                default:
                    throw new Exception("Unknown mode returned by pigpiod: " + modeCode);
            }

        }

        /// <inheritdoc />
        public ResultCode GpioSetPullUpDown(SystemGpio gpio, GpioPullMode pullMode)
        {
            char code;
            switch (pullMode)
            {
                case GpioPullMode.Off:
                    code = 'o';
                    break;
                case GpioPullMode.Down:
                    code = 'd';
                    break;
                case GpioPullMode.Up:
                    code = 'u';
                    break;
                default:
                    throw new ArgumentException("Unknown mode: " + pullMode);
            }

            return SendCommandWithResultCode($"pud {(int)gpio} {code}");
        }

        /// <inheritdoc />
        public bool GpioRead(SystemGpio gpio)
        {
            var result = SendCommandWithResult($"r {(int)gpio}");
            return result == "1";
        }

        /// <inheritdoc />
        public ResultCode GpioWrite(SystemGpio gpio, bool value)
        {
            return SendCommandWithResultCode($"w {(int)gpio} {(value ? 1 : 0)}");
        }

        /// <inheritdoc />
        public ResultCode GpioTrigger(UserGpio userGpio, uint pulseLength, bool value)
        {
            return SendCommandWithResultCode($"trig {(int)userGpio} {pulseLength} {(value ? 1 : 0)}");
        }

        /// <inheritdoc />
        public uint GpioReadBits00To31()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public uint GpioReadBits32To53()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public ResultCode GpioWriteBits00To31Clear(BitMask bits)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public ResultCode GpioWriteBits32To53Clear(BitMask bits)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public ResultCode GpioWriteBits00To31Set(BitMask bits)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public ResultCode GpioWriteBits32To53Set(BitMask bits)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public ResultCode GpioSetAlertFunc(UserGpio userGpio, PiGpioAlertDelegate callback)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public ResultCode GpioSetAlertFuncEx(UserGpio userGpio, PiGpioAlertExDelegate callback, UIntPtr userData)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public ResultCode GpioSetWatchdog(UserGpio userGpio, uint timeoutMilliseconds)
        {
            return SendCommandWithResultCode($"wdog {(int)userGpio} {timeoutMilliseconds}");
        }

        /// <inheritdoc />
        public ResultCode GpioSetIsrFunc(SystemGpio gpio, EdgeDetection edge, int timeout, PiGpioIsrDelegate callback)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public ResultCode GpioSetIsrFuncEx(SystemGpio gpio, EdgeDetection edge, int timeout, PiGpioIsrExDelegate callback, UIntPtr userData)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public ResultCode GpioSetGetSamplesFunc(PiGpioGetSamplesDelegate callback, BitMask bits)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public ResultCode GpioSetGetSamplesFuncEx(PiGpioGetSamplesExDelegate callback, BitMask bits, UIntPtr userData)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public ResultCode GpioGlitchFilter(UserGpio userGpio, uint steadyMicroseconds)
        {
            return SendCommandWithResultCode($"fg {(int)userGpio} {steadyMicroseconds}");
        }

        /// <inheritdoc />
        public ResultCode GpioNoiseFilter(UserGpio userGpio, uint steadyMicroseconds, uint activeMicroseconds)
        {
            return SendCommandWithResultCode($"fn {(int)userGpio} {steadyMicroseconds} {activeMicroseconds}");
        }

        /// <inheritdoc />
        public GpioPadStrength GpioGetPad(GpioPadId pad)
        {
            var result = SendCommandWithResult($"pads {(int)pad}");
            return (GpioPadStrength)Convert.ToInt32(result, CultureInfo.InvariantCulture);
        }

        /// <inheritdoc />
        public ResultCode GpioSetPad(GpioPadId pad, GpioPadStrength padStrength)
        {
            return SendCommandWithResultCode($"pads {(int)pad} {padStrength}");
        }

        /// <inheritdoc />
        public int GpioReadUnmanaged(SystemGpio gpio)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public ResultCode GpioWriteUnmanaged(SystemGpio gpio, DigitalValue value)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public int GpioGetPadUnmanaged(GpioPadId pad)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public int GpioGetModeUnmanaged(SystemGpio gpio)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public ResultCode GpioTriggerUnmanaged(UserGpio userGpio, uint pulseLength, DigitalValue value)
        {
            throw new NotImplementedException();
        }

        private void SendCommand(string cmd)
        {
            PipeWriter.WriteLine(cmd);
        }

        private string SendCommandWithResult(string cmd)
        {
            PipeWriter.WriteLine(cmd);
            return PipeReader.ReadLine();
        }

        private ResultCode SendCommandWithResultCode(string cmd)
        {
            PipeWriter.WriteLine(cmd);
            if (PipeReader.EndOfStream)
            {
                return ResultCode.Ok;
            }

            var result = PipeReader.ReadLine();
            var code = Convert.ToInt32(result, CultureInfo.InvariantCulture);
            return (ResultCode)code;
        }
    }
}