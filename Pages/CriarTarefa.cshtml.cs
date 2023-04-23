using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using todolist.Models;

namespace todolist.Pages
{
    public class CriarTarefaModel : PageModel
    {
        public void OnGet()
        {


        }

        public void OnPost() { 
            string descricao = Request.Form["descricao"];
            string dataInicio = Request.Form["dateInicio"];
            string dataFim = Request.Form["dateFim"]; 
            TodoContext context = new TodoContext();
            context.criarTarefa(descricao, dataInicio, dataFim);


            ViewData["Mensagem"] = "Tarefa inserida com sucesso!";

        }




    }
}



