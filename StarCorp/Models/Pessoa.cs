using System.Text.RegularExpressions;
using ErrorOr;

namespace StarCorp.Models;

public class Pessoa
{
    public const int MinIdade = 0;
    public const int MaxIdade = 130;
    public int PessoaId {get;}
    public string Nome {get;} 
    public DateTime DataNascimento {get;} 
    public int Idade {get;}
    public string Email {get;}	
    public string Telefone {get;}
    public string Celular {get;}
    public DateTime Cadastro {get;} 
    public DateTime Alteracao {get;}
    public Pessoa(string nome, DateTime dataNascimento, int idade, string email, string telefone, string celular, DateTime cadastro, DateTime alteracao)
    {
        Nome = nome;
        DataNascimento = dataNascimento;
        Idade = idade;
        Email = email;
        Telefone = telefone;
        Celular = celular;
        Cadastro = cadastro;
        Alteracao = alteracao;
    }

    public Pessoa()
    {
    }

    public static ErrorOr<Pessoa> Create(string nome, DateTime dataNascimento, int idade, string email, string telefone, string celular, DateTime cadastro, DateTime alteracao)
    {
        List<Error> errors = new ();

        bool CheckEmail = EmailValidate(email);
        if (CheckEmail == false)
        {
            errors.Add(ErrorsPessoa.Pessoa.EmailInvalido);
        }

        if (idade < MinIdade || idade > MaxIdade)
        {
            errors.Add(ErrorsPessoa.Pessoa.IdadeInvalida);
        }

        if (errors.Count >= 1)
        {
            return errors;
        }

        return new Pessoa
        (
            nome,
            dataNascimento,
            idade,
            email,
            telefone,
            celular,
            cadastro,
            alteracao
        );
    }

    private static bool EmailValidate(string email)
    {   
        string regex = @"^[^@\s]+@[^@\s]+.(com|net|org|gov)$";
        return Regex.IsMatch(email, regex, RegexOptions.IgnoreCase);
    }
}
