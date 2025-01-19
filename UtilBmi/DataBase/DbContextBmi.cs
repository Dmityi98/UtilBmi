using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilBmi.Models;

namespace UtilBmi.DataBase
{
    public class DbContextBmi : DbContext
    {
        public DbContextBmi(DbContextOptions<DbContextBmi> options) : base(options) { }
        public DbSet<BmiModel> bmiModels { get; set; }
    }
}
