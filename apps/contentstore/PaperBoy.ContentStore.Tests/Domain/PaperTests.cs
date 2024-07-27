using Moq;
using PaperBoy.ContentStore.Domain;
using PaperBoy.ContentStore.Domain.Commands;
using PaperBoy.ContentStore.Domain.Events;

namespace PaperBoy.ContentStore.Tests.Domain;

public class PaperTests
{
    [Fact]
    public async Task PaperIsImportedCorrectly()
    {
        // Arrange
        var command = new ImportPaperCommand(
            Guid.NewGuid(), "http://trusted.com/paper", "Sample Paper",
            new SubmitterInformation("John Doe","test@domain.org"));
        
        var contentExtractorMock = new Mock<IContentExtractor>();
        
        contentExtractorMock.Setup(x => x.ExtractPagesAsync(It.IsAny<string>()))
            .ReturnsAsync(new List<Page> { new Page(1, "Content") });

        // Act
        var paper = await Paper.ImportAsync(command, contentExtractorMock.Object);

        // Assert
        Assert.Equal(command.PaperId, paper.Id);
        Assert.Equal(command.Url, paper.Url);
        Assert.Equal(command.Title, paper.Title);
        Assert.Equal(command.Submitter, paper.Submitter);
        Assert.Single(paper.Pages);
    }

    [Fact]
    public async Task SummaryIsSubmittedCorrectly()
    {
        // Arrange
        var paper = await TestObjectFactory.CreatePaperAsync();
        
        var command = new SubmitSummaryCommand(paper.Id, "Summary",
            new List<PageSummary> { new PageSummary(1, "Page Summary") });

        // Act
        paper.SubmitSummary(command);

        // Assert
        Assert.Equal(PaperStatus.Summarized, paper.Status);
        Assert.Equal(command.Summary, paper.Summary);
        Assert.Equal(command.PageSummaries.First().Summary, paper.Pages.First().Summary);
    }

    [Fact]
    public async Task ScoreIsSubmittedCorrectly()
    {
        // Arrange
        var paper = await TestObjectFactory.CreatePaperAsync();
        var command = new SubmitScoreCommand(paper.Id, 95, "High quality paper");

        // Act
        paper.SubmitScore(command);

        // Assert
        Assert.Equal(PaperStatus.Scored, paper.Status);
        Assert.Equal(command.Score, paper.Score.Value);
        Assert.Equal(command.Explanation, paper.Score.Explanation);
    }
}