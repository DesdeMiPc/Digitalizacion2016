using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Digitalizacion2014.Clases
{
    public class ComboBoxItem
    {
        public string Text { get; set; }
        public object Value { get; set; }
        public object Tag { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}
