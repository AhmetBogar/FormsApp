namespace FormsApp.Models
{
	public class Repository
	{
		private static readonly List<Product> _products = new();
		private static readonly List<Category> _categories = new();
		public static List<Product> Products
		{
			get
			{
				return _products;
			}
		}

		static Repository()
		{
			_categories.Add(new Category { CategoryId=1,Name="Bilgisayar"});
			_categories.Add(new Category { CategoryId=2,Name="Telefon"});

			_products.Add(new Product { ProductId=1,Name="Iphone 14",Price=40000,IsActive=true,Image="1.jpg",CategoryId=2});
			_products.Add(new Product { ProductId=2,Name="Iphone 15",Price=50000,IsActive=true,Image="2.jpg",CategoryId=2});
			_products.Add(new Product { ProductId=3,Name="Iphone 16",Price=60000,IsActive=true,Image="3.jpg",CategoryId=2});
			_products.Add(new Product { ProductId=4,Name="Iphone 17",Price=70000,IsActive=true,Image="4.jpg",CategoryId=2});

			_products.Add(new Product { ProductId=5,Name="Macbook 14",Price=80000,IsActive=true,Image="5.jpg",CategoryId=1});
			_products.Add(new Product { ProductId=6,Name= "Macbook 15", Price=95000,IsActive=true,Image="6.jpg",CategoryId=1});

		}

		public static void CreateProduct(Product entity)
		{
			_products.Add(entity);
		}

		public static List<Category> Categories
		{
			get
			{
				return _categories;
			}
		}

	}
}
