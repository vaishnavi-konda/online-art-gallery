using ARTGALLERYRESTSERVICE.Models;
using ARTGALLERYRESTSERVICE.Models.Db;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ARTGALLERYRESTSERVICE.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class ArtGalleryController : ControllerBase
    {
        public ArtGalleryService service;

        public ArtGalleryController(ArtGalleryService svc)
        {
            service = svc;
        }


        [Authorize(Policies.Admin)]
        [HttpGet]
        [Route("users")]   //http:localhost:port/api/ArtGallery/users;  
        public ObjectResult GetAllUsers()
        {
            var list = service.GetAllUsers();
            return Ok(list);
        }


        [Authorize(Policies.Admin)]
        [HttpGet]
        [Route("users/{id}")]

        public IActionResult GetUser(int id)
        {
            User u = service.GetUser(id);
            if (u == null)
                return NotFound();
            else
                return Ok(u);
        }

        
        [HttpPost]
        [Route("users/add")]

        public IActionResult PostUser(User u)
        {
            int result = service.AddUser(u);
            if (result == 1) return Ok();
            else
                return new StatusCodeResult(501);  //HttpStatusCode
        }

        [Authorize(Policies.Admin)]
        [HttpDelete]
        [Route("users/delete")]

        public IActionResult DeleteUser(int id)
        {
            bool result = service.DeleteUser(id);
            if (result == true)
                return Ok();
            else return NotFound();
        }


        [HttpPut]
        [Route("users/update")]

        public IActionResult UpdateEmp(int Userid,String address)
        {
            bool result = service.UpdateUserAddress(Userid,address);
            if (result == true) return Ok();
            else return NotFound();
        }

        [Authorize(Policies.Admin)]
        [HttpPost]
        [Route("category/add")]

        public IActionResult PostCategory(Category c)
        {
            int result = service.AddCategory(c);
            if (result == 1) return Ok();
            else
                return new StatusCodeResult(501);  //HttpStatusCode
        }

        
        [HttpGet]
        [Route("products/all")]   
        public ObjectResult GetAllProducts()
        {
            var list = service.GetAllProducts();
            return Ok(list);
        }



        //[Authorize]
        [HttpGet]
        [Route("products/{name}")]

        public IActionResult GetProduct(string name)
        {
            Product p = service.GetProduct(name);
            if (p == null)
                return NotFound();
            else
                return Ok(p);
        }

        
        [HttpGet]
        [Route("products/{id}")]
        public IActionResult GetProduct(int id)
        {
            var product = service.GetProductById(id);
            return Ok(product);
        }

        [HttpGet]
        [Route("products")]
        public IActionResult GetProducts()
        {
            List<ProductsAll> pc = service.GetProducts();
            if (pc == null)
                return NotFound();
            else
                return Ok(pc);
        }


        [HttpPost]
        [Route("getproductsbyIds")]
        public IActionResult GetProductsByIds(List<int> Ids)
        {
            List<Product> p = service.GetProductsByIds(Ids);
            if (p == null)
                return NotFound();
            else
                return Ok(p);
        }



        [HttpPost]
        [Route("products/add")]

        public IActionResult PostProduct(Product p)
        {
            int result = service.AddProduct(p);
            if (result == 1) return Ok();
            else
                return new StatusCodeResult(501);  //HttpStatusCode
        }

        [Authorize(Policies.Admin)]
        [HttpDelete]
        [Route("products/delete")]

        public IActionResult DeleteProduct(int id)
        {
            bool result = service.DeleteProduct(id);
            if (result == true)
                return Ok();
            else return NotFound();
        }


       
        [HttpGet]
        [Route("products/category/{category}")]



        public IActionResult GetProductsByCategory(string category)
        {
            List<Product_Category> pc= service.GetProductsByCategory(category);
            if (pc == null)
                return NotFound();
            else
                return Ok(pc);
        }

        [Authorize(Policies.Admin)]
        [HttpGet]
        [Route("completeorderdetails")]



        public IActionResult GetCompleteOrderDetails()
        {
            List<CompleteOrderDetails> orders = service.GetCompleteOrderDetails();
            if (orders == null)
                return NotFound();
            else
                return Ok(orders);
        }






    }
}
