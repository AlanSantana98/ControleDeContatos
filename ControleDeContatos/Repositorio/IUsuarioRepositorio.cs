using ControleDeContatos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDeContatos.Repositorio
{
    public interface IUsuarioRepositorio
    {

    UsuarioModel BuscarPorLogin(string login);
    UsuarioModel BuscarPorEmailELogin(string email, string Login);     
    UsuarioModel ListarporId(int id);
    List<UsuarioModel> BuscarTodos();
    UsuarioModel Adicionar(UsuarioModel usuario);
    UsuarioModel Atualizar(UsuarioModel usuario);
    UsuarioModel AlterarSenha(AlterarSenhaModel alterarSenhaModel); 
    bool Apagar(int id);
}
}
