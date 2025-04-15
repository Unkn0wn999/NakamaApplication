using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NakamaApplication
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btn_ingresar_Click(object sender, EventArgs e)
        {
            string usuario = txt_usuario.Text.ToLower();
            string contraseña = txt_contraseña.Text.ToLower();
            string cargo = cb_cargo.Text.ToLower();

            if (txt_usuario.Text == "" || txt_contraseña.Text == "" || cb_cargo.Text == "")
            {
                MessageBox.Show("Complete todos los campos.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if ((usuario == "admin" && contraseña == "admin" && cargo == "administrador") ||
                (usuario == "usuario" && contraseña == "usuario" && cargo == "motorizado"))
            {
                this.Hide();
                Menu menuadm = new Menu();
                menuadm.ShowDialog();
                this.Close();
            }

            else if (cargo != "administrador" && cargo != "motorizado")
            {
                MessageBox.Show("Seleccione un cargo válido", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            else
            {
                MessageBox.Show("Usuario, contraseña o cargo incorrectos.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_salir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

