namespace StarCorp.Models;

public class Pessoa
{
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
}
