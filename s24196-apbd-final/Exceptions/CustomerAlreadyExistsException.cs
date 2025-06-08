namespace s24196_apbd_final.Exceptions;

public class CustomerAlreadyExistsException : Exception
{
    public CustomerAlreadyExistsException()
    {
    }

    public CustomerAlreadyExistsException(string? message) : base(message)
    {
    }

    public CustomerAlreadyExistsException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}