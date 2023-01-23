using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static class BowlingScore
{
    public static int Calculate(BowlingTurn[] turns)
    {
        if(turns.Any(turn => turn.Shot1 + turn.Shot2 > 10))
        {
            throw new ArgumentOutOfRangeException();
        }
        return -1;
    }
}
