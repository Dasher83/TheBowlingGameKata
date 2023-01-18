using NUnit.Framework;


public class TestUtils
{
    [Test]
    public void ShouldReturnZero()
    {
        Assert.AreEqual(0, Utils.ReturnZero());
    }
}
