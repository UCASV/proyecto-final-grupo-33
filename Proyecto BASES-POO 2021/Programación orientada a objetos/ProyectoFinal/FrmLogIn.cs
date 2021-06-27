using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using ProyectoFinal.SQLContext;

namespace ProyectoFinal
{
    public partial class FrmLogIn : Form
    {
        public FrmLogIn()
        {
            InitializeComponent();
        }

        // CHECKING MANAGER INFO AND SHOWING FRMVACCINATIONPROCESS
        private void btnLogIn_Click(object sender, EventArgs e)
        {
            
            //CHECKIN INFO FROM THE MANAGER
            var db = new FinalProjectContext();
            List<Manager> managers = db.Managers
                .ToList();
            string userName = txtUser.Text;
            string password = txtPassword.Text;

            List<Manager> manager = managers
                .Where(u => u.UserEmployee == userName &&
                            u.PasswordManager == password)
                .ToList();
            if (manager.Count() > 0)
            {
                MessageBox.Show("Bienvenido al sistema gestor!", "Cabina COVID 19", MessageBoxButtons.OK);
                
                // SAVING INFO ABOUT MANAGER AND CABIN
                Manager c = (from x in db.Managers
                    where x.UserEmployee == txtUser.Text
                    select x).First();
                Managerxcabin newManagerxcabin = new Managerxcabin();
                newManagerxcabin.IdCabin = Int32.Parse(txtIdCabina.Text);
                newManagerxcabin.IdManagerNavigation = c;
                newManagerxcabin.DateTime = DateTime.Now;

                db.Add(newManagerxcabin);
                db.SaveChanges();
                
                // SHOWING FRMVACCIONATION PROCESS
                using (FrmVaccinationProcess vaccinationProcess = new FrmVaccinationProcess())
                {

                    DialogResult result = vaccinationProcess.ShowDialog();

                    if (result == DialogResult.OK)
                    {
                        MessageBox.Show("Se ha cerrado sesion corectamente!", "Cabina",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
            }
            else
            {
                MessageBox.Show("Usuario y/o clave incorrectos. Por favor verifique los datos ingresados.", "Inicio de sesion",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ASKING THE USER BEFORE CLOSING TIS FORM AND GOING BACK TO FORM1
        private void FrmLogIn_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Desea cerrar esta ventana y regresar a la ventana principal?", "Inicio de Sesion",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);

            if (result == DialogResult.Cancel)
            {
                this.Close();
            }
            this.DialogResult = DialogResult.OK;
        }
    }
}