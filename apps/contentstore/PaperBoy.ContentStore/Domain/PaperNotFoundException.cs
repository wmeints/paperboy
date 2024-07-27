namespace PaperBoy.ContentStore.Domain;

/// <summary>
/// Exception thrown when a paper is not found in the content store.
/// </summary>
public class PaperNotFoundException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PaperNotFoundException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public PaperNotFoundException(string message) : base(message)
    {
    }
}