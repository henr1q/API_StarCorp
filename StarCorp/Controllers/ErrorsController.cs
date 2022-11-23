using Microsoft.AspNetCore.Mvc;

namespace StarCorp.Controllers;

public class ErrorsController : ControllerBase
{
    [Route("/errors")]

    public IActionResult Error()
    {
        return Problem();
        // return StatusCode(500, Response);
    }

}