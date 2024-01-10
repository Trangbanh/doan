using Fruit.Base.Models;

namespace FruitKha.Models
{
	public class HomeModel
	{
		public List<Product> ListOurProduct { get; set; }
		public List<ProductType> ListFeaturedCate { get; set; }
		public List<Blog> ListBlogs { get; set; }
	}
}
