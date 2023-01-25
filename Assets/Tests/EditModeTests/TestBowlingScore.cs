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
    
    [Test]
    public void ShouldYield300PointsAsIsAPerfectGame()
    {
        BowlingTurn[] turns = {
            new BowlingTurn(10, 0),
            new BowlingTurn(10, 0),
            new BowlingTurn(10, 0),
            new BowlingTurn(10, 0),
            new BowlingTurn(10, 0),
            new BowlingTurn(10, 0),
            new BowlingTurn(10, 0),
            new BowlingTurn(10, 0),
            new BowlingTurn(10, 0),
            new BowlingTurn(10, 0),
            new BowlingTurn(10, 10),
        };

        int expectedResult = 300;

        Assert.AreEqual(expectedResult, BowlingScore.Calculate(turns));
    }
    
    [Test]
    public void ShouldFollowBaseHappyPath()
    {
        BowlingTurn[] turns = {
            new BowlingTurn(10, 0), // TurnScore = 15
            new BowlingTurn(5, 0), // TurnScore = 5
            new BowlingTurn(2, 8), // TurnScore = 16
            new BowlingTurn(6, 4), // TurnScore = 20
            new BowlingTurn(10, 0), // TurnScore = 21
            new BowlingTurn(10, 0), // TurnScore = 11
            new BowlingTurn(1, 0), // TurnScore = 1
            new BowlingTurn(0, 0), //  TurnScore = 0
            new BowlingTurn(9, 0), // TurnScore = 9
            new BowlingTurn(7, 3), // TurnScore = 20
            new BowlingTurn(10, 0), // This is a bonus turn :D
        };

        int expectedResult = 118;

        Assert.AreEqual(expectedResult, BowlingScore.Calculate(turns));
    }
}
