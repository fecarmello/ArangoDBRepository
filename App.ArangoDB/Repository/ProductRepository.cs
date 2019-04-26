using App.ArangoDB.Context;

namespace App.ArangoDB.Repository
{
    public class ProductRepository : RepositoryBase<Product>
    {
        public ProductRepository(ContextBase contextBase) : base(contextBase)
        {
        }
    }
}