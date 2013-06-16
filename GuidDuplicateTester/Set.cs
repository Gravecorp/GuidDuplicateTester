using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace GuidDuplicateTester
{
    class Set
    {
        public struct SetGuid
        {
            public Guid card;
            public Guid set;
        }

        private string p;

        public Set(string p)
        {
            // TODO: Complete member initialization
            this.p = p;
        }

        internal List<SetGuid> GetGuids()
        {
            List<SetGuid> ret = new List<SetGuid>();
            XmlDocument doc = new XmlDocument();
            FileInfo[] d = new DirectoryInfo(p).GetFiles("*.xml");
            FileInfo f = null;
            if (d.Length > 0)
            {
                f = d[0];
            }
            if (f != null)
            {
                doc.Load(f.FullName);
                XmlNode n = doc.GetElementsByTagName("set").Item(0);
                Guid s = Guid.Empty;
                if (n.Attributes["id"] != null)
                {
                    s = Guid.Parse(n.Attributes["id"].Value);
                }

                XmlNodeList l = doc.GetElementsByTagName("card");
                foreach (XmlNode node in l)
                {
                    if (node.Attributes["id"] != null)
                    {
                        Guid g = Guid.Parse(node.Attributes["id"].Value);
                        SetGuid setguid = new SetGuid()
                        {
                            set = s,
                            card = g
                        };
                        ret.Add(setguid);
                    }
                }
            }
            doc.RemoveAll();
            doc = null;
            return (ret);
        }
    }
}
