using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NakamaApplication
{
    public partial class RegistroVehiculos : Form
    {
        public RegistroVehiculos()
        {
            InitializeComponent();
            cb_tipoV.Items.Add("Mecanico");
            cb_tipoV.Items.Add("Automatico");
        }

        private void RegistroVehiculos_Load(object sender, EventArgs e)
        {
            ListarVehiculos();
        }

        private void ListarVehiculos()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();

            try
            {
                ConexionBD.AbrirConexion();
                da = new SqlDataAdapter("Sp_ListarVehiculos", ConexionBD.con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@nroPlaca", txt_buscar.Text.Trim());

                da.Fill(dt);
                dgv_vehiculos.DataSource = dt;

                ConexionBD.CerrarConexion();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al listar vehículos: " + ex.Message, "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btn_nuevo_Click(object sender, EventArgs e)
        {

        }

        private void btn_eliminar_Click(object sender, EventArgs e)
        {

        }

        private void btn_modificar_Click(object sender, EventArgs e)
        {

        }


        private void btn_registrar_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            string msje = "";

            try
            {
                ConexionBD.AbrirConexion();
                cmd = new SqlCommand("Sp_RegistrarVehiculo", ConexionBD.con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@tipoVehiculo", cb_tipoV.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@marca", txt_marca.Text);
                cmd.Parameters.AddWithValue("@modelo", txt_modelo.Text);
                cmd.Parameters.AddWithValue("@color", txt_color.Text);
                cmd.Parameters.AddWithValue("@nroPlaca", txt_nplaca.Text);
                cmd.Parameters.AddWithValue("@nroSerie", txt_nserie.Text);
                cmd.Parameters.AddWithValue("@nroVin", txt_nvin.Text);
                cmd.Parameters.AddWithValue("@nroMotor", txt_nmotor.Text);
                cmd.Parameters.AddWithValue("@propietario", txt_propietario.Text);
                cmd.Parameters.AddWithValue("@estado", txt_estado.Text);
                cmd.Parameters.AddWithValue("@anioModelo", int.Parse(txt_amodelo.Text));

                cmd.Parameters.Add("@msje", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                msje = cmd.Parameters["@msje"].Value.ToString();
                MessageBox.Show(msje, "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ConexionBD.CerrarConexion();
                ListarVehiculos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        

        private void btn_siguiente_Click(object sender, EventArgs e)
        {

        }

        private void btn_salir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btn_menu_Click(object sender, EventArgs e)
        {
            Menu menu = new Menu();
            menu.ShowDialog();
            this.Hide();
        }
    }
}
