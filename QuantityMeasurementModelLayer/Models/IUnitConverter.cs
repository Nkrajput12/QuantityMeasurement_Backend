using System;

namespace QuantityMeasurementModelLayer.Interfaces
{
    public interface IUnitConverter<TUnit> where TUnit : struct, Enum
    {
        double ConvertToBase(TUnit unit, double amount);
        double ConvertFromBase(TUnit unit, double baseValue);
        string GetSymbol(TUnit unit);
    }
}