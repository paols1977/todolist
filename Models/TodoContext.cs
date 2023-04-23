using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;

namespace todolist.Models   
{
    public class TodoContext
    {
       
        private string ConnectionString { get; set; }



        public TodoContext()

        {

            ConnectionString = "server=localhost;port=3306;database=todolist;user=root;password=2377";

        }



        // Permite obter a connecção com o MySQL 

        private MySqlConnection GetConnection()

        {

            return new MySqlConnection(ConnectionString);

        }



        public void ActualizarTarefa(Tarefa tarefa)

        {

            using (MySqlConnection conn = GetConnection())

            {

                //Abrimos uma coneção com a base de dados 

                conn.Open();



                //Criamos uma query de update  

                MySqlCommand query = new MySqlCommand("UPDATE Tarefa SET descricao=@descricao, dataInicio=STR_TO_DATE(@dataInicio, \"%d/%m/%Y %T\"), dataFim=STR_TO_DATE(@dataFim, \"%d/%m/%Y %T\") WHERE id=@id;", conn);



                //Para evitar o SQL Injection usamos o mecanismo AddWithValue em vez de colocarmos directamente na string da Query 

                query.Parameters.AddWithValue("@id", tarefa.Id);

                query.Parameters.AddWithValue("@descricao", tarefa.Descricao);

                query.Parameters.AddWithValue("@dataInicio", tarefa.DataInicio);

                query.Parameters.AddWithValue("@dataFim", tarefa.DataFim);

                //Como as queries de update não devolvem dados usamos o execute non query em vez do Reader como no exemplo ListAll 

                query.ExecuteNonQuery();



                //Fechamos a ligação com a base de dados 

                conn.Close();

            }

        }



        public List<Tarefa> ConsultarTarefa()
        {



            //Lista para guardar as tarefas num formato que seja válido para programar facilmente.  

            List<Tarefa> tarefas = new List<Tarefa>();



            //Estabelecer uma ligação com a base de dados 

            using (MySqlConnection conn = GetConnection())

            {

                conn.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT * FROM Tarefa;", conn);

                using (MySqlDataReader reader = cmd.ExecuteReader())

                {

                    while (reader.Read())

                    {

                        tarefas.Add(new Tarefa()

                        {

                            Id = Int32.Parse(reader.GetString("id")),

                            Descricao = reader.GetString("descricao"),

                            DataInicio = reader.GetString("dataInicio"),

                            DataFim = reader.GetString("dataFim")

                        });



                    }

                }



            }



            return tarefas;



        }



        public void ApagarTarefa(int id)

        {
            Console.WriteLine("###################### DEBUG ####################");

            Console.WriteLine(id);
            using (MySqlConnection conn = GetConnection())

            {

                //Abrimos uma coneção com a base de dados 

                conn.Open();



                //Criamos uma query de update  

                MySqlCommand query = new MySqlCommand("DELETE FROM tarefa WHERE id=@id;", conn);



                //Para evitar o SQL Injection usamos o mecanismo AddWithValue em vez de colocarmos directamente na string da Query 

                query.Parameters.AddWithValue("@id", id);



                //Como as queries de update não devolvem dados usamos o execute non query em vez do Reader como no exemplo ListAll 

                query.ExecuteNonQuery();



                //Fechamos a ligação com a base de dados 

                conn.Close();

            }

        }




        public void criarTarefa(String descricao, String dataInicio, String dataFim)
        {



            Console.WriteLine("###################### DEBUG ####################");

            Console.WriteLine(descricao + " " + dataFim + " " + dataInicio);





            using (MySqlConnection conn = GetConnection())

            {

                //Abrimos uma coneção com a base de dados 

                conn.Open();



                //Criamos uma query de update  

                MySqlCommand query = new MySqlCommand("INSERT INTO Tarefa (Descricao,datainicio,datafim) VALUES (@descricao,@datainicio,@datafim)", conn);



                //Para evitar o SQL Injection usamos o mecanismo AddWithValue em vez de colocarmos directamente na string da Query 

                query.Parameters.AddWithValue("@descricao", descricao);

                query.Parameters.AddWithValue("@datainicio", dataInicio);

                query.Parameters.AddWithValue("@datafim", dataFim);



                //Como as queries de update não devolvem dados usamos o execute non query em vez do Reader como no exemplo ListAll 

                query.ExecuteNonQuery();



                //Fechamos a ligação com a base de dados 

                conn.Close();

            }

        }



        public void CriarProduto(decimal preco, string nome, string link)
        {
            using (MySqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();

                    MySqlCommand query = new MySqlCommand("INSERT INTO produto (preco, nome, link) VALUES (@preco, @nome, @link)", conn);

                    query.Parameters.AddWithValue("@preco", preco);
                    query.Parameters.AddWithValue("@nome", nome);
                    query.Parameters.AddWithValue("@link", link);

                    query.ExecuteNonQuery();

                    conn.Close();

                    Console.WriteLine("Produto inserido com sucesso.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao inserir produto: " + ex.Message);
                }
            }
        }




        public List<Produto> ConsultarProduto()
        {
            var produtos = new List<Produto>();
            
            using (MySqlConnection conn = GetConnection())
            using (var cmd = new SqlCommand("SELECT nome, preco, link FROM Produto"))
            {
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var produto = new Produto
                        {
                            Nome = reader.GetString(0),
                            Preco = reader.GetInt32(1),
                            Link = reader.GetString(2)
                        };
                        produtos.Add(produto);
                    }
                }
            }

            return produtos;
        }

        public void AtualizarProduto(Produto produto)
        {
            using (MySqlConnection conn = GetConnection())
            using (var cmd = new SqlCommand("UPDATE Produto SET nome = @nome, preco = @preco, link = @link"))
            {
                cmd.Parameters.AddWithValue("@nome", produto.Nome);
                cmd.Parameters.AddWithValue("@preco", produto.Preco);
                cmd.Parameters.AddWithValue("@link", produto.Link);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}


       

        
