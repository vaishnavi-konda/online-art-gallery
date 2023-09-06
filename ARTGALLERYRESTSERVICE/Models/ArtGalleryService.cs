using ARTGALLERYRESTSERVICE.Models.Db;

namespace ARTGALLERYRESTSERVICE.Models
{
    public class ArtGalleryService
    {
        ArtGalleryContext context;
        public ArtGalleryService(ArtGalleryContext context) //dependency will be injected here
        {
            this.context = context;
        }

        public List<User> GetAllUsers()
        {
            List<User> users = context.Users.ToList();
            return users;
        }

        public User GetUser(int id)
        {
            var user = context.Users.SingleOrDefault(c => c.UserId == id);
            return user;
        }

        public int AddUser(User u)
        {
            context.Users.Add(u);
            int entrieswritten = context.SaveChanges();
            return entrieswritten;
        }

        public bool DeleteUser(int id)
        {
            var user = context.Users.SingleOrDefault(c => c.UserId == id);
            if (user != null)
            {
                context.Users.Remove(user);
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdateUserAddress(int Userid , string address)
        {
            var user = context.Users.SingleOrDefault(c => c.UserId == Userid);
            if (user != null)
            {
                //user.UserName = u.UserName;
                user.UserAddress = address;
                context.SaveChanges();
                return true;
            }
            else return false;
        }

        public int AddCategory(Category c)
        {
            context.Categories.Add(c);
            int entrieswritten = context.SaveChanges();
            return entrieswritten;
        }

        public List<Product> GetAllProducts()
        {
            List<Product> prods = context.Products.ToList();
            return prods;
        }

        public List<ProductsAll> GetProducts()
        {
            var result = (from p in context.Products
                                       join c in context.Categories on p.CategoryId equals c.CategoryId

                                       select new ProductsAll()
                                       {
                                           ProductName = p.ProductName!,
                                           Artist = p.Artist,
                                           CategoryName = c.CatogoryName!,
                                           Price = p.Price,
                                           ImageName = p.ImageName


                                       }).ToList();
                                       return result;
         }

        public Product GetProductById(int id)
        {
            Product product = context.Products.SingleOrDefault(p => p.ProductId == id);
            return product;
        }

        public List<Product> GetProductsByIds(List<int> Ids)
        {
            List<Product> prods=new List<Product>();
            foreach(int id in Ids)
            {
                prods.Add(context.Products.SingleOrDefault(p => p.ProductId == id));
            }
            return prods.ToList();
        }

        public Product GetProduct(string name)
        {
            var prod = context.Products.SingleOrDefault(c => c.ProductName == name);
            return prod;
        }

        public int AddProduct(Product p)
        {
            context.Products.Add(p);
            int entrieswritten = context.SaveChanges();
            return entrieswritten;
        }

        public bool DeleteProduct(int id)
        {
            var prod = context.Products.SingleOrDefault(c => c.ProductId == id);
            if (prod != null)
            {
                context.Products.Remove(prod);
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        

        public List<Product_Category> GetProductsByCategory(string Category)
        {
               var result = (from p in context.Products
                            join c in context.Categories on p.CategoryId equals c.CategoryId
                            where c.CatogoryName == Category
                            select new Product_Category(){ ProductName = p.ProductName, CategoryName = c.CatogoryName, Price = p.Price }).ToList();
            return result;
        }

        public List<CompleteOrderDetails> GetCompleteOrderDetails()
        {
            var result = (from order in context.Orders
                          join orderDetail in context.OrderDetails on order.OrderId equals orderDetail.OrderId
                          join product in context.Products on orderDetail.ProductId equals product.ProductId
                          join user in context.Users on order.UserId equals user.UserId
                          join category in context.Categories on product.CategoryId equals category.CategoryId
                          select new CompleteOrderDetails()
                          {
                              OrderId = order.OrderId,
                              OrderDate = order.OrderDate,
                              UserName = user.UserName,
                              ProductName = product.ProductName,
                              CategoryName = category.CatogoryName,
                              Quantity = orderDetail.Quantity,
                              Amount = orderDetail.Amount,
                              PaymentStatus = orderDetail.PaymentStatus
                          }).ToList();
            return result;
        }

        public int AddOrder(Order  o, OrderDetail od)
        {
            context.Orders.Add(o);
            context.OrderDetails.Add(od);
            int entrieswritten = context.SaveChanges();
            return entrieswritten;
        }

        public bool DeleteOrder(int oid)
        {
            var order = context.Orders.SingleOrDefault(c => c.OrderId == oid);
            var orderdetails = context.OrderDetails.SingleOrDefault(c => c.OrderId == oid);
            if (order != null)
            {
                context.Orders.Remove(order);
                context.OrderDetails.Remove(orderdetails);
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }


    }
    public class Product_Category
    {
        public string ProductName { get; set;}
        public string CategoryName { get; set;}
        public decimal? Price { get; set;}
    }

    public class CompleteOrderDetails
    {
        public int OrderId { get; set; }
        public DateTime? OrderDate { get; set; }
        public string UserName { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
       
        public int? Quantity { get; set; }
        public decimal? Amount { get; set; }
        public string PaymentStatus { get; set; }
    }

    public class ProductsAll
    {
        

        public string ProductName { get; set; } 

        public string? Artist { get; set; }




        public decimal Price { get; set; }

        public string? ProductDescription { get; set; }

        public string? ImageName { get; set; }

        public string CategoryName { get; set; }
    }
}




/*var query = from order in orders
            join orderDetail in orderDetails on order.OrderId equals orderDetail.OrderId
            join product in products on orderDetail.ProductId equals product.ProductId
            select new
            {
                OrderId = order.OrderId,
                OrderDate = order.OrderDate,
                ProductName = product.ProductName,
                Quantity = orderDetail.Quantity,
                Amount = orderDetail.Amount,
                PaymentStatus = orderDetail.PaymentStatus
            };*/

/*var query = from order in orders
            join orderDetail in orderDetails on order.OrderId equals orderDetail.OrderId
            join product in products on orderDetail.ProductId equals product.ProductId
            join user in users on order.UserId equals user.UserId
            join category in categories on product.CategoryId equals category.CategoryId
            select new
            {
                OrderId = order.OrderId,
                OrderDate = order.OrderDate,
                UserName = user.UserName,
                ProductName = product.ProductName,
                CategoryName = category.CategoryName,
                Quantity = orderDetail.Quantity,
                Amount = orderDetail.Amount,
                PaymentStatus = orderDetail.PaymentStatus
            };*/

/*var query = from order in orders
            join orderDetail in orderDetails on order.OrderId equals orderDetail.OrderId
            join product in products on orderDetail.ProductId equals product.ProductId
            join user in users on order.UserId equals user.UserId
            select new
            {
                OrderId = order.OrderId,
                OrderDate = order.OrderDate,
                UserName = user.UserName,
                ProductName = product.ProductName,
                Quantity = orderDetail.Quantity,
                Amount = orderDetail.Amount,
                PaymentStatus = orderDetail.PaymentStatus
            };*/

