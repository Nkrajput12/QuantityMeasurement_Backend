using System;

namespace QuantityMeasurementModelLayer.DTOs
{
    public class CacheRecordDto
    {
        public string OperationType { get; set; } = string.Empty;
        public string InputDetails { get; set; } = string.Empty;
        public string Result { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } = DateTime.Now;
    }
}