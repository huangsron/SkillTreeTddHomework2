using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

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

        //Scenario: 第一集買了一本，其他都沒買，價格應為100*1=100元
        [TestMethod]
        public void Test_buy_1_book_return_amount_100()
        {
            var orders = new List<Order>
            {
                new Order {Id = 1, Quantity = 1,Price = 100},
                new Order {Id = 2, Quantity = 0,Price = 100},
                new Order {Id = 3, Quantity = 0,Price = 100},
                new Order {Id = 4, Quantity = 0,Price = 100},
                new Order {Id = 5, Quantity = 0,Price = 100},
            };

            var target = new ShoppingCart(_book);

            var expected = 100;

            var actual = target.GetAmount(orders);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_buy_2_diff_book_return_amount_190()
        {
            var orders = new List<Order>
            {
                new Order {Id = 1, Quantity = 1,Price = 100},
                new Order {Id = 2, Quantity = 1,Price = 100},
                new Order {Id = 3, Quantity = 0,Price = 100},
                new Order {Id = 4, Quantity = 0,Price = 100},
                new Order {Id = 5, Quantity = 0,Price = 100},
            };

            var target = new ShoppingCart(_book);

            var expected = 190;

            var actual = target.GetAmount(orders);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_buy_3_diff_book_return_amount_270()
        {
            var orders = new List<Order>
            {
                new Order {Id = 1, Quantity = 1,Price = 100},
                new Order {Id = 2, Quantity = 1,Price = 100},
                new Order {Id = 3, Quantity = 1,Price = 100},
                new Order {Id = 4, Quantity = 0,Price = 100},
                new Order {Id = 5, Quantity = 0,Price = 100},
            };

            var target = new ShoppingCart(_book);

            var expected = 270;

            var actual = target.GetAmount(orders);

            Assert.AreEqual(expected, actual);
        }
    }

    public class ShoppingCart
    {
        private readonly IEnumerable<Book> _book;

        public ShoppingCart(IEnumerable<Book> book)
        {
            _book = book;
        }

        public double GetAmount(IEnumerable<Order> order)
        {
            // keep
            var orders = order as IList<Order> ?? order.ToList();

            var count = 0;
            double amount = 0;

            while (orders.Any(e => e.Quantity > count))
            {
                var temp = orders.Where(e => e.Quantity > count).ToList();

                var discount = GetDiscount(temp);

                amount += temp.Sum(e => (e.Price * discount));

                count++;
            }

            return amount;
        }

        private double GetDiscount(IEnumerable<Order> @where)
        {
            var count = where.Count();
            switch (count)
            {
                case 1:
                    //當資料只有一筆不打折
                    return 1;

                case 2:
                    return 0.95;

                case 3:
                    return 0.9;

                default:
                    return 1;
            }
        }
    }

    public class Order
    {
        public int Id { get; set; }

        public int Quantity { get; set; }

        public int Price { get; set; }
    }

    public class Book
    {
        public int Id { get; internal set; }

        public string Name { get; set; }

        public int Price { get; set; }
    }
}