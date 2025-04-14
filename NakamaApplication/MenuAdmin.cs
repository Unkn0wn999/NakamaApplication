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
    public partial class Menu : Form
    {
        private bool isSidebarExpanded = true;
        private int sidebarExpandedWidth = 200;
        private int sidebarCollapsedWidth = 50;


        public Menu()
        {
            InitializeComponent();
            

        }

        //private void splitContainer1_Panel1_MouseEnter(object sender, EventArgs e)
        //{
        //    ExpandirSidebar();
        //}

        //private void splitContainer1_Panel1_MouseLeave(object sender, EventArgs e)
        //{
        //    ColapsarSidebar();
        //}

        private void ExpandirSidebar()
        {
            splitContainer1.SplitterDistance = sidebarExpandedWidth;
            isSidebarExpanded = true;
            MostrarBotonesSidebar(true);
        }

        private void ColapsarSidebar()
        {
            splitContainer1.SplitterDistance = sidebarCollapsedWidth;
            isSidebarExpanded = false;
            MostrarBotonesSidebar(false);
        }

        private void MostrarBotonesSidebar(bool mostrar)
        {
            foreach (Control ctrl in splitContainer1.Panel1.Controls)
            {
                if (ctrl is Button btn && btn != btnToggleMenu)
                {
                    btn.Visible = mostrar;
                }
            }
        }

        private void btnToggleMenu_Click(object sender, EventArgs e)
        {
            if (isSidebarExpanded)
                ColapsarSidebar();
            else
                ExpandirSidebar();
        }

        private void btn_cerrarSesion_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        private void btn_rmotorizados_Click(object sender, EventArgs e)
        {
            RegistroMotorizados rmotorizados = new RegistroMotorizados();
            rmotorizados.Show();
            this.Hide();
        }

        private void btn_rvehiculos_Click(object sender, EventArgs e)
        {
            RegistroVehiculos rvehiculos = new RegistroVehiculos();
            rvehiculos.Show();
            this.Hide();
        }

        private void btn_rpedidos_Click(object sender, EventArgs e)
        {
            RegistroPedidos rpedidos = new RegistroPedidos();
            rpedidos.Show();
            this.Hide();
        }

        private void btn_cincidencias_Click(object sender, EventArgs e)
        {
            Controlncidencias cincidencias = new Controlncidencias();
            cincidencias.Show();
            this.Hide();
        }

        private void btn_hpedidos_Click(object sender, EventArgs e)
        {
            HistorialPedidos hpedidos = new HistorialPedidos();
            hpedidos.Show();
            this.Hide();
        }

        private void btn_gproductos_Click(object sender, EventArgs e)
        {
            GestionProductos gproductos = new GestionProductos();
            gproductos.Show();
            this.Hide();
        }

        private void btn_apedidos_Click(object sender, EventArgs e)
        {
            AsignacionPedidos apedidos = new AsignacionPedidos();
            apedidos.Show();
            this.Hide();
        }

        private void btn_salir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btn_minimizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
    }
}
