namespace ApplicationCore.Exceptions;

public class NonPaperMediaAlreadyAssignedException : Exception
{
    public NonPaperMediaAlreadyAssignedException(string serialnumber)
        : base($"Диск (s/n: {serialnumber}) уже установлен в другую рабочую станцию.")
    { }
}
