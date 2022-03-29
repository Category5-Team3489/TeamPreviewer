public class DataExportRow
{
    // event #
    // team #
    // match #
    // scout
    // note
    // aq1 Cargo loaded at start of match: no, yes
    // aq2 Fully exited the tarmac at least once: no, yes
    // aq3 Cargo scored in low goal
    // aq4 Cargo scored in high goal
    // tq1 Cargo in low goal
    // tq2 Cargo in high goal
    // tq3 Max cargo ever in robot: 0, 1, 2, >2
    // tq4 Was penalized at least once: no, yes
    // eq1 Bar climbed: no attempt, failed, low, mid, high, traversal
    // eq2 Climbing time after entering hanging station

    public readonly int eventId;
    public readonly int teamNum;
    public readonly int matchNum;
    public readonly int scoutId;
    public readonly string note;
    
    public readonly int aq1;
    public readonly int aq2;
    public readonly int aq3;
    public readonly int aq4;

    public readonly int tq1;
    public readonly int tq2;
    public readonly int tq3;
    public readonly int tq4;

    public readonly int eq1;
    public readonly int eq2;

    public DataExportRow(string rawRow)
    {
        List<string> row = ParseExportRow(rawRow);

        int.TryParse(row[0], out eventId);
        int.TryParse(row[1], out teamNum);
        int.TryParse(row[2], out matchNum);
        int.TryParse(row[3], out scoutId);
        note = row[4];

        int.TryParse(row[5], out aq1);
        int.TryParse(row[6], out aq2);
        int.TryParse(row[7], out aq3);
        int.TryParse(row[8], out aq4);

        int.TryParse(row[9], out tq1);
        int.TryParse(row[10], out tq2);
        int.TryParse(row[11], out tq3);
        int.TryParse(row[12], out tq4);

        int.TryParse(row[13], out eq1);
        int.TryParse(row[14], out eq2);
    }

    private static List<string> ParseExportRow(string row)
    {
        List<string> parsedRow = new List<string>();
        StringBuilder sb = new StringBuilder();
        foreach (char c in row)
        {
            if (c == '\t')
            {
                parsedRow.Add(sb.ToString());
                sb.Clear();
            }
            sb.Append(c);
        }
        return parsedRow;
    }
}