using Microsoft.EntityFrameworkCore;
using UtilBmi.DataBase;
using UtilBmi.Models;

namespace UtilBmi
{
    public class DatabaseBmi
    {
        private readonly DbContextBmi _context;
        public DatabaseBmi(DbContextBmi context)
        {
            _context = context;
        }
        public async Task SaveBmiResultAsync(BmiModel bmiResult)
        {
            _context.bmiModels.Add(bmiResult);
            await _context.SaveChangesAsync();
        }

        public async Task<List<BmiModel>> GetNormalBmiAsync()
        {
            return await _context.bmiModels.Where(b => b.Bmi > 18.5 && b.Bmi < 24.9).ToListAsync();
        }
        public async Task<List<BmiModel>> GetInsufficientBmiAsync()
        {
            return await _context.bmiModels.Where(b => b.Bmi < 18.5 ).ToListAsync();
        }
        public async Task<List<BmiModel>> GetRedundantBmiAsync()
        {
            return await _context.bmiModels.Where(b => b.Bmi < 24.9).ToListAsync();
        }
        public async Task <BmiModel?> GetHighestAsunc()
        {
            return await _context.bmiModels.OrderByDescending(r => r.Height).FirstOrDefaultAsync();
        }
        public async Task<BmiModel?> GetHeaviestAsunc()
        {
            return await _context.bmiModels.OrderByDescending(r => r.Weight).FirstOrDefaultAsync();
        }
        public async Task<int> GetCountUsersAsunc()
        {
           return await _context.bmiModels.CountAsync();
        }
    }
}
