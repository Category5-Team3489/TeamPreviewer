public class DataRow
{
    public readonly int team;
    public readonly string notes;
    public readonly int[] answers;

    public DataRow(int team, string notes, params int[] answers)
    {
        this.team = team;
        this.notes = notes;
        this.answers = answers;
    }
}