using System;
using System.Collections.Generic;
using QuantityMeasurementModelLayer.DTOs;
using QuantityMeasurementRepoLayer.Entities;

namespace QuantityMeasurementBusinessLayer.Interfaces
{
    public interface IQuantityMeasurementService
    {
        QuantityDTO Convert<T>(QuantityDTO source, string targetUnit,int? userId) where T : struct, Enum;
        QuantityDTO Add<T>(QuantityDTO q1, QuantityDTO q2, string targetUnit,int? userId) where T : struct, Enum;
        QuantityDTO Subtract<T>(QuantityDTO q1, QuantityDTO q2, string targetUnit,int? userId) where T : struct, Enum;
        double Divide<T>(QuantityDTO q1, QuantityDTO q2,int? userId) where T : struct, Enum;
        bool Compare<T>(QuantityDTO q1, QuantityDTO q2,int? userId) where T : struct, Enum;
        IEnumerable<CacheRecordDto> GetHistory(bool fromDatabase);
        IEnumerable<CacheRecordDto> GetHistoryById(int userId);
        int GetOperationCount(int userId);
        IEnumerable<CacheRecordDto> GetHistoryByOperation(string Operation,int userId);
    }
}