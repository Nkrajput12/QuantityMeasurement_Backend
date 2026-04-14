using System;

namespace QuantityMeasurementModelLayer.DTOs;

public class LoginDto
{
    public string Username {get; set;} = string.Empty;
    public string PasswordHash {get; set;} = string.Empty;
}