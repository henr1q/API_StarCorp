namespace StarCorp.Contracts;

public record DataPessoaResponse(
    Guid transactionId,
    bool failure,
    Object data,
    List<string> errors,
    string code,
    DateTime date
);
