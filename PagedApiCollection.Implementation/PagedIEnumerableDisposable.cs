// DO NOT MODIFY THIS FILE
using PagedApi;
using System.Collections;
using System.Collections.Generic;

namespace PagedApiCollection.Implementation
{
    public class PagedIEnumerableDisposable<TItem> : IEnumerableDisposable<TItem>
    {
        private IPagedApi _pagedApi;
        private PagedApiCollectionProxyBaseEnum<TItem> _enumerator;
        public PagedIEnumerableDisposable(IPagedApi pagedApi)
        {
            _pagedApi = pagedApi;
        }

        public PagedApiCollectionProxyBaseEnum<TItem> GetEnumerator()  {
            _enumerator = new PagedApiCollectionProxyBaseEnum<TItem>(_pagedApi);
            return _enumerator;
        }
        public void Dispose()
        {
            _enumerator.Dispose();
        }

        IEnumerator IEnumerable.GetEnumerator() => (IEnumerator)GetEnumerator();
        IEnumerator<TItem> IEnumerable<TItem>.GetEnumerator()
            => (IEnumerator<TItem>)GetEnumerator();
    }
}