namespace MyGeneralNotes.Exceptions.ExceptionsBase;
public class ErrorOnValidationException(IList<string> errorMensages) : MyGeneralNotesExceptions(string.Empty)
{
    public IList<string> ErrorsMessages { get; set; } = errorMensages;
}
