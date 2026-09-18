namespace Domain.Models.NPMs;

public class NpmId
{
    private readonly int _id;

    public NpmId(int id) =>
        _id = id;

    public int Value => _id;
}
