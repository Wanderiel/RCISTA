namespace ApplicationCore.Exceptions;

public class MissingUserException : Exception
{
    public MissingUserException(int id)
        : base($"Пользователь c ID {id} не найдена.")
    { }
}
