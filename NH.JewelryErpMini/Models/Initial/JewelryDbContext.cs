using NH.JewelryErpMini.Models.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace NH.JewelryErpMini.Models.Initial
{
    public class JewelryDbContext:DbContext
    {
        public JewelryDbContext() : base("Default") { }

        public IDbSet<BusinessMessage> BusinessMessages { get; set; }
        public IDbSet<BusinessProgress> BusinessProgresses { get; set; }
        public IDbSet<BusinessDartProgress> BusinessDartProgresses { get; set; }
        public IDbSet<BusinessPartMasterFiles> BusinessPartMasterFileses { get; set; }
        public IDbSet<DeptWarehouse> DeptWarehousese { get; set; }
        public IDbSet<Dept> Depts { get; set; }
        public IDbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}