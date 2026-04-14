using Microsoft.EntityFrameworkCore;
using QuantityMeasurementRepoLayer.Entities;

namespace QuantityMeasurementRepoLayer.Data;

public class MeasurementDbContext : DbContext
{
    public MeasurementDbContext(DbContextOptions<MeasurementDbContext> options) : base(options){}

    public DbSet<MeasurementHistory> MeasurementHistories{get; set;}
    public DbSet<User> Users { get; set; }
}