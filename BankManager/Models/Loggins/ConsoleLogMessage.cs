using Serilog.Events;

namespace BankManager.Models.Loggins;

public class ConsoleLogMessage
{
    public LogEventLevel LogLevel { get; set; }
    public string? Text { get; set; }
}