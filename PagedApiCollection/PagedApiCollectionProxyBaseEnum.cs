using PagedApi;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PagedApiCollection
{
    public class PagedApiCollectionProxyBaseEnum<TItem> : IEnumerator<TItem>, IDisposable
    {
        private IPagedApi _pagedApi;
        private int? _requestId;
        private IEnumerator<TItem> _itemsEnumerator;
        private Page _currentPage;
        private bool _disposing;

        public PagedApiCollectionProxyBaseEnum(IPagedApi pagedApi)
        {
            _pagedApi = pagedApi;
            _disposing = false;
        }

        public bool MoveNext()
        {
            if (_disposing)
                return false;

            if(_requestId == null)
                BeginRequest();

            if (_itemsEnumerator == null)
            {
                return MoveNextEnumerator();
            }
            else
            {
                return _itemsEnumerator.MoveNext() ? true : MoveNextEnumerator();
            }
        }

        public void Reset()
        {
            if (_requestId.HasValue)
            {
                _pagedApi?.EndPagesRequest(_requestId.Value);
                _requestId = null;
            }
        }

        public TItem Current
        {
            get
            {
                if (_disposing)
                    throw new ObjectDisposedException(nameof(PagedApiCollectionProxyBaseEnum<TItem>));
                try
                {
                    return _itemsEnumerator.Current;
                }
                catch (Exception)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        public void Dispose()
        {
            Reset();
            _disposing = true;
        }

        private  void BeginRequest()
        {
            Type type = typeof(TItem);
            if (type.Equals(typeof(Foo)))
            {
                BeginRequest(ItemTypeId.Foo);
                return;
            }

            if (type.Equals(typeof(Bar)))
            {
                BeginRequest(ItemTypeId.Bar);
                return;
            }

            throw new InvalidOperationException("Only compatible with Foo and Bar.");
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        private bool MoveNextEnumerator()
        {
            if (_requestId.HasValue)
            {
                if (_currentPage == null || _currentPage.HasNextPage)
                {
                    _currentPage = _pagedApi.GetNextPage(_requestId.Value);
                    var mappedItems = _currentPage.Items.Select(i => (TItem)i).ToList();
                    _itemsEnumerator = mappedItems.GetEnumerator();
                    return (_itemsEnumerator?.MoveNext() ?? false);
                }
                else
                {
                    _itemsEnumerator = null;
                    return false;
                }
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        private void BeginRequest(ItemTypeId itemType)
        {
            _requestId = _pagedApi.BeginPagesRequest(itemType);
            _itemsEnumerator = null;
            _currentPage = null;
        }
    }
}