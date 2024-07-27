using Moq;
using PaperBoy.ContentStore.Domain;
using PaperBoy.ContentStore.Domain.Commands;

namespace PaperBoy.ContentStore.Tests;

public class TestObjectFactory
{
    public static async Task<Paper> CreatePaperAsync()
    {
        var contentProcessorMock = new Mock<IContentExtractor>();
        contentProcessorMock.Setup(x => x.ExtractPagesAsync(It.IsAny<string>())).ReturnsAsync([new Page(1, "Content")]);

        var paperId = Guid.NewGuid();
        var paperTitle = "Analysis of Explainers of Black Box Deep Neural\nNetworks for Computer Vision: A Survey";
        var paperUrl = "https://arxiv.org/pdf/1911.12116";

        var paper = await Paper.ImportAsync(new ImportPaperCommand(paperId, paperTitle, paperUrl,
            new SubmitterInformation("John Doe", "test@domain.org")), contentProcessorMock.Object);

        paper.ClearPendingDomainEvents();
        
        return paper;
    }

    public static Mock<IHttpClientFactory> CreateMockHttpClientFactory()
    {
        var httpClientFactoryMock = new Mock<IHttpClientFactory>();
        var httpClient = new HttpClient();

        httpClientFactoryMock.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClient);

        return httpClientFactoryMock;
    }
}