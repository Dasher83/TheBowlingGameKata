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
            new BowlingTurn(4, 5),
            new BowlingTurn(0, 0),
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
            new BowlingTurn(4, 6),
            new BowlingTurn(4, 6),
        };

        try
        {
            BowlingScore.Calculate(turns);
            Assert.Fail();
        }
        catch (ArgumentOutOfRangeException exception) {
            Assert.AreEqual("A regular turn total base score is over 11", exception.Message);
        }
    }

    [Test]
    public void ShouldThrowExceptionIfNotExactly11Turns()
    {
        BowlingTurn[] turns = { new BowlingTurn(4, 7) };

        try
        {
            BowlingScore.Calculate(turns);
            Assert.Fail();
        }
        catch (ArgumentOutOfRangeException exception)
        {
            Assert.AreEqual("turns length is not 11", exception.Message);
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
            new BowlingTurn(0, 0),
            new BowlingTurn(0, 0),
        };

        int expectedResult = 7;
        
        Assert.AreEqual(expectedResult, BowlingScore.Calculate(turns));
    }

    [Test]
    public void ShouldApplySpareScoreBonusWithoutBonusShot()
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
            new BowlingTurn(0, 0),
            new BowlingTurn(0, 0),
        };

        int expectedResult = 23;

        Assert.AreEqual(expectedResult, BowlingScore.Calculate(turns));
    }

    [Test]
    public void ShouldNotHaveNonZeroBonusTurnIfNotSpareNorStrike()
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
            new BowlingTurn(0, 0),
            new BowlingTurn(8, 0),
        };

        try
        {
            BowlingScore.Calculate(turns);
            Assert.Fail();
        }
        catch (ArgumentException exception)
        {
            Assert.AreEqual("Invalid non empty bonus turn", exception.Message);
        }
    }

    [Test]
    public void ShouldApplySpareScoreBonusWithBonusShot()
    {
        BowlingTurn[] turns = {
            new BowlingTurn(0, 0),
            new BowlingTurn(0, 0),
            new BowlingTurn(0, 0),
            new BowlingTurn(0, 0),
            new BowlingTurn(0, 0),
            new BowlingTurn(0, 0),
            new BowlingTurn(0, 0),
            new BowlingTurn(0, 0),
            new BowlingTurn(0, 0),
            new BowlingTurn(5, 5),
            new BowlingTurn(10, 0),
        };

        int expectedResult = 20;

        Assert.AreEqual(expectedResult, BowlingScore.Calculate(turns));
    }
    
    [Test]
    public void ShouldApplyStrikeScoreBonusWithoutBonusShot()
    {
        BowlingTurn[] turns = {
            new BowlingTurn(10, 0),
            new BowlingTurn(4, 5),
            new BowlingTurn(0, 0),
            new BowlingTurn(0, 0),
            new BowlingTurn(0, 0),
            new BowlingTurn(0, 0),
            new BowlingTurn(0, 0),
            new BowlingTurn(0, 0),
            new BowlingTurn(0, 0),
            new BowlingTurn(0, 0),
            new BowlingTurn(0, 0),
        };

        int expectedResult = 28;

        Assert.AreEqual(expectedResult, BowlingScore.Calculate(turns));
    }

    [Test]
    public void ShouldApplyStrikeBounsForConsecutiveStrikes()
    {
        BowlingTurn[] turns = {
            new BowlingTurn(10, 0),
            new BowlingTurn(10, 0),
            new BowlingTurn(5, 4),
            new BowlingTurn(0, 0),
            new BowlingTurn(0, 0),
            new BowlingTurn(0, 0),
            new BowlingTurn(0, 0),
            new BowlingTurn(0, 0),
            new BowlingTurn(0, 0),
            new BowlingTurn(0, 0),
            new BowlingTurn(0, 0),
        };

        int expectedResult = 53;

        Assert.AreEqual(expectedResult, BowlingScore.Calculate(turns));
    }

    [Test]
    public void ShouldApplyStrikeBounsForLastRegularTurn()
    {
        BowlingTurn[] turns = {
            new BowlingTurn(0, 0),
            new BowlingTurn(0, 0),
            new BowlingTurn(0, 0),
            new BowlingTurn(0, 0),
            new BowlingTurn(0, 0),
            new BowlingTurn(0, 0),
            new BowlingTurn(0, 0),
            new BowlingTurn(0, 0),
            new BowlingTurn(0, 0),
            new BowlingTurn(10, 0),
            new BowlingTurn(10, 10),
        };

        int expectedResult = 30;

        Assert.AreEqual(expectedResult, BowlingScore.Calculate(turns));
    }
}
