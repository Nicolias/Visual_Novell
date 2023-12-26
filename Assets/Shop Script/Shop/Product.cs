namespace Shop
{
    public class Product
    {
        public Product(ProductSO data)
        {
            Data = data;
        }

        public ProductSO Data { get; private set; }
    }
}