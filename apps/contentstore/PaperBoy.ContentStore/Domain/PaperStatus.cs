namespace PaperBoy.ContentStore.Domain;

/// <summary>
/// Describes the possible states for a paper.
/// </summary>
public enum PaperStatus
{
    /// <summary>
    /// The paper is imported but no further processing was done.
    /// </summary>
    Imported,
    
    /// <summary>
    /// The paper is summarized for scoring.
    /// </summary>
    Summarized,
    
    /// <summary>
    /// The paper is scored, after this it's either accepted or declined based on the score.
    /// </summary>
    Scored,
    
    /// <summary>
    /// The paper is approved and there's a description for it that you can use to post about it.
    /// </summary>
    Approved,
    
    /// <summary>
    /// The paper was not approved, maybe a good idea to report back to the user who submitted it.
    /// </summary>
    Declined,
    
    /// <summary>
    /// The paper is ready for publication. This is the final state.
    /// </summary>
    ReadyForPublication,
}