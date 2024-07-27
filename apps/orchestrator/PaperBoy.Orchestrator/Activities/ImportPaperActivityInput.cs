using PaperBoy.Orchestrator.Models;

namespace PaperBoy.Orchestrator.Activities;

/// <summary>
/// Input information for the <see cref="ImportPaperActivityInput"/>
/// </summary>
/// <param name="Url">The URL where the paper can be downloaded.</param>
/// <param name="Submitter">The information of the person who submitted the paper.</param>
public record ImportPaperActivityInput(string Url, SubmitterInformation Submitter);