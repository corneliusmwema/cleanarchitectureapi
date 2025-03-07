using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Video : BaseEntity
{
    [Key] public Guid Id { get; set; }


    public required string Title { get; set; }

    public required string Thumbnail { get; set; }

    public required string VideoUrl { get; set; }

    public required string Prompt { get; set; }

    public Guid UserId { get; set; }

    public User User { get; set; }
}