using Microsoft.AspNetCore.Mvc;
using MyGeneralNotes.API.Filters;

namespace MyGeneralNotes.API.Attributes;

public class AuthenticatedUserAttribute : TypeFilterAttribute
{
    public AuthenticatedUserAttribute() : base(typeof(AuthenticatedUserFilter))
    {
    }
}