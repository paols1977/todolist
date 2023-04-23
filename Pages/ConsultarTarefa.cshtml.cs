using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using todolist.Models;

namespace todolist.Pages
{
    public class ConsultarTarefaModel : PageModel
    {

        public IEnumerable<Tarefa> tarefas { get; set; }
        public void OnGet()
        {

            TodoContext context = new TodoContext();

            tarefas = context.ConsultarTarefa();


        }
    }
}
