namespace PaperBoy.ContentStore.Domain;

/// <summary>
/// Represents the information of a submitter, including their name and email address.
/// </summary>
/// <param name="Name">The name of the submitter.</param>
/// <param name="EmailAddress">The email address of the submitter.</param>
public record SubmitterInformation(string Name, string EmailAddress);