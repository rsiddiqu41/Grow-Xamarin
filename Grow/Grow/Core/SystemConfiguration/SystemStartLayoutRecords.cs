using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Grow.Core.SystemConfiguration
{
    [Serializable()]
    public class SystemStartLayoutRecords
    {
        [XmlElement("SlideTitle")]
        public string SlideTitle { get; set; }

        [XmlElement("Details")]
        public string Details { get; set; }

        [XmlElement("ImageUrl")]
        public string ImageUrl { get; set; }

        public SystemStartLayoutRecords()
        {}
    }

    [Serializable()]
    [XmlRoot("SystemStartScreen")]
    public class SystemStartScreen
    {
        [XmlElement("SystemStartLayoutRecords")]
        public List<SystemStartLayoutRecords> StartScreenTypes { get; set; }

        public SystemStartScreen()
        {
            StartScreenTypes = new List<SystemStartLayoutRecords>();
        }
    }
}
