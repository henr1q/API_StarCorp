namespace StarCorp.Contracts;

public record CreateEnderecoRequest(
    int pessoaId,
    string logradouro,
    string numero,
    string bairro,
    string cidade,
    string uf
);