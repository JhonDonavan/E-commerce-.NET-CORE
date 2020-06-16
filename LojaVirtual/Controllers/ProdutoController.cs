using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using LojaVirtual.Models;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.Controllers
{
    public class ProdutoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult Visualizar()
        {
            Produto produto = getProduto();

            return View(produto);
            //return new ContentResult() { Content = "<h3>Produto -> visualizar</h3>", ContentType = "text/html" };
        }


        private Produto getProduto() 
        {
            return new Produto()
            {
                id = 1,
                nome = "Xbox one ",
                descricao = "Jogue em 4K",
                valor = 2200.00
            };
        }
    }
}