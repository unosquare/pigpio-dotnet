namespace Unosquare.PiGpio.Workbench.Runners
{
    using System.Threading;
    using Unosquare.Swan;

    internal class BoardInfo : RunnerBase
    {
        public BoardInfo(bool isEnabled)
            : base(isEnabled) { }

        protected override void DoBackgroundWork(CancellationToken ct)
        {
            $"{nameof(Board.BoardType),-22}: {Board.BoardType}".Info(Name);
            $"{nameof(Board.HardwareRevision),-22}: {Board.HardwareRevision}".Info(Name);
            $"{nameof(Board.IsAvailable),-22}: {Board.IsAvailable}".Info(Name);
            $"{nameof(Board.LibraryVersion),-22}: {Board.LibraryVersion}".Info(Name);
            $"{nameof(Board.Timing.Timestamp),-22}: {Board.Timing.Timestamp}".Info(Name);
            $"{nameof(Board.Timing.TimestampSeconds),-22}: {Board.Timing.TimestampSeconds}".Info(Name);
            $"{nameof(Board.Timing.TimestampMicroseconds),-22}: {Board.Timing.TimestampMicroseconds}".Info(Name);
        }
    }
}
