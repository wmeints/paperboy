using PaperBoy.Orchestrator.Models;

namespace PaperBoy.Orchestrator.Clients.ContentStore;

public class UpdatePaperStatusRequest
{
    public Guid PaperId { get; set; }
    public PaperStatus Status { get; set; }
    public string? Summary { get; set; }
    public int? Score { get; set; }
    public string? ScoreExplanation { get; set; }
    public string? Description { get; set; }
}