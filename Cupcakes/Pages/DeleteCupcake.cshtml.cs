
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Cupcakes.Data;
using Cupcakes.Models;


namespace Cupcakes.Pages
{
    public class DeleteCupcakeModel : PageModel
    {
        private readonly ILogger<AddNewCupcakeModel> _logger;

        private readonly IWebHostEnvironment _environment;

        [BindProperty(SupportsGet = true)]
        public int CupcakeId { get; set; }
        public Cupcake Cupcake { get; set; } = new();

        public DeleteCupcakeModel(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public void OnGet()
        {
            Cupcake = DbContext.GetCupcakeById(CupcakeId);
        }

        public IActionResult OnPost()
        {

          
            // Deletes the image file
            string fileToDelete = Path.Combine(_environment.WebRootPath, "uploads", Cupcake.ImageFilename);
            if (System.IO.File.Exists(fileToDelete))
            {
                System.IO.File.Delete(fileToDelete);
            }

            // Deletes the cupcake from the database
            DbContext.RemoveCupcake(CupcakeId);

            return RedirectToPage("/Index");
        }
    }
}
