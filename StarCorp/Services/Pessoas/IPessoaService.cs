using StarCorp.Contracts;
using StarCorp.Models;

namespace StarCorp.Services.Pessoas;

public interface IPessoaService
{
    public int CreatePessoa(Pessoa Pessoa);

    public List<Pessoa> GetAllPessoa();

    public Pessoa GetPessoaById(int id);

    public int DeletePessoa(int id);

    public int EditPessoa(int id, UpdatePessoaRequest request);

    public bool EmailValidate(string email);

}
