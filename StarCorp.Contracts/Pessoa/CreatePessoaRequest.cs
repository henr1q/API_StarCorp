namespace StarCorp.Contracts;

public record CreatePessoaRequest
(
    string nome,
    DateTime dataNascimento,
    int idade,
    string email,
    string telefone,
    string celular
);