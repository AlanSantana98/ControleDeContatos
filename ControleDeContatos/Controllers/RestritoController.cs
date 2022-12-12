
using ControleDeContatos.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDeContatos.Controllers
{

    public class RestritoController : Controller
    {
        // Só permite acesso se o usuario estiver logado //
                  [PaginaParaUsuarioLogado]

        public IActionResult Index()
        {
            return View();
        }
    }
}
