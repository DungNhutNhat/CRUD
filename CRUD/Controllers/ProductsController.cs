using CRUD.Data;
using CRUD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUD.Controllers
{
	public class ProductsController : Controller
	{
        private readonly applicationDbContext dbContext;

        public ProductsController(applicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        
        [HttpGet]
		public IActionResult Add()
		{
			return View();
		}
		[HttpPost]
        public async Task<IActionResult> Add(AddProductViewModel viewModel)
        {
            var product = new Product
            {
                Name = viewModel.Name,
                Category = viewModel.Category,
                Price = viewModel.Price,
            };
            await dbContext.Products.AddAsync(product);
            await dbContext.SaveChangesAsync();

            return View();
        }
        //lấy ra ds sản phẩm
        [HttpGet]
        public async Task<IActionResult> list()
        {
            var products = await dbContext.Products.ToListAsync();
            return View(products);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
           var product = await dbContext.Products.FindAsync(id);
            return View(product);   
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Product viewModel)
        {
           var product = await dbContext.Products.FindAsync(viewModel.Id);
            if(product is not null)
            {
                product.Name = viewModel.Name;
                product.Category = viewModel.Category;
                product.Price = viewModel.Price;
                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List","Products");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Product viewModel)
        {
            var product = await dbContext.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(x =>x.Id == viewModel.Id);
            if(product is not null)
            {
                dbContext.Products.Remove(viewModel);
                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Products");
        }

    }
}
