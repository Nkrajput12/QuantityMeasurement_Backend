using System.Collections.Generic;
//using Microsoft.Data.SqlClient;
using QuantityMeasurementModelLayer.DTOs;
using QuantityMeasurementRepoLayer.Interfaces;
using QuantityMeasurementRepoLayer.Data;
using Microsoft.Extensions.Configuration;
using QuantityMeasurementRepoLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace QuantityMeasurementRepoLayer;

public class AuthRepository : IAuthRepository
{
    private readonly MeasurementDbContext _context;
    public AuthRepository(MeasurementDbContext context)
    {
        _context = context;
    }

    public void Register(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
    }

    public User? GetUserByUsername(string username)
    {
        return _context.Users.FirstOrDefault(u=> u.Username == username);
        
    }
}