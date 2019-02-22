using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Digitalizacion2014.frmBases
{
    public partial class frmGeneral01 : Form
    {
        public frmGeneral01()
        {
            InitializeComponent();
        }
        //Bases para implementación en cada Formulario Heredado
        public virtual void newRecord() { }
        public virtual void deleteRecord() { }
        public virtual void editRecord() { }
        public virtual void printCatalog() { }
        public virtual void runProcess() { }
        public virtual void reloadRecords() { }
    }
}
