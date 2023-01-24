using GluonGui.Dialog;
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

        int result = 0, turnBaseScore;

        for (int i = 0; i < turns.Length; i++)
        {
            turnBaseScore = turns[i].Shot1 + turns[i].Shot2;
            result += turnBaseScore;

            if(IsSpare(turnBaseScore)) result += turns[i + 1].Shot1;
        }

        return result;
    }

    private static bool IsSpare(int turnBaseScore)
    {
        return turnBaseScore == 10;
    }
}
