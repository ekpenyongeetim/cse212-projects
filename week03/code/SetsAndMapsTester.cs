using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

public static class SetsAndMapsTester {
    public static void Run() {
        // Problem 1: Find Pairs with Sets
        Console.WriteLine("\n=========== Finding Pairs TESTS ===========");
        DisplayPairs(new[] { "am", "at", "ma", "if", "fi" });
        // ma & am
        // fi & if
        Console.WriteLine("---------");
        DisplayPairs(new[] { "ab", "bc", "cd", "de", "ba" });
        // ba & ab
        Console.WriteLine("---------");
        DisplayPairs(new[] { "ab", "ba", "ac", "ad", "da", "ca" });
        // ba & ab
        // da & ad
        // ca & ac
        Console.WriteLine("---------");
        DisplayPairs(new[] { "ab", "ac" }); // No pairs displayed
        Console.WriteLine("---------");
        DisplayPairs(new[] { "ab", "aa", "ba" });
        // ba & ab
        Console.WriteLine("---------");
        DisplayPairs(new[] { "23", "84", "49", "13", "32", "46", "91", "99", "94", "31", "57", "14" });
        // 32 & 23
        // 94 & 49
        // 31 & 13

        // Problem 2: Degree Summary
        // Sample Test Cases (may not be comprehensive) 
        Console.WriteLine("\n=========== Census TESTS ===========");
        Console.WriteLine(string.Join(", ", SummarizeDegrees("census.txt")));
        // Results may be in a different order:
        // <Dictionary>{[Bachelors, 5355], [HS-grad, 10501], [11th, 1175],
        // [Masters, 1723], [9th, 514], [Some-college, 7291], [Assoc-acdm, 1067],
        // [Assoc-voc, 1382], [7th-8th, 646], [Doctorate, 413], [Prof-school, 576],
        // [5th-6th, 333], [10th, 933], [1st-4th, 168], [Preschool, 51], [12th, 433]}

        // Problem 3: Anagrams
        // Sample Test Cases (may not be comprehensive) 
        Console.WriteLine("\n=========== Anagram TESTS ===========");
        Console.WriteLine(IsAnagram("CAT", "ACT")); // true
        Console.WriteLine(IsAnagram("DOG", "GOOD")); // false
        Console.WriteLine(IsAnagram("AABBCCDD", "ABCD")); // false
        Console.WriteLine(IsAnagram("ABCCD", "ABBCD")); // false
        Console.WriteLine(IsAnagram("BC", "AD")); // false
        Console.WriteLine(IsAnagram("Ab", "Ba")); // true
        Console.WriteLine(IsAnagram("A Decimal Point", "Im a Dot in Place")); // true
        Console.WriteLine(IsAnagram("tom marvolo riddle", "i am lord voldemort")); // true
        Console.WriteLine(IsAnagram("Eleven plus Two", "Twelve Plus One")); // true
        Console.WriteLine(IsAnagram("Eleven plus One", "Twelve Plus One")); // false

        // Problem 4: Maze
        Console.WriteLine("\n=========== Maze TESTS ===========");
        Dictionary<ValueTuple<int, int>, bool[]> map = SetupMazeMap();
        var maze = new Maze(map);
        maze.ShowStatus(); // Should be at (1,1)
        maze.MoveUp(); // Error
        maze.MoveLeft(); // Error
        maze.MoveRight();
        maze.MoveRight(); // Error
        maze.MoveDown();
        maze.MoveDown();
        maze.MoveDown();
        maze.MoveRight();
        maze.MoveRight();
        maze.MoveUp();
        maze.MoveRight();
        maze.MoveDown();
        maze.MoveLeft();
        maze.MoveDown(); // Error
        maze.MoveRight();
        maze.MoveDown();
        maze.MoveDown();
        maze.MoveRight();
        maze.ShowStatus(); // Should be at (6,6)

        // Problem 5: Earthquake
        // Sample Test Cases (may not be comprehensive) 
        Console.WriteLine("\n=========== Earthquake TESTS ===========");
        EarthquakeDailySummary().Wait();
    }

    /// <summary>
    /// The words parameter contains a list of two character 
    /// words (lower case, no duplicates). Using sets, find an O(n) 
    /// solution for displaying all symmetric pairs of words.  
    ///
    /// For example, if <c>words</c> was: <c>[am, at, ma, if, fi]</c>, we would display:
    /// <code>
    /// am &amp; ma
    /// if &amp; fi
    /// </code>
    /// The order of the display above does not matter. <c>at</c> would not 
    /// be displayed because <c>ta</c> is not in the list of words.
    ///
    /// As a special case, if the letters are the same (example: 'aa') then
    /// it would not match anything else (remember the assumption above
    /// that there were no duplicates) and therefore should not be displayed.
    /// </summary>
    /// <param name="words">An array of 2-character words (lowercase, no duplicates)</param>
    private static void DisplayPairs(string[] words) {
        var wordSet = new HashSet<string>(words);
    
        foreach (var word in words) {
            var reversed = new string(word.Reverse().ToArray());
            if (wordSet.Contains(reversed) && word != reversed) {
                Console.WriteLine($"{word} & {reversed}");
                wordSet.Remove(word);  // Remove to avoid duplicate pairs
                wordSet.Remove(reversed);
            }
        }
    }

    /// <summary>
    /// Read a census file and summarize the degrees (education)
    /// earned by those contained in the file.  The summary
    /// should be stored in a dictionary where the key is the
    /// degree earned and the value is the number of people that 
    /// have earned that degree.  The degree information is in
    /// the 4th column of the file.  There is no header row in the
    /// file.
    /// </summary>
    /// <param name="filename">The name of the file to read</param>
    /// <returns>Dictionary where key is degree and value is count</returns>
    private static Dictionary<string, int> SummarizeDegrees(string filename) {
        var degrees = new Dictionary<string, int>();
    
        foreach (var line in File.ReadLines(filename)) {
            var fields = line.Split(",");
            var degree = fields[3].Trim();
        
            if (degrees.ContainsKey(degree)) {
                degrees[degree]++;
            } else {
                degrees[degree] = 1;
            }
        }

        return degrees;
    }

    /// <summary>
    /// Determine if 'word1' and 'word2' are anagrams.  An anagram
    /// is when the same letters in a word are re-organized into a 
    /// new word.  A dictionary is used to solve the problem.
    /// 
    /// Examples:
    /// is_anagram("CAT","ACT") would return true
    /// is_anagram("DOG","GOOD") would return false because GOOD has 2 O's
    /// 
    /// Important Note: When determining if two words are anagrams, you
    /// should ignore any spaces.  You should also ignore cases.  For 
    /// example, 'Ab' and 'Ba' should be considered anagrams
    /// 
    /// Reminder: You can access a letter by index in a string by 
    /// using the [] notation.
    /// </summary>
    /// #############
    /// # Problem 3 #
    /// #############
    private static bool IsAnagram(string word1, string word2) {
        string Normalize(string str) => new string(str.ToLower().Where(char.IsLetter).ToArray());
    
        var normalizedWord1 = Normalize(word1);
        var normalizedWord2 = Normalize(word2);
    
        if (normalizedWord1.Length != normalizedWord2.Length) return false;
    
        var charCount1 = new Dictionary<char, int>();
        var charCount2 = new Dictionary<char, int>();
    
        foreach (var ch in normalizedWord1) {
            if (charCount1.ContainsKey(ch)) {
                charCount1[ch]++;
            } else {
                charCount1[ch] = 1;
            }
        }
    
        foreach (var ch in normalizedWord2) {
            if (charCount2.ContainsKey(ch)) {
                charCount2[ch]++;
            } else {
                charCount2[ch] = 1;
            }
        }
    
        return charCount1.Count == charCount2.Count && !charCount1.Except(charCount2).Any();
    }

    /// <summary>
    /// Sets up the maze dictionary for problem 4
    /// </summary>
    private static Dictionary<ValueTuple<int, int>, bool[]> SetupMazeMap() {

        Dictionary<ValueTuple<int, int>, bool[]> map = new() {
            { (1, 1), new[] { false, false, true, true } },
            { (1, 2), new[] { false, true, true, false } },
            { (1, 3), new[] { false, true, false, true } },
            { (2, 1), new[] { true, false, true, false } },
            { (2, 2), new[] { true, true, false, true } },
            { (2, 3), new[] { false, true, true, true } },
            { (3, 1), new[] { true, false, false, true } },
            { (3, 2), new[] { false, true, true, true } },
            { (3, 3), new[] { true, true, true, false } },
            { (4, 1), new[] { true, false, true, false } },
            { (4, 2), new[] { true, true, false, true } },
            { (4, 3), new[] { false, true, true, false } },
            { (5, 1), new[] { true, false, true, false } },
            { (5, 2), new[] { true, true, true, true } },
            { (5, 3), new[] { true, true, false, true } },
            { (6, 1), new[] { true, false, true, true } },
            { (6, 2), new[] { true, true, false, true } },
            { (6, 3), new[] { false, true, false, false } },
        };
        return map;
    }

    /// <summary>
    /// Represents a Maze with a map indicating possible movements.
    /// </summary>
    private class Maze {
        private Dictionary<(int, int), bool[]> map;
        private (int, int) position;

        public Maze(Dictionary<(int, int), bool[]> map) {
            this.map = map;
            this.position = (1, 1); // Starting position
        }

        public void MoveUp() => Move(0, -1);
        public void MoveRight() => Move(1, 0);
        public void MoveDown() => Move(0, 1);
        public void MoveLeft() => Move(-1, 0);

        private void Move(int deltaX, int deltaY) {
            var newPosition = (position.Item1 + deltaX, position.Item2 + deltaY);
            if (map.ContainsKey(position) && map.ContainsKey(newPosition)) {
                bool[] currentMoves = map[position];
                bool[] newMoves = map[newPosition];

                if (deltaX == 1 && currentMoves[2] && newMoves[3] ||
                    deltaX == -1 && currentMoves[3] && newMoves[2] ||
                    deltaY == 1 && currentMoves[1] && newMoves[0] ||
                    deltaY == -1 && currentMoves[0] && newMoves[1]) {
                    position = newPosition;
                    Console.WriteLine($"Moved to {position}");
                } else {
                    Console.WriteLine("Error: Cannot move in that direction");
                }
            } else {
                Console.WriteLine("Error: Cannot move outside the maze boundaries");
            }
        }

        public void ShowStatus() {
            Console.WriteLine($"Current position: {position}");
        }
    }

    /// <summary>
    /// Fetches earthquake data and summarizes the number of earthquakes per day.
    /// </summary>
    private static async Task EarthquakeDailySummary() {
        HttpClient client = new HttpClient();
        string url = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";
        string responseBody = await client.GetStringAsync(url);

        using JsonDocument document = JsonDocument.Parse(responseBody);
        var features = document.RootElement.GetProperty("features");

        Dictionary<string, int> quakeSummary = new Dictionary<string, int>();

        foreach (var feature in features.EnumerateArray()) {
            string date = DateTimeOffset.FromUnixTimeMilliseconds(feature.GetProperty("properties").GetProperty("time").GetInt64()).Date.ToString("yyyy-MM-dd");

            if (quakeSummary.ContainsKey(date)) {
                quakeSummary[date]++;
            } else {
                quakeSummary[date] = 1;
            }
        }

        foreach (var entry in quakeSummary) {
            Console.WriteLine($"{entry.Key}: {entry.Value} earthquake(s)");
        }
    }
}
