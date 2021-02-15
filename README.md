[![Build status](https://ci.appveyor.com/api/projects/status/n5xt8b07j65a65tb/branch/master?svg=true)](https://ci.appveyor.com/project/geoperez/pigpio-dotnet/branch/master)

# <img src="https://raw.githubusercontent.com/unosquare/pigpio-dotnet/master/Support/pigpio-dotnet.png" alt="pgipio-dotnet" style="width:16px; height:16px" /> Raspbery Pi - libpigpio for .net

**WE ARE LOOKING FOR A NEW HOME FOR THIS PROJECT. APPLY AT:** https://adoptoposs.org/p/d3470190-942b-44ac-84fc-90259cbdee43

*:star: Please star this project if you find it useful!*

Provides complete managed access to the popular pigpio C library.

The premise is simple: using the powerful features of C# to control the ARM peripherals of the Raspberry Pi. This library provides a comprehensive way to access the hardware of the Pi. It uses the fantastic C library [pigpio](https://github.com/joan2937/pigpio/). The [documentation of the library can be found here](http://abyz.me.uk/rpi/pigpio/).

As a programmer, the choice is yours. You can call the native methods either directly or via the comprehensive API of PiGpio.net.

## Example of blinking an LED with direct native calls

```csharp
Setup.GpioInitialise();
var pin = SystemGpio.Bcm18;
IO.GpioSetMode(pin, PinMode.Output);

while (true)
{
    IO.GpioWrite(pin, true);
    Thread.Sleep(500);
    IO.GpioWrite(pin, false);
    Thread.Sleep(500);
}
```

## Example of blinking an LED with the PiGpio.net Managed API

```csharp
var pin = Board.Pins[18];

while (true)
{
    pin.Value = !pin.Value;
    Thread.Sleep(500);
}
```

## Related Projects and Nugets
| Name | Author | Description |
| ---- | ------ | ----------- |
| [RaspberryIO](https://github.com/unosquare/raspberryio) | [Unosquare](https://github.com/unosquare) | The Raspberry Pi's IO Functionality in an easy-to-use API for .NET (Mono/.NET Core). |
| [PiGpio.net](https://github.com/unosquare/pigpio-dotnet) | [Unosquare](https://github.com/unosquare) | Provides complete managed access to the popular pigpio C library |
| [Raspberry Abstractions](https://www.nuget.org/packages/Unosquare.Raspberry.Abstractions) | [Unosquare](https://www.nuget.org/profiles/Unosquare) | Allows you to implement your own provider for RaspberryIO. |
| [Raspberry# IO](https://github.com/raspberry-sharp/raspberry-sharp-io) | [raspberry-sharp](https://github.com/raspberry-sharp) | Raspberry# IO is a .NET/Mono IO Library for Raspberry Pi. This project is an initiative of the [Raspberry#](http://www.raspberry-sharp.org/) Community. |
| [WiringPi.Net](https://github.com/danriches/WiringPi.Net) | [Daniel Riches](https://github.com/danriches) | A simple C# wrapper for Gordon's WiringPi library. |
| [PiSharp](https://github.com/andycb/PiSharp) |[Andy Bradford](https://github.com/andycb) | Pi# is a library to expose the GPIO functionality of the Raspberry Pi computer to the C# and Visual Basic.Net languages |
