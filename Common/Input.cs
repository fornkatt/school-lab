using System.Text.RegularExpressions;

namespace AdventOfCode.Common;

/// <summary>
/// Small helpers for reading Advent of Code puzzle input.
/// Parsing is not the point of the lab, so use these and focus on the rules.
/// </summary>
public static class Input
{
    /// <summary>The non-empty, trimmed lines of the text.</summary>
    public static string[] Lines(string text) =>
        text.Split('\n')
            .Select(l => l.Trim())
            .Where(l => l.Length > 0)
            .ToArray();

    /// <summary>Every integer in the text, in order, whatever separates them.</summary>
    public static int[] Numbers(string text) =>
        Regex.Split(text.Trim(), @"[^0-9-]+")
            .Where(s => s.Length > 0)
            .Select(int.Parse)
            .ToArray();
}
