using System;
using System.Windows.Forms;
using ClaseGUI05.Controlador;
using ClaseGUI05.Modelo;

namespace ClaseGUI05
{
    public partial class frmInicioSesion : Form
    {
        public frmInicioSesion()
        {
            InitializeComponent();
        }

        private void frmInicioSesion_Load(object sender, EventArgs e)
        {
            poblarControles();
        }

        private void poblarControles()
        {
            // Actualizar ComboBox
            cmbUsuario.DataSource = null;
            cmbUsuario.ValueMember = "userid";
            cmbUsuario.DisplayMember = "username";
            cmbUsuario.DataSource = ConsultarAppuser.getLista();
        }
        
        

        private void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            if (Encriptador.CompararMD5(txtpassword.Text, cmbUsuario.SelectedValue.ToString()))
            {
               
                Appuser c = (Appuser) cmbUsuario.SelectedItem;

                if (c.userType)
                {
                    RegistroDAO.iniciarSesion(c.username);
                    
                    MessageBox.Show("¡Bienvenido!", 
                        "apphugo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    frmPrincipal ventana = new frmPrincipal();
                    ventana.Show();
                    this.Hide();
                }
                
            }
            else
                MessageBox.Show("¡Bienvenido!", "hugoapp",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                frmPrincipal aventana = new frmPrincipal();
                aventana.Show();
                this.Hide();
        }

        private void txtContrasena_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) btnIniciarSesion_Click(sender, e);
        }

        private void btnCambiarContra_Click(object sender, EventArgs e)
        {
            frmCambiarContra unaVentana = new frmCambiarContra();
            unaVentana.ShowDialog();
            poblarControles();
        }

        
    }
}