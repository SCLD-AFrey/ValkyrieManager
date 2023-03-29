using System;
using BankManager.Services.Database;
using DevExpress.Xpo;
using DevExpress.Xpo.Logger;
using Microsoft.Extensions.Logging;
using ValkyrieData.Banking;

namespace BankManager.Services;

public class HistoryService
{
    private readonly ILogger<HistoryService> m_logger;
    private readonly DatabaseInterface m_databaseInterface;

    public HistoryService(ILogger<HistoryService> p_logger, DatabaseInterface p_databaseInterface)
    {
        m_logger = p_logger;
        m_databaseInterface = p_databaseInterface;
    }
    
    public void AddUserHistory(User p_user, string p_message)
    {
        UnitOfWork uow = m_databaseInterface.ProvisionUnitOfWork();
        var history = new UserHistory(uow)
        {
            User = p_user,
            Message = p_message,
            Timestamp = DateTime.UtcNow
        };
        uow.CommitChanges();
    }
    
    public void AddTransactionHistory(User p_user, Transaction p_transaction, string p_message)
    {
        UnitOfWork uow = m_databaseInterface.ProvisionUnitOfWork();
        var history = new TransactionHistory(uow)
        {
            Transaction = p_transaction,
            User = p_user,
            Message = p_message,
            Timestamp = DateTime.UtcNow
        };
        uow.CommitChanges();
    }
    
    
}