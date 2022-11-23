using System.Data.Common;
using System.Text;
using System.Collections.Generic;
using StarCorp.Contracts;
using StarCorp.Models;
using StarCorp.Repository;
using StarCorp.Services.Enderecos;
using ErrorOr;
using System.Data.SqlClient;

public class EnderecoService : IEnderecoService
{   
    public readonly IEnderecoRepository _enderecoRepository;

    public EnderecoService(IEnderecoRepository enderecoRepository)
    {
        _enderecoRepository = enderecoRepository;
    }

    public ErrorOr<int> CreateEndereco(Endereco endereco)
    {
        try
        {
            var data = _enderecoRepository.CreateEndereco(endereco);
            return data;
        }
        catch(Exception Ex)
        {
            return ErrorsEndereco.Endereco.NotFound;
        }
    }

    public ErrorOr<List<Endereco>> GetAllEndereco(int pessoaId)
    {
        var data = _enderecoRepository.GetAllEndereco(pessoaId);
        return data;
    }

    public ErrorOr<Endereco> GetEnderecoById(int id)
    {
        try
        {
            var data = _enderecoRepository.GetEnderecoById(id);
            return data;
        }
        catch
        {
            return ErrorsEndereco.Endereco.NotFound;
        }   
    }

    public ErrorOr<int> DeleteEndereco(int id)
    {
        try
        {
            var data = _enderecoRepository.DeleteEndereco(id);
            return data;
        }
        catch(SqlException)
        {
            return ErrorsEndereco.Endereco.FailDelete;
        }
    }

    public ErrorOr<int> EditEndereco(int id, UpdateEnderecoRequest request)
    {
        var data = _enderecoRepository.EditEndereco(id, request);
        return data;
    }

    
}
