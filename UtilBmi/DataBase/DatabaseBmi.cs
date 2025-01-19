using Microsoft.EntityFrameworkCore;
using UtilBmi.Models;

namespace UtilBmi.DataBase
{
    public class DatabaseBmi
    {
        private readonly DbContextBmi _context;
        public DatabaseBmi(DbContextBmi context)
        {
            _context = context;
        }
        public void SaveBmiResultAsync(BmiModel bmiResult)
        {
            _context.bmiModels.Add(bmiResult);
            _context.SaveChangesAsync();
        }
        public int GetCount()
        {
            return _context.bmiModels.Count();
        }
    }
}
