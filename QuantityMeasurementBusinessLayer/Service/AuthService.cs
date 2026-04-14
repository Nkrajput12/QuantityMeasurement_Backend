using QuantityMeasurementBusinessLayer.Interfaces;
using QuantityMeasurementModelLayer.DTOs;
using QuantityMeasurementRepoLayer.Entities;
using QuantityMeasurementRepoLayer.Interfaces;
using BCrypt.Net;
namespace QuantityMeasurementBusinessLayer.Services;
public class AuthService : IAuthService
{
    private readonly IAuthRepository _authRepo;
    public AuthService(IAuthRepository authRepo)
    {
        _authRepo = authRepo;
    }

    public string Register(LoginDto login)
    {
        try
        {
            var user = new User
            {
                Username = login.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(login.PasswordHash)
            };
            _authRepo.Register(user);
            return "Success: Register successful";
        }
        catch(Exception )
        {
            return "Error: User not Register";
        }
    }
    public User? Login(LoginDto login)
    {
        var User = _authRepo.GetUserByUsername(login.Username);
        if (User != null && BCrypt.Net.BCrypt.Verify(login.PasswordHash, User.PasswordHash))
        {
            return User;
        }
        return null;
    }
}

