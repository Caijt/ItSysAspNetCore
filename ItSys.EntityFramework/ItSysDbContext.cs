using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ItSys.Entity;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ItSys.EntityFramework
{
    public class ItSysDbContext : DbContext
    {
        public DbSet<SysUser> SysUsers { get; set; }
        public DbSet<ItAsset> ItAssets { get; set; }
        public DbSet<SysConfig> SysConfigs { get; set; }
        public DbSet<SysMenu> SysMenus { get; set; }
        public DbSet<SysRole> SysRoles { get; set; }
        public DbSet<SysUserLoginLog> SysUserLoginLogs { get; set; }
        public DbSet<SysUpdateRecord> SysUpdateRecords { get; set; }
        public DbSet<SysCompany> SysCompanys { get; set; }
        public DbSet<SysMail> SysMails { get; set; }
        public DbSet<SysMailTemplate> SysMailTempldates { get; set; }
        public DbSet<Attach> Attaches { get; set; }
        public DbSet<HrDep> HrDeps { get; set; }
        public DbSet<HrEmployee> HrEmployees { get; set; }
        public DbSet<ItSupplier> ItSuppliers { get; set; }
        public DbSet<ItAssetType> ItAssetTypes { get; set; }
        public DbSet<ItAssetStockWarning> ItAssetStockWarnings { get; set; }
        public DbSet<ItAccount> ItAccounts { get; set; }
        public DbSet<ItNetwork> ItNetworks { get; set; }
        public DbSet<ItContract> ItContracts { get; set; }
        public DbSet<ItContractView> ItContractsView { get; set; }
        public DbSet<ItContractPayRecord> ItContractPayRecords { get; set; }
        public DbSet<ItAssetUseRecord> ItAssetUseRecords { get; set; }
        public DbSet<ItAssetUseRecordItem> ItAssetUseRecordItems { get; set; }
        public DbSet<ItAssetRepairRecord> ItAssetRepairRecords { get; set; }
        public DbSet<ItAssetScrapRecord> ItAssetScrapRecords { get; set; }
        public DbSet<ItAssetUseStatusView> ItAssetUseStatusView { get; set; }
        public ItSysDbContext(DbContextOptions<ItSysDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseMySql("Server=localhost;Database=ItSys;User=root;Password=root;");
        //}
    }
}
