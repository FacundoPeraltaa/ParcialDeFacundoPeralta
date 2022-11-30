using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ParcialDeYagoTorres.Entidades;
using ParcialDeYagoTorres.Datos;
namespace ParcialDeJohnDoe.Windows
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }
        private RepositorioDeCubos repositorio;
        private List<Cubo> lista;
        private void frmPrincipal_Load(object sender, EventArgs e)
        {

            CargarTrazos();
            repositorio = new RepositorioDeCubos();
            var cantidad = repositorio.GetCantidad();
            MostrarCantidad(cantidad);

            if (cantidad > 0)
            {
                lista = repositorio.GetLista();
                MostrarLista();
            }
            else
            {
                MessageBox.Show("No hay elementos en el repositorio",
                  "Mensaje",
                  MessageBoxButtons.OK,
                  MessageBoxIcon.Exclamation);
            }
        }

        private void CargarTrazos()
        {
            var listaTrazos = Enum.GetValues(typeof(Trazo)).Cast<Trazo>().ToList();
            foreach (var trazo in listaTrazos)
            {
                FiltroToolStripComboBox.Items.Add(trazo);
            }
        }

        private void MostrarLista()
        {
            dgvDatos.Rows.Clear();
            foreach (var cubo in lista)
            {
                var r = ConstruirFila();
                SetearFila(r, cubo);
                AgregarFila(r);
            }
        }

        private void AgregarFila(DataGridViewRow r)
        {
            dgvDatos.Rows.Add(r);
        }

        private void SetearFila(DataGridViewRow r, Cubo cubo)
        {
            r.Cells[colArista.Index].Value = cubo.Arista;
            r.Cells[colRelleno.Index].Value = cubo.Relleno.ToString();
            r.Cells[colTrazo.Index].Value = cubo.Trazo.ToString();
            r.Cells[colVolumen.Index].Value = cubo.GetVolumen().ToString("N2");
            r.Cells[colArea.Index].Value = cubo.GetArea().ToString("N2");

            r.Tag = cubo;
        }

        private DataGridViewRow ConstruirFila()
        {
            DataGridViewRow r = new DataGridViewRow();
            r.CreateCells(dgvDatos);
            return r;
        }

        private void MostrarCantidad(int cantidad)
        {
            CantidadTextBox.Text = cantidad.ToString();
        }

        private void tsbNuevo_Click(object sender, EventArgs e)
        {
            frmOrtoedroAE frm = new frmOrtoedroAE() { Text = "Agregar nuevo cubo" };
            DialogResult dr = frm.ShowDialog(this);
            if (dr == DialogResult.Cancel)
            {
                return;
            }

            Cubo cubo = frm.GetCubo();
            repositorio.Agregar(cubo);
            DataGridViewRow r = ConstruirFila();
            SetearFila(r, cubo);
            AgregarFila(r);
            MostrarCantidad(repositorio.GetCantidad());
        }

        private void tsbBorrar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.SelectedRows.Count == 0)
            {
                return;
            }

            var r = dgvDatos.SelectedRows[0];
            Cubo cubo = (Cubo)r.Tag;
            DialogResult dr = MessageBox.Show($"¿Desea borrar el cubo de Arista {cubo.Arista}?",
                "Confirmar Operación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);
            if (dr == DialogResult.No)
            {
                return;
            }
            repositorio.Borrar(cubo);
            dgvDatos.Rows.Remove(r);
        }

        private void tsbEditar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.SelectedRows.Count == 0)
            {
                return;
            }
            var r = dgvDatos.SelectedRows[0];
            Cubo cubo = (Cubo)r.Tag;
            frmOrtoedroAE frm = new frmOrtoedroAE() { Text = "Editar cubo" };
            frm.SetCubo(cubo);
            DialogResult dr = frm.ShowDialog(this);
            if (dr == DialogResult.Cancel)
            {

                return;
            }

            cubo = frm.GetCubo();
            repositorio.Editar(cubo);
            SetearFila(r, cubo);
        }

        private void ascendenteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lista = repositorio.GetListaOrdenadaAsc();
            MostrarLista();
        }

        private void descendenteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lista = repositorio.GetListaOrdenadaDesc();
            MostrarLista();
        }

        private void FiltroToolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var TrazoFiltrar = (Trazo)FiltroToolStripComboBox.ComboBox.SelectedItem;
            lista = repositorio.FiltrarPorTrazo(TrazoFiltrar);
            MostrarLista();
        }

        private void tsbRefrescar_Click(object sender, EventArgs e)
        {
            lista = repositorio.GetLista();
            MostrarLista();
        }

        private void tsbSalir_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show($"¿Desea guardar antes de salir?", "Confirmación de operación", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3);
            if (dialogResult == DialogResult.Yes)
            {
                repositorio.GuardarEnArchivo();
                Application.Exit();
            }
            else if (dialogResult == DialogResult.No)
            {
                Application.Exit();
            }
            else
            {
                return;
            }
        }
    }
}
