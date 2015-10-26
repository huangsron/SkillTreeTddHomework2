using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PotterShoppingCart.Tests
{
    [TestClass]
    public class PotterShoppingCartTest
    {
        private static List<Book> _book;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            _book = new List<Book>
            {
                new Book {Id = 1, Name = "第一集", Price = 100},
                new Book {Id = 2, Name = "第二集", Price = 100},
                new Book {Id = 3, Name = "第三集", Price = 100},
                new Book {Id = 4, Name = "第四集", Price = 100},
                new Book {Id = 5, Name = "第五集", Price = 100}
            };
        }

        [TestMethod]
        public void Test_buy_1_book_return_amount_100()
        {
            var order = new List<Order>
            {
                new Order {Id = 1, Quantity = 1}
            };
            var target = new ShoppingCart(_book);

            var expected = 100;
            var actual = target.Calculture(order);

            Assert.AreEqual(expected, actual);
        }
    }

    public class Order
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
    }

    public class Book
    {
        public int Id { get; internal set; }
        public string Name { get; set; }
        public int Price { get; set; }
    }

    public class ShoppingCart
    {
        public ShoppingCart(IEnumerable<Book> book)
        {
            throw new NotImplementedException();
        }

        public int Calculture(IEnumerable<Order> order)
        {
            throw new NotImplementedException();
        }
    }
}