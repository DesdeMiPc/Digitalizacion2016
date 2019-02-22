using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Digitalizacion2014.Controles
{
    public partial class frmBusqueda : Form
    {

        WSD2014.cRetorno regreso = new WSD2014.cRetorno();
        WSD2014.WSDatosSoap AccesoDatos = new WSD2014.WSDatosSoapClient();

        public string Catalogo = "";
        public string ValorRegreso = "";
        public string textoRegreso = "";
        public int colTexto = 0;
        string procedimiento = "";
        string opcion = "";
        string parametros = "";
        Clases.ColumnSorter m_lstColumnSorter = new Clases.ColumnSorter();

        public frmBusqueda()
        {
            InitializeComponent();
        }

        private void frmBusqueda_Load(object sender, EventArgs e)
        {

            //Specify the listviewcolumnsorter 
            this.lvDatos.ListViewItemSorter = m_lstColumnSorter; 

            procedimiento = "SP_Innova_MetaAyuda";
            opcion = "2";
            parametros = "V1=" + this.Catalogo + "|";

            //Cargar los Catalogos de Busquedas
            regreso = AccesoDatos.ivkProcedimiento(procedimiento, opcion, parametros, Clases.vGlobales.conexion, null);
            if (regreso.bOk)
            { 
                //Todo Correcto
                cboCampos.DataSource = regreso.ds.Tables[0];
            }

            //Traer los Datos del Server
            opcion = "1";
            regreso = AccesoDatos.ivkProcedimiento(procedimiento, opcion, parametros, Clases.vGlobales.conexion, null);
            if (regreso.bOk)
            { 
                //Datos Obtenidos con exito
                this.Text = regreso.ds.Tables[0].Rows[0]["cDescripcion"].ToString();
                procedimiento = regreso.ds.Tables[0].Rows[0]["cProcedimiento"].ToString();
                opcion = regreso.ds.Tables[0].Rows[0]["cValidar"].ToString();
                parametros = "|";

                //Proceder a Traer Datos para Llenar Tabla Inicial
                regreso = AccesoDatos.ivkProcedimiento(procedimiento, opcion, parametros, Clases.vGlobales.conexion, null);
                if (regreso.bOk)
                {
                    //Se otuvieron los Datos para el llenado del Grid
                    llenarGrid(regreso.ds.Tables[0]);
                }
            }
        }

        //Llenar el Grid en base a los Campos que llegaron
        private void llenarGrid(DataTable datosOrigen, bool crearCols = true)
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
            foreach(DataRow ren in datosOrigen.Rows)
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
        }

        private void lvDatos_DoubleClick(object sender, EventArgs e)
        {
            ValorRegreso = lvDatos.FocusedItem.Text.ToString();
            if (colTexto != 0)
            {
                textoRegreso = lvDatos.FocusedItem.SubItems[colTexto].Text;
            }

            DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            //Si se tecleo algo
            procedimiento = "SP_Innova_MetaAyuda";
            opcion = "3";
            parametros = "V1=" + Catalogo + "|V2=" + cboCampos.Text.ToString() + "|V3=" + cboCampos.SelectedValue.ToString() + "|";

            regreso = AccesoDatos.ivkProcedimiento(procedimiento, opcion, parametros, Clases.vGlobales.conexion, null);

            if (regreso.bOk)
            {
                //Consulta Exitosa
                procedimiento = regreso.ds.Tables[0].Rows[0]["cProcedimiento"].ToString();
                opcion = regreso.ds.Tables[0].Rows[0]["cValidar"].ToString();
                parametros = "|" + regreso.ds.Tables[0].Rows[0]["vValor"].ToString() + "=" + txtBuscar.Text.Trim() + "|";

                regreso = AccesoDatos.ivkProcedimiento(procedimiento, opcion, parametros, Clases.vGlobales.conexion, null);
                if (regreso.bOk)
                {
                    //Se cargaron los Registros correctamente
                    llenarGrid(regreso.ds.Tables[0], false);
                }
            }
        }

        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                btnBuscar.PerformClick();
                e.Handled = e.SuppressKeyPress = true;
            }
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
