using PaperBoy.Orchestrator.Models;

namespace PaperBoy.Orchestrator.Activities;

/// <summary>
/// Represents the output of the GetPaperDetailsActivity.
/// </summary>
/// <param name="Title">The title of the paper.</param>
/// <param name="Pages">The list of pages in the paper.</param>
public record GetPaperDetailsActivityOutput(string Title, List<PageData> Pages);