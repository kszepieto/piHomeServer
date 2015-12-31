namespace piHome.DataAccess.Implementation
{
    public abstract class BaseRepository
    {
        protected readonly IDbContext _dbContext;

        protected BaseRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}