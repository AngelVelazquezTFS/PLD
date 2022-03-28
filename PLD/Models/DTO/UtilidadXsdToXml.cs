using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace Toyota.Financial.SupportApps.DTO
{
    public class UtilidadXsdToXml : StringWriter
    {
        private readonly Encoding mencoding;
        public UtilidadXsdToXml(Encoding encoding) : base()
        {
            this.mencoding = encoding;
        }

        public override Encoding Encoding
        {
            get
            {
                return this.mencoding;
            }
        }
    }

}