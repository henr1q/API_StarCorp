using System.Net.Http.Headers;
using System.Net;
using System.Net.Cache;
using Microsoft.AspNetCore.Mvc;
using StarCorp.Contracts;
using StarCorp.Models;
using StarCorp.Services.Pessoas;

namespace StarCorp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PessoaController : ControllerBase
{
    public readonly IPessoaService _PessoaService;
    
    public readonly string FakeKey = "1234";
    public PessoaController(IPessoaService PessoaService)
    {
        _PessoaService = PessoaService;
    }

    [HttpPost]
    public IActionResult CreatePessoa(CreatePessoaRequest request)
    {   
        List<string> errors = new List<string>();
        bool failure = false;
        int? data;
        string code = "200";

        if(request.idade < 0 || request.idade > 130)
        {
            errors.Add("O valor para Idade deve ser entre 0 e 130");
            failure = true;
            code = "400";
        }

        bool isEmailValid = _PessoaService.EmailValidate(request.email);

        if(isEmailValid == false)
        {
            errors.Add("The Email field is not a valid e-mail address.");
            failure = true;
            code = "400";
        }
        
        var Pessoa = new Pessoa
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

        if(errors.Count >= 1)
        {
            data = null;
        }
        else
        {
            data = _PessoaService.CreatePessoa(Pessoa);
        }

        var response = new DataPessoaResponse
        (
            Guid.NewGuid(),
            failure, 
            data,
            errors,
            code,
            DateTime.Now
        );

        return StatusCode(Int32.Parse(response.code), response);
    }

    [HttpGet("GetAll")]
    public IActionResult GetAll()
    {
        var API_KEY = Request.Headers["API_KEY"];

        if (API_KEY != FakeKey)
        {
            return StatusCode(404, "WROOOONG");
        }

        var data = _PessoaService.GetAllPessoa();
        List<string> errors = new List<string>();
        string code = "200";

        var response = new DataPessoaResponse
        (
            Guid.NewGuid(),
            false, 
            data,
            errors,
            code,
            DateTime.Now
        );

        return StatusCode(Int32.Parse(response.code), response);
    }

    [HttpGet("{id}")]
    public IActionResult GetPessoa(int id)
    {
        var data = _PessoaService.GetPessoaById(id);
        List<string> errors = new List<string>();
        string code = "200";

        var response = new DataPessoaResponse
        (
            Guid.NewGuid(),
            false, 
            data,
            errors,
            code,
            DateTime.Now
        );

        return StatusCode(Int32.Parse(response.code), response);
    }

    [HttpPut("{id}")]
    public IActionResult EditPessoa(int id, UpdatePessoaRequest request)
    {
        var data = _PessoaService.EditPessoa(id, request);
        List<string> errors = new List<string>();
        string code = "200";

        var response = new DataPessoaResponse
        (
            Guid.NewGuid(),
            false, 
            data,
            errors,
            code,
            DateTime.Now
        );

        return StatusCode(Int32.Parse(response.code), response);
    }

    [HttpDelete("{id}")]
    public IActionResult DeletePessoa(int id)
    {
        var data = _PessoaService.DeletePessoa(id);

        List<string> errors = new List<string>();
        string code = "200";

        var response = new DataPessoaResponse
        (
            Guid.NewGuid(),
            false, 
            data,
            errors,
            code,
            DateTime.Now
        );

        return StatusCode(Int32.Parse(response.code), response);
    }
}
