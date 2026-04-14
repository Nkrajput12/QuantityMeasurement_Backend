using System;
using QuantityMeasurementModelLayer.Interfaces;
using QuantityMeasurementModelLayer.DTOs;
using QuantityMeasurementRepoLayer.Entities;

namespace QuantityMeasurementBusinessLayer.Interfaces;
public interface IAuthService
{
    string Register(LoginDto login);
    User? Login(LoginDto login);
}