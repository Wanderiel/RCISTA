namespace ApplicationCore.Exceptions;

public class MissingNonPaperMediaException : Exception
{
    public MissingNonPaperMediaException(IEnumerable<int> missingIds)
        : base($"Не найдены диски с ID: {string.Join(", ", missingIds)}.")
    { }

    public MissingNonPaperMediaException(int id)
        : base($"Диск с ID: {id} не найден.")
    { }
}
