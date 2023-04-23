using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using todolist.Models;

namespace todolist.Pages.Produto
{
    public class CriarProdutoModel : PageModel
    {
        public void OnGet()
        {
        }

        public void OnPost()
        {

            string nome = Request.Form["nome"];
            int preco = int.Parse(Request.Form["preco"]);
            string link = Request.Form["link"];
            TodoContext context = new TodoContext();
            context.CriarProduto(preco, nome, link);


            ViewData["Mensagem"] = "Tarefa inserida com sucesso!";


           
        }
    }
}



 