using System.Collections.Generic;
using QuantityMeasurementModelLayer.DTOs;

namespace QuantityMeasurementRepoLayer.Interfaces
{
    public interface ICacheRepository
    {
        void SaveToCache(CacheRecordDto record);
        IEnumerable<CacheRecordDto> GetAllHistory();
        void ClearCache();
    }
}