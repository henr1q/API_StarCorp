using Microsoft.AspNetCore.Mvc;
using ErrorOr;
using StarCorp.Contracts;

namespace StarCorp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ApiController : ControllerBase
{
    public IActionResult StatusCode(List<Error> errors)
    {
        var firstError = errors[0];
        List<string> _errors = new List<string>();

        var statusCode = firstError.Type switch
        {
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Validation => StatusCodes.Status400BadRequest,  
        };


        foreach(var error in errors)
        {   
            _errors.Add(error.Description);
        }

        string[] data = null;
        var _response = new DataPessoaResponse
        (
            Guid.NewGuid(),
            true, 
            data,
            _errors,
            statusCode.ToString(),
            DateTime.Now
        );

        return StatusCode(statusCode, _response);
    } 
}