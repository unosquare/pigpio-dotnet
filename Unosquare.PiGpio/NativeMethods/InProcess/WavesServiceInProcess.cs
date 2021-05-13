
namespace Unosquare.PiGpio.NativeMethods.InProcess
{
    using Unosquare.PiGpio.NativeEnums;
    using Unosquare.PiGpio.NativeMethods.InProcess.DllImports;
    using Unosquare.PiGpio.NativeMethods.Interfaces;
    using Unosquare.PiGpio.NativeTypes;

    /// <summary>
    /// Waves Service In Process strategy pattern.
    /// </summary>
    internal class WavesServiceInProcess : IWavesService
    {
        public int GpioWaveClear()
        {
            return Waves.GpioWaveClear();
        }

        public int GpioWaveAddNew()
        {
            return Waves.GpioWaveAddNew();
        }

        public int GpioWaveAddGeneric(uint numPulses, GpioPulse[] pulses)
        {
            return Waves.GpioWaveAddGeneric(numPulses, pulses);
        }

        public int GpioWaveAddSerial(UserGpio userGpio, uint baudRate, uint dataBits, uint stopBits, uint offset, uint numBytes, byte[] str)
        {
            return Waves.GpioWaveAddSerial(userGpio, baudRate, dataBits, stopBits, offset, numBytes, str);
        }

        public int GpioWaveCreate()
        {
            return Waves.GpioWaveCreate();
        }

        public ResultCode GpioWaveDelete(uint waveId)
        {
            return Waves.GpioWaveDelete(waveId);
        }

        public int GpioWaveTxSend(uint waveId, WaveMode waveMode)
        {
            return Waves.GpioWaveTxSend(waveId, waveMode);
        }

        public ResultCode GpioWaveChain(byte[] buffer, uint bufferSize)
        {
            return Waves.GpioWaveChain(buffer, bufferSize);
        }

        public int GpioWaveTxAt()
        {
            return Waves.GpioWaveTxAt();
        }

        public int GpioWaveTxBusy()
        {
            return Waves.GpioWaveTxBusy();
        }

        public int GpioWaveTxStop()
        {
            return Waves.GpioWaveTxStop();
        }

        public int GpioWaveGetMicros()
        {
            return Waves.GpioWaveGetMicros();
        }

        public int GpioWaveGetHighMicros()
        {
            return Waves.GpioWaveGetHighMicros();
        }

        public int GpioWaveGetMaxMicros()
        {
            return Waves.GpioWaveGetMaxMicros();
        }

        public int GpioWaveGetPulses()
        {
            return Waves.GpioWaveGetPulses();
        }

        public int GpioWaveGetHighPulses()
        {
            return Waves.GpioWaveGetHighPulses();
        }

        public int GpioWaveGetMaxPulses()
        {
            return Waves.GpioWaveGetMaxPulses();
        }

        public int GpioWaveGetCbs()
        {
            return Waves.GpioWaveGetCbs();
        }

        public int GpioWaveGetHighCbs()
        {
            return Waves.GpioWaveGetHighCbs();
        }

        public int GpioWaveGetMaxCbs()
        {
            return Waves.GpioWaveGetMaxCbs();
        }
    }
}