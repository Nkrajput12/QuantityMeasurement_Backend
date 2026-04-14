using Microsoft.AspNetCore.Mvc;
using QuantityMeasurementModelLayer.DTOs;
using QuantityMeasurementBusinessLayer.Interfaces;
using QuantityMeasurementModelLayer.Enums;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using System;

namespace QuantityMeasurementApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _auth;
    public AuthController(IAuthService auth)
    {
        _auth = auth;
    }

    [HttpPost("Register")]
    public IActionResult Register(LoginDto login)
    {
        var result = _auth.Register(login);
        if(result.Contains("Success")) return Ok(new {message = result});
        return BadRequest(new {message = result});
    }

    [HttpPost("Login")]
    public IActionResult Login(LoginDto login)
    {
        var user = _auth.Login(login);
        if(user == null) return BadRequest(new {message = "Invalid Credential"});
        //generate the jwt token
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes("Quantity_Measurement_App_Authentication");

        var tokenDescripter = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name,user.Username),
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString())
            }),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescripter);
        var tokenstring = tokenHandler.WriteToken(token);
        
        return Ok(new {message = "Login successful",user = user.Username,token = tokenstring});
    }
}