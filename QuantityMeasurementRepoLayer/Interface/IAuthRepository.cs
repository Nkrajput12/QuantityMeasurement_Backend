using System.Collections.Generic;
using QuantityMeasurementModelLayer.DTOs;
using QuantityMeasurementRepoLayer.Entities;

namespace QuantityMeasurementRepoLayer.Interfaces;

public interface IAuthRepository
{
    void Register(User user);
    User? GetUserByUsername(string username);
}