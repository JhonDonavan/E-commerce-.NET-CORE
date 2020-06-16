using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LojaVirtual.Database;
using LojaVirtual.Libraries.Email;
using LojaVirtual.Libraries.Login;
using LojaVirtual.Models;
using LojaVirtual.Repositories;
using LojaVirtual.Repositories.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.Controllers
{
    public class HomeController : Controller
    {

        private IClienterepository _repositoryCliente;
        private INewsLetterRepository _repositoryNewsLetter;
        private LoginCliente _loginCliente;

        public HomeController(IClienterepository repositoryCliente, INewsLetterRepository repositoryNewsLetter, LoginCliente loginCliente)
        {
            _repositoryCliente = repositoryCliente;
            _repositoryNewsLetter = repositoryNewsLetter;
            _loginCliente = loginCliente;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index([FromForm]NewsLetterEmail newsLetter)
        {
            if (ModelState.IsValid)
            {
                _repositoryNewsLetter.Create(newsLetter);

                TempData["MSG_S"] = "E-mail cadastrado! Agora você vai receber promoções especiais no seu e-mail! Fique atento as novidades!";
                 
                return RedirectToAction(nameof(Index));
               
            }
            else {
                return View();
            }           
        }

        public IActionResult Contato()
        {
            return View();
        }

        public IActionResult ContatoAcao()
        {
            try
            {
                Contato contato = new Contato();
                contato.Nome = HttpContext.Request.Form["nome"];
                contato.Email = HttpContext.Request.Form["email"];
                contato.Texto = HttpContext.Request.Form["texto"];

               

                var listaMensagens = new List<ValidationResult>();

                var contexto = new ValidationContext(contato);

                bool isvalid = Validator.TryValidateObject(contato, contexto, listaMensagens, true);

                if (isvalid)
                {
                    Contatoemail.EnviarContatoPorEmail(contato);
                    ViewData["MSG_S"] = "Mensagem enviada com sucesso!";
                }
                else
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (var texto in listaMensagens)
                    {
                        sb.Append(texto.ErrorMessage + "<br/>");
                    }

                    ViewData["MSG_E"] = sb.ToString();

                    ViewData["Contato"] = contato;
                }

              
            }
            catch (Exception)
            {
                ViewData["MSG_E"] = "Ops! Tivemos um Erro, tente novamente mais tarde.";

                //TODO - implementar LOG
            }
            return View("Contato");
        }

        [HttpGet]
        public IActionResult Login() 
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login([FromForm]Cliente cliente) 
        {
            /*Cliente clienteDB = _repositoryCliente.Login(cliente.email, cliente.senha);
            if (cliente.email == "jhondonavan@gmail.com" && cliente.senha == "123456")
            {
                HttpContext.Session.Set("ID", new byte[]{ 52 });
                //_loginCliente.Login(clienteDB);
                // return new RedirectResult(Url.Action(nameof(Painel)));
                return new ContentResult() { Content = "Login executado!" };
            }
            else
            {
                ViewData["MSG_E"] = "Usuário ou senha inválido!";
                return View();
            }
            */

            Cliente clienteDB = _repositoryCliente.Login(cliente.email, cliente.senha);

            if (clienteDB != null)
            {
                _loginCliente.Login(clienteDB);

                return new RedirectResult(Url.Action(nameof(Painel)));
            }
            else
            {
                ViewData["MSG_E"] = "Usuário não encontrado, verifique o e-mail e senha digitado!";
                return View();
            }

        }

        public IActionResult Painel() 
        {

            Cliente cliente = _loginCliente.GetCliente();

            if (cliente != null)
            {
                return new ContentResult { Content = "Usuario  logado! " + cliente.email };
            }
            else 
            {
                return new ContentResult() { Content = "Acesso Negado!!!" };
            }

            /* Cliente cliente = _loginCliente.GetCliente();
             
             if (cliente != null)
             {
                 return new ContentResult() { Content = "Usuário " + cliente.id + " " + cliente.email  + ". Logado!" };
             }
             else
             {
                 return new ContentResult() { Content = "Acesso negado@" };

             }
             */
            
        }

        [HttpGet]
        public IActionResult CadastroCliente() 
        {
            return View();
        }

        [HttpPost]
        public IActionResult CadastroCliente([FromForm]Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _repositoryCliente.Create(cliente);
             
                TempData["MSG_S"] = "Cadastro realizado com sucesso";
                
                //TODO Implementar redirecionamentos diferentes (painel, carrinho de compras, .... )

                return RedirectToAction(nameof(CadastroCliente));

            }
            return View();
        }

        public IActionResult CarrinhoCompras()
        {
            return View();
        }


    }
}