using QuantityMeasurementModelLayer.DTOs;

namespace QuantityMeasurementModelLayer.DTOs
{
    public class MeasurementApiRequest
    {
        public string Category {get; set;} = string.Empty;

        public QuantityDTO Value1{get; set;} = null!;

        public QuantityDTO? Value2{get; set;}
        public string? TargetUnit{get; set;}
    }
}