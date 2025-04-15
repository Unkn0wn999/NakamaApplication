using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Drawing;
using System.Data.SqlClient;
using System.IO;

namespace NakamaApplication
{
    public partial class RegistroMotorizados : Form
    {
        public static byte[] Foto;
        public static string ruta = "";
        public RegistroMotorizados()
        {

            InitializeComponent();
            cb_tipoDoc.Items.Add("DNI");
            cb_tipoDoc.Items.Add("CE");
            cb_sexo.Items.Add("M");
            cb_sexo.Items.Add("F");
        }

        private void RegistroMotorizados_Load(object sender, EventArgs e)
        {
            ListarMotorizados();
        }
        private void btn_nuevo_Click(object sender, EventArgs e)
        {
            txt_nombres.Text = "";
            txt_primerApellido.Text = "";
            txt_segundoApellido.Text = "";
            txt_edad.Text = "";
            cb_tipoDoc.SelectedIndex = -1;
            txt_numDoc.Text = "";
            txt_numLic.Text = "";
            txt_nacionalidad.Text = "";
            cb_sexo.SelectedIndex = -1;
            txt_telf.Text = "";
            txt_email.Text = "";
            txt_nombres.Focus();
        }

        private void GuardarImagen(string ruta)
        {
            try
            {
                if (!string.IsNullOrEmpty(ruta) && ruta != "OpenFileDialog1")
                    pictureBox1.Image = Image.FromFile(ruta);
                else
                    pictureBox1.Image = Properties.Resources.Upload_Stack;

                using (MemoryStream ms = new MemoryStream())
                {
                    pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
                    Foto = ms.ToArray();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error con la imagen: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ListarMotorizados()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();

            try
            {
                ConexionBD.AbrirConexion();
                da = new SqlDataAdapter("Sp_ListarMotorizados", ConexionBD.con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;

                da.SelectCommand.Parameters.AddWithValue("@numeroDocumento", txt_buscar.Text.Trim());

                da.Fill(dt);
                dgv_motorizados.DataSource = dt;

                if (dgv_motorizados.Columns.Contains("Foto"))
                {
                    dgv_motorizados.Columns["Foto"].Visible = false;
                }

                ConexionBD.CerrarConexion();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al listar motorizados: " + ex.Message, "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_registrar_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            string msje = "";

            try
            {
                ConexionBD.AbrirConexion();
                cmd = new SqlCommand("Sp_RegistrarMotorizado", ConexionBD.con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nombres", txt_nombres.Text);
                cmd.Parameters.AddWithValue("@primerApellido", txt_primerApellido.Text);
                cmd.Parameters.AddWithValue("@segundoApellido", txt_segundoApellido.Text);
                cmd.Parameters.AddWithValue("@edad", txt_edad.Text);
                cmd.Parameters.AddWithValue("@tipoDocumento", cb_tipoDoc.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@numeroDocumento", txt_numDoc.Text);
                cmd.Parameters.AddWithValue("@numeroLicencia", txt_numLic.Text);
                cmd.Parameters.AddWithValue("@nacionalidad", txt_nacionalidad.Text);
                cmd.Parameters.AddWithValue("@sexo", cb_sexo.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@telefono", txt_telf.Text);
                cmd.Parameters.AddWithValue("@email", txt_email.Text);
                cmd.Parameters.AddWithValue("@Foto", Foto);
                GuardarImagen(ruta);

                cmd.Parameters.Add("@msje", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                msje = cmd.Parameters["@msje"].Value.ToString();
                MessageBox.Show(msje, "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ConexionBD.CerrarConexion();
                ListarMotorizados();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "Sistema" +
                    "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            ofd.ShowDialog();
        }

        private void btn_modificar_Click(object sender, EventArgs e)
        {
            if (dgv_motorizados.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un motorizado para modificar.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                GuardarImagen(ruta); // Actualiza la imagen

                int idMotorizado = Convert.ToInt32(dgv_motorizados.CurrentRow.Cells["IdMotorizado"].Value);

                SqlCommand cmd = new SqlCommand("Sp_ActualizarMotorizado", ConexionBD.con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@idMotorizado", idMotorizado);
                cmd.Parameters.AddWithValue("@nombres", txt_nombres.Text);
                cmd.Parameters.AddWithValue("@primerApellido", txt_primerApellido.Text);
                cmd.Parameters.AddWithValue("@segundoApellido", txt_segundoApellido.Text);
                cmd.Parameters.AddWithValue("@edad", int.Parse(txt_edad.Text));
                cmd.Parameters.AddWithValue("@tipoDocumento", cb_tipoDoc.SelectedItem?.ToString() ?? "");
                cmd.Parameters.AddWithValue("@numeroDocumento", txt_numDoc.Text);
                cmd.Parameters.AddWithValue("@numeroLicencia", txt_numLic.Text);
                cmd.Parameters.AddWithValue("@nacionalidad", txt_nacionalidad.Text);
                cmd.Parameters.AddWithValue("@sexo", cb_sexo.SelectedItem?.ToString() ?? "");
                cmd.Parameters.AddWithValue("@telefono", txt_telf.Text);
                cmd.Parameters.AddWithValue("@email", txt_email.Text);
                cmd.Parameters.AddWithValue("@foto", Foto);

                ConexionBD.AbrirConexion();
                cmd.ExecuteNonQuery();
                ConexionBD.CerrarConexion();

                MessageBox.Show("Datos actualizados correctamente.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ListarMotorizados();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar: " + ex.Message, "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_eliminar_Click(object sender, EventArgs e)
        {
            if (dgv_motorizados.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un motorizado para eliminar.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("¿Está seguro de eliminar este motorizado?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
                return;

            try
            {
                int idMotorizado = Convert.ToInt32(dgv_motorizados.CurrentRow.Cells["IdMotorizado"].Value);

                SqlCommand cmd = new SqlCommand("Sp_EliminarMotorizado", ConexionBD.con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idMotorizado", idMotorizado);

                ConexionBD.AbrirConexion();
                cmd.ExecuteNonQuery();
                ConexionBD.CerrarConexion();

                MessageBox.Show("Motorizado eliminado correctamente.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ListarMotorizados();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar: " + ex.Message, "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_salir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btn_minimizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btn_menu_Click(object sender, EventArgs e)
        {
            Menu menu = new Menu();
            menu.ShowDialog();
            this.Hide();
        }

        private void txt_buscar_TextChanged(object sender, EventArgs e)
        {
            ListarMotorizados();
        }
    }
}
