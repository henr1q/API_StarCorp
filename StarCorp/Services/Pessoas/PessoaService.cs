using StarCorp.Models;
using StarCorp.Services.Pessoas;
using StarCorp.Contracts;
using StarCorp.Repository;
using System.Text.RegularExpressions;

public class PessoaService : IPessoaService
{
    public readonly IPessoaRepository _pessoaRepository;

    public PessoaService(IPessoaRepository pessoaRepository)
    {
        _pessoaRepository = pessoaRepository;
    }
    public int CreatePessoa(Pessoa pessoa)
    {
        var data = _pessoaRepository.CreatePessoa(pessoa);

        return data;
    }
    public bool EmailValidate(string email)
    { 
        string regex = @"^[^@\s]+@[^@\s]+\.(com|net|org|gov)$";
        return Regex.IsMatch(email, regex, RegexOptions.IgnoreCase);
    }

    public int DeletePessoa(int id)
    {
        var data = _pessoaRepository.DeletePessoa(id);

        return data;
    }

    public int EditPessoa(int id, UpdatePessoaRequest request)
    {
        var data = _pessoaRepository.EditPessoa(id, request);

        return data;
    }

    public List<Pessoa> GetAllPessoa()
    {
        var data = _pessoaRepository.GetPessoas();

        return data;
    }

    public Pessoa GetPessoaById(int id)
    {
        var data = _pessoaRepository.GetPessoaById(id);

        return data;
    }
}
