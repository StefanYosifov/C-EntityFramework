namespace ProductShop
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using ProductShop.Data;
    using ProductShop.DTOs.Export;
    using ProductShop.DTOs.Import;
    using ProductShop.Models;

    public class StartUp
    {
        public static void Main()
        {

            using var context = new ProductShopContext();
            //string jsonUserFile = File.ReadAllText("../../../Datasets/users.json");

            //string usersImported = ImportUsers(context, jsonUserFile);
            //Console.WriteLine(usersImported);

            //string jsonProductFile= File.ReadAllText("../../../Datasets/products.json");

            //string productsImported = ImportProducts(context, jsonProductFile);
            //Console.WriteLine(productsImported);

            //string jsonCategoriesFile = File.ReadAllText("../../../Datasets/categories.json");
            //string categories = ImportCategories(context, jsonCategoriesFile);
            //Console.WriteLine(categories);

            //string jsonCategoryProducts = File.ReadAllText("../../../Datasets/categories-products.json");
            //string categoryProducts=ImportCategoryProducts(context, jsonCategoryProducts);
            //Console.WriteLine(categoryProducts);

            Console.WriteLine(GetSoldProducts(context));
            Console.WriteLine(GetCategoriesByProductsCount(context));

        }

        public static string GetUsersWithProducts(ProductShopContext context)
        {

            var getAllUsersWithProduct = context.Users
                .Where(u=>u.ProductsSold.Any(p=>p.Buyer!=null))
                .Select(u => new
                {
                    u.FirstName,
                    u.LastName,
                    u.Age,
                    SoldProducts = new
                    {
                        count = u.ProductsSold.Count,
                        products = u.ProductsSold.Where(p => p.Buyer != null)
                        .Select(p => new
                        {
                            p.Name,
                            p.Price
                        }).ToArray()

                    }
                }).OrderBy(u => u.SoldProducts.count)
                .AsNoTracking()
                .ToArray();
                


        }

        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            var categories = context.Categories
                .OrderBy(cp => cp.CategoriesProducts.Count)
                .Select(c => new
                {
                    category = c.Name,
                    productCount = c.CategoriesProducts.Count,
                    averagePrice = (c.CategoriesProducts.Any() ? c.CategoriesProducts.Average(cp => cp.Product.Price): 0).ToString("f2"),
                    totalRevenue = c.CategoriesProducts.Sum(cp => cp.Product.Price).ToString("f2")
                })
                .AsNoTracking()
                .ToArray();

             
            return JsonConvert.SerializeObject(categories,Formatting.Indented);
        }

        public static string GetSoldProducts(ProductShopContext context)
        {

            var users = context.Users
                .Where(u => u.ProductsSold.Count > 0)
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .Select(u => new
                {
                    firstName = u.FirstName,
                    lastName = u.LastName,
                    soldProducts = u.ProductsSold.Select(ps => new
                    {
                        name = ps.Name,
                        price = ps.Price,
                        buyerFirstName = ps.Buyer.FirstName,
                        buyerLastName = ps.Buyer.LastName
                    })
                    .ToArray()
                })
                .AsNoTracking()
                .ToArray();

            return JsonConvert.SerializeObject(users,Formatting.Indented);
        }


        public static string GetProductsInRange(ProductShopContext context)
        {
            IMapper mapper = InitializeMapper();

            var products = context.Products
                .Where(p => p.Price >= 500 && p.Price <= 1000)
                .OrderBy(p => p.Price)
                .AsNoTracking()
                .ProjectTo<ExportProductInRangeDto>(mapper.ConfigurationProvider)
                .ToArray();


            return JsonConvert.SerializeObject(products,Formatting.Indented);
        }


        public static string ImportCategoryProducts(ProductShopContext context, string inputJson)
        {

            var deserializedCategoryProduct=JsonConvert.DeserializeObject<IEnumerable<ImportCategoryProductsDto>>(inputJson);
            IMapper mapper=InitializeMapper();
            ICollection<CategoryProduct> categoryProducts = new HashSet<CategoryProduct>();

            foreach(var categoryP in deserializedCategoryProduct)
            {
                //if (!context.Categories.Any(c => c.Id == categoryP.CategoryId ||
                //    !context.Products.Any(p => p.Id == categoryP.CategoryId)))
                //{
                //    continue;
                //}

                CategoryProduct newCategoryProduct = mapper.Map<CategoryProduct>(categoryP);
                categoryProducts.Add(newCategoryProduct);
            }

            context.CategoriesProducts.AddRange(categoryProducts);
            context.SaveChanges();

            return $"Successfully imported {categoryProducts.Count}";

        }

        public static string ImportCategories(ProductShopContext context, string inputJson)
        {
            var deserializedCategories= JsonConvert.DeserializeObject<IEnumerable<ImportCategoriesDto>>(inputJson);
            IMapper mapper = InitializeMapper();
            ICollection<Category> categories = new HashSet<Category>();
            foreach(var category in deserializedCategories)
            {
                if (string.IsNullOrEmpty(category.Name))
                {
                    continue;
                }
                Category newCategory=mapper.Map<Category>(category);
                categories.Add(newCategory);
            }


            context.Categories.AddRange(categories);
            context.SaveChanges();
            return $"Successfully imported {categories.Count()}";
        }

        public static string ImportProducts(ProductShopContext context, string inputJson)
        {
            var deserializedProducts=JsonConvert.DeserializeObject<IEnumerable<ImportProductDto>>(inputJson);
            IMapper mapper = InitializeMapper();
            var products=mapper.Map<IEnumerable<Product>>(deserializedProducts);

            return $"Successfully imported {products.Count()}";
        }

        public static string ImportUsers(ProductShopContext context, string inputJson)
        {
            var deserializedUsers = JsonConvert.DeserializeObject<IEnumerable<ImportUser>>(inputJson);
            IMapper mapper = InitializeMapper();

            var users = mapper.Map<IEnumerable<User>>(deserializedUsers);

            context.AddRange(users);
            context.SaveChanges();

            return $"Successfully imported {users.Count()}";
        }


        private static IMapper InitializeMapper()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ProductShopProfile>();
            });

            IMapper mapper = new Mapper(mapperConfig);
            return mapper;
        }
    }
}