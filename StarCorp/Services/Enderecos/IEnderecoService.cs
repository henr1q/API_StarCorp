using StarCorp.Contracts;
using StarCorp.Models;

namespace StarCorp.Services.Enderecos;

public interface IEnderecoService
{
    public DataEnderecoResponse CreateEndereco(Endereco endereco);

    public List<Endereco> GetAllEndereco(int pessoaId);

    public Endereco GetEnderecoById(int id);

    public int DeleteEndereco(int id);

    public int EditEndereco(int id, UpdateEnderecoRequest request);

}
