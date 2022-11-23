using StarCorp.Models;

using StarCorp.Contracts;

namespace StarCorp.Repository;

public interface IEnderecoRepository
{
    public List<Endereco> GetEnderecos(int _pessoaId);

    public EnderecoValidate CreateEndereco(Endereco endereco);

    public Endereco GetEnderecoById(int id);

    public int DeleteEndereco(int id);

    public int EditEndereco(int id, UpdateEnderecoRequest request);
}