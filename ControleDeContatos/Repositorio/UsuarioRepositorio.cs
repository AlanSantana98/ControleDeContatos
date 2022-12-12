using ControleDeContatos.Data;
using ControleDeContatos.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDeContatos.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly BancoContext _bancoContext;
        public UsuarioRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        public UsuarioModel BuscarPorLogin(string login)
        {                                                                 // ("ToUpper")Transforma tudo para maiusculo //
            return _bancoContext.Usuarios.FirstOrDefault(x => x.Login.ToUpper() == login.ToUpper());
        }

        public UsuarioModel BuscarPorEmailELogin(string email, string Login)
        {
            return _bancoContext.Usuarios.FirstOrDefault(x => x.Email.ToUpper() == email.ToUpper() && x.Login.ToUpper() == Login.ToUpper());
        }

        public UsuarioModel ListarporId(int id)
        {
            return _bancoContext.Usuarios.FirstOrDefault(x => x.Id == id);
        }

        public List<UsuarioModel> BuscarTodos()
        {
            return _bancoContext.Usuarios
                .Include(x => x.Contatos)
                .ToList();
        }

        public UsuarioModel Adicionar(UsuarioModel usuario)
        {
             usuario.DataCadastro = DateTime.Now;
             usuario.SetSenhaHash();                             // Função não está funcionando //
            _bancoContext.Usuarios.Add(usuario);
            _bancoContext.SaveChanges();

            return usuario;
        }

        public UsuarioModel Atualizar(UsuarioModel usuario)
        {
            UsuarioModel UsuarioDB = ListarporId(usuario.Id);

            if (UsuarioDB == null) throw new System.Exception("Houve um erro na atualização do contato!");

            UsuarioDB.Nome = usuario.Nome;           
            UsuarioDB.Email = usuario.Email;
            UsuarioDB.Login = usuario.Login;
            UsuarioDB.Perfil = usuario.Perfil;
            UsuarioDB.DataAtualizacao = DateTime.Now;
                           
            _bancoContext.Usuarios.Update(UsuarioDB);           
            _bancoContext.SaveChanges();

            return UsuarioDB;
        }

        public UsuarioModel AlterarSenha(AlterarSenhaModel alterarSenhaModel)
        {
            UsuarioModel usuarioDB = ListarporId(alterarSenhaModel.Id);                    

            // Regra de negocio.(condições) //

            if (usuarioDB == null) throw new Exception("Houve um erro na atualização da senha, usuario não encontrado!");

            if (!usuarioDB.SenhaValida(alterarSenhaModel.SenhaAtual)) throw new Exception("Senha atual não confere!");

            if (usuarioDB.SenhaValida(alterarSenhaModel.NovaSenha)) throw new Exception("Nova senha dever ser diferente da senha atual!");

            usuarioDB.SetNovaSenha(alterarSenhaModel.NovaSenha);
            usuarioDB.DataAtualizacao = DateTime.Now;

            _bancoContext.Usuarios.Update(usuarioDB);
            _bancoContext.SaveChanges();

            return usuarioDB;

        }

        public bool Apagar(int id)
        {
            UsuarioModel UsuarioDB = ListarporId(id);

            if (UsuarioDB == null) throw new System.Exception("Houve um erro na exclusão do Usuario!");

            _bancoContext.Usuarios.Remove(UsuarioDB);
            _bancoContext.SaveChanges();

            return true;
        }     
    }
}
