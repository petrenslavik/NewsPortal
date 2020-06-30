using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace NewsPortal.Managers
{
    public enum Storage
    {
        Remote,
        Local
    }

    public class StorageManager
    {
        public static void ToggleStorage(Storage storage)
        {
            string connectionName = String.Empty;

            switch (storage)
            {
                case Storage.Remote:
                    connectionName = "DefaultConnection";
                    break;
                case Storage.Local:
                    connectionName = "LocalConnection";
                    break;
            }

            string connectionString = WebConfigManager.GetConnectionString(connectionName);
            string nHibernateConfigPath = WebConfigManager.GetAppSetting("nHibernateConfigPath");

            if (String.IsNullOrEmpty(connectionString) || String.IsNullOrEmpty(connectionString))
                throw new ArgumentException();

            var doc = XDocument.Load(HttpContext.Current.Server.MapPath(nHibernateConfigPath));

            var elements = from element in doc.Nodes().OfType<XElement>().First().Elements().First().Elements()
                           where (element.Attribute("name").Value == "connection.connection_string")
                           select element;

            var connectionStringElement = elements.FirstOrDefault();
            connectionStringElement.Value = connectionString;
            doc.Save(HttpContext.Current.Server.MapPath(nHibernateConfigPath));
        }

    }
}