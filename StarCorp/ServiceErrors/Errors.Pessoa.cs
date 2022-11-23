using ErrorOr;

public static class ErrorsPessoa
{
    public static class Pessoa
    {
        public static Error ChaveInvalida => Error.Validation(
            code: "InvalidKey",
            description: "Chave inválida."
            );
            
        public static Error NotFound => Error.NotFound(
            code: "Pessoa.NotFound",
            description: "Não foi possivel recuperar dados de Pessoa"
            );

        public static Error Validation => Error.Validation(description: "Dados Inválidos!");

        public static Error IdadeInvalida => Error.Validation(
            code: "Pessoa.Invalid",
            description: "Idade precisa ser entre 0 e 130!"
        );

        public static Error EmailInvalido => Error.Validation(
            code: "Email.Invalid",
            description: "Email Inválido!"
        );

        public static Error ChildElement => Error.Validation(
            code: "Delete.Invalid",
            description: "Não foi possivel deletar Pessoa porque está vinculado a FK endereco"
        );
    }
}