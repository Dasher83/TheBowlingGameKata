using GluonGui.Dialog;
using System;
using System.Linq;

public static class BowlingScore
{
    private const int TurnsLength = 11;
    private const int TotalPins = 10;

    public static int Calculate(BowlingTurn[] turns)
    {
        if (turns.Length != TurnsLength)
        {
            throw new ArgumentOutOfRangeException(message: $"turns length is not {TurnsLength}", innerException: null);
        }

        if(turns.Any(turn => turn.Shot1 + turn.Shot2 > 10))
        {
            throw new ArgumentOutOfRangeException(message: $"A turn total base score is over {TurnsLength}", innerException: null);
        }

        BowlingTurn lastRegularTurn = turns[TurnsLength - 2];

        if (!IsSpare(lastRegularTurn) && !IsStrike(lastRegularTurn) && !IsTurnEmpty(turns[TurnsLength - 1]))
        {
            throw new ArgumentException(message: "Invalid non empty bonus turn", innerException: null);
        }

        int result = 0, turnBaseScore;

        for (int i = 0; i < turns.Length; i++)
        {
            turnBaseScore = turns[i].Shot1 + turns[i].Shot2;

            if(i != TurnsLength - 1) result += turnBaseScore;

            if (IsSpare(turns[i])) result += turns[i + 1].Shot1;
        }

        return result;
    }

    private static bool IsSpare(BowlingTurn turn)
    {
        return turn.Shot1 != TotalPins && turn.Shot1 + turn.Shot2 == TotalPins;
    }

    private static bool IsStrike(BowlingTurn turn)
    {
        return turn.Shot1 == TotalPins;
    }

    private static bool IsTurnEmpty(BowlingTurn turn)
    {
        return turn.Shot1 == 0 && turn.Shot2 == 0;
    }
}
