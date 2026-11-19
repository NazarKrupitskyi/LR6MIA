namespace LR_3.BridgeProccesing.Implementation
{
    public interface IReportFormatter
    {
        string Format(string reportTitle, Dictionary<string, string> reportData);
    }
}
