using ErrorOr;
using StarCorp.Contracts;
using StarCorp.Models;

namespace StarCorp.Services.Pessoas;

public interface IPessoaService
{
    public ErrorOr<int> CreatePessoa(Pessoa Pessoa);

    public ErrorOr<List<Pessoa>> GetAllPessoa();

    public ErrorOr<Pessoa> GetPessoaById(int id);

    public ErrorOr<int> DeletePessoa(int id);

    public ErrorOr<int> EditPessoa(int id, UpdatePessoaRequest request);

}
