using System.Net;
using Moq;
using PaperBoy.ContentStore.Infrastructure;

namespace PaperBoy.ContentStore.Tests.Infrastructure;

public class PdfContentExtractorTests
{
    [Fact]
    public async Task ExtractPagesAsync_ReturnsPages_WhenUrlIsValid()
    {
        // Arrange
        var httpClientFactoryMock = TestObjectFactory.CreateMockHttpClientFactory();

        var pdfContentExtractor = new PdfContentExtractor(httpClientFactoryMock.Object);
        var validUrl = "https://arxiv.org/pdf/1911.12116";

        // Act
        var result = await pdfContentExtractor.ExtractPagesAsync(validUrl);

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
    }

    [Fact]
    public async Task ExtractPagesAsync_ThrowsArgumentException_WhenUrlIsNotTrusted()
    {
        // Arrange
        var httpClientFactoryMock = TestObjectFactory.CreateMockHttpClientFactory();
        var pdfContentExtractor = new PdfContentExtractor(httpClientFactoryMock.Object);
        var untrustedUrl = "https://untrusted.com/paper.pdf";

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => pdfContentExtractor.ExtractPagesAsync(untrustedUrl));
    }

    [Fact]
    public async Task ExtractPagesAsync_ThrowsHttpRequestException_WhenResponseIsNotSuccessful()
    {
        // Arrange
        var httpClientFactoryMock = TestObjectFactory.CreateMockHttpClientFactory();

        var pdfContentExtractor = new PdfContentExtractor(httpClientFactoryMock.Object);
        var invalidUrl = "https://arxiv.org/pdf/invalid-paper.pdf";

        // Act & Assert
        await Assert.ThrowsAsync<HttpRequestException>(() => pdfContentExtractor.ExtractPagesAsync(invalidUrl));
    }
}