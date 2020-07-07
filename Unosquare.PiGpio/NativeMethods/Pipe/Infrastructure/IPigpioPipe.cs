namespace Unosquare.PiGpio.NativeMethods.Pipe.Infrastructure
{
    using Unosquare.PiGpio.NativeEnums;

    internal interface IPigpioPipe
    {
        void SendCommand(string cmd);
        string SendCommandWithResult(string cmd);
        int SendCommandWithIntResult(string cmd);
        uint SendCommandWithUIntResult(string cmd);
        ResultCode SendCommandWithResultCode(string cmd);
    }
}