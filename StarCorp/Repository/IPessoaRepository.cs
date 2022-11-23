using StarCorp.Models;

using StarCorp.Contracts;

namespace StarCorp.Repository;

public interface IPessoaRepository
{
    public List<Pessoa> GetPessoas();

    public int CreatePessoa(Pessoa Pessoa);

    public Pessoa GetPessoaById(int id);

    public int DeletePessoa(int id);

    public int EditPessoa(int id, UpdatePessoaRequest request);
}