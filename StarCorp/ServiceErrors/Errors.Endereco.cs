using ErrorOr;

public static class ErrorsEndereco
{
    public static class Endereco
    {   
        public static Error ChaveInvalida => Error.Validation(
            code: "InvalidKey",
            description: "Chave inválida."
            );

        public static Error NotFound => Error.NotFound(
            code: "Endereco.NotFound",
            description: "Não foi possivel recuperar dados de Endereco"
            );

        public static Error Validation => Error.Validation(description: "Dados Inválidos!");

        public static Error UfInvalid => Error.Validation(description: "UF não pode ter mais que 2 caracteres");

        public static Error FailDelete => Error.Validation(
            code: "Delete.Invalid",
            description: "Não foi possivel deletar o Endereco, não existe"
        );
    }
}