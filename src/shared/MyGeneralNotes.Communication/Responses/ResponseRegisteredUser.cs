using MyGeneralNotes.Communication.Responses;

namespace MyGeneralNotes.Communication.Response;
public class ResponseRegisteredUser
{
    public string Name { get; set; } = string.Empty;
    public ResponseTokens Tokens { get; set; } = default!;
}