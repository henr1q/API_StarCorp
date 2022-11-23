using System.Data.Common;
using System.Text;
using System.Collections.Generic;
using StarCorp.Contracts;
using StarCorp.Models;
using StarCorp.Repository;
using StarCorp.Services.Enderecos;

public class EnderecoService : IEnderecoService
{   
    public readonly IEnderecoRepository _enderecoRepository;

    public EnderecoService(IEnderecoRepository enderecoRepository)
    {
        _enderecoRepository = enderecoRepository;
    }

    public DataEnderecoResponse CreateEndereco(Endereco endereco)
    {
        EnderecoValidate data = new(null, new List<string>());
        // List<string> errors = new List<string>();
        bool failure = false;
        string code = "200";

        if(endereco.UF.Length > 2)
        {
            data.errors.Add("O valor para UF deve ser até 2 caracteres.");
            failure = true;
            code = "400";
        }

        data = _enderecoRepository.CreateEndereco(endereco);

        if (data.errors.Count >= 1)
        {
            failure = true;
            code = "400";
        }
        
        var response = new DataEnderecoResponse
        (
            Guid.NewGuid(),
            failure, 
            data.data,
            data.errors,
            code,
            DateTime.Now
        );

        return response;
    }

    public List<Endereco> GetAllEndereco(int pessoaId)
    {
        var data = _enderecoRepository.GetEnderecos(pessoaId);
        return data;
    }

    public Endereco GetEnderecoById(int id)
    {
        
        var data = _enderecoRepository.GetEnderecoById(id);       
        return data;
    }

    public int DeleteEndereco(int id)
    {
        var data = _enderecoRepository.DeleteEndereco(id);
        return data;
    }

    public int EditEndereco(int id, UpdateEnderecoRequest request)
    {
        var data = _enderecoRepository.EditEndereco(id, request);
        return data;
    }

    
}
