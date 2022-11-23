using System.Runtime.InteropServices.ComTypes;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using StarCorp.Factory;
using StarCorp.Models;
using StarCorp.Contracts;

namespace StarCorp.Repository
{
    public class EnderecoRepository : IEnderecoRepository
    {   
        private readonly IDbConnection _connection;
        public Endereco _Endereco { get; set; }

        public EnderecoRepository()
        {
            _connection = new SqlFactory().SqlConnection();
        }

        public List<Endereco> GetEnderecos(int _pessoaId)
        {   
            List<Endereco> enderecoData = new List<Endereco>();
            var query = @"SELECT * FROM [StarCorp].[dbo].[enderecos] 
            WHERE 
            pessoaId = @pessoaId";
            
            var paramaters = new 
            {
                pessoaId = _pessoaId
            };

            using(_connection)
            {
                enderecoData = _connection.Query<Endereco>(query, paramaters).ToList();
            }

            return enderecoData;
        }

        public EnderecoValidate CreateEndereco(Endereco endereco)
        {
            int result = 0;
            List<string> errors = new List<string>();

            string insertQuery = @"INSERT INTO [StarCorp].[dbo].[enderecos] 
            OUTPUT INSERTED.[enderecoId]
            VALUES 
            (@pessoaId, @logradouro, @numero, @bairro, @cidade, @uf, @cadastro, @alteracao)";

            var paramaters = new 
            {
                pessoaId = endereco.PessoaId, 
                logradouro = endereco.Logradouro, 
                numero = endereco.Numero, 
                bairro = endereco.Bairro, 
                cidade = endereco.Cidade, 
                uf = endereco.UF, 
                cadastro = endereco.Cadastro, 
                alteracao = endereco.Alteracao
            };

            using(_connection)
            {
                try
                {
                    result = _connection.QuerySingle<int>(insertQuery, paramaters);
                }
                catch(Exception ex)
                {
                    errors.Add(ex.Message);
                }
            }

            EnderecoValidate response = new
            (
                result,
                errors
            );

            return response;
        } 

        public Endereco  GetEnderecoById(int id)
        {
            Endereco enderecoData = new Endereco();

            string query = @"SELECT * FROM [StarCorp].[dbo].[enderecos] 
            WHERE 
            enderecoId = @id";
            var paramaters = new 
            {
                id = id
            };

            using (_connection)
            {
                try
                {
                    enderecoData = _connection.QuerySingle<Endereco>(query, paramaters);
                }
                catch 
                {
                    enderecoData = null;
                }
            }

            return enderecoData;
        }

        public int DeleteEndereco(int id)
        {
            int result = 0;
            string query = @"DELETE FROM [StarCorp].[dbo].[enderecos] 
            WHERE
            enderecoId = @id ";
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

        public int EditEndereco(int id, UpdateEnderecoRequest request)
        {
            int result = 0;
            string query = @"UPDATE [StarCorp].[dbo].[enderecos] 
            SET 
            logradouro = @logradouro, numero = @numero, 
            bairro = @bairro, cidade = @cidade, 
            uf = @uf, alteracao = @alteracao
            WHERE 
            enderecoId = @id";

            var paramaters = new
            {
                logradouro = request.logradouro,
                numero = request.numero,
                bairro = request.bairro,
                cidade = request.cidade,
                uf = request.uf,
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


//TODO ERRORS HANDLING, STATUS CODES, DATA FORMATION, PESSOAS MODEL AND CRUD :DDDDDDDDD!