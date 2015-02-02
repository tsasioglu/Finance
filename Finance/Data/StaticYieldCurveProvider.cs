namespace Finance.Data
{
    public class YieldPoint
    {
        public Tenor Tenor { get; set; }
        public decimal Yield { get; set; }

        public YieldPoint(Tenor tenor, decimal yield)
        {
            Tenor = tenor;
            Yield = yield;
        }
    }

    public interface IYieldCurveProvider
    {
        YieldPoint[] GetCurve();
    }

    public class StaticYieldCurveProvider : IYieldCurveProvider
    {
        private static readonly YieldPoint[] UkData = 
        { 
            new YieldPoint(new Tenor("3M"),  0.411m), 
            new YieldPoint(new Tenor("6M"),  0.476m), 
            new YieldPoint(new Tenor("1Y"),  0.327m), 
            new YieldPoint(new Tenor("2Y"),  0.403m), 
            new YieldPoint(new Tenor("3Y"),  0.574m), 
            new YieldPoint(new Tenor("4Y"),  0.767m), 
            new YieldPoint(new Tenor("5Y"),  0.931m), 
            new YieldPoint(new Tenor("6Y"),  1.018m), 
            new YieldPoint(new Tenor("7Y"),  1.094m), 
            new YieldPoint(new Tenor("8Y"),  1.214m), 
            new YieldPoint(new Tenor("9Y"),  1.3m), 
            new YieldPoint(new Tenor("10Y"), 1.373m), 
            new YieldPoint(new Tenor("15Y"), 1.694m), 
            new YieldPoint(new Tenor("20Y"), 1.873m), 
            new YieldPoint(new Tenor("25Y"), 1.979m), 
            new YieldPoint(new Tenor("30Y"), 2.05m), 
            new YieldPoint(new Tenor("40Y"), 1.992m), 
            new YieldPoint(new Tenor("50Y"), 2.002m), 
        };
        
        public YieldPoint[] GetCurve()
        {
            return UkData;
        }
    }
}
