using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoFinal
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        // SHOWING FRMLOGIN AND HIDING THIS ONE
        private void label3_Click(object sender, EventArgs e)
        {
            using (FrmLogIn logIn = new FrmLogIn())
            {
                this.Hide();
                DialogResult result = logIn.ShowDialog();

                if (result == DialogResult.OK)
                {
                    this.Show();
                }
            }
        }

        // SHOWING FRMMAKEAPPOINTMENT AND HIDING THIS ONE
        private void btnRegisterAppoCitizen_Click(object sender, EventArgs e)
        {
            using (FrmMakeAppointment makeAppointment = new FrmMakeAppointment())
            {
                this.Hide();
                DialogResult result = makeAppointment.ShowDialog();
                if (result == DialogResult.OK)
                {
                    this.Show();
                }
            }
        }
    }
}