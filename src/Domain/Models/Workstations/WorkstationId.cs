namespace Domain.Models.Workstations;

public struct WorkstationId
{
    private readonly int _id;

    public WorkstationId(int id) =>
        _id = id;

    public int Value => _id;
}
