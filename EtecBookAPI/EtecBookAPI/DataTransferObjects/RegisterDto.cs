using System.ComponentModel.DataAnnotations;


namespace EtecBookAPI.DataTransferObjects;

 public class RegisterDto
 {
    [Required( ErrorMessage = " Informe o Nome")]    
    [StringLength(60, ErrorMessage = " O Nome deve possuir no máximo 60 carcteres")]

    public string Name{ get; set; }

    [Required( ErrorMessage = " Informe o Email")]    
    [EmailAddress(ErrorMessage = "Informe um Email válido")]
    [StringLength(100, ErrorMessage = " O Email deve possuir no máximo 100 carcteres")]
    public string Email{ get; set; }

    [DataType(DataType.Password)]
    [Required( ErrorMessage = " Informe a Senha")]    
    [StringLength(20, MinimumLength = 6,
     ErrorMessage = " A Senha deve possuir no máximo 20 carcteres")]
    public string Password { get; set; }

 }
