namespace Unosquare.PiGpio.Peripherals.cs
{
    using ManagedModel;
    using NativeEnums;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class OledDisplaySSD1306
    {
        static private readonly Dictionary<DisplayModel, byte> DisplayClockDivider = new Dictionary<DisplayModel, byte>
        {
            { DisplayModel.Display128x64, 0x80 },
            { DisplayModel.Display128x32, 0x80 },
            { DisplayModel.Display96x16, 0x60 },
        };

        static private readonly Dictionary<DisplayModel, byte> MultiplexSetting = new Dictionary<DisplayModel, byte>
        {
            { DisplayModel.Display128x64, 0x3F },
            { DisplayModel.Display128x32, 0x1F },
            { DisplayModel.Display96x16, 0x0F },
        };

        static private readonly Dictionary<DisplayModel, byte> ComPins = new Dictionary<DisplayModel, byte>
        {
            { DisplayModel.Display128x64, 0x12 },
            { DisplayModel.Display128x32, 0x02 },
            { DisplayModel.Display96x16, 0x02 },
        };

        public OledDisplaySSD1306(DisplayModel model)
           : this(GetDefaultDevice(), model, VccStateMode.SwitchingCapPower)
        {
            // placeholder
        }

        private byte m_Contrast = 0;
        private readonly BitArray BitBuffer;
        private int BufferPageCount;
        private int BitsPerPage;

        public OledDisplaySSD1306(I2cDevice device, DisplayModel model, VccStateMode vccState)
        {
            switch (model)
            {
                case DisplayModel.Display128x32:
                    {
                        Width = 128;
                        Height = 32;
                        break;
                    }

                case DisplayModel.Display128x64:
                    {
                        Width = 128;
                        Height = 64;
                        break;
                    }

                case DisplayModel.Display96x16:
                    {
                        Width = 96;
                        Height = 16;
                        break;
                    }

                default:
                    {
                        throw new ArgumentException("Invalid display model", nameof(model));
                    }
            }

            Model = model;
            Device = device ?? throw new ArgumentNullException(nameof(device));
            VccState = vccState;
            BufferPageCount = Height / 8;
            BitBuffer = new BitArray(Width * Height);
            BitsPerPage = 8 * Width;
            Initialize();
        }

        public I2cDevice Device { get; }

        public int Width { get; }

        public int Height { get; }

        public VccStateMode VccState { get; }

        public DisplayModel Model { get; }

        public byte Contrast
        {
            get
            {
                return m_Contrast;
            }
            set
            {
                SendCommand(Command.SetContrast, value);
                m_Contrast = value;
            }
        }

        public bool this[int x, int y]
        {
            get => BitBuffer[GetBitIndex(x, y)];
            set =>  BitBuffer[GetBitIndex(x, y)] = value;
        }

        public static I2cDevice GetDefaultDevice() =>
            Board.Peripherals.OpenI2cDevice(I2cBusId.Bus1, 0x3c);

        private void Initialize()
        {
            SendCommand(Command.TurnOff);
            SendCommand(Command.SetClockDivider, DisplayClockDivider[Model]);
            SendCommand(Command.SetMultiplexer, MultiplexSetting[Model]);
            SendCommand(Command.SetDisplayOffset);
            SendCommand(Command.SetStartLine, 0x00);
            SendCommand(Command.SetChargePumpMode, (byte)(VccState == VccStateMode.ExternalPower ? 0x10 : 0x14));
            SendCommand(Command.SetMemoryMode, 0x00);
            SendCommand(Command.SegmentRemapModeOn);
            SendCommand(Command.ScanDirectionModeDecrement);
            SendCommand(Command.SetComPins, ComPins[Model]);

            if (Model == DisplayModel.Display128x64)
                m_Contrast = (byte)(VccState == VccStateMode.ExternalPower ? 0x9F : 0xCF);
            else
                m_Contrast = 0x8F;

            SendCommand(Command.SetContrast, m_Contrast);
            SendCommand(Command.SetPrechargeMode, (byte)(VccState == VccStateMode.ExternalPower ? 0x22 : 0xF1));
            SendCommand(Command.SetVoltageOutput, 0x40);
            SendCommand(Command.Resume);
            SendCommand(Command.DisplayModeNormal);
        }

        private int GetBitIndex(int x, int y) =>
            ((y / 8) * BitsPerPage) + (x * 8) + (y % 8);

        public void SendCommand(Command command, byte argument)
        {
            SendCommand(command);
            SendCommand(argument);
        }

        public void SendCommand(Command command, byte arg0, byte arg1)
        {
            SendCommand(command);
            SendCommand(arg0);
            SendCommand(arg1);
        }

        public void SendCommand(byte command)
        {
            Device.Write(0x00, command);
        }

        public void SendCommand(Command command) =>
            SendCommand((byte)command);

        public void SendData(byte data) =>
            Device.Write(0x40, data);

        public void Render()
        {
            SendCommand(Command.SetColumnAddressRange, 0, (byte)(Width - 1));
            SendCommand(Command.SetPageAddressRange, 0, (byte)(BufferPageCount - 1));
            var byteBuffer = new byte[16];

            for (var i = 0; i < BitBuffer.Length; i+= byteBuffer.Length * 8)
            {
                BitBuffer.CopyTo(byteBuffer, i);
                Device.Write(0x40, byteBuffer);
            }
        }

        public enum DisplayModel
        {
            Display128x64 = 0,
            Display128x32 = 1,
            Display96x16 = 2,
        }

        public enum Command
        {
            SetContrast = 0x81,
            Resume = 0xA4,
            EntireDisplayOn = 0xA5,
            DisplayModeNormal = 0xA6,
            DisplayModeInvert = 0xA7,
            TurnOff = 0xAE,
            TurnOn = 0xAF,
            SetDisplayOffset = 0xD3,
            SetComPins = 0xDA,
            SetVoltageOutput = 0xDB,
            SetClockDivider = 0xD5,
            SetPrechargeMode = 0xD9,
            SetMultiplexer = 0xA8,
            SetLowColumn = 0x00,
            SetHighColumn = 0x10,
            SetStartLine = 0x40,
            SetMemoryMode = 0x20,
            SetColumnAddressRange = 0x21,
            SetPageAddressRange = 0x22,
            ScanDirectionModeIncrement = 0xC0,
            ScanDirectionModeDecrement = 0xC8,
            SegmentRemapModeOff = 0xA0,
            SegmentRemapModeOn = 0xA1,
            SetChargePumpMode = 0x8D,
        }

        public enum VccStateMode
        {
            ExternalPower = 0x1,
            SwitchingCapPower = 0x2,
        }

        private enum ScrollMode
        {
            SSD1306_ACTIVATE_SCROLL = 0x2F,
            SSD1306_DEACTIVATE_SCROLL = 0x2E,
            SSD1306_SET_VERTICAL_SCROLL_AREA = 0xA3,
            SSD1306_RIGHT_HORIZONTAL_SCROLL = 0x26,
            SSD1306_LEFT_HORIZONTAL_SCROLL = 0x27,
            SSD1306_VERTICAL_AND_RIGHT_HORIZONTAL_SCROLL = 0x29,
            SSD1306_VERTICAL_AND_LEFT_HORIZONTAL_SCROLL = 0x2A,
        }
    }
}
