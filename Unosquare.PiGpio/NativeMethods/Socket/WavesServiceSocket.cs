
namespace Unosquare.PiGpio.NativeMethods.Socket
{
    using Unosquare.PiGpio.NativeEnums;
    using Unosquare.PiGpio.NativeMethods.Interfaces;
    using Unosquare.PiGpio.NativeTypes;

    /// <summary>
    /// Waves Service Socket strategy pattern.
    /// </summary>
    internal class WavesServiceSocket : IWavesService
    {
        public int GpioWaveClear()
        {
            throw new System.NotImplementedException();
        }

        public int GpioWaveAddNew()
        {
            throw new System.NotImplementedException();
        }

        public int GpioWaveAddGeneric(uint numPulses, GpioPulse[] pulses)
        {
            throw new System.NotImplementedException();
        }

        public int GpioWaveAddSerial(UserGpio userGpio, uint baudRate, uint dataBits, uint stopBits, uint offset, uint numBytes,
            byte[] str)
        {
            throw new System.NotImplementedException();
        }

        public int GpioWaveCreate()
        {
            throw new System.NotImplementedException();
        }

        public ResultCode GpioWaveDelete(uint waveId)
        {
            throw new System.NotImplementedException();
        }

        public int GpioWaveTxSend(uint waveId, WaveMode waveMode)
        {
            throw new System.NotImplementedException();
        }

        public ResultCode GpioWaveChain(byte[] buffer, uint bufferSize)
        {
            throw new System.NotImplementedException();
        }

        public int GpioWaveTxAt()
        {
            throw new System.NotImplementedException();
        }

        public int GpioWaveTxBusy()
        {
            throw new System.NotImplementedException();
        }

        public int GpioWaveTxStop()
        {
            throw new System.NotImplementedException();
        }

        public int GpioWaveGetMicros()
        {
            throw new System.NotImplementedException();
        }

        public int GpioWaveGetHighMicros()
        {
            throw new System.NotImplementedException();
        }

        public int GpioWaveGetMaxMicros()
        {
            throw new System.NotImplementedException();
        }

        public int GpioWaveGetPulses()
        {
            throw new System.NotImplementedException();
        }

        public int GpioWaveGetHighPulses()
        {
            throw new System.NotImplementedException();
        }

        public int GpioWaveGetMaxPulses()
        {
            throw new System.NotImplementedException();
        }

        public int GpioWaveGetCbs()
        {
            throw new System.NotImplementedException();
        }

        public int GpioWaveGetHighCbs()
        {
            throw new System.NotImplementedException();
        }

        public int GpioWaveGetMaxCbs()
        {
            throw new System.NotImplementedException();
        }
    }
}