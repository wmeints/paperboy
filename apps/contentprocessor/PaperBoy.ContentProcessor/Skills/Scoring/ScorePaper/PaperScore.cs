using System.Text.Json.Serialization;

namespace PaperBoy.ContentProcessor.Skills.Scoring.ScorePaper;

public class PaperScore
{
    [JsonPropertyName("score")]
    public int Score { get; set; }
    
    [JsonPropertyName("explanation")]
    public string Explanation { get; set; } = default!;
}
