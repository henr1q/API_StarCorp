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
    public const string Fake_Key = "C29C0C2A-6125-4BA9-B0DB-B7A3A8B725BB";

    public PessoaController(IPessoaService PessoaService)
    {
        _pessoaService = PessoaService;
    }

    [HttpPost]
    public IActionResult CreatePessoa(CreatePessoaRequest request)
    {    
        string API_KEY = Request.Headers["Chave"];
        List<Error> errors = new List<Error>();

        if (IsInvalidAPIKey(API_KEY))
        {
            errors.Add(ErrorsPessoa.Pessoa.ChaveInvalida);
            return StatusCode(errors);
        }

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
        string API_KEY = Request.Headers["Chave"];
        List<Error> errors = new List<Error>();

        if (IsInvalidAPIKey(API_KEY))
        {
            errors.Add(ErrorsPessoa.Pessoa.ChaveInvalida);
            return StatusCode(errors);
        }

        ErrorOr<List<Pessoa>> GetAllResult = _pessoaService.GetAllPessoa();

        return GetAllResult.Match(
            data => Ok(MapPessoaResponse(data)),
            errors => StatusCode(errors));
    }

    [HttpGet("{id}")]
    public IActionResult GetPessoa(int id)
    {
        string API_KEY = Request.Headers["Chave"];
        List<Error> errors = new List<Error>();

        if (IsInvalidAPIKey(API_KEY))
        {
            errors.Add(ErrorsPessoa.Pessoa.ChaveInvalida);
            return StatusCode(errors);
        }

        ErrorOr<Pessoa> getPessoaResult = _pessoaService.GetPessoaById(id);

        return getPessoaResult.Match(
            data => Ok(MapPessoaResponse(data)),
            errors => StatusCode(errors));
    }

    [HttpPut("{id}")]
    public IActionResult EditPessoa(int id, UpdatePessoaRequest request)
    {
        string API_KEY = Request.Headers["Chave"];
        List<Error> errors = new List<Error>();

        if (IsInvalidAPIKey(API_KEY))
        {
            errors.Add(ErrorsPessoa.Pessoa.ChaveInvalida);
            return StatusCode(errors);
        }

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
        string API_KEY = Request.Headers["Chave"];
        List<Error> errors = new List<Error>();

        if (IsInvalidAPIKey(API_KEY))
        {
            errors.Add(ErrorsPessoa.Pessoa.ChaveInvalida);
            return StatusCode(errors);
        }
  
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

    private static bool IsInvalidAPIKey(string key)
    {
        if(key != Fake_Key)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
