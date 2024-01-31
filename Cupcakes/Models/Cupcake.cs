using System.ComponentModel;

namespace Cupcakes.Models
{
    
    // Cupcake model to store cupcake data in database.
    public class Cupcake
    {
        public int CupcakeId { get; set; }

        public string Name { get; set; } = string.Empty;

        [DisplayName("Image")]
        public string ImageFilename { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }

       
    }
}
