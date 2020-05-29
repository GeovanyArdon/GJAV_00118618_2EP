using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ClaseGUI05.Controlador;
using ClaseGUI05.Modelo;
using LiveCharts;
using LiveCharts.Wpf;
using CartesianChart = LiveCharts.WinForms.CartesianChart;

namespace ClaseGUI05
{
    public partial class frmPrincipal : Form
    {
        private Appuser usuario;
        private CartesianChart graficoEstadisticas;
        
        public frmPrincipal()
        {
            InitializeComponent();
            poblarControles();

            
        }
        
     

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            lblBienvenida.Text = 
                "Bienvenido " + usuario.username + " [" + (usuario.userType ? "Administrador" : "Usuario") + "]";

            if (usuario.userType)
            {
                // Los administradores si tienen acceso a esta informacion
                configurarGrafico();
                actualizarControles();
            }
            else
            {
                // Los usuarios NO administradores no tienen permiso de acceder a estas pestanas
                tabContenedor.TabPages[1].Parent = null;
                tabContenedor.TabPages[1].Parent = null;
                tabContenedor.TabPages[1].Parent = null;
            }
        }

        private void actualizarControles()
        {
            // Realizar consulta a la base de datos
            List<Appuser> lista = ConsultarAppuser.getLista();
            
            // Tabla (data grid view)
            dgvEmpleados.DataSource = null;
            dgvEmpleados.DataSource = lista;
            List<Address> list = ConsultarAddress.getLista();

            datagrid.DataSource = null;
            datagrid.DataSource = list;

            List<Negocio> listaa = ConsultarNegocio.getLista();
            datanegocio.DataSource = null;
            datanegocio.DataSource=listaa;
            
            List<Producto> ayuda =ConsultarProducto.getLista();
            dataproducto.DataSource = null;
                        dataproducto.DataSource=ayuda;
            
                        List<Orden>helpme=ConsultarOrden.getLista();
                        dateOrden.DataSource = null;
                        dateOrden.DataSource=helpme;
                        
            // Menu desplegable (combo box)
            cmbuser.DataSource = null;
            cmbuser.ValueMember = "userid";
            cmbuser.DisplayMember = "username";
            cmbuser.DataSource = ConsultarAppuser.getLista();
            
            comboBox1.DataSource = null;
            comboBox1.ValueMember = "userid";
            comboBox1.DisplayMember = "username";
            comboBox1.DataSource = ConsultarAppuser.getLista();
            
           

             
            // Grafico con estadisticas
            poblarGrafico();
        }
        
        private void poblarControles()
        {
            // Actualizar ComboBox
            comboAdress.DataSource = null;
            comboAdress.ValueMember = "Idadress";
            comboAdress.DisplayMember = "address";
            comboAdress.DataSource = ConsultarAddress.getLista();
            
            cmbuser.DataSource = null;
            cmbuser.ValueMember = "userid";
            cmbuser.DisplayMember = "username";
             cmbuser.DataSource = ConsultarAppuser.getLista();
             
             comboBox1.DataSource = null;
             comboBox1.ValueMember = "userid";
             comboBox1.DisplayMember = "username";
             comboBox1.DataSource = ConsultarAppuser.getLista();
             
             combouserdirrecion.DataSource = null;
             combouserdirrecion.ValueMember = "userid";
             combouserdirrecion.DisplayMember = "username";
             combouserdirrecion.DataSource= ConsultarAppuser.getLista();
             
             combonegocio.DataSource = null;
             combonegocio.ValueMember = "idBusiness";
             combonegocio.DisplayMember = "name";
             combonegocio.DataSource= ConsultarNegocio.getLista();
             
             
             
             
            
            
        }
        private void configurarGrafico()
        {
            graficoEstadisticas.Top = 10;
            graficoEstadisticas.Left = 10;
            graficoEstadisticas.Width = graficoEstadisticas.Parent.Width - 20;
            graficoEstadisticas.Height = graficoEstadisticas.Parent.Height - 20;

            graficoEstadisticas.Series = new SeriesCollection
            {
                new ColumnSeries{Title = "Cantidad de inicios de sesion", Values = new ChartValues<int>(), DataLabels = true}
            };
            graficoEstadisticas.AxisX.Add(new Axis{Labels = new List<string>()});
            graficoEstadisticas.AxisX[0].Separator = new Separator() {Step = 1, IsEnabled = false};
            graficoEstadisticas.LegendLocation = LegendLocation.Top;
        }
        
        private void poblarGrafico()
        {
            graficoEstadisticas.Series[0].Values.Clear();
            graficoEstadisticas.AxisX[0].Labels.Clear();
            
            foreach (Frecuencia f in UsuarioDAO.getEstadisticas())
            {
                graficoEstadisticas.Series[0].Values.Add(f.cantidad);
                graficoEstadisticas.AxisX[0].Labels.Add(f.usuario);
            }
        }

        private void frmPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("¿Seguro que desea salir, " + usuario.fullname + "?", 
                "Clase GUI 05", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                try
                {
                    RegistroDAO.cerrarSesion(usuario.fullname);

                    // No se pone el App.Exit() aquí porque volvería a llamar al evento
                    // form closing, ejecutando 2 veces el message box
                    e.Cancel = false;
                }
                catch (Exception)
                {
                    MessageBox.Show("Ha sucedido un error, favor intente dentro de un minuto.", 
                        "Clase GUI 05", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void frmPrincipal_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Necesario porque el frmInicioSesion está escondido
            Application.Exit();
        }

        private void btnCrearUsuario_Click(object sender, EventArgs e)
        {
        
             Appuser c = new Appuser();
            c.fullname = txtfullname.Text;
            c.username = txtusername.Text;
            c.password= txtpassword.Text;
            
            
            try
            {
                // Enviar a modelo, el se encargara de almacenarlo en la BDD
                ConsultarAppuser.agregarAppuser(c);
                
                MessageBox.Show("Cliente agregado exitosamente", "Clase GUI 04",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                
              
            
                // Actualizar la vista, los ComboBox de la primer pestana
                actualizarControles();
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error: " + exception.Message, "Clase GUI 04",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            actualizarControles();
        }

        private void cmbUsuario_SelectedIndexChanged(object sender, EventArgs e)
        {
            Usuario u = (Usuario) comboBox1.SelectedItem;

            if (u.admin)
                radAdmin.Checked = true;
            else
                radUsuario.Checked = true;
            
            if (u.activo)
                radActivo.Checked = true;
            else
                radInactivo.Checked = true;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            UsuarioDAO.actualizarPermisos(comboBox1.Text, radAdmin.Checked, radActivo.Checked);
            
            MessageBox.Show("¡Usuario actualizado exitosamente!", 
                "Clase GUI 06", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            actualizarControles();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Seguro que desea eliminar al usuario " + comboBox1.Text + "?", 
                "Clase GUI 06", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                
                
                ConsultarAppuser.eliminar(comboBox1.Text);
                
                MessageBox.Show("¡Usuario eliminado exitosamente!", 
                    "Clase GUI 06", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
                actualizarControles();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void buttondirrecion_Click(object sender, EventArgs e)
        {
            Address c = new Address();

            int dato = 6;
            c.address= txtdirrecion.Text;
            c.idUser = dato;

            
            
            try
            {
                // Enviar a modelo, el se encargara de almacenarlo en la BDD
                ConsultarAddress.agregarAddress(c);
                
                MessageBox.Show("Cliente agregado exitosamente", "HugoApp",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                
              
            
                // Actualizar la vista, los ComboBox de la primer pestana
                actualizarControles();
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error: " + exception.Message, "HugoApp",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Negocio r = new Negocio();

           
            r.name= txtnegocio.Text;
            r.description = txtdes.Text;

            
            
            try
            {
                // Enviar a modelo, el se encargara de almacenarlo en la BDD
                ConsultarNegocio.agregarNegocio(r);
                
                MessageBox.Show("Cliente agregado exitosamente", "HugoApp",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                
              
            
                // Actualizar la vista, los ComboBox de la primer pestana
                actualizarControles();
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error: " + exception.Message, "HugoApp",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Actualizar_Click(object sender, EventArgs e)
        {
            actualizarControles();
        }

        private void button2_Click(object sender, EventArgs e)
        {
          Producto r = new Producto();

          int ahre = 5;
          r.idBusiness=ahre;
                      r.name= txtproducto.Text;
                      
                      
                      
                      try
                      {
                          // Enviar a modelo, el se encargara de almacenarlo en la BDD
                          ConsultarProducto.agregarProducto(r);
                          
                          MessageBox.Show("Cliente agregado exitosamente", "HugoApp",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
                          
                        
                      
                          // Actualizar la vista, los ComboBox de la primer pestana
                          actualizarControles();
                      }
                      catch (Exception exception)
                      {
                          MessageBox.Show("Error: " + exception.Message, "HugoApp",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                      }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            actualizarControles();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Orden r = new Orden();

            int ahre = 1;
            int ah=2;
            
            r.date= txtorden.Text;
            r.idProducto=ahre;
            r.idAddress = ah;
            
                      
                      
                      
            try
            {
                // Enviar a modelo, el se encargara de almacenarlo en la BDD
                ConsultarOrden.agregarOrden(r);
                          
                MessageBox.Show("Cliente agregado exitosamente", "HugoApp",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                          
                        
                      
                // Actualizar la vista, los ComboBox de la primer pestana
                actualizarControles();
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error: " + exception.Message, "HugoApp",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            actualizarControles();
        }
    }
}