using System.Net;
using System.Net.Cache;
using Microsoft.AspNetCore.Mvc;
using StarCorp.Contracts;
using StarCorp.Models;
using StarCorp.Services.Enderecos;
using ErrorOr;
using System.ComponentModel.DataAnnotations;

namespace StarCorp.Controllers;

public class EnderecoController : ApiController
{

    public readonly IEnderecoService _enderecoService;
    public const string Fake_Key = "C29C0C2A-6125-4BA9-B0DB-B7A3A8B725BB";

    public EnderecoController(IEnderecoService EnderecoService)
    {
        _enderecoService = EnderecoService;
    }

    [HttpPost]
    public IActionResult CreateEndereco(CreateEnderecoRequest request)
    {   
        string API_KEY = Request.Headers["Chave"];
        List<Error> errors = new List<Error>();

        if (IsInvalidAPIKey(API_KEY))
        {
            errors.Add(ErrorsEndereco.Endereco.ChaveInvalida);
            return StatusCode(errors);
        }

        ErrorOr<Endereco> requestEnderecoResult = Endereco.Create
        (
            request.pessoaId,
            request.logradouro,
            request.numero,
            request.bairro,
            request.cidade,
            request.uf,
            DateTime.UtcNow,
            DateTime.UtcNow
        );

        if (requestEnderecoResult.IsError)
        {
            return StatusCode(requestEnderecoResult.Errors);
        }

        ErrorOr<int> createEnderecoResult = _enderecoService.CreateEndereco(requestEnderecoResult.Value);
        
        return createEnderecoResult.Match(
            data => Ok(MapEnderecoResponse(data)),
            errors => StatusCode(errors));
    }

    [HttpGet("GetAll/{pessoaId}")]
    public IActionResult GetAll(int pessoaId)
    {
        string API_KEY = Request.Headers["Chave"];
        List<Error> errors = new List<Error>(); 
       
        if (IsInvalidAPIKey(API_KEY))
        {
            errors.Add(ErrorsEndereco.Endereco.ChaveInvalida);
            return StatusCode(errors);
        }

        ErrorOr<List<Endereco>> getEnderecoResult = _enderecoService.GetAllEndereco(pessoaId);
        
        return getEnderecoResult.Match(
            data => Ok(MapEnderecoResponse(data)),
            errors => StatusCode(errors));
    }

    [HttpGet("{id}")]
    public IActionResult GetEndereco(int id)
    {
        string API_KEY = Request.Headers["Chave"];
        List<Error> errors = new List<Error>();   
        
        if (IsInvalidAPIKey(API_KEY))
        {
            errors.Add(ErrorsEndereco.Endereco.ChaveInvalida);
            return StatusCode(errors);
        }

        ErrorOr<Endereco> getEnderecoResult = _enderecoService.GetEnderecoById(id);

        return getEnderecoResult.Match(
            data => Ok(MapEnderecoResponse(data)),
            errors => StatusCode(errors));
    }

    [HttpPut("{id}")]
    public IActionResult EditEndereco(int id, UpdateEnderecoRequest request)
    {
        string API_KEY = Request.Headers["Chave"];
        List<Error> errors = new List<Error>();

        if (IsInvalidAPIKey(API_KEY))
        {
            errors.Add(ErrorsEndereco.Endereco.ChaveInvalida);
            return StatusCode(errors);
        }

        ErrorOr<Endereco> requestEnderecoResult = Endereco.Edit
        (
            request.logradouro,
            request.numero,
            request.bairro,
            request.cidade,
            request.uf
        );

        if (requestEnderecoResult.IsError)
        {
            return StatusCode(requestEnderecoResult.Errors);
        }

        ErrorOr<int> editEnderecoResult = _enderecoService.EditEndereco(id, request);

        return editEnderecoResult.Match(
            data => Ok(MapEnderecoResponse(data)),
            errors => StatusCode(errors));
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteEndereco(int id)
    {
        string API_KEY = Request.Headers["Chave"];
        List<Error> errors = new List<Error>();

        if (IsInvalidAPIKey(API_KEY))
        {
            errors.Add(ErrorsEndereco.Endereco.ChaveInvalida);
            return StatusCode(errors);
        }

        ErrorOr<int> getEnderecoResult = _enderecoService.DeleteEndereco(id);

        return getEnderecoResult.Match(
            data => Accepted(MapEnderecoResponse(data)),
            errors => StatusCode(errors));
    }

    private static DataEnderecoResponse MapEnderecoResponse(Object data)
    {
        List<string> errors = new List<string>();
        return new DataEnderecoResponse
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
