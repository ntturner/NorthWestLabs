using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.DAL
{
    public class NorthWestContext : DbContext
    {
        public NorthWestContext()
            : base("NorthWestContext")
        {
            //USE THIS IF YOU NEED TO CHANGE A PASSWORD Database.Connection.ConnectionString = Database.Connection.ConnectionString.Replace("Gocougars1", "TurkeyDay1");
            //this.Database.Connection.ConnectionString = @"Data Source=tcp:byuis403.database.windows.net,1433;Initial Catalog=Registration;Persist Security Info=False;User ID=byuis403;Password=TurkeyDay1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        }

        public DbSet<Assay>  Assays{ get; set; }
        public DbSet<Assay_Test> Assay_Tests { get; set; }
        public DbSet<Compound> Compounds { get; set; }
        public DbSet<Compound_Assay> Compound_Assays { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Materials> Materials { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<Test_Materials> Test_Materials { get; set; }
        public DbSet<WorkOrder> WorkOrders { get; set; }


    }
}