
using Microsoft.AspNetCore.Mvc.RazorPages;
using todolist.Models;

namespace todolist.Pages
{
    public class ApagarTarefaModel : PageModel
    {
        public IEnumerable<Tarefa> tarefas { get; set; }

        public void OnGet()
        {
            TodoContext context = new TodoContext();

            tarefas = context.ConsultarTarefa();

        }


        public void OnPost()

        {

            TodoContext context = new TodoContext();

            //O Request.Form["IDENTIFICATOR"] usa o campo name das tags de input do Html para fazer o mapeamento aqui. 

            int id = Int32.Parse(Request.Form["id"]);

            context.ApagarTarefa(id);



            //Carregar as tarefas actualizadas 

            tarefas = context.ConsultarTarefa();

        

        }



    }
}
