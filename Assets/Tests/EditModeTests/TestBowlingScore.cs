using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestBowlingScore
{
    [Test]
    public void ShouldReturnAnInteger()
    {
        BowlingTurn[] turns = { };

        Assert.IsTrue(BowlingScore.Calculate(turns) is int);
    }

    [Test]
    public void ShouldThrowExceptionIfTurnHasMoreThan10PointsTotal()
    {
        BowlingTurn[] turns = { new BowlingTurn(4,7) };

        try
        {
            BowlingScore.Calculate(turns);
            Assert.Fail();
        }
        catch (ArgumentOutOfRangeException) {
            Assert.Pass();
        }
    }
}
