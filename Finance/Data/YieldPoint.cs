namespace Finance.Data
{
    public class YieldPoint
    {
        public Tenor   Tenor { get; set; }
        public decimal Yield { get; set; }

        public YieldPoint(Tenor tenor, decimal yield)
        {
            Tenor = tenor;
            Yield = yield;
        }
    }
}