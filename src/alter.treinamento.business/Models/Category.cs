using System.Collections.Generic;

namespace alter.treinamento.business.Models
{
    public class Category : EntityBase
    {
        public string Description { get; private set; }
        public bool IsActive { get; private set; }
        public void SetIsActive(bool isActive) => IsActive = isActive;

        /* EF Relations */
        public ICollection<Product> Products { get; set; }
    }
}
