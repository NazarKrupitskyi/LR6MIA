using System.Text;
using System.Text.Json;

namespace LR_3.BridgeProccesing.Implementation
{
    public class CsvReportFormatter : IReportFormatter
    {
        public string Format(string reportTitle, Dictionary<string, string> reportData)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Report,{reportTitle}");
            sb.AppendLine("Field,Value");

            foreach (var item in reportData)
            {
                sb.AppendLine($"{item.Key},{item.Value}");
            }

            return sb.ToString();
        }
    }

    public class JsonReportFormatter : IReportFormatter
    {
        public string Format(string reportTitle, Dictionary<string, string> reportData)
        {
            var payload = new
            {
                Title = reportTitle,
                Data = reportData
            };

            return JsonSerializer.Serialize(payload, new JsonSerializerOptions { WriteIndented = true });
        }
    }

    public class PlainTextReportFormatter : IReportFormatter
    {
        public string Format(string reportTitle, Dictionary<string, string> reportData)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"=== {reportTitle} ===");

            foreach (var item in reportData)
            {
                sb.AppendLine($"{item.Key}: {item.Value}");
            }

            return sb.ToString();
        }
    }
}
