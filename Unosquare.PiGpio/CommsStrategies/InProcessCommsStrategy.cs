﻿using System;
using Swan.DependencyInjection;
using Unosquare.PiGpio.NativeMethods.InProcess;
using Unosquare.PiGpio.NativeMethods.Interfaces;

namespace Unosquare.PiGpio.CommsStrategies
{
    using Unosquare.PiGpio.NativeEnums;
    using Unosquare.PiGpio.NativeMethods.InProcess.DllImports;

    public class InProcessCommsStrategy : IPiGpioCommsStrategy
    {
        /// <inheritdoc />
        public CommsStrategy CommsStrategy => CommsStrategy.InProcess;

        public void RegisterServices()
        {
            DependencyContainer.Current.Register<IIOService>(new IOServiceInProcess());
        }

        public bool Initialize()
        {
            try
            {
                // Retrieve internal configuration
                var config = (int)Setup.GpioCfgGetInternals();

                // config = config.ApplyBits(false, 3, 2, 1, 0); // Clear debug flags
                /*
                MJA
                If you use Visual Studio 2019 together with VSMonoDebugger and X11 remote debugging,
                you need to enable the next line, otherwise Mono will catch the Signals and stop
                debugging immediately although native started program will work ok
                */
                config = config | (int)ConfigFlags.NoSignalHandler;
                Setup.GpioCfgSetInternals((ConfigFlags)config);

                var initResultCode = Setup.GpioInitialise();

                /*
                MJA
                Setup.GpioInitialise() gives back value greater than zero if it has success.
                More in detail:
                The given back number is the version of the library version you use on RasPi.
                Therefore a greater or equal comparison would make potentially more sense.
                */

                // IsAvailable = initResultCode == ResultCode.Ok;
                return initResultCode >= ResultCode.Ok;

                // You will need to compile libgpio.h adding
                // #define EMBEDDED_IN_VM
                // Also, remove the atexit( ... call in pigpio.c
                // So there is no output or signal handling
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

        }
    }
}