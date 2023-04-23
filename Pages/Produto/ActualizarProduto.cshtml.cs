using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

using todolist.Models;

namespace ProdutoApp.Pages.Produto1
{
    public class ActualizarProdutoModel : PageModel
    {
        [BindProperty]
        public List<Produto> produtos { get; set; }

        public void OnGet()
        {
            using (SqlConnection conn = new SqlConnection("connectionString"))
            using (SqlCommand cmd = new SqlCommand("SELECT nome, preco, link FROM Produto", conn))
            {
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    produtos = new List<Produto>();

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
        }

        public IActionResult OnPost(string nome, int preco, string link)
        {
            using (SqlConnection conn = new SqlConnection("connectionString"))
            using (SqlCommand cmd = new SqlCommand("UPDATE Produto SET nome=@nome, preco=@preco, link=@link", conn))
            {
                cmd.Parameters.AddWithValue("@nome", nome);
                cmd.Parameters.AddWithValue("@preco", preco);
                cmd.Parameters.AddWithValue("@link", link);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            return RedirectToPage("/ConsultarProduto");
        }
    }

}