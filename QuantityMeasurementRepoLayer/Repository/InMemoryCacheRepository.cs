using System.Collections.Generic;
using QuantityMeasurementRepoLayer.Interfaces;
using System.Text.Json;
using QuantityMeasurementModelLayer.DTOs;
using System.Runtime.CompilerServices;
using System.Net;

namespace QuantityMeasurementRepoLayer.Repositories
{
    public class InMemoryCacheRepository : ICacheRepository
    {
        private readonly string filepath = "F:\\QuantityMesurementApp\\QuantityMeasurementRepoLayer\\cachefile\\measurementCache.json";
        private readonly List<CacheRecordDto> _cache;

        public InMemoryCacheRepository()
        {
            if (File.Exists(filepath))
            {
                string json = File.ReadAllText(filepath);

                if (string.IsNullOrWhiteSpace(json))
                {
                    _cache = new List<CacheRecordDto>();
                }
                else
                {
                    _cache = JsonSerializer.Deserialize<List<CacheRecordDto>>(json) ?? new List<CacheRecordDto>();
                }
            }
            else
            {
                _cache = new List<CacheRecordDto>();
            }
        }

        public void SaveToFile()
        {
            var json = new JsonSerializerOptions{WriteIndented = true};
            string jsonString = JsonSerializer.Serialize(_cache,json);

            File.WriteAllText(filepath,jsonString);
        }

        public void SaveToCache(CacheRecordDto record)
        {
            _cache.Add(record);
            SaveToFile();

        }

        public IEnumerable<CacheRecordDto> GetAllHistory()
        {
            return _cache;
        }

        public void ClearCache()
        {
            _cache.Clear();
        }
    }
}