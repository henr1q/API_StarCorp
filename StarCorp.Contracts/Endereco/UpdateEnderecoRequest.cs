using System;

namespace StarCorp.Contracts;

public record UpdateEnderecoRequest(
    string logradouro,
    string numero,
    string bairro,
    string cidade,
    string uf
);