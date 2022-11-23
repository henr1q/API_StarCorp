namespace StarCorp.Contracts;

public record UpdatePessoaRequest
(
    string nome,
    DateTime dataNascimento,
    int idade,
    string email,
    string telefone,
    string celular
);