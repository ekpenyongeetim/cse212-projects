/*
 * CSE 212 Lesson 6C 
 * 
 * This code will analyze the NBA basketball data and create a table showing
 * the players with the top 10 career points.
 * 
 * Note about columns:
 * - Player ID is in column 0
 * - Points is in column 8
 * 
 * Each row represents the player's stats for a single season with a single team.
 */

using Microsoft.VisualBasic.FileIO;

public class Basketball
{
    public static void Run()
    {
        var players = new Dictionary<string, int>();

        using var reader = new TextFieldParser("basketball.csv");
        reader.TextFieldType = FieldType.Delimited;
        reader.SetDelimiters(",");
        reader.ReadFields(); // ignore header row
        while (!reader.EndOfData) {
            var fields = reader.ReadFields()!;
            var playerId = fields[0];
            var points = int.Parse(fields[8]);

            if (players.ContainsKey(playerId))
                players[playerId] += points;
            else
                players[playerId] = points;
        }

        // Sort the players by points in descending order and take the top 10
        var topPlayers = players.OrderByDescending(p => p.Value).Take(10).ToArray();

        // Display the top 10 players
        Console.WriteLine("Top 10 Players with Highest Total Points:");
        Console.WriteLine("Player ID\tTotal Points");
        foreach (var player in topPlayers)
        {
            Console.WriteLine($"{player.Key}\t{player.Value}");
        }
    }
}