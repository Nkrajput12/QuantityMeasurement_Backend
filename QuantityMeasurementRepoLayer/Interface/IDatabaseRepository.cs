using System.Collections.Generic;
using QuantityMeasurementModelLayer.DTOs;
using QuantityMeasurementRepoLayer.Entities;

namespace QuantityMeasurementRepoLayer.Interfaces;
public interface IDatabaseRepository
    {
        void SaveToDatabase(CacheRecordDto record,int? userId);
        IEnumerable<CacheRecordDto> GetAllFromDatabase();
        IEnumerable<CacheRecordDto> GetHistoryByUserId(int userId);
        void ClearDatabase();
        int GetOperationCount(int userId);
        IEnumerable<CacheRecordDto> GetHistoyByOperation(string Operation,int userId);
    }