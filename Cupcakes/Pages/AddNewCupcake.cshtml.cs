using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Cupcakes.Data;
using Cupcakes.Models;
using System.ComponentModel;

namespace Cupcakes.Pages
{
    public class AddNewCupcakeModel : PageModel
    {
        private readonly ILogger<AddNewCupcakeModel> _logger;
        private readonly IHostEnvironment _environment;

        [BindProperty]
        public Cupcake Cupcake { get; set; } = new();

        [BindProperty]
        [DisplayName("Upload Image")]
        public IFormFile FileUpload { get; set; }
        
        public AddNewCupcakeModel(ILogger<AddNewCupcakeModel> logger, IHostEnvironment environment)
        {
            _logger = logger;
            _environment = environment;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            
            // Validate input
            
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Upload file to server

            string filename = FileUpload.FileName;

            // Update Cupcake object to include the image filename
            Cupcake.ImageFilename = filename;

            // Save the file
            string projectRootPath = _environment.ContentRootPath;
            string fileSavePath = Path.Combine(projectRootPath, "wwwroot\\uploads", filename);

            // We use a "using" to ensure the filestream is disposed of when we're done with it
            using (FileStream fileStream = new FileStream(fileSavePath, FileMode.Create))
            {
                FileUpload.CopyTo(fileStream);
            }

            // Save to database

            DbContext.AddNewCupcake(Cupcake);

            return RedirectToPage("Index");
        }
    }
}
