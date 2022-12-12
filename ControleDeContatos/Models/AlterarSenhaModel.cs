using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDeContatos.Models
{
    public class AlterarSenhaModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Digite a senha atual do usuario")]
        public string SenhaAtual { get; set; }

        [Required(ErrorMessage = "Digite a Nova senha do usuario")]
        public string NovaSenha { get; set; }

        [Required(ErrorMessage = "Comfirme a Nova senha do usuario")]
        [Compare("NovaSenha",ErrorMessage ="Senha não confere com a nova senha")]  // Faz comparação entre as senhas //
        public string ConfirmarNovaSenha { get; set; }
    }
}
