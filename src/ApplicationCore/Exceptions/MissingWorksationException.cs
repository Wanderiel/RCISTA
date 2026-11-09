namespace ApplicationCore.Exceptions;

public class MissingWorksationException : Exception
{
    public MissingWorksationException(int id)
        : base($"Рабочая станция c ID {id} не найдена.")
    { }
}
