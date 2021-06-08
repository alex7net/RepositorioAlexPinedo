using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace ZEDPAITA.SRV.Core.Models.ServicioEmail
{
    [DataContract]
    [Serializable]
    
    public class SendEmailIn : INotifyPropertyChanged
    {
        [DataMember(Order = 1, Name = "strFrom")]
        public String From { get; set; }

        [DataMember(Order = 2, Name = "strTo")]
        public String To { get; set; }

        [DataMember(Order = 3, Name = "strCc")]
        public String Cc { get; set; }

        [DataMember(Order = 4, Name = "strMessage")]
        [XmlIgnore]
        public String Message
        {
            get
            { // Retrieves the content of the encapsulated CDATA
                return cDataAttributeField.Value;
            }

            set
            { // Encapsulate in a CDATA XmlNode
                XmlDocument xmlDocument = new XmlDocument();
                this.cDataAttributeField = xmlDocument.CreateCDataSection(value);
            }
        }

        [DataMember(Order = 5, Name = "strSubject")]
        public String Subject { get; set; }

        [DataMember(Order = 6, Name = "strPass")]
        public String Pass { get; set; }

        [DataMember(Order = 7, Name = "strHost")]
        public String Host { get; set; }

        [DataMember(Order = 8, Name = "blIsBodyHtml")]
        public bool IsBodyHtml { get; set; }

        [DataMember(Order = 9, Name = "intPort")]
        public Int32 Port { get; set; }

        [DataMember(Order = 10, Name = "lstFiles")]
        public List<FileEmail> Files { get; set; }


        private XmlNode cDataAttributeField;
        /// <remarks/>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public XmlNode CDataAttribute
        {
            get { return this.cDataAttributeField; }
            set { this.cDataAttributeField = value; }
        }

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        #endregion
    }
}
