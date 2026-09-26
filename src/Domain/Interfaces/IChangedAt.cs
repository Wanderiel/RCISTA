namespace Domain.Interfaces;

public interface IChangedAt
{
    DateTime CreatedAt { get; set; }
    DateTime UpdatedAt { get; set; }
}
