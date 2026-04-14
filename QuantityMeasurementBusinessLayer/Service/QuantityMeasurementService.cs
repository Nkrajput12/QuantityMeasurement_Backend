using System;
using System.Collections.Generic;
using QuantityMeasurementModelLayer.DTOs;
using QuantityMeasurementModelLayer.Enums;
using QuantityMeasurementRepoLayer.Interfaces;
using QuantityMeasurementBusinessLayer.Interfaces;

namespace QuantityMeasurementBusinessLayer.Services
{
    public class QuantityMeasurementService : IQuantityMeasurementService
    {
        private readonly ICacheRepository _cacheRepo;
        private readonly IDatabaseRepository dataRepo;
        private readonly LengthUnitConverter lengthConv = new LengthUnitConverter();
        private readonly WeightUnitConverter weightConv = new WeightUnitConverter();
        private readonly VolumeUnitConverter volumeConv = new VolumeUnitConverter();
        private readonly TemperatureUnitConverter tempConv = new TemperatureUnitConverter();

        public QuantityMeasurementService(ICacheRepository cacheRepo,IDatabaseRepository databaseRepo)
        {
            _cacheRepo = cacheRepo;
            dataRepo = databaseRepo;
        }

        //helper method to get the base value
        private double GetBaseValue<T>(QuantityDTO dto) where T : struct, Enum
        {
            T unit = Enum.Parse<T>(dto.Unit, true);

            if (typeof(T) == typeof(LengthUnit)) return lengthConv.ConvertToBase((LengthUnit)(object)unit, dto.Value);
            if (typeof(T) == typeof(WeightUnit)) return weightConv.ConvertToBase((WeightUnit)(object)unit, dto.Value);
            if (typeof(T) == typeof(VolumeUnit)) return volumeConv.ConvertToBase((VolumeUnit)(object)unit, dto.Value);
            if (typeof(T) == typeof(TemperatureUnit)) return tempConv.ConvertToBase((TemperatureUnit)(object)unit, dto.Value);

            throw new Exception("Unsupported Unit Category");
        }

        private QuantityDTO CreateFromBase<T>(double baseValue, string targetUnitStr) where T : struct, Enum
        {
            T target = Enum.Parse<T>(targetUnitStr, true);
            double converted = 0;
            string symbol = "";

            if (typeof(T) == typeof(LengthUnit)) 
            { 
                converted = lengthConv.ConvertFromBase((LengthUnit)(object)target, baseValue); 
                symbol = lengthConv.GetSymbol((LengthUnit)(object)target); 
            }
            else if (typeof(T) == typeof(WeightUnit)) 
            { 
                converted = weightConv.ConvertFromBase((WeightUnit)(object)target, baseValue); 
                symbol = weightConv.GetSymbol((WeightUnit)(object)target); 
            }
            else if (typeof(T) == typeof(VolumeUnit)) 
            { 
                converted = volumeConv.ConvertFromBase((VolumeUnit)(object)target, baseValue); 
                symbol = volumeConv.GetSymbol((VolumeUnit)(object)target); 
            }
            else if (typeof(T) == typeof(TemperatureUnit)) 
            { 
                converted = tempConv.ConvertFromBase((TemperatureUnit)(object)target, baseValue); 
                symbol = tempConv.GetSymbol((TemperatureUnit)(object)target); 
            }

            return new QuantityDTO(Math.Round(converted, 2), symbol); 
        }

        public QuantityDTO Convert<T>(QuantityDTO source, string targetUnit,int? userId) where T : struct, Enum
        {
            double baseValue = GetBaseValue<T>(source);
            var result = CreateFromBase<T>(baseValue, targetUnit);

            var record = new CacheRecordDto
            {
                OperationType = "Conversion",
                InputDetails = $"{source.Value} {source.Unit} to {targetUnit}",
                Result = $"{result.Value} {result.Unit}"
            };

            _cacheRepo.SaveToCache(record);
            dataRepo.SaveToDatabase(record,userId);

            return result;
        }

        public QuantityDTO Add<T>(QuantityDTO q1, QuantityDTO q2, string targetUnit,int? userId) where T : struct, Enum
        {
            double base1 = GetBaseValue<T>(q1);
            double base2 = GetBaseValue<T>(q2);
            var result = CreateFromBase<T>(base1 + base2, targetUnit);
            
            var record = new CacheRecordDto 
            { 
                OperationType = "Addition", 
                InputDetails = $"{q1.Value} {q1.Unit} + {q2.Value} {q2.Unit}", 
                Result = $"{result.Value} {result.Unit}" 
            };

            _cacheRepo.SaveToCache(record);
            dataRepo.SaveToDatabase(record,userId);
            return result;
        }

        public QuantityDTO Subtract<T>(QuantityDTO q1, QuantityDTO q2, string targetUnit,int? userId) where T : struct, Enum
        {
            double base1 = GetBaseValue<T>(q1);
            double base2 = GetBaseValue<T>(q2);
            var result = CreateFromBase<T>(base1 - base2, targetUnit);

            var record = new CacheRecordDto 
            { 
                OperationType = "Subtraction", 
                InputDetails = $"{q1.Value} {q1.Unit} - {q2.Value} {q2.Unit}", 
                Result = $"{result.Value} {result.Unit}" 
            };
            _cacheRepo.SaveToCache(record);
            dataRepo.SaveToDatabase(record,userId);
            return result;
        }

        public double Divide<T>(QuantityDTO q1, QuantityDTO q2,int? userId) where T : struct, Enum
        {
            if (typeof(T) == typeof(TemperatureUnit))
                throw new InvalidOperationException("Temperature does not support division.");

            double base1 = GetBaseValue<T>(q1);
            double base2 = GetBaseValue<T>(q2);

            if (Math.Abs(base2) < 1e-6) throw new DivideByZeroException("Cannot divide by zero.");
            
            double result = Math.Round(base1 / base2, 4);
            
            var record = new CacheRecordDto 
            { 
                OperationType = "Division", 
                InputDetails = $"{q1.Value} {q1.Unit} / {q2.Value} {q2.Unit}", 
                Result = result.ToString() 
            };
            _cacheRepo.SaveToCache(record);
            dataRepo.SaveToDatabase(record,userId);
            return result;
        }

        
        public bool Compare<T>(QuantityDTO q1, QuantityDTO q2,int? userId) where T : struct, Enum
        {
            bool equal = Math.Abs(GetBaseValue<T>(q1) - GetBaseValue<T>(q2)) < 1e-6;

            var record = new CacheRecordDto 
            { 
                OperationType = "Comparison",
                InputDetails = $"{q1.Value} {q1.Unit} == {q2.Value} {q2.Unit}", 
                Result = equal.ToString() 
            };

            _cacheRepo.SaveToCache(record);
            dataRepo.SaveToDatabase(record,userId);
            return equal;
        }

        public IEnumerable<CacheRecordDto> GetHistory(bool fromDatabase = true)
        {
            if (fromDatabase)
            {
                return dataRepo.GetAllFromDatabase();
            }
            return _cacheRepo.GetAllHistory();
        }

        public IEnumerable<CacheRecordDto> GetHistoryById(int userId)
        {
            return dataRepo.GetHistoryByUserId(userId);
        }

        public int GetOperationCount(int userId)
        {
            return dataRepo.GetOperationCount(userId);
        }

        public IEnumerable<CacheRecordDto> GetHistoryByOperation(string Operation,int userId)
        {
            return dataRepo.GetHistoyByOperation(Operation,userId);
        }
    }
}