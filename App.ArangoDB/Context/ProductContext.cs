namespace App.ArangoDB.Context
{
    public class ProductContext : ContextBase
    {
        public ProductContext(string connectionString) : base(connectionString)
        {
        }

        public override string CollectionName => "Produto";

        public override string DatabaseName => "abbc";
    }
}