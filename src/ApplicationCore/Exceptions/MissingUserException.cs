namespace ApplicationCore.Exceptions;

public class MissingUserException : Exception
{
    public MissingUserException(string login)
        : base($"Пользователь с логином {login} не найден.")
    { }

    public MissingUserException(int id)
        : base($"Пользователь c ID {id} не найдена.")
    { }
}
