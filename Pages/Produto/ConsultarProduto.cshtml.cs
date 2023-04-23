using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

using todolist.Models;

namespace ProdutoApp.Pages.Produto1
{
    public class ConsultarProdutoModel : PageModel
    {
        public List<Produto> Produtos { get; set; }

        public void OnGet()
        {
            Produtos = new List<Produto>();

            using (SqlConnection conn = new SqlConnection("connectionString"))
            using (SqlCommand cmd = new SqlCommand("SELECT nome, preco, link FROM Produto", conn))
            {
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Produtos.Add(new Produto
                        {
                            Nome = reader.GetString(0),
                            Preco = reader.GetInt32(1),
                            Link = reader.GetString(2)
                        });
                    }
                }
            }
        }
    }
}
