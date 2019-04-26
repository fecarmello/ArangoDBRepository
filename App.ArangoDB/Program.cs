using System;

namespace App.ArangoDB
{
    using App.ArangoDB.Context;
    using App.ArangoDB.Repository;
    using global::ArangoDB.Client;
    using System.Collections.Generic;

    public class Program
    {
        public static void Main(string[] args)
        {
            var connectionString = "http://localhost:8529#root#1234";

            var context = new ProductContext(connectionString);
            var repository = new ProductRepository(context);

            var qtd = 1000000;

            TimerFunc(() =>
                {
                    for (int i = 0; i < qtd; i++)
                    {
                        var productFor = new Product
                        {
                            Description = $"Product {i}",
                            Id = Guid.NewGuid(),
                            Value = (decimal)100.00,
                        };

                        repository.InsertAsync(productFor).Wait();
                    }

                    return true;
                }, $"Insert {qtd} new products => For");

            TimerFunc(() =>
            {
                var list = new List<Product>();

                for (int i = 0; i < qtd; i++)
                {
                    var productFor = new Product
                    {
                        Description = $"Product {i}",
                        Id = Guid.NewGuid(),
                        Value = (decimal)100.00,
                    };

                    list.Add(productFor);
                }

                repository.InsertMultipleAsync(list).Wait();

                return true;
            }, $"Insert {qtd} new products => List");

            TimerFunc(() =>
            {
                var listProd = repository.GetAllAsync().Result;

                return true;
            }, "List products");

            TimerFunc(() =>
            {
                var produto = repository.Get(x => x.Description == "Product 12345").Result;

                return true;
            }, "Read product query");

            Console.ReadLine();
        }

        private static void TimerFunc(Func<bool> func, string message)
        {
            Console.WriteLine($"Start => {DateTime.Now} => {message}");
            func();
            Console.WriteLine($"Finish => {DateTime.Now} => {message}");
        }
    }
}