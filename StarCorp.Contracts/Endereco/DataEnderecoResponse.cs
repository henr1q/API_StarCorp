namespace StarCorp.Contracts;

public record DataEnderecoResponse(
    Guid transactionId,
    bool failure,
    Object data,
    List<string> errors,
    string code,
    DateTime date
);
