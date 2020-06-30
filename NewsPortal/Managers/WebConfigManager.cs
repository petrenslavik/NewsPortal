using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace NewsPortal.Managers
{
    public class WebConfigManager
    {
        private static XDocument doc = XDocument.Load(HttpContext.Current.Server.MapPath("Web.config"));

        public static string GetConnectionString(string connectionName)
        {
            var elements = from element in doc.Nodes().OfType<XElement>().Single(x => x.Name == "configuration").Element("connectionStrings").Elements()
                           where (element.Attribute("name").Value == connectionName)
                           select element;

            return elements.FirstOrDefault().Attribute("connectionString").Value;
        }

        public static string GetAppSetting(string settingName)
        {
            var elements = from element in doc.Nodes().OfType<XElement>().Single(x => x.Name == "configuration").Element("appSettings").Elements()
                           where (element.Attribute("key").Value == settingName)
                           select element;

            return elements.FirstOrDefault().Attribute("value").Value;
        }
    }
}