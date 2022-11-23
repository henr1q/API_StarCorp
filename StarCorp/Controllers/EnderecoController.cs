using System.Net;
using System.Net.Cache;
using Microsoft.AspNetCore.Mvc;
using StarCorp.Contracts;
using StarCorp.Models;
using StarCorp.Services.Enderecos;

namespace StarCorp.Controllers;

[ApiController]
[Route("api/[controller]")]

public class EnderecoController : ControllerBase{

    public readonly IEnderecoService _enderecoService;
    
    public EnderecoController(IEnderecoService enderecoService)
    {
        _enderecoService = enderecoService;
    }

    [HttpPost]
    public IActionResult CreateEndereco(CreateEnderecoRequest request)
    {   
        // List<string> errors = new List<string>();
        // bool failure = false;
        // int? data;
        // string code = "200";

        var endereco = new Endereco
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

        // data = _enderecoService.CreateEndereco(endereco);
        
        // if (data == 3)
        // {
        //     errors.Add("PessoaId não existe na tabela de Pessoas.");
        //     failure = true;
        //     code = "400";
        //     data = null;
        // }

        // var response = new DataEnderecoResponse
        // (
        //     Guid.NewGuid(),
        //     failure, 
        //     data,
        //     errors,
        //     code,
        //     DateTime.Now
        // );

        DataEnderecoResponse data = _enderecoService.CreateEndereco(endereco);
        
        return StatusCode(Int32.Parse(data.code), data);
    }

    [HttpGet("GetAll/{pessoaId}")]
    public IActionResult GetAll(int pessoaId)
    {
        var data = _enderecoService.GetAllEndereco(pessoaId);
        List<string> errors = new List<string>();
        string code = "200";

        var response = new DataEnderecoResponse
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
    public IActionResult GetEndereco(int id)
    {
        var data = _enderecoService.GetEnderecoById(id);
        List<string> errors = new List<string>();
        string code = "200";

        var response = new DataEnderecoResponse
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
    public IActionResult EditEndereco(int id, UpdateEnderecoRequest request)
    {
        var data = _enderecoService.EditEndereco(id, request);
        List<string> errors = new List<string>();
        string code = "200";

        var response = new DataEnderecoResponse
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
    public IActionResult DeleteEndereco(int id)
    {
        var data = _enderecoService.DeleteEndereco(id);

        List<string> errors = new List<string>();
        string code = "200";

        var response = new DataEnderecoResponse
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
