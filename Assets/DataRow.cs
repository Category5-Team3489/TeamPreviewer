public class DataRow
{
    public readonly int team;
    public readonly string[] answers;

    public DataRow(int team, params string[] answers)
    {
        this.team = team;
        this.answers = answers;
    }
}