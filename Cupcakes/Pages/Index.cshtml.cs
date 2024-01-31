using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Cupcakes.Data;
using Cupcakes.Models;

namespace Cupcakes.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }
        public List<Cupcake> Cupcakes { get; private set; }

        public void OnGet()
        {
            // Fetch the list of cupcakes from the database
            Cupcakes = DbContext.GetAllCupcakes();

            // Sorts the cupcakes by name in ascending order
            Cupcakes.Sort((x, y) => x.Name.CompareTo(y.Name));
        }
    }
}
