using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using EtecBookAPI.Data;
using EtecBookAPI.DataTransferObjects;
using EtecBookAPI.Helpers;
using EtecBookAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EtecBookAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly AppDbContext _context;
    public AccountController(AppDbContext context)
    {
        _context = context;
    }       

    [HttpPost("authenticate")]
    public async Task<IActionResult> Authenticate([FromBody]  LoginDto login)
    {
        if (login == null)
            return BadRequest();

        if (!ModelState.IsValid)
            return BadRequest();
            
        AppUser user = new();
        if (IsEmail(login.Email))
        {
            user = await _context.Users.FirstOrDefaultAsync(
                u =>u.Email.Equals(login.Email)
            );
        }
        else 
        {
            user = await _context.Users.FirstOrDefaultAsync(
                u =>u.UserName.Equals(login.Email)
            );
        }
        if (user == null)
            return NotFound(new {Message = "Usuário e/ou Senha Inválidos"});
        if (!PasswordHasher.VerifyPassword(login.Password, user.Password))
            return NotFound(new {Message = "Usuário e/ou Senha Inválidos"});
        return Ok (new { Message ="Usuário Autenticado"});

    }

    [HttpPost("register") ]
    public async Task<IActionResult> Register([FromBody] RegisterDto register)
    {
         if (register == null)
            return BadRequest();
        if (!ModelState.IsValid)
            return BadRequest();
        // Checar se o email já existe
        if (await _context.Users.AllAsync(
            u => u.Email.Equals(register.Email)
        ))
            return BadRequest(new {Message = "Email já cadastrado! Tente recuperar sua senha ou entre em contato com o Administrador!"});
        //Checar a força da Senha

    }

    private string CheckPasswordStrenght(string password)
    {
        StringBuilder sb = new();
        if (password.Length < 6)
            sb.Append("A senha deve possuir no mínimo 6 carcteres" + Environment.NewLine);
        if(!(Regex.IsMatch(password, "[a-z]")) && (Regex.IsMatch(password, "[A-Z]") && (Regex.IsMatch(password, "[0-9]"))))
            sb.Append("A senha deve ser alfanumériaca " + Environment.NewLine);
        if(!(Regex.IsMatch(password, "colocar caracteres especiais")))
            sb.Append("A senha deve ser alfanumériaca " + Environment.NewLine);
        return sb.ToString();
    }

    private bool IsEmail(String email)
    {
        try
        {
            MailAddress mail = new(email);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
