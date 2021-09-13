using System;
using System.ComponentModel.DataAnnotations;

namespace alter.treinamento.api.ViewModel
{
    public class CategoryViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The {0} field is mandatory")]
        [StringLength(200, ErrorMessage = "Field {0} must have between {2} and {1} characters", MinimumLength = 2)]
        public string Description { get; set; }

        [Required(ErrorMessage = "The {0} field is mandatory")]
        public bool IsActive { get; set; }
    }
}
