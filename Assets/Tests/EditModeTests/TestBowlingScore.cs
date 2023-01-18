using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestBowlingScore
{
    // A Test behaves as an ordinary method
    [Test]
    public void ShouldReturnAnInteger()
    {
        BowlingTurn[] turns = { };

        Assert.IsTrue(BowlingScore.Calculate(turns) is int);
    }
}
