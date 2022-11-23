namespace StarCorp.Models;

public class Endereco 
{
    public int EnderecoId {get;}
    public int PessoaId {get;}
    public string Logradouro {get;} 
    public string Numero {get;} 
    public string Bairro {get;}
    public string Cidade {get;}	
    public string UF {get;}
    public DateTime Cadastro {get;} 
    public DateTime Alteracao {get;}

    public Endereco(int pessoaId, string logradouro, string numero, string bairro, string cidade, string uf, DateTime cadastro, DateTime alteracao)
    {
        PessoaId = pessoaId;
        Logradouro = logradouro;
        Numero = numero;
        Bairro = bairro;
        Cidade = cidade;
        UF = uf;
        Cadastro = cadastro;
        Alteracao = alteracao;
    }

    public Endereco()
    {
    }
}
