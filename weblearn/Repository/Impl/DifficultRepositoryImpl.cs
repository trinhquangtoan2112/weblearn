using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using weblearn.Data;
using weblearn.Models.Domain;

namespace weblearn.Repository.Impl
{
    public class DifficultRepositoryImpl : IDifficultRepository
    {
        private readonly TestCDbContext _dbContext;
        public DifficultRepositoryImpl(TestCDbContext testCDbContext)
        {
            _dbContext = testCDbContext;
        }
        public async Task<List<Difficulty>> getAll()
        {
            List<Difficulty> region =await _dbContext.Difficulties.ToListAsync();
            return region;
        }
    }
}
