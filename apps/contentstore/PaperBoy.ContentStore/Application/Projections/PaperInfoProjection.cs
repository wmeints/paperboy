using Marten;
using Marten.Events.Projections;
using PaperBoy.ContentStore.Domain.Events;

namespace PaperBoy.ContentStore.Application.Projections;

public class PaperInfoProjection : EventProjection
{
    public PaperInfoProjection()
    {
        Project<PaperImportedEvent>(OnPaperImported);
        Project<PageSummaryGeneratedEvent>(OnPageSummaryGenerated);
        Project<SummaryGeneratedEvent>(OnPaperSummarized);
        Project<ScoreGeneratedEvent>(OnPaperScored);
        Project<PaperDeclinedEvent>(OnPaperDeclined);
        Project<DescriptionGeneratedEvent>(OnDescriptionGenerated);
    }

    private void OnPaperScored(ScoreGeneratedEvent @event, IDocumentOperations operations)
    {
        var paperInfo = operations.Load<PaperInfo>(@event.PaperId)!;

        paperInfo.Status = Domain.PaperStatus.Scored;
        paperInfo.Score = new Domain.PaperScore(@event.Score, @event.Explanation);

        operations.Update(paperInfo);
    }

    private void OnPaperSummarized(SummaryGeneratedEvent @event, IDocumentOperations operations)
    {
        var paperInfo = operations.Load<PaperInfo>(@event.PaperId)!;

        paperInfo.Status = Domain.PaperStatus.Summarized;
        paperInfo.Summary = @event.Summary;

        operations.Update(paperInfo);
    }

    private void OnPageSummaryGenerated(PageSummaryGeneratedEvent @event, IDocumentOperations operations)
    {
        var paperInfo = operations.Load<PaperInfo>(@event.PaperId)!;
        paperInfo.SectionsSummarized++;

        operations.Update(paperInfo);
    }

    private void OnPaperImported(PaperImportedEvent @event, IDocumentOperations operations)
    {
        var paperInfo = new PaperInfo
        {
            Id = @event.PaperId,
            Title = @event.Title,
            TotalSections = @event.Pages.Count,
            Status = Domain.PaperStatus.Imported,
            Submitter = @event.Submitter,
            DateCreated = DateTime.UtcNow,
        };

        operations.Insert(paperInfo);
    }

    private void OnPaperDeclined(PaperDeclinedEvent @event, IDocumentOperations operations)
    {
        var paperInfo = operations.Load<PaperInfo>(@event.PaperId)!;
        paperInfo.Status = Domain.PaperStatus.Declined;

        operations.Update(paperInfo);
    }

    private void OnDescriptionGenerated(DescriptionGeneratedEvent @event, IDocumentOperations operations)
    {
        var paperInfo = operations.Load<PaperInfo>(@event.PaperId)!;
        paperInfo.Status = Domain.PaperStatus.Approved;
        paperInfo.Description = @event.Description;

        operations.Update(paperInfo);
    }
}
