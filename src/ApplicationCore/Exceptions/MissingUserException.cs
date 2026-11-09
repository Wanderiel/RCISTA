namespace ApplicationCore.Exceptions;

public class MissingUserException : Exception
{
    public MissingUserException()
        : base("Пользователь с таким логином не найден.")
    { }

    public MissingUserException(int id)
        : base($"Пользователь c ID {id} не найдена.")
    { }
}
