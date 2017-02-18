using Dapper;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
using WebAPIApplication.Models;

namespace WebAPIApplication.Daos
{
    public interface IContatoDao
    {
        string ObterTodos();
        void Adicionar(string nome, string telefone);
    }

    public class ContatoDao : IContatoDao
    {

        public string ObterTodos() 
        {
            using (var conexao = CriarConexao())
            {
                conexao.Open();

                var contatos = conexao.Query<Contato>("select * from Contato").AsList();
                return JsonConvert.SerializeObject(contatos);
            }
        }

        public void Adicionar(string nome, string telefone)
        {
            var novoContato = new Contato(nome, telefone);
            
            using (var conexao = CriarConexao())
            {
                conexao.Open();
                
                using (var transacao = conexao.BeginTransaction())
                {
                    const string query = @"
                        INSERT INTO Contato (Nome, Telefone)
                        VALUES (@Nome, @Telefone)";

                    conexao.Execute(query, novoContato, transacao);

                    transacao.Commit();
                }
            }
        }

        private IDbConnection CriarConexao()
        {
            const string stringDeConexao = @"
                Server=localhost;
                Database=test;
                User Id=sa;
                Password=6F9755f1c;";

            return new SqlConnection(stringDeConexao);
        }
    }
}