﻿using System;
using PagedApi;

namespace PagedApiCollection.Implementation
{
	public class MyPagedApiCollection : IPagedApiCollection
	{
		private readonly IPagedApi _pagedApi;

		public MyPagedApiCollection(IPagedApi pagedApi)
		{
			_pagedApi = pagedApi;
		}

		public IEnumerableDisposable<TItem> GetItems<TItem>()
		{
		    var x = _pagedApi.BeginPagesRequest(ItemTypeId.Bar);
		}
	}
}
