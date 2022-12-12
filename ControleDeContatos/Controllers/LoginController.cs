using ControleDeContatos.Helper;
using ControleDeContatos.Models;
using ControleDeContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDeContatos.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ISessao _sessao;
        private readonly IEmail _email;

        public LoginController(IUsuarioRepositorio usuarioRepositorio, ISessao sessao, IEmail email)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _sessao = sessao;
            _email = email;
        }
                                                   
        public IActionResult Index()
        {
            // Se ususario estiver logado, redirecionar para home //
            if (_sessao.BuscarSessaoDoSUsuario() != null) return RedirectToAction("Index", "Home");

            return View();
        }

        public IActionResult Sair()
        {
            _sessao.RemoverSessaoUsuario();

            return RedirectToAction("Index", "Login");
        }

        public IActionResult RedefinirSenha()                       
        {
            return View();
        }


        [HttpPost]
        public IActionResult Entrar(LoginModel loginModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel usuario = _usuarioRepositorio.BuscarPorLogin(loginModel.Login);

                    if (usuario != null)                         // Validação //
                    {
                        if (usuario.SenhaValida(loginModel.Senha)) // Validação //
                        {
                            _sessao.CriarSessaoDoUsuario(usuario);
                            return RedirectToAction("Index", "Home");
                        }
                        TempData["MensagemErro"] = $"Senha do usuário é inválida.Por favor, tente novamente!";
                    }
                    TempData["MensagemErro"] = $"Usuário e/ou Senha inválido(s).Por favor, tente novamente!";
                }
                return View("Index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos realizar seu Login, tente novamente,detalhe do erro:{erro.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
         public IActionResult EnviarlinkParaRedefinirSenha(RedefinirSenhaModel redefinirSenhaModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel usuario = _usuarioRepositorio.BuscarPorEmailELogin(redefinirSenhaModel.Email, redefinirSenhaModel.Login);

                    if (usuario != null)                         // Validação //
                    {
                        string novaSenha = usuario.GerarNovaSenha();                       
                        string mensagem = $"Sua nova senha é:{novaSenha}";

                        bool emailEnviado =  _email.Enviar(usuario.Email, "Sistema de contatos -Nova Senha",mensagem);

                        if (emailEnviado)
                        {
                            _usuarioRepositorio.Atualizar(usuario);
                            TempData["MensagemSucesso"] = $"Enviamos para seu e-mail cadastrado uma nova senha.";
                        }
                        else
                        {
                            TempData["MensagemErro"] = $" não conseguimos enviar o e-mail. Por favor,tente novamente!";
                        }
                        
                        return RedirectToAction("Index", "Login");
                    }
                    TempData["MensagemErro"] = $" não conseguimos redefinir sua senha. Por favor,verifique os dados imformados!";
                }
                return View("Index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos redefinir sua senha, tente novamente,detalhe do erro:{erro.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}

    
