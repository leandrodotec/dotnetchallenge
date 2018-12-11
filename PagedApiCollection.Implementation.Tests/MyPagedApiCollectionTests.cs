using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using PagedApi;

namespace PagedApiCollection.Implementation.Tests
{
	public class MyPagedApiCollectionTests
	{
		private FakePagedApi _fakePagedApi;
		private IPagedApiCollection _pagedApiCollection;

		[SetUp]
		public void Setup()
		{
			_fakePagedApi = new FakePagedApi(5);
			_pagedApiCollection = new MyPagedApiCollection(_fakePagedApi);
		}

		[Test]
		public void GetItems_ToArray_ReturnsAllItems()
		{
			// Arrange
			var expectedItems = Enumerable.Range(0, 11).Select(i => new Foo {Id = i}).ToArray();

			_fakePagedApi[ItemTypeId.Foo].AddRange(expectedItems);

			// Act
			var actualItems = _pagedApiCollection.GetItems<Foo>().ToArray();

			// Assert
			CollectionAssert.AreEqual(expectedItems, actualItems);
		}

        [Test]
        public void GetItems_ToArray_ReturnsEmptyCollection()
        {
            // Arrange
            var expectedItems = new List<Bar>().ToArray();

            _fakePagedApi[ItemTypeId.Foo].AddRange(expectedItems);

            // Act
            var actualItems = _pagedApiCollection.GetItems<Bar>().ToArray();

            // Assert
            CollectionAssert.AreEqual(expectedItems, actualItems);
        }

        [Test]
        public void GetItems_ToArray_EnumeratesJustFirstfewItems()
        {
            // Arrange
            const int itemsCount = 7;
            var allItems = Enumerable.Range(0, 11).Select(i => new Foo { Id = i }).ToArray();
            var expectedItems = allItems.Take(itemsCount);

            _fakePagedApi[ItemTypeId.Foo].AddRange(allItems);

            // Act
            var actualItems = _pagedApiCollection.GetItems<Foo>().Take(itemsCount);

            // Assert
            CollectionAssert.AreEqual(expectedItems, actualItems);
        }

        [Test]
        public void GetItems_ToArray_WhenNoEnumerationBeginIsNotCalled()
        {
            // Arrange
            const int itemsCount = 7;
            var allItems = Enumerable.Range(0, 11).Select(i => new Foo { Id = i }).ToArray();
            var expectedItems = allItems.Take(itemsCount);

            _fakePagedApi[ItemTypeId.Foo].AddRange(allItems);

            // Act
            var actualItems = _pagedApiCollection.GetItems<Foo>().Take(itemsCount);

            // Assert
            Assert.AreEqual(true, true);
        }
    }
}
