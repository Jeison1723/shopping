using Humanizer.Bytes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shopping.Controllers.Data;
using Shopping.Controllers.Data.Entities;
using Shopping.Helpers;
using Shopping.Models;

namespace Shopping.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductsController : Controller
    {       
        private readonly DataContext _context;
        private readonly ICombosHelper _combosHelper;
        private readonly IBlobHelper _blobHelper;
        private Guid Imagefile;
        private byte[] imagefile;

        public ProductsController(DataContext context, ICombosHelper combosHelper, IBlobHelper blobHelper)

        {
            _context = context;
            _combosHelper = combosHelper;
            _blobHelper = blobHelper;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Products
                .Include(p => p.ProductImages)
                .Include(p => p.ProductCategories)
                .ThenInclude(pc => pc.category)
                .ToListAsync());
        }

        public async Task<IActionResult> Create()

        {

            CreateProductViewModel model = new()

            {

                Categories = await _combosHelper.GetComboCategoriesAsync(),

            };


            return View(model);

        }


        [HttpPost]

        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(CreateProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Leer la imagen desde el modelo
                Byte[] bytesImage;
                using (var stream = new MemoryStream())
                {
                    await model.ImageFile.CopyToAsync(stream);
                    bytesImage = stream.ToArray();
                }

                Product product = new()
                {
                    Description = model.Description,
                    Name = model.Name,
                    Price = model.Price,
                    Stock = model.Stock,
                    ProductCategories = new List<ProductCategory>
            {
                new ProductCategory
                {
                    category = await _context.Categories.FindAsync(model.CategoryId)
                }
            },
                    ProductImages = new List<ProductImage>
            {
                new ProductImage { Imagefile = bytesImage }
            }
                };

                try
                {
                    _context.Add(product);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe un producto con el mismo nombre.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }

            // Si llegamos aquí, hay un error de validación, recarga la vista con los datos del modelo y las categorías
            model.Categories = await _combosHelper.GetComboCategoriesAsync();
            return View(model);
        }

    }


    
}

 

