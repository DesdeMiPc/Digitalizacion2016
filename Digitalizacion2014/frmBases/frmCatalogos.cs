using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;


namespace Digitalizacion2014.frmBases
{
    public partial class frmCatalogos : frmGeneral01
    {
        //Acceso a Carga inicial y refresh de datos
        string _procedimiento = "";
        string _opcion = "";
        string _parametros = "";

        //Acceso a Datos
        WSD2014.cRetorno regreso;// = new WSD2014.cRetorno();
        WSD2014.WSDatosSoap AccesoDatos;// = new WSD2014.WSDatosSoapClient();

        //Para porder ordenar las columnas por medio de un Click
        Clases.ColumnSorter m_lstColumnSorter = new Clases.ColumnSorter();

        public frmCatalogos()
        {
            InitializeComponent();
        }


        public virtual void CargarDatos(string procedimiento, string opcion, string parametros)
        {
            if (procedimiento == "" || opcion == "")
            {
                return;
            }

            _procedimiento = procedimiento;
            _opcion = opcion;
            _parametros = parametros;
            this.CargarDatos();
        }

        public virtual void CargarDatos(bool crearCols = true)
        {
            if (_procedimiento == "" || _opcion == "")
            {
                return;
            }
            if (regreso == null)
            {
                regreso = new WSD2014.cRetorno();
            }
            if (AccesoDatos == null)
            {
                AccesoDatos = new WSD2014.WSDatosSoapClient();
            }

            regreso = AccesoDatos.ivkProcedimiento(_procedimiento, _opcion, _parametros, Clases.vGlobales.conexion, null);
            if (regreso.bOk)
            {
                //Datos cargados Correctamente
                this.llenarGrid(regreso.ds.Tables[0], crearCols);
            }
        }

        //Llenar el Grid en base a los Campos que llegaron
        public virtual void llenarGrid(DataTable datosOrigen, bool crearCols = true)
        {
            if (crearCols)
            {
                lvDatos.Clear();
                //Creación de Columnas
                foreach (DataColumn col in datosOrigen.Columns)
                {
                    lvDatos.Columns.Add(col.ColumnName.ToString(), -2, HorizontalAlignment.Left);
                }
            }
            else
            {
                lvDatos.Items.Clear();
            }

            //Llenar todos los Datos
            foreach (DataRow ren in datosOrigen.Rows)
            {
                ListViewItem nr = new ListViewItem(ren[0].ToString());
                for (int c = 1; c < ren.ItemArray.Length; c++)
                {
                    nr.SubItems.Add(ren.ItemArray[c].ToString());
                }
                
                lvDatos.Items.Add(nr);
            }

            //Cambiar el ancho de columnas de manera automatica
            for (int i = 0; i < this.lvDatos.Columns.Count; i++)
            {
                this.lvDatos.Columns[i].Width = -2;
            }

            try
            {
                this.lvDatos.Items[0].Selected = true;
            }
            catch
            {
                //No hacer nada
            }

        }

        private void frmCatalogos_Load(object sender, EventArgs e)
        {
            //Especifica the ListViewColumnSorter
            this.lvDatos.ListViewItemSorter = m_lstColumnSorter; 
        }

        private void lvDatos_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            ListView myListView = (ListView)sender;

            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == m_lstColumnSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (m_lstColumnSorter.Order == SortOrder.Ascending)
                {
                    m_lstColumnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    m_lstColumnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                m_lstColumnSorter.SortColumn = e.Column;
                m_lstColumnSorter.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            myListView.Sort();
            myListView.SetSortIcon(m_lstColumnSorter.SortColumn, m_lstColumnSorter.Order);
        }

        private void frmCatalogos_Activated(object sender, EventArgs e)
        {
            try
            {
                ((frmPrincipal)this.MdiParent).EnableTab("Acciones");
            }
            catch { }
        }

        private void frmCatalogos_Deactivate(object sender, EventArgs e)
        {
            try
            {
                ((frmPrincipal)this.MdiParent).DisableTab("Acciones");
            }
            catch { }
        }
    }

    internal static class ListViewExtensions
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct LVCOLUMN
        {
            public Int32 mask;
            public Int32 cx;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string pszText;
            public IntPtr hbm;
            public Int32 cchTextMax;
            public Int32 fmt;
            public Int32 iSubItem;
            public Int32 iImage;
            public Int32 iOrder;
        }

        const Int32 HDI_WIDTH = 0x0001;
        const Int32 HDI_HEIGHT = HDI_WIDTH;
        const Int32 HDI_TEXT = 0x0002;
        const Int32 HDI_FORMAT = 0x0004;
        const Int32 HDI_LPARAM = 0x0008;
        const Int32 HDI_BITMAP = 0x0010;
        const Int32 HDI_IMAGE = 0x0020;
        const Int32 HDI_DI_SETITEM = 0x0040;
        const Int32 HDI_ORDER = 0x0080;
        const Int32 HDI_FILTER = 0x0100;

        const Int32 HDF_LEFT = 0x0000;
        const Int32 HDF_RIGHT = 0x0001;
        const Int32 HDF_CENTER = 0x0002;
        const Int32 HDF_JUSTIFYMASK = 0x0003;
        const Int32 HDF_RTLREADING = 0x0004;
        const Int32 HDF_OWNERDRAW = 0x8000;
        const Int32 HDF_STRING = 0x4000;
        const Int32 HDF_BITMAP = 0x2000;
        const Int32 HDF_BITMAP_ON_RIGHT = 0x1000;
        const Int32 HDF_IMAGE = 0x0800;
        const Int32 HDF_SORTUP = 0x0400;
        const Int32 HDF_SORTDOWN = 0x0200;

        const Int32 LVM_FIRST = 0x1000;         // List messages
        const Int32 LVM_GETHEADER = LVM_FIRST + 31;
        const Int32 HDM_FIRST = 0x1200;         // Header messages
        const Int32 HDM_SETIMAGELIST = HDM_FIRST + 8;
        const Int32 HDM_GETIMAGELIST = HDM_FIRST + 9;
        const Int32 HDM_GETITEM = HDM_FIRST + 11;
        const Int32 HDM_SETITEM = HDM_FIRST + 12;

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        private static extern IntPtr SendMessageLVCOLUMN(IntPtr hWnd, Int32 Msg, IntPtr wParam, ref LVCOLUMN lPLVCOLUMN);


        //This method used to set arrow icon
        public static void SetSortIcon(this ListView listView, int columnIndex, SortOrder order)
        {
            IntPtr columnHeader = SendMessage(listView.Handle, LVM_GETHEADER, IntPtr.Zero, IntPtr.Zero);

            for (int columnNumber = 0; columnNumber <= listView.Columns.Count - 1; columnNumber++)
            {
                IntPtr columnPtr = new IntPtr(columnNumber);
                LVCOLUMN lvColumn = new LVCOLUMN();
                lvColumn.mask = HDI_FORMAT;

                SendMessageLVCOLUMN(columnHeader, HDM_GETITEM, columnPtr, ref lvColumn);

                if (!(order == SortOrder.None) && columnNumber == columnIndex)
                {
                    switch (order)
                    {
                        case System.Windows.Forms.SortOrder.Ascending:
                            lvColumn.fmt &= ~HDF_SORTDOWN;
                            lvColumn.fmt |= HDF_SORTUP;
                            break;
                        case System.Windows.Forms.SortOrder.Descending:
                            lvColumn.fmt &= ~HDF_SORTUP;
                            lvColumn.fmt |= HDF_SORTDOWN;
                            break;
                    }
                    lvColumn.fmt |= (HDF_LEFT | HDF_BITMAP_ON_RIGHT);
                }
                else
                {
                    lvColumn.fmt &= ~HDF_SORTDOWN & ~HDF_SORTUP & ~HDF_BITMAP_ON_RIGHT;
                }

                SendMessageLVCOLUMN(columnHeader, HDM_SETITEM, columnPtr, ref lvColumn);
            }
        }
    }     

}
