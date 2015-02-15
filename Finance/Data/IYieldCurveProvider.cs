namespace Finance.Data
{
    public interface IYieldCurveProvider
    {
        YieldPoint[] GetCurve(string currency);
    }
}