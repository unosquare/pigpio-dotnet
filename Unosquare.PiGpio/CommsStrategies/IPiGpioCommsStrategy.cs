namespace Unosquare.PiGpio.CommsStrategies
{
    public interface IPiGpioCommsStrategy
    {
        CommsStrategy CommsStrategy { get; }
        void RegisterServices();
        bool Initialize();
        void Terminate();
    }
}