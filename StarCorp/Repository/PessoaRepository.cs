using StarCorp.Contracts;
using StarCorp.Models;
using StarCorp.Repository;
using StarCorp.Factory;
using System.Data;
using Dapper;

namespace StarCorp.Repository
{
    public class PessoaRepository : IPessoaRepository
    {
        private readonly IDbConnection _connection;
        public Pessoa _Pessoa { get; set; }

        public PessoaRepository()
        {
            _connection = new SqlFactory().SqlConnection();
        }

        public List<Pessoa> GetPessoas()
        {
            List<Pessoa> pessoaData = new List<Pessoa>();
            var query = @"SELECT * FROM [StarCorp].[dbo].[Pessoas]";
            
            using(_connection)
            {
                pessoaData = _connection.Query<Pessoa>(query).ToList();
            }

            return pessoaData;
        }

        public int CreatePessoa(Pessoa Pessoa)
        {
            int result = 0;
            string insertQuery = @"INSERT INTO [StarCorp].[dbo].[pessoas] 
            OUTPUT INSERTED.[pessoaId]
            VALUES 
            (@nome, @dataNascimento, @idade, @email, @telefone, @celular, @cadastro, @alteracao)";

            var paramaters = new 
            {
                nome = Pessoa.Nome, 
                dataNascimento = Pessoa.DataNascimento, 
                idade = Pessoa.Idade, 
                email = Pessoa.Email, 
                telefone = Pessoa.Telefone,
                celular = Pessoa.Celular,
                cadastro = Pessoa.Cadastro, 
                alteracao = Pessoa.Alteracao
            };

            using(_connection)
            {
                result = _connection.QuerySingle<int>(insertQuery, paramaters);
            }

            return result;
        }

        public Pessoa GetPessoaById(int id)
        {
            Pessoa pessoaData = new Pessoa();

            string query = @"SELECT * FROM [StarCorp].[dbo].[pessoas] 
            WHERE 
            pessoaId = @id";
            var paramaters = new 
            {
                id = id
            };

            using (_connection)
            {
                try
                {
                    pessoaData = _connection.QuerySingle<Pessoa>(query, paramaters);
                }
                catch 
                {
                    pessoaData = null;
                }
            }

            return pessoaData;
        }

        public int DeletePessoa(int id)
        {
            int result = 0;
            string query = @"DELETE FROM [StarCorp].[dbo].[pessoas] 
            WHERE
            pessoaId = @id ";
            var paramaters = new 
            {
                id = id
            };

            using (_connection)
            {
                result = _connection.Execute(query, paramaters);
            }

            return result;
        }

        public int EditPessoa(int id, UpdatePessoaRequest request)
        {
            int result = 0;
            string query = @"UPDATE [StarCorp].[dbo].[pessoas] 
            SET 
            nome = @nome, dataNascimento = @dataNascimento, 
            idade = @idade, email = @email, 
            telefone = @telefone,
            alteracao = @alteracao
            WHERE 
            pessoaId = @id";

            var paramaters = new
            {
                nome = request.nome,
                dataNascimento = request.dataNascimento,
                idade = request.idade,
                email = request.email,
                telefone = request.telefone,
                alteracao = DateTime.Now,
                id = id
            };

            using (_connection)
            {
                result = _connection.Execute(query, paramaters);
            }

            return result;
        }
    }
}
