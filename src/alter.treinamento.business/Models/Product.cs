using System;

namespace alter.treinamento.business.Models
{
    public class Product : EntityBase
    {
        public string Description { get; private set; }
        public string Code { get; private set; }
        public string Reference { get; private set; }
        public int StockBalance { get; private set; }
        public decimal Price { get; private set; }
        public bool IsActive { get; private set; }
        public Dimension Dimension { get; private set; }

        /* EF Relations */
        public Category Category { get; set; }
        public Guid CategoryId { get; set; }

        public void SetIsActive(bool isActive) => IsActive = isActive;
    }
}
