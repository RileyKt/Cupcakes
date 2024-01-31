using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Cupcakes.Data;
using Cupcakes.Models;
using System.IO;
using System.ComponentModel;

namespace Cupcakes.Pages
{
    public class DeleteCupcakeModel : PageModel
    {
        private readonly IWebHostEnvironment _environment;

        [BindProperty(SupportsGet = true)]
        public int CupcakeId { get; set; }

        [BindProperty]
        public Cupcake Cupcake { get; set; } = new();

        [BindProperty]
        [DisplayName("Upload Image")]
        public IFormFile FileUpload { get; set; }
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
            Cupcake = DbContext.GetCupcakeById(CupcakeId);

           

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
