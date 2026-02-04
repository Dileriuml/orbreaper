using System;

namespace OrbReaper.UI.Progress
{
    public interface IProgressBar
    {
        float Value { get; set; }

        float MaxValue { get; set; }

        void SetFormatter(Func<float, string> valueFormatter);
    }
}