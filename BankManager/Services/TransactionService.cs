using Microsoft.Extensions.Logging;

namespace BankManager.Services;

public class TransactionService
{
    private readonly ILogger<TransactionService> m_logger;

    public TransactionService(ILogger<TransactionService> p_logger)
    {
        m_logger = p_logger;
        m_logger.LogInformation("TransactionService created");
    }
}