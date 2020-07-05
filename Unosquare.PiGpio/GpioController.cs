using System.Collections.ObjectModel;
using Swan;
using Unosquare.PiGpio.ManagedModel;
using Unosquare.PiGpio.NativeEnums;

namespace Unosquare.PiGpio
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using RaspberryIO.Abstractions;

    /// <summary>
    /// Represents the Raspberry Pi GPIO controller
    /// as an IReadOnlyCollection of GpioPins
    /// Low level operations are accomplished by using the PiGPIO library.
    /// </summary>
    public class GpioController : IGpioController
    {
        /// <inheritdoc />

        private static readonly object SyncRoot = new object();
        private readonly List<GpioPin> _pins;

        /// <summary>
        /// Initializes static members of the <see cref="GpioController"/> class.
        /// </summary>
        static GpioController()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GpioController"/> class.
        /// </summary>
        /// <exception cref="System.Exception">Unable to initialize the GPIO controller.</exception>
        internal GpioController(ControllerMode mode)
        {
            if (_pins != null)
                return;

            if (IsInitialized == false)
            {
                var initResult = Initialize(mode);
                if (initResult == false)
                    throw new Exception("Unable to initialize the GPIO controller.");
            }

            _pins = new List<GpioPin>
            {
                GpioPin.Pin00.Value,
                GpioPin.Pin01.Value,
                GpioPin.Pin02.Value,
                GpioPin.Pin03.Value,
                GpioPin.Pin04.Value,
                GpioPin.Pin05.Value,
                GpioPin.Pin06.Value,
                GpioPin.Pin07.Value,
                GpioPin.Pin08.Value,
                GpioPin.Pin09.Value,
                GpioPin.Pin10.Value,
                GpioPin.Pin11.Value,
                GpioPin.Pin12.Value,
                GpioPin.Pin13.Value,
                GpioPin.Pin14.Value,
                GpioPin.Pin15.Value,
                GpioPin.Pin16.Value,
                GpioPin.Pin17.Value,
                GpioPin.Pin18.Value,
                GpioPin.Pin19.Value,
                GpioPin.Pin20.Value,
                GpioPin.Pin21.Value,
                GpioPin.Pin22.Value,
                GpioPin.Pin23.Value,
                GpioPin.Pin24.Value,
                GpioPin.Pin25.Value,
                GpioPin.Pin26.Value,
                GpioPin.Pin27.Value,
                GpioPin.Pin28.Value,
                GpioPin.Pin29.Value,
                GpioPin.Pin30.Value,
                GpioPin.Pin31.Value,
            };

            var headerP1 = new Dictionary<int, GpioPin>(_pins.Count);
            var headerP5 = new Dictionary<int, GpioPin>(_pins.Count);
            foreach (var pin in _pins)
            {
                if (pin.PhysicalPinNumber < 0)
                    continue;

                var header = pin.Header == GpioHeader.P1 ? headerP1 : headerP5;
                header[pin.PhysicalPinNumber] = pin;
            }

            HeaderP1 = new ReadOnlyDictionary<int, GpioPin>(headerP1);
            HeaderP5 = new ReadOnlyDictionary<int, GpioPin>(headerP5);
        }

        #region Individual Pin Properties

        /// <summary>
        /// Provides direct access to Pin known as BCM0.
        /// </summary>
        public static GpioPin Pin00 => GpioPin.Pin00.Value;

        /// <summary>
        /// Provides direct access to Pin known as BCM1.
        /// </summary>
        public static GpioPin Pin01 => GpioPin.Pin01.Value;

        /// <summary>
        /// Provides direct access to Pin known as BCM2.
        /// </summary>
        public static GpioPin Pin02 => GpioPin.Pin02.Value;

        /// <summary>
        /// Provides direct access to Pin known as BCM3.
        /// </summary>
        public static GpioPin Pin03 => GpioPin.Pin03.Value;

        /// <summary>
        /// Provides direct access to Pin known as BCM4.
        /// </summary>
        public static GpioPin Pin04 => GpioPin.Pin04.Value;

        /// <summary>
        /// Provides direct access to Pin known as BCM5.
        /// </summary>
        public static GpioPin Pin05 => GpioPin.Pin05.Value;

        /// <summary>
        /// Provides direct access to Pin known as BCM6.
        /// </summary>
        public static GpioPin Pin06 => GpioPin.Pin06.Value;

        /// <summary>
        /// Provides direct access to Pin known as BCM7.
        /// </summary>
        public static GpioPin Pin07 => GpioPin.Pin07.Value;

        /// <summary>
        /// Provides direct access to Pin known as BCM8.
        /// </summary>
        public static GpioPin Pin08 => GpioPin.Pin08.Value;

        /// <summary>
        /// Provides direct access to Pin known as BCM9.
        /// </summary>
        public static GpioPin Pin09 => GpioPin.Pin09.Value;

        /// <summary>
        /// Provides direct access to Pin known as BCM10.
        /// </summary>
        public static GpioPin Pin10 => GpioPin.Pin10.Value;

        /// <summary>
        /// Provides direct access to Pin known as BCM11.
        /// </summary>
        public static GpioPin Pin11 => GpioPin.Pin11.Value;

        /// <summary>
        /// Provides direct access to Pin known as BCM12.
        /// </summary>
        public static GpioPin Pin12 => GpioPin.Pin12.Value;

        /// <summary>
        /// Provides direct access to Pin known as BCM13.
        /// </summary>
        public static GpioPin Pin13 => GpioPin.Pin13.Value;

        /// <summary>
        /// Provides direct access to Pin known as BCM14.
        /// </summary>
        public static GpioPin Pin14 => GpioPin.Pin14.Value;

        /// <summary>
        /// Provides direct access to Pin known as BCM15.
        /// </summary>
        public static GpioPin Pin15 => GpioPin.Pin15.Value;

        /// <summary>
        /// Provides direct access to Pin known as BCM16.
        /// </summary>
        public static GpioPin Pin16 => GpioPin.Pin16.Value;

        /// <summary>
        /// Provides direct access to Pin known as BCM17.
        /// </summary>
        public static GpioPin Pin17 => GpioPin.Pin17.Value;

        /// <summary>
        /// Provides direct access to Pin known as BCM18.
        /// </summary>
        public static GpioPin Pin18 => GpioPin.Pin18.Value;

        /// <summary>
        /// Provides direct access to Pin known as BCM19.
        /// </summary>
        public static GpioPin Pin19 => GpioPin.Pin19.Value;

        /// <summary>
        /// Provides direct access to Pin known as BCM20.
        /// </summary>
        public static GpioPin Pin20 => GpioPin.Pin20.Value;

        /// <summary>
        /// Provides direct access to Pin known as BCM21.
        /// </summary>
        public static GpioPin Pin21 => GpioPin.Pin21.Value;

        /// <summary>
        /// Provides direct access to Pin known as BCM22.
        /// </summary>
        public static GpioPin Pin22 => GpioPin.Pin22.Value;

        /// <summary>
        /// Provides direct access to Pin known as BCM23.
        /// </summary>
        public static GpioPin Pin23 => GpioPin.Pin23.Value;

        /// <summary>
        /// Provides direct access to Pin known as BCM24.
        /// </summary>
        public static GpioPin Pin24 => GpioPin.Pin24.Value;

        /// <summary>
        /// Provides direct access to Pin known as BCM25.
        /// </summary>
        public static GpioPin Pin25 => GpioPin.Pin25.Value;

        /// <summary>
        /// Provides direct access to Pin known as BCM26.
        /// </summary>
        public static GpioPin Pin26 => GpioPin.Pin26.Value;

        /// <summary>
        /// Provides direct access to Pin known as BCM27.
        /// </summary>
        public static GpioPin Pin27 => GpioPin.Pin27.Value;

        /// <summary>
        /// Provides direct access to Pin known as BCM28 (available on Header P5).
        /// </summary>
        public static GpioPin Pin28 => GpioPin.Pin28.Value;

        /// <summary>
        /// Provides direct access to Pin known as BCM29 (available on Header P5).
        /// </summary>
        public static GpioPin Pin29 => GpioPin.Pin29.Value;

        /// <summary>
        /// Provides direct access to Pin known as BCM30 (available on Header P5).
        /// </summary>
        public static GpioPin Pin30 => GpioPin.Pin30.Value;

        /// <summary>
        /// Provides direct access to Pin known as BCM31 (available on Header P5).
        /// </summary>
        public static GpioPin Pin31 => GpioPin.Pin31.Value;

        #endregion

        #region IReadOnlyCollection Implementation

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<GpioPin> GetEnumerator() => Pins.GetEnumerator();

        /// <inheritdoc />
        IEnumerator<IGpioPin> IEnumerable<IGpioPin>.GetEnumerator() => Pins.GetEnumerator();

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => Pins.GetEnumerator();

        #endregion

        /// <summary>
        /// Initializes the controller given the initialization mode and pin numbering scheme.
        /// </summary>
        /// <param name="mode">The mode.</param>
        /// <returns>True when successful.</returns>
        /// <exception cref="PlatformNotSupportedException">
        /// This library does not support the platform.
        /// </exception>
        /// <exception cref="InvalidOperationException">Library was already Initialized.</exception>
        /// <exception cref="ArgumentException">The init mode is invalid.</exception>
        private bool Initialize(ControllerMode mode)
        {
            if (SwanRuntime.OS != Swan.OperatingSystem.Unix)
                throw new PlatformNotSupportedException("This library does not support the platform");

            lock (SyncRoot)
            {
                if (IsInitialized)
                    throw new InvalidOperationException($"Cannot call {nameof(Initialize)} more than once.");

                int setupResult;

                switch (mode)
                {
                    case ControllerMode.Direct:
                        {
                            setupResult = 0;
                            break;
                        }

                    case ControllerMode.Pipe:
                        {
                            throw new NotImplementedException("Use Direct mode");
                            break;
                        }

                    case ControllerMode.Socket:
                        {
                            throw new NotImplementedException("Use Direct mode");
                            break;
                        }

                    default:
                        {
                            throw new ArgumentException($"'{mode}' is not a valid initialization mode.");
                        }
                }

                Mode = setupResult == 0 ? mode : ControllerMode.NotInitialized;
                return IsInitialized;
            }
        }

        /// <summary>
        /// Determines if the underlying GPIO controller has been initialized properly.
        /// </summary>
        /// <value>
        /// <c>true</c> if the controller is properly initialized; otherwise, <c>false</c>.
        /// </value>
        public static bool IsInitialized
        {
            get
            {
                lock (SyncRoot)
                {
                    return Mode != ControllerMode.NotInitialized;
                }
            }
        }

        /// <summary>
        /// Gets a read-only collection of all pins.
        /// </summary>
        public ReadOnlyCollection<GpioPin> Pins => new ReadOnlyCollection<GpioPin>(_pins);

        /// <summary>
        /// Provides all the pins on Header P1 of the Pi as a lookup by physical header pin number.
        /// This header is the main header and it is the one commonly used.
        /// </summary>
        public ReadOnlyDictionary<int, GpioPin> HeaderP1 { get; }

        /// <summary>
        /// Provides all the pins on Header P5 of the Pi as a lookup by physical header pin number.
        /// This header is the secondary header and it is rarely used.
        /// </summary>
        public ReadOnlyDictionary<int, GpioPin> HeaderP5 { get; }
        
        /// <inheritdoc />
                                                                         /// <summary>
                                                                         /// Gets the number of registered pins in the controller.
                                                                         /// </summary>
        public int Count => Pins.Count;

        /// <summary>
        /// Gets or sets the initialization mode.
        /// </summary>
        private static ControllerMode Mode { get; set; } = ControllerMode.NotInitialized;

        /// <inheritdoc />
        public IGpioPin this[int bcmPinNumber] => _pins[bcmPinNumber];

        /// <inheritdoc />
        public IGpioPin this[BcmPin bcmPin] => _pins[(int)bcmPin];

        /// <inheritdoc />
        public IGpioPin this[P1 pinNumber] => throw new NotImplementedException();

        /// <inheritdoc />
        public IGpioPin this[P5 pinNumber] => throw new NotImplementedException();
    }
}
