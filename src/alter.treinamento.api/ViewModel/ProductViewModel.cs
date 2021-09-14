using System;
using System.ComponentModel.DataAnnotations;

namespace alter.treinamento.api.ViewModel
{
    public class ProductViewModel
    {
        public Guid id { get; set; }

        [Required(ErrorMessage = "The {0} field is mandatory")]
        [StringLength(200, ErrorMessage = "Field {0} must have between {2} and {1} characters", MinimumLength = 2)]
        public string Description { get; set; }

        [Required(ErrorMessage = "The {0} field is mandatory")]
        public string Code { get; set; }

        [Required(ErrorMessage = "The {0} field is mandatory")]
        public string Reference { get; set; }
        public int StockBalance { get; set; }

        [Required(ErrorMessage = "The {0} field is mandatory")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "The {0} field is mandatory")]
        public decimal Height { get; set; }

        [Required(ErrorMessage = "The {0} field is mandatory")]
        public decimal Width { get; set; }

        [Required(ErrorMessage = "The {0} field is mandatory")]
        public decimal Depth { get; set; }

        [Required(ErrorMessage = "The {0} field is mandatory")]
        public bool IsActive { get; set; }
        public Guid CategoryId { get; set; }
    }
}
