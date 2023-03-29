using DevExpress.Xpo;
using DevExpress.Xpo.DB;

namespace BankManager.Services.Database
{
    public class DatabaseUtilities
    {
        private readonly FileService m_fileService;

        public DatabaseUtilities(FileService p_fileService)
        {
            m_fileService = p_fileService;
        }

        public IDataLayer GetDataLayer()
        {
            var connectionString = SQLiteConnectionProvider.GetConnectionString(m_fileService.DatabaseFile);
            return new SimpleDataLayer(XpoDefault.GetConnectionProvider(connectionString, AutoCreateOption.DatabaseAndSchema));
        }
    }
}