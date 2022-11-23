using System.Data.SqlTypes;
using StarCorp.Models;
using StarCorp.Services.Pessoas;
using StarCorp.Contracts;
using StarCorp.Repository;
using System.Text.RegularExpressions;
using ErrorOr;
using System.Data.SqlClient;

public class PessoaService : IPessoaService
{
    public readonly IPessoaRepository _pessoaRepository;

    public PessoaService(IPessoaRepository pessoaRepository)
    {
        _pessoaRepository = pessoaRepository;
    }

    public ErrorOr<int> CreatePessoa(Pessoa pessoa)
    {
        try
        {
            var data = _pessoaRepository.CreatePessoa(pessoa);
            return data;
        }
        catch(Exception Ex)
        {
            return ErrorsPessoa.Pessoa.NotFound;
        }
    }

    public ErrorOr<int> DeletePessoa(int id)
    {
        try
        {
            var data = _pessoaRepository.DeletePessoa(id);
            return data;
        }
        catch(SqlException)
        {
            return ErrorsPessoa.Pessoa.ChildElement;
        }
        
    }

    public ErrorOr<int> EditPessoa(int id, UpdatePessoaRequest request)
    {
        var data = _pessoaRepository.EditPessoa(id, request);

        return data;
    }

    public ErrorOr<List<Pessoa>> GetAllPessoa()
    {
        var data = _pessoaRepository.GetPessoas();

        return data;
    }

    public ErrorOr<Pessoa> GetPessoaById(int id)
    {
        try
        {
            var data = _pessoaRepository.GetPessoaById(id);
            return data;
        }
        catch
        {
            return ErrorsPessoa.Pessoa.NotFound;
        }   
    }
}
