public class BowlingTurn
{
    private int shot1, shot2;
    public int Shot1 => shot1;
    public int Shot2 => shot2;

    public BowlingTurn(int shot1, int shot2)
    {
        this.shot1 = shot1;
        this.shot2 = shot2;
    }

    public override string ToString()
    {
        return $"{{{shot1},{shot2}}}";
    }
}
