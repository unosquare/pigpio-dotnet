# PiGpio (for .net)
Provides complete managed access to the popular pigpio C library

The premise is simple: using the powerful features of C# to control the ARM peripherals of the Raspberry Pi. This library provides a comprehensive way to access the hardware of the Pi. It uses the fantastic C library [pigpio](https://github.com/joan2937/pigpio/). The [documentation of the library can be found here](http://abyz.me.uk/rpi/pigpio/).

As a programmer, the choice is yours. You can call the netive methods either directly or via the comprhensive API of PiGpio.net.

## Example of blinking an LED with native calls

```csharp
Setup.GpioInitialise();
var pin = SystemGpio.Bcm18;
IO.GpioSetMode(pin, PortMode.Output);

while (true)
{
    IO.GpioWrite(pin, true);
    Thread.Sleep(500);
    IO.GpioWrite(pin, false);
    Thread.Sleep(500);
}
```

## Example of blinking an LED with the PiGpio.net

```csharp
var pin = PiGpioController.Pins[18];

while (true)
{
    pin.Value = !pin.Value;
    Thread.Sleep(500);
}
```