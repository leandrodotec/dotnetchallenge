﻿using PagedApi;
using System;
using System.Collections;
using System.Collections.Generic;

namespace PagedApiCollection
{
    public class PagedApiCollectionProxyBaseEnum<TItem> : IEnumerator, IDisposable
    {
        private IPagedApi _pagedApi;
        private int? _requestId;
        private IEnumerator<TItem> _itemsEnumerator;
        private Page _currentPage;

        public PagedApiCollectionProxyBaseEnum(IPagedApi pagedApi)
        {
            _pagedApi = pagedApi;
            BeginRequest();
        }

        public bool MoveNext()
        {
            if(_itemsEnumerator == null)
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
            _pagedApi?.EndPagesRequest(_requestId.Value);
            BeginRequest();
        }

        public TItem Current
        {
            get
            {
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
            _pagedApi?.EndPagesRequest(_requestId.Value);
        }

        private  void BeginRequest()
        {
            Type type = typeof(TItem);
            if (type is Foo)
            {
                BeginRequest(ItemTypeId.Foo);
            }

            if (type is Bar)
            {
                BeginRequest(ItemTypeId.Bar);
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
                    _itemsEnumerator = (IEnumerator<TItem>) _currentPage.Items.GetEnumerator();
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