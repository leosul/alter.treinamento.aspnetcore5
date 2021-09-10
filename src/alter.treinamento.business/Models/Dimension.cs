namespace alter.treinamento.business.Models
{
    public class Dimension
    {
        public decimal Height { get; private set; }
        public decimal Width { get; private set; }
        public decimal Depth { get; private set; }

        private string FormatDimension()
        {
            return $"WxHxD: {Width} x {Height} x {Depth}";
        }

        public override string ToString()
        {
            return FormatDimension();
        }
    }
}
