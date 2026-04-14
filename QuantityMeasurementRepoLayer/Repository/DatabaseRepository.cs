using System.Collections.Generic;
//using Microsoft.Data.SqlClient;
using QuantityMeasurementModelLayer.DTOs;
using QuantityMeasurementRepoLayer.Interfaces;
using QuantityMeasurementRepoLayer.Data;
using Microsoft.Extensions.Configuration;
using QuantityMeasurementRepoLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace QuantityMeasurementRepoLayer
{
    public class DatabaseRepository : IDatabaseRepository
    {
        private readonly MeasurementDbContext context;

        public DatabaseRepository(MeasurementDbContext cont)
        {
            context = cont;
        }

        public void SaveToDatabase(CacheRecordDto record,int? userId)
        {
            var newEntry = new MeasurementHistory
            {
                OperationType = record.OperationType,
                InputDetails = record.InputDetails,
                Result = record.Result,
                Timestamp = record.Timestamp,
                UserId = userId
            };

            context.MeasurementHistories.Add(newEntry);
            context.SaveChanges();
        }

        public IEnumerable<CacheRecordDto> GetAllFromDatabase()
        {
            var list = context.MeasurementHistories.OrderByDescending(m => m.Timestamp).Select(m => new CacheRecordDto
            {
                OperationType = m.OperationType,
                InputDetails = m.InputDetails,
                Result = m.Result,
                Timestamp = m.Timestamp
            }).ToList();

            return list;
        }

        public void ClearDatabase()
        {
            context.MeasurementHistories.ExecuteDelete();
        }

        public IEnumerable<CacheRecordDto> GetHistoryByUserId(int userId)
        {
            return context.MeasurementHistories.Where(M => M.UserId == userId).OrderByDescending(M => M.Timestamp).Select(M => new CacheRecordDto
            {
                OperationType = M.OperationType,
                InputDetails = M.InputDetails,
                Result = M.Result,
                Timestamp = M.Timestamp
            }).ToList();
        }

        public int GetOperationCount(int userId)
        {
            return context.MeasurementHistories.Count(m => m.UserId == userId);
        }

        public IEnumerable<CacheRecordDto> GetHistoyByOperation(string Operation,int userId)
        {
            return context.MeasurementHistories.
            Where(M => M.OperationType == Operation && M.UserId == userId).
            OrderByDescending(M => M.Timestamp).
            Select(M => new CacheRecordDto
            {
                OperationType = M.OperationType,
                InputDetails = M.InputDetails,
                Result = M.Result,
                Timestamp = M.Timestamp
            });

        }


    }
}