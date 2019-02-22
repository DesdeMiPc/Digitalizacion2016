using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Digitalizacion2014.Controles
{
    public partial class InnovaTXT : TextBox
    {
        //Control para Desplegar Descripcion Generalmente un Label
        private Control _ControlDestino;
        private bool _activarAyuda = false;
        private bool _isNumeric = false;
        private Keys _tecla = Keys.F2;
        private Color _backColorFocus = Color.Yellow;
        private Color _backColorActual = Color.White;
        private string _Catalogo = "";
        private int _colTexto = 0;
        private bool _activarEnter = false;

        public InnovaTXT()
        {
            InitializeComponent();
        }

        //Activar Enter como Tab
        public bool ActivarEnter
        {
            get { return this._activarEnter; }
            set { this._activarEnter = value; }
        }

        //Color del Texto
        public int colTexto
        {
            get
            {
                return this._colTexto;
            }
            set
            {
                this._colTexto = value;
            }
        }

        //Catalogo a controlar para ayudas o busquedas rapidas
        public string Catalogo
        {
            get
            {
                return this._Catalogo;
            }
            set
            {
                this._Catalogo = value;
            }
        }

        //Color al Recibir el Foco
        public Color BackColorFocus
        {
            get
            {
                return this._backColorFocus;
            }
            set
            {
                this._backColorFocus = value;
            }
        }

        //Tecla para invocar la Ayuda de Catalogos
        public Keys TeclaAyudaCatalogo
        {
            get
            {
                return this._tecla;
            }
            set
            {
                this._tecla = value;
            }
        }

        //Control para desplegar el resultado generalmete un Label
        public Control ControlDestinoDescripcion
        {
            get
            {
                return this._ControlDestino;
            }
            set
            {
                this._ControlDestino = value;
            }
        }

        //Si funciona la Tecla programada
        public bool ActivarAyuda
        {
            get
            {
                return _activarAyuda;
            }
            set
            {
                this._activarAyuda = value;
            }
        }

        //Solo Aceptar Valores númericos
        public bool IsNumeric
        {
            get
            {
                return _isNumeric;
            }
            set
            {
                this._isNumeric = value;
                if (_isNumeric)
                {
                    this.TextAlign = HorizontalAlignment.Right;
                }
            }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);
            this.SelectAll();
            this._backColorActual = this.BackColor;
            this.BackColor = this._backColorFocus;

        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if (this._activarEnter)
            {
                if (e.KeyChar == (Char)Keys.Enter)
                {
                    e.Handled = true;
                    SendKeys.Send("{TAB}");
                    return;
                }
            }

            if (_isNumeric)
            {
                char c = e.KeyChar;
                if ("1234567890.".IndexOf(c) < 0)
                {
                    e.Handled = true;
                }
                else
                {
                    e.Handled = false;
                }
            }
            base.OnKeyPress(e);
        }

        protected override void OnLeave(EventArgs e)
        {
            base.OnLeave(e);
            this.BackColor = this._backColorActual;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (!this._activarAyuda)
            {
                //Ayuda Desactivada
                return;
            }

            if (e.KeyCode == this._tecla)
            {
                //Se Activo la Ayuda
                frmBusqueda ventana = new frmBusqueda();
                ventana.Catalogo = this._Catalogo;
                ventana.colTexto = this._colTexto;
                var absolute = this.PointToScreen(new Point(0, 0));
                ventana.Location = new Point(absolute.X + 5, absolute.Y + this.Height + 10);
                if (ventana.ShowDialog() == DialogResult.OK)
                {
                    this.Text = ventana.ValorRegreso;
                    if (this._colTexto != 0)
                    {
                        this._ControlDestino.Text = ventana.textoRegreso;
                    }
                    SendKeys.Send("{TAB}");
                }
            }
        }

    }
}
