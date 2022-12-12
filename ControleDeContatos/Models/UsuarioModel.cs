using ControleDeContatos.Enums;
using ControleDeContatos.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDeContatos.Models
{
    public class UsuarioModel
    {
    
    public int Id { get; set; }

        [Required(ErrorMessage = "Digite o nome do Usuario")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Digite o Login do Usuario")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Digite o e-mail do Usuario")]
        [EmailAddress(ErrorMessage = "O e-mail imformado não é valido!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Digite o Perfil do Usuario")]
        public PerfilEnum Perfil { get; set; }

        [Required(ErrorMessage = "Digite a senha do Usuario")]    
        public string Senha { get; set; }

        [Display]
        public DateTime DataCadastro { get; set; }
                                                                
        
        public DateTime? DataAtualizacao{ get; set; }

        public virtual List<ContatoModel> Contatos { get; set; }

        public bool SenhaValida(string senha)
        {
            return Senha == senha; // ".GerarHash()" Criptografia da senha // 
        }

        // Metodo criado para gerar criptografia da senha. //
        public void SetSenhaHash()
        {                              

            Senha = Senha.GerarHash();  // (Só é permitido fazer essa extensão com o "GerarHash" devido ao "this") //
        }

        public void SetNovaSenha(string novaSenha)
        {
            Senha = novaSenha.GerarHash();             
        }
                                                          // GerarHash();  Cria ou edita uma senha já com criptografia no BANCO DE DADOS//
        public string GerarNovaSenha()
        {
            string novaSenha = Guid.NewGuid().ToString().Substring(0, 8);
            Senha = novaSenha.GerarHash();                  
            return novaSenha;
        }

    }
}
