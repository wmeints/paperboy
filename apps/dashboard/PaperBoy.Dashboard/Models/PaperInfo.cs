using System;

namespace PaperBoy.Dashboard.Models
{
    public class PaperInfo
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = default!;
        public SubmitterInformation Submitter { get; set; } = default!;
        public string Url { get; set; } = default!;
        public PaperStatus Status { get; set; } = default!;
        public int SectionsSummarized { get; set; }
        public int TotalSections { get; set; }
    }

    public class SubmitterInformation
    {
        public string Name { get; set; } = default!;
        public string EmailAddress { get; set; } = default!;
    }

    public enum PaperStatus
    {
        Imported,
        Summarized,
        Scored,
        Approved,
        Declined,
        ReadyForPublication
    }
}
