using ParcialDeYagoTorres.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ParcialDeJohnDoe.Windows
{
    public partial class frmOrtoedroAE : Form
    {
        public frmOrtoedroAE()
        {
            InitializeComponent();
        }
        private ortoedro ortoedro;
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            CargarComboColores();
            if (ortoedro != null)
            {
                AristaTextBox.Text = ortoedro.Arista.ToString();
               
                RellenoComboBox.SelectedItem = (Relleno)ortoedro.Relleno;
            }
        }

        private void CargarComboColores()
        {
            RellenoComboBox.DataSource = Enum.GetValues(typeof(Relleno));
            RellenoComboBox.SelectedIndex = 0;
        }

        public ortoedro GetOrtoedro()
        {
            return ortoedro;
        }

          public void SetOrtoedro(ortoedro ortoedro)
          {
            this.ortoedro = ortoedro;
          }
        
           
        

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                if (ortoedro == null)
                {
                    ortoedro = new ortoedro();
                }

                ortoedro.Arista = int.Parse(AristaTextBox.Text);
                ortoedro.Relleno = (Relleno)RellenoComboBox.SelectedItem;
               

                if (ortoedro.Validar())
                {
                    DialogResult = DialogResult.OK;
                }
                errorProvider1.SetError(AristaTextBox, "La arista debe ser positiva");

            }
        }

        private bool ValidarDatos()
        {
            bool valido = true;
            errorProvider1.Clear();
            if (!int.TryParse(AristaTextBox.Text, out int radio))
            {
                valido = false;
                errorProvider1.SetError(AristaTextBox, "Arista mal ingresada");
            }

            return valido;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void Arista1TextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void Arista2TextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void Arista3TextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
    }
}
