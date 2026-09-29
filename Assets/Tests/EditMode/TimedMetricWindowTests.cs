using System;
using Churub.Core;
using NUnit.Framework;

public sealed class TimedMetricWindowTests
{
    [Test]
    public void Sum_KeepsOnlySamplesInsideWindow()
    {
        var window = new TimedMetricWindow(60d);
        window.Add(0d, 2d);
        window.Add(30d, 3d);

        Assert.That(window.Sum(60d), Is.EqualTo(5d));
        Assert.That(window.Sum(60.01d), Is.EqualTo(3d));
    }

    [Test]
    public void Add_RejectsOutOfOrderTimestamps()
    {
        var window = new TimedMetricWindow(60d);
        window.Add(10d, 1d);

        Assert.Throws<ArgumentException>(() => window.Add(9d, 1d));
    }

    [Test]
    public void Clear_RemovesSamplesAndAllowsFreshTimeline()
    {
        var window = new TimedMetricWindow(60d);
        window.Add(100d, 4d);
        window.Clear();
        window.Add(1d, 2d);

        Assert.That(window.Sum(1d), Is.EqualTo(2d));
        Assert.That(window.SampleCount(1d), Is.EqualTo(1));
    }
}
