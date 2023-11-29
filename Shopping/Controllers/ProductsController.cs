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

        public async Task<IActionResult> Edit(int? id)

        {

            if (id == null)

            {

                return NotFound();

            }


            Product product = await _context.Products.FindAsync(id);

            if (product == null)

            {

                return NotFound();

            }


            EdiProductViewModel model = new()

            {

                Description = product.Description,

                Id = product.Id,

                Name = product.Name,

                Price = product.Price,

                Stock = product.Stock,

            };


            return View(model);

        }

        [HttpPost]

        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int id, CreateProductViewModel model)

        {

            if (id != model.Id)

            {

                return NotFound();

            }


            try

            {

                Product product = await _context.Products.FindAsync(model.Id);

                product.Description = model.Description;

                product.Name = model.Name;

                product.Price = model.Price;

                product.Stock = model.Stock;

                _context.Update(product);

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


            return View(model);
        }
        public async Task<IActionResult> Details(int? id)

        {

            if (id == null)

            {

                return NotFound();

            }


            Product product = await _context.Products

                .Include(p => p.ProductImages)

                .Include(p => p.ProductCategories)

                .ThenInclude(pc => pc.category)

                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)

            {

                return NotFound();

            }


            return View(product);

        }
        public async Task<IActionResult> AddImage(int? id)

        {

            if (id == null)

            {

                return NotFound();

            }


            Product product = await _context.Products.FindAsync(id);

            if (product == null)

            {

                return NotFound();

            }


            AddProductImagens model = new()

            {

                ProductId = product.Id,

            };


            return View(model);

        }


        [HttpPost]

        [ValidateAntiForgeryToken]

        public async Task<IActionResult> AddImage(AddProductImagens model)

        {

            if (ModelState.IsValid)

            {

                Byte[] bytesImage;
                using (var stream = new MemoryStream())
                {
                    await model.ImageFile.CopyToAsync(stream);
                    bytesImage = stream.ToArray();
                }           

                Product product = await _context.Products.FindAsync(model.ProductId);

                ProductImage productImage = new()

                {

                    Product = product,

                    Imagefile = bytesImage,

                };


                try

                {

                    _context.Add(productImage);

                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Details), new { Id = product.Id });

                }

                catch (Exception exception)

                {

                    ModelState.AddModelError(string.Empty, exception.Message);

                }

            }


            return View(model);

        }

        public async Task<IActionResult> DeleteImage(int? id)

        {

            if (id == null)

            {

                return NotFound();

            }


            ProductImage productImage = await _context.ProductImages

                .Include(pi => pi.Product)

                .FirstOrDefaultAsync(pi => pi.Id == id);

            if (productImage == null)

            {

                return NotFound();

            }
          
            _context.ProductImages.Remove(productImage);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { Id = productImage.Product.Id });

        }
        public async Task<IActionResult> AddCategory(int? id)

        {

            if (id == null)

            {

                return NotFound();

            }


            Product product = await _context.Products.Include(p => p.ProductCategories)
                .ThenInclude(pc => pc.category)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)

            {

                return NotFound();

            }

            List<Category> categories = product.ProductCategories.Select(pc => new Category
            {
                Id= pc.category.Id,
                Name = pc.category.Name,
            }).ToList();
            AddCategoryProductViewModel model = new()

            {

                ProductId = product.Id,

                Categories = await _combosHelper.GetComboCategoriesAsync(categories),

            };


            return View(model);

        }


        [HttpPost]

        [ValidateAntiForgeryToken]

        public async Task<IActionResult> AddCategory(AddCategoryProductViewModel model)

        {

            Product product = await _context.Products.Include(p => p.ProductCategories)
                .ThenInclude(pc => pc.category)
                .FirstOrDefaultAsync(p => p.Id == model.ProductId);

            if (ModelState.IsValid)

            {

                
                ProductCategory productCategory = new()

                {

                    category = await _context.Categories.FindAsync(model.CategoryId),

                    product = product,

                };


                try

                {

                    _context.Add(productCategory);

                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Details), new { Id = product.Id });

                }

                catch (Exception exception)

                {

                    ModelState.AddModelError(string.Empty, exception.Message);

                }

            }

            List<Category> categories = product.ProductCategories.Select(pc => new Category
            {
                Id = pc.category.Id,
                Name = pc.category.Name,
            }).ToList();

            model.Categories = await _combosHelper.GetComboCategoriesAsync();
            

            return View(model);

        }


    }
}

 

