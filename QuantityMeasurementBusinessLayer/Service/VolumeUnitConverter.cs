using QuantityMeasurementModelLayer.Interfaces;
using QuantityMeasurementModelLayer.Enums;
using QuantityMeasurementBusinessLayer.Interfaces;

namespace QuantityMeasurementBusinessLayer.Services
{

    public class VolumeUnitConverter: IMeasurable<VolumeUnit>
    {
        private static readonly double[] ConversionFactors =
        {
            1.0,
            0.001,
            3.78541
        };

        public double GetConversionFactor(VolumeUnit unit)
        {
            return ConversionFactors[(int)unit];
        }

        public double ConvertToBase(VolumeUnit unit, double amount)
        {
            return amount * GetConversionFactor(unit);
        }

        public double ConvertFromBase(VolumeUnit unit, double baseValue)
        {
            return baseValue / GetConversionFactor(unit);
        }

        public string GetSymbol(VolumeUnit unit)
        {
            switch (unit)
            {
                case VolumeUnit.Litre: return "L";
                case VolumeUnit.MilliLiter: return "ML";
                case VolumeUnit.Gallon: return "gal";
                default: return unit.ToString().ToLower();
            }
        }
    }
}