
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Cupcakes.Data;
using Cupcakes.Models;


namespace Cupcakes.Pages
{
    public class DeleteCupcakeModel : PageModel
    {
        private readonly ILogger<DeleteCupcakeModel> _logger;

        private readonly IWebHostEnvironment _environment;

        [BindProperty]
        public Cupcake Cupcake { get; set; } = new();

        public DeleteCupcakeModel(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public void OnGet(int id)
        {
            Cupcake = DbContext.GetCupcakeById(id);
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
            DbContext.RemoveCupcake(Cupcake.CupcakeId);

            return RedirectToPage("/Index");
        }
    }
}
