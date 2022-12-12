using ControleDeContatos.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDeContatos.Models
{
    public class UsuarioSemSenhaModel
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
        public PerfilEnum? Perfil { get; set; }


    }    
}
