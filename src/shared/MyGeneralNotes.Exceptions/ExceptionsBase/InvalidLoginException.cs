namespace MyGeneralNotes.Exceptions.ExceptionsBase;
public class InvalidLoginException : MyGeneralNotesExceptions
{
    public InvalidLoginException() : base(MessagesException.EMAIL_OR_PASSWORD_INVALID)
    {
    }
}
