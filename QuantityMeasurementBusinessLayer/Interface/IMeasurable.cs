using System;
using QuantityMeasurementModelLayer.Interfaces;

namespace QuantityMeasurementBusinessLayer.Interfaces
{
    public interface IMeasurable<TUnit> : IUnitConverter<TUnit> where TUnit : struct, Enum
    {
        double GetConversionFactor(TUnit unit);
    }
}