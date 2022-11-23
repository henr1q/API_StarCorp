using System.Net.Http.Headers;
using System.Net;
using System.Net.Cache;
using Microsoft.AspNetCore.Mvc;
using StarCorp.Contracts;
using StarCorp.Models;
using StarCorp.Services.Pessoas;
using ErrorOr;

namespace StarCorp.Controllers;


public class PessoaController : ApiController
{
    public readonly IPessoaService _pessoaService;
    
    // public readonly string FakeKey = "1234";
    public PessoaController(IPessoaService PessoaService)
    {
        _pessoaService = PessoaService;
    }

    [HttpPost]
    public IActionResult CreatePessoa(CreatePessoaRequest request)
    {    
        ErrorOr<Pessoa> requestPessoaResult = Pessoa.Create
        (
            request.nome,
            request.dataNascimento,
            request.idade,
            request.email,
            request.telefone,
            request.celular,
            DateTime.UtcNow,
            DateTime.UtcNow
        );

        if (requestPessoaResult.IsError)
        {
            return StatusCode(requestPessoaResult.Errors);
        }

        ErrorOr<int> createPessoaResult = _pessoaService.CreatePessoa(requestPessoaResult.Value);
        
        return createPessoaResult.Match(
            data => Ok(MapPessoaResponse(data)),
            errors => StatusCode(errors));
    }
    
    [HttpGet("GetAll")]
    public IActionResult GetAll()
    {
        ErrorOr<List<Pessoa>> GetAllResult = _pessoaService.GetAllPessoa();

        return GetAllResult.Match(
            data => Ok(MapPessoaResponse(data)),
            errors => StatusCode(errors));
    }

    [HttpGet("{id}")]
    public IActionResult GetPessoa(int id)
    {
        List<Error> errors = new List<Error>();   
        ErrorOr<Pessoa> getPessoaResult = _pessoaService.GetPessoaById(id);

        return getPessoaResult.Match(
            data => Ok(MapPessoaResponse(data)),
            errors => StatusCode(errors));
    }

    [HttpPut("{id}")]
    public IActionResult EditPessoa(int id, UpdatePessoaRequest request)
    {
        ErrorOr<Pessoa> requestPessoaResult = Pessoa.Create
        (
            request.nome,
            request.dataNascimento,
            request.idade,
            request.email,
            request.telefone,
            request.celular,
            DateTime.UtcNow,
            DateTime.UtcNow
        );

        if (requestPessoaResult.IsError)
        {
            return StatusCode(requestPessoaResult.Errors);
        }

        ErrorOr<int> editPessoaResult = _pessoaService.EditPessoa(id, request);

        return editPessoaResult.Match(
            data => Ok(MapPessoaResponse(data)),
            errors => StatusCode(errors));
        
    }

    [HttpDelete("{id}")]
    public IActionResult DeletePessoa(int id)
    {
        List<Error> errors = new List<Error>();   
        ErrorOr<int> getPessoaResult = _pessoaService.DeletePessoa(id);

        return getPessoaResult.Match(
            data => Accepted(MapPessoaResponse(data)),
            errors => StatusCode(errors));
    }

    private static DataPessoaResponse MapPessoaResponse(Object data)
    {
        List<string> errors = new List<string>();
        return new DataPessoaResponse
                (
                    Guid.NewGuid(),
                    false,
                    data,
                    errors,
                    "200",
                    DateTime.Now
                );
    }
}
