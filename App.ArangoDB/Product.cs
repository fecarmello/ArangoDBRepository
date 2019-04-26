using ArangoDB.Client;
using System;

namespace App.ArangoDB
{
    public class Product
    {
        [DocumentProperty(Identifier = IdentifierType.Key)]
        public string Key;

        public string Description { get; set; }
        public Guid Id { get; set; }
        public decimal Value { get; set; }
    }
}