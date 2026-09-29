using Churub.Core;
using NUnit.Framework;

public sealed class NumberAbbreviatorTests
{
    [TestCase(0, "0")]
    [TestCase(999, "999")]
    [TestCase(1000, "1K")]
    [TestCase(1500, "1.5K")]
    [TestCase(1000000, "1M")]
    [TestCase(1250000, "1.25M")]
    [TestCase(1000000000, "1B")]
    [TestCase(1000000000000, "1T")]
    [TestCase(-1500, "-1.5K")]
    [TestCase(1001000, "1M")]          // 1.001M rounds down, trailing zeros stripped
    [TestCase(1234000, "1.23M")]       // two decimals max
    [TestCase(999999, "1M")]           // 999.999K rounds up and promotes to M
    public void Format_AbbreviatesWithSuffix(double value, string expected)
    {
        Assert.That(NumberAbbreviator.Format(value), Is.EqualTo(expected));
    }

    [Test]
    public void Format_BeyondLargestSuffix_KeepsLargestUnitInsteadOfOverflowing()
    {
        Assert.That(NumberAbbreviator.Format(1e15), Is.EqualTo("1000T"));
        Assert.That(NumberAbbreviator.Format(2.5e15), Is.EqualTo("2500T"));
    }

    [Test]
    public void Format_NonFiniteValues_DoNotThrow()
    {
        Assert.That(NumberAbbreviator.Format(double.NaN), Is.EqualTo("NaN"));
        Assert.That(NumberAbbreviator.Format(double.PositiveInfinity), Is.EqualTo("Infinity"));
        Assert.That(NumberAbbreviator.Format(double.NegativeInfinity), Is.EqualTo("-Infinity"));
    }

    [Test]
    public void Format_NearZero_DropsSignWhenRoundingToZero()
    {
        Assert.That(NumberAbbreviator.Format(0.001), Is.EqualTo("0"));
        Assert.That(NumberAbbreviator.Format(-0.001), Is.EqualTo("0"));
        Assert.That(NumberAbbreviator.Format(0.004), Is.EqualTo("0"));
        Assert.That(NumberAbbreviator.Format(-0.004), Is.EqualTo("0"));
        Assert.That(NumberAbbreviator.Format(0.5), Is.EqualTo("0.5"));
        Assert.That(NumberAbbreviator.Format(-0.5), Is.EqualTo("-0.5"));
        Assert.That(NumberAbbreviator.Format(-0d), Is.EqualTo("0"));
    }

    [Test]
    [SetCulture("de-DE")] // German uses ',' as decimal separator; output must not.
    public void Format_IgnoresCurrentCulture()
    {
        Assert.That(NumberAbbreviator.Format(1500), Is.EqualTo("1.5K"));
        Assert.That(NumberAbbreviator.Format(1250000), Is.EqualTo("1.25M"));
    }
}
