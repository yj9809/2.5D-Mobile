using System;
using System.Collections.Generic;

namespace Churub.Core
{
    public sealed class TimedMetricWindow
    {
        private readonly struct Sample
        {
            public Sample(double time, double value)
            {
                Time = time;
                Value = value;
            }

            public double Time { get; }
            public double Value { get; }
        }

        private readonly Queue<Sample> samples = new Queue<Sample>();
        private double total;
        private double lastTimestamp = double.NegativeInfinity;

        public TimedMetricWindow(double windowSeconds)
        {
            if (windowSeconds <= 0d || double.IsNaN(windowSeconds) || double.IsInfinity(windowSeconds))
                throw new ArgumentOutOfRangeException(nameof(windowSeconds));
            WindowSeconds = windowSeconds;
        }

        public double WindowSeconds { get; }

        public void Add(double timestamp, double value)
        {
            if (double.IsNaN(timestamp) || double.IsInfinity(timestamp))
                throw new ArgumentOutOfRangeException(nameof(timestamp));
            if (timestamp < lastTimestamp)
                throw new ArgumentException("Metric timestamps must be monotonic.", nameof(timestamp));
            if (double.IsNaN(value) || double.IsInfinity(value))
                throw new ArgumentOutOfRangeException(nameof(value));

            lastTimestamp = timestamp;
            samples.Enqueue(new Sample(timestamp, value));
            total += value;
            Prune(timestamp);
        }

        public double Sum(double now)
        {
            Prune(now);
            return total;
        }

        public int SampleCount(double now)
        {
            Prune(now);
            return samples.Count;
        }

        public void Clear()
        {
            samples.Clear();
            total = 0d;
            lastTimestamp = double.NegativeInfinity;
        }

        private void Prune(double now)
        {
            double cutoff = now - WindowSeconds;
            while (samples.Count > 0 && samples.Peek().Time < cutoff)
                total -= samples.Dequeue().Value;
            if (Math.Abs(total) < 0.0000001d) total = 0d;
        }
    }
}
