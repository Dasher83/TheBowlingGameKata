using System;
using System.Linq;

public static class BowlingScore
{
    private const int TurnsLength = 10;

    public static int Calculate(BowlingTurn[] turns)
    {
        if (turns.Length != TurnsLength)
        {
            throw new ArgumentOutOfRangeException(message: "turns length is not 10", innerException: null);
        }

        if(turns.Any(turn => turn.Shot1 + turn.Shot2 > 10))
        {
            throw new ArgumentOutOfRangeException(message: "A turn total score is over 10", innerException: null);
        }

        return -1;
    }
}
