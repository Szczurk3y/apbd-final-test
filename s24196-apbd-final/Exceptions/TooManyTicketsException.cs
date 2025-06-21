namespace s24196_apbd_final.Exceptions;

public class TooManyTicketsException : Exception
{
    public TooManyTicketsException()
    {
    }

    public TooManyTicketsException(string? message) : base(message)
    {
    }

    public TooManyTicketsException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}