﻿using PaperBoy.ContentStore.Domain;

namespace PaperBoy.ContentStore.Requests;

/// <summary>
/// Represents a request to submit a summary for a paper.
/// </summary>
/// <param name="Summary">The summary of the paper.</param>
public record SubmitPaperSummaryRequest(string Summary);
