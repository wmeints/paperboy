using Microsoft.CodeAnalysis.Emit;
using PaperBoy.ContentStore.Domain.Commands;
using PaperBoy.ContentStore.Domain.Events;
using PaperBoy.ContentStore.Domain.Shared;

namespace PaperBoy.ContentStore.Domain;

/// <summary>
/// Models a scientific paper that was submitted to the paper-of-the-week application.
/// </summary>
/// <remarks>
/// <para>
/// A paper can be submitted by myself or someone from the audience (later on). The paper is imported from
/// the provided URL and split into separate pages of textual content for further processing. The URL must be
/// from a trusted domain, otherwise I'm not importing it into the content store.
/// </para>
/// <para>
/// The paper is then summarized and scored to determine its quality and relevance to the audience.
/// We use GPT-4 with a specialized prompt to determine the score. GPT-4 also gives an explanation for the score.
/// </para>
/// <para>
/// Scored papers are manually approved by me, I want to make sure that the score is valid, and the paper is indeed
/// interesting to the target audience. Also, I need to make sure we're not getting some weird crap.
/// </para>
/// <para>
/// Once a paper is approved, a new description is made for the paper using GPT-4. The description is used by me to
/// write a short section of content for the Global AI community newsletter, our internal slack environment, and
/// my linkedin profile.
/// </para>
/// </remarks>
public class Paper : AggregateRoot
{
    private List<Page> _pages = new();

    /// <summary>
    /// Gets the identifier of the paper.
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Gets the title of the paper.
    /// </summary>
    public string Title { get; private set; } = default!;

    /// <summary>
    /// Gets the URL where the paper originates from.
    /// </summary>
    public string Url { get; private set; } = default!;

    /// <summary>
    /// Gets the information about the person who submitted the paper.
    /// </summary>
    public SubmitterInformation Submitter { get; private set; } = default!;

    /// <summary>
    /// Gets the status of the paper
    /// </summary>
    public PaperStatus Status { get; private set; }

    /// <summary>
    /// Gets the description of the paper.
    /// </summary>
    public string? Description { get; private set; }

    /// <summary>
    /// Gets the summary of the paper.
    /// </summary>
    public string? Summary { get; private set; }

    /// <summary>
    /// Gets the score for the paper.
    /// </summary>
    public PaperScore Score { get; private set; }

    /// <summary>
    /// Gets the pages of content for the paper.
    /// </summary>
    public IReadOnlyCollection<Page> Pages => _pages.AsReadOnly();

    /// <summary>
    /// Imports a paper by downloading it from the specified website.
    /// </summary>
    /// <param name="cmd">Command data used to import the paper.</param>
    /// <param name="contentExtractor">Content extractor to use for obtaining the page content.</param>
    /// <returns>Returns the imported paper data.</returns>
    public static async Task<Paper> ImportAsync(ImportPaperCommand cmd, IContentExtractor contentExtractor)
    {
        var pages = await contentExtractor.ExtractPagesAsync(cmd.Url);

        var paper = new Paper();
        paper.EmitDomainEvent(new PaperImportedEvent(cmd.PaperId, cmd.Url, cmd.Title, cmd.Submitter, pages));

        return paper;
    }

    /// <summary>
    /// Submits the generated summary for the paper.
    /// </summary>
    /// <param name="cmd">Command data used to update the paper with a generated summary.</param>
    public void SubmitSummary(SubmitSummaryCommand cmd)
    {
        EmitDomainEvent(new SummaryGeneratedEvent(cmd.PaperId, cmd.Summary));
    }

    /// <summary>
    /// Submits the paper score that was established by the content processor.
    /// </summary>
    /// <param name="cmd">Command data used to update the paper with the score.</param>
    public void SubmitScore(SubmitScoreCommand cmd)
    {
        EmitDomainEvent(new ScoreGeneratedEvent(cmd.PaperId, cmd.Score, cmd.Explanation));
    }
    
    /// <summary>
    /// Submits a generated summary for a page in the paper.
    /// </summary>
    /// <param name="command">Command data used to update the summary data</param>
    public void SubmitPageSummary(SubmitPageSummaryCommand command)
    {
        EmitDomainEvent(new PageSummaryGeneratedEvent(command.PaperId,command.PageNumber,command.Summary));   
    }

    protected override bool TryApplyDomainEvent(object domainEvent)
    {
        switch (domainEvent)
        {
            case PaperImportedEvent paperImportedEvent:
                Apply(paperImportedEvent);
                break;
            case SummaryGeneratedEvent summaryGeneratedEvent:
                Apply(summaryGeneratedEvent);
                break;
            case ScoreGeneratedEvent scoreUpdatedEvent:
                Apply(scoreUpdatedEvent);
                break;
            case PageSummaryGeneratedEvent pageSummaryGeneratedEvent:
                Apply(pageSummaryGeneratedEvent);
                break;
        }

        return true;
    }

    private void Apply(PageSummaryGeneratedEvent pageSummaryGeneratedEvent)
    {
        var page = _pages.First(x => x.PageNumber == pageSummaryGeneratedEvent.PageNumber);
        var updatedPage = page with { Summary = pageSummaryGeneratedEvent.Summary };

        _pages.Remove(page);
        _pages.Add(updatedPage);
        
        Version++;
    }

    private void Apply(ScoreGeneratedEvent scoreGeneratedEvent)
    {
        Status = PaperStatus.Scored;
        Score = new PaperScore(scoreGeneratedEvent.Score, scoreGeneratedEvent.Explanation);
        
        Version++;
    }

    private void Apply(SummaryGeneratedEvent summaryGeneratedEvent)
    {
        Status = PaperStatus.Summarized;
        Summary = summaryGeneratedEvent.Summary;

        Version++;
    }

    private void Apply(PaperImportedEvent paperImportedEvent)
    {
        Id = paperImportedEvent.PaperId;
        Url = paperImportedEvent.Url;
        Title = paperImportedEvent.Title;
        Submitter = paperImportedEvent.Submitter;
        _pages = paperImportedEvent.Pages;
        
        Version++;
    }

    
}