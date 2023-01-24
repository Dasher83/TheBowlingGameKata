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

        if(turns.Where((_, index) => index < TurnsLength - 1).Any(turn => turn.Shot1 + turn.Shot2 > 10))
        {
            throw new ArgumentOutOfRangeException(message: $"A regular turn total base score is over {TurnsLength}", innerException: null);
        }

        int bonusTurnIndex = TurnsLength - 1;
        int lastRegularTurnIndex = TurnsLength - 2;
        BowlingTurn lastRegularTurn = turns[lastRegularTurnIndex];

        if (!IsSpare(lastRegularTurn) && !IsStrike(lastRegularTurn) && !IsTurnEmpty(turns[bonusTurnIndex]))
        {
            throw new ArgumentException(message: "Invalid non empty bonus turn", innerException: null);
        }

        int result = 0, turnBaseScore;

        for (int i = 0; i < bonusTurnIndex; i++)
        {
            turnBaseScore = turns[i].Shot1 + turns[i].Shot2;

            if(i != bonusTurnIndex) result += turnBaseScore;

            if (IsSpare(turns[i])) result += turns[i + 1].Shot1;

            if (IsStrike(turns[i])) {
                if (i < lastRegularTurnIndex && IsStrike(turns[i + 1]))
                {
                    result += turns[i + 1].Shot1 + turns[i + 2].Shot1;
                }
                else
                {
                    result += turns[i + 1].Shot1 + turns[i + 1].Shot2; 
                }
            }
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
