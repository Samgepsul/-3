using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Laboratory.Model
{
    [Serializable]
    public class Word
    {
        [XmlElement("ID")]
        public int ID {  get; set; }
        [XmlElement("Full")]
        public string Full { get; set; }
        [XmlElement("Prefix")]
        public string Prefix { get; set; }
        [XmlElement("Root")]
        public string Root { get; set; }
        
        [XmlElement("Suffix")]
        public string Suffix { get; set; }
    }
}