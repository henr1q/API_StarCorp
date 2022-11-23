using StarCorp.Contracts;
using StarCorp.Models;
using ErrorOr;

namespace StarCorp.Services.Enderecos;

public interface IEnderecoService
{
    public ErrorOr<int> CreateEndereco(Endereco endereco);

    public ErrorOr<List<Endereco>> GetAllEndereco(int pessoaId);

    public ErrorOr<Endereco> GetEnderecoById(int id);

    public ErrorOr<int> DeleteEndereco(int id);

    public ErrorOr<int> EditEndereco(int id, UpdateEnderecoRequest request);

}
