using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class PitScoutData
{
    public readonly int teamNumber;
    public readonly string shooter;


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
                continue;
            }
            sb.Append(c);
        }
        parsedRow.Add(sb.ToString());
        return parsedRow;
    }
}
