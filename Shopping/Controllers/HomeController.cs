using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shopping.Controllers.Data;
using Shopping.Controllers.Data.Entities;
using Shopping.Helpers;
using Shopping.Models;
using System.Diagnostics;

namespace Shopping.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataContext _Context;
        private readonly IUserHelper _UserHelper;

        public HomeController(ILogger<HomeController> logger, DataContext context, IUserHelper userHelper)
        {
            _logger = logger;
            _Context = context;
            _UserHelper = userHelper;
        }

        public async Task<IActionResult> Index()

        {

            List<Product>? products = await _Context.Products

                .Include(p => p.ProductImages)

                .Include(p => p.ProductCategories)

                .OrderBy(p => p.Description)

                .ToListAsync();

            List<ProductHomeViewModel> productsHome = new() { new ProductHomeViewModel() };

            int i = 1;

            foreach (Product? product in products)

            {

                if (i == 1)

                {

                    productsHome.LastOrDefault().Product1 = product;

                }

                if (i == 2)

                {

                    productsHome.LastOrDefault().Product2 = product;

                }

                if (i == 3)

                {

                    productsHome.LastOrDefault().Product3 = product;

                }

                if (i == 4)

                {

                    productsHome.LastOrDefault().Product4 = product;

                    productsHome.Add(new ProductHomeViewModel());

                    i = 0;

                }

                i++;

            }
            HomeViewModel model = new() { Products = productsHome };

            User user = await _UserHelper.GetUserAsync(User.Identity.Name);

            if (user != null)

            {

                model.Quantity = await _Context.TemporalSale

                    .Where(ts => ts.user.Id == user.Id)

                    .SumAsync(ts => ts.Quantity);

            }


            return View(model);

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Route("error/404")]

        public IActionResult Error404()

        {

            return View();

        }

        public async Task<IActionResult> Add(int? id)
        {
            if(id == null)
            {
                return NotFound();

            }
            if(!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            Product product = await _Context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }


            User user = await _UserHelper.GetUserAsync(User.Identity.Name);

            if (user == null)

            {

                return NotFound();

            }


            TemporalSale temporalSale = new()

            {

                product = product,

                Quantity = 1,

                user = user

            };


            _Context.TemporalSale.Add(temporalSale);

            await _Context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));


        }
    }
}