using System;
using NUnit.Framework;


public class TestBowlingScore
{
    [Test]
    public void ShouldReturnAnInteger()
    {
        BowlingTurn[] turns = {
            new BowlingTurn(4, 6),
            new BowlingTurn(4, 6),
            new BowlingTurn(4, 6),
            new BowlingTurn(4, 6),
            new BowlingTurn(4, 6),
            new BowlingTurn(4, 6),
            new BowlingTurn(4, 6),
            new BowlingTurn(4, 6),
            new BowlingTurn(4, 6),
            new BowlingTurn(4, 6)
        };

        Assert.IsTrue(BowlingScore.Calculate(turns) is int);
    }

    [Test]
    public void ShouldThrowExceptionIfTurnHasMoreThan10PointsTotal()
    {
        BowlingTurn[] turns = {
            new BowlingTurn(4, 7),
            new BowlingTurn(4, 6),
            new BowlingTurn(4, 6),
            new BowlingTurn(4, 6),
            new BowlingTurn(4, 6),
            new BowlingTurn(4, 6),
            new BowlingTurn(4, 6),
            new BowlingTurn(4, 6),
            new BowlingTurn(4, 6),
            new BowlingTurn(4, 6)
        };

        try
        {
            BowlingScore.Calculate(turns);
            Assert.Fail();
        }
        catch (ArgumentOutOfRangeException exception) {
            Assert.AreEqual("A turn total score is over 10", exception.Message);
        }
    }

    [Test]
    public void ShouldThrowExceptionIfNotExactly10Turns()
    {
        BowlingTurn[] turns = { new BowlingTurn(4, 7) };

        try
        {
            BowlingScore.Calculate(turns);
            Assert.Fail();
        }
        catch (ArgumentOutOfRangeException exception)
        {
            Assert.AreEqual("turns length is not 10", exception.Message);
        }
    }
    
    [Test]
    public void ShouldPartiallyReturnTheSumOfBothShotsIfNotStrikeNorSpare()
    {
        BowlingTurn[] turns = {
            new BowlingTurn(2, 5),
            new BowlingTurn(0, 0),
            new BowlingTurn(0, 0),
            new BowlingTurn(0, 0),
            new BowlingTurn(0, 0),
            new BowlingTurn(0, 0),
            new BowlingTurn(0, 0),
            new BowlingTurn(0, 0),
            new BowlingTurn(0, 0),
            new BowlingTurn(0, 0)
        };

        int expectedResult = 7;
        
        Assert.AreEqual(expectedResult, BowlingScore.Calculate(turns));
    }

    [Test]
    public void ShouldApplySpareScoreBonus()
    {
        BowlingTurn[] turns = {
            new BowlingTurn(8, 2),
            new BowlingTurn(4, 5),
            new BowlingTurn(0, 0),
            new BowlingTurn(0, 0),
            new BowlingTurn(0, 0),
            new BowlingTurn(0, 0),
            new BowlingTurn(0, 0),
            new BowlingTurn(0, 0),
            new BowlingTurn(0, 0),
            new BowlingTurn(0, 0)
        };

        int expectedResult = 23;

        Assert.AreEqual(expectedResult, BowlingScore.Calculate(turns));
    }
}
