namespace piHome.DataAccess.Implementation
{
    public abstract class BaseDalHelper
    {
        protected readonly IDbContext _dbContext;

        protected BaseDalHelper(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}