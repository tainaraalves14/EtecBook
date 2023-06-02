using System.ComponentModel.DataAnnotations;


namespace EtecBookAPI.DataTransferObjects;

 public class LoginDto
{
    [Required( ErrorMessage = " Informe o Email ou Nome de Usuário")]    
    [StringLength(100, ErrorMessage = " O Email ou Nome deve possuir no máximo 100 carcteres")]
    public string Email{ get; set; }


    [DataType(DataType.Password)]
    [Required( ErrorMessage = " Informe a Senha")]    
    [StringLength(20, ErrorMessage = " A Senha deve possuir no máximo 20 carcteres")]
    public string Password { get; set; }


}
