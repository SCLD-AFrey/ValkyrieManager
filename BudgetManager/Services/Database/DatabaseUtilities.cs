using BudgetTracker.Services;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;

namespace BudgetManager.Services.Database;

public class DatabaseUtilities
{
    private readonly FilesService m_filesService;

    public DatabaseUtilities(FilesService p_filesService)
    {
        m_filesService = p_filesService;
    }

    public IDataLayer GetDataLayer()
    {
        var connectionString = SQLiteConnectionProvider.GetConnectionString(m_filesService.AppDataFilePath);
        return new SimpleDataLayer(XpoDefault.GetConnectionProvider(connectionString, AutoCreateOption.DatabaseAndSchema));
    }
}