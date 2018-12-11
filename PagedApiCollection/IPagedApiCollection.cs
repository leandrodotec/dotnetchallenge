// DO NOT MODIFY THIS FILE

namespace PagedApiCollection
{
    public interface IPagedApiCollection
	{
		IEnumerableDisposable<TItem> GetItems<TItem>();
	}

    public class PagedApiCollection : IPagedApiCollection
    {
        public IEnumerableDisposable<TItem> GetItems<TItem>()
        {
            throw new System.NotImplementedException();
        }
    }
}