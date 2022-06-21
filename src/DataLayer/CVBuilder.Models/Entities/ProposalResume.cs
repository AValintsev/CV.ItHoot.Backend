using System;
using CVBuilder.Models.Entities.Interfaces;

namespace CVBuilder.Models.Entities;

public class ProposalResume : IDeletableEntity<int>
{
    public int Id { get; set; }
    public int ResumeId { get; set; }
    public Resume Resume { get; set; }
    public StatusProposalResume StatusResume { get; set; }
    public int? ShortUrlId { get; set; }
    public ShortUrl ShortUrl { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? DeletedAt { get; set; }
    public int ProposalId { get; set; }
    public Proposal Proposal { get; set; }
}