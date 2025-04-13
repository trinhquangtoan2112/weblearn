using weblearn.Models.Domain;

namespace weblearn.Repository
{
    public interface IDifficultRepository
    {
        Task<List<Difficulty>> getAll();
    }
}
