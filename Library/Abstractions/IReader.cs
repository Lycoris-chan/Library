using Library.Models;

namespace Library.Abstractions
{
    public interface IReader<T> where T : class
    {
        IEnumerable<T> ReadData();
    }
}
