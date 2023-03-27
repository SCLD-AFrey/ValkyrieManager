using BudgetTracker.Services;
using DevExpress.Xpo;

namespace BudgetManager.Services.Database;

public class DatabaseInterface : IDatabaseInterface
{
    public DatabaseInterface(DatabaseUtilities p_utilities)
    {
        DataLayer = p_utilities.GetDataLayer();
    }
    
    public IDataLayer DataLayer     { get; }
    
    public UnitOfWork ProvisionUnitOfWork()
    {
        return new UnitOfWork(DataLayer);
    }
}