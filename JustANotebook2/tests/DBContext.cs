using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace tests
{
    class DBContext : DbContext
    {
        public DBContext() : base("conStr")
        {
            
        }
        public DbSet<Model.Note> Notes { get; set; }


    }
}
