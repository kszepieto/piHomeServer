namespace piHome.DataAccess.Interfaces
{
    public interface IBaseRepository
    {
        void Insert<T>(T item);
        void Update<T>(T item);
    }
}
