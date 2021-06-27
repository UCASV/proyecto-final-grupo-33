using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using ProyectoFinal.SQLContext;
using System.Data.SqlClient;



namespace ProyectoFinal
{
    public partial class FrmMakeAppointment : Form
    {
        // VARS FOR HOURS
        private int timeHour;
        private int timeMinutes;
        private string finalHour;

        public FrmMakeAppointment()
        {
            InitializeComponent();

        }

        // RANDOM DATE
        private Random gen = new Random();

        DateTime RandomDay()
        {

            DateTime start = new DateTime();
            start = DateTime.Today;
            DateTime end = new DateTime(2021, 7, 30);
            int range = (end - start).Days;
            return start.AddDays(gen.Next(range));
        }

        private void FrmMakeAppointment_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Desea cerrar esta ventana y regresar a la ventana principal?",
                "Registro Cita",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);

            if (result == DialogResult.Cancel)
            {
                this.Close();
            }

            this.DialogResult = DialogResult.OK;
        }

        // MAKING THE APPOINTMENT AND SAVING THE CITIZEN INFO
        private void btnSaveCitizenAppointment_Click(object sender, EventArgs e)
        {
            if (txtDuiCitizen.Text.Length == 9 && txtAdressCitizen.Text.Length > 0 && txtEmailCitizen.Text.Length > 0 &&
                txtFullnameCitizen.Text.Length > 0)
            {
                DialogResult result = MessageBox.Show(
                    "Esta seguro que desea realizar la cita con los datos proporcionados\n Una vez hecha, la cita no podra modificarse.",
                    "Seguridad", MessageBoxButtons.YesNo);


                if (result == DialogResult.Yes)
                {
                    var db = new FinalProjectContext();

                    int idDepa = cmbDepartamentCitizen.SelectedIndex;
                    idDepa += 1;
                    int idTypeC = cmbTypeCitizen.SelectedIndex;
                    idTypeC += 1;

                    Cabin cabin = (Cabin) cmbCabinCitizen.SelectedItem;
                    Cabin cabi = db.Set<Cabin>()
                        .SingleOrDefault(c => c.Id == cabin.Id);

                    Municipality municipality = (Municipality) cmbMunicipalityCitizen.SelectedItem;

                    Municipality muni = db.Set<Municipality>()
                        .SingleOrDefault(m => m.Id == municipality.Id);


                    // SAVING CITIZEN INFO
                    Citizen newCitizen = new Citizen();
                    newCitizen.Dui = Int32.Parse(txtDuiCitizen.Text);
                    newCitizen.Addres = txtAdressCitizen.Text;
                    newCitizen.Email = txtEmailCitizen.Text;
                    newCitizen.Fullname = txtFullnameCitizen.Text;
                    newCitizen.Phone = txtPhoneCitizen.Text;
                    newCitizen.IdDepartment = idDepa;
                    newCitizen.IdMunicipalityNavigation = muni;
                    newCitizen.IdTypeCitizen = idTypeC;

                    // TIME RANDOM
                    Random random = new Random();
                    timeHour = random.Next(9, 14);
                    timeMinutes = random.Next(10, 50);

                    finalHour = timeHour.ToString() + ":" + timeMinutes.ToString();

                    // SANVING APPOINTMENT INFO
                    Appointment newAppointment = new Appointment();
                    newAppointment.DateAppointment = RandomDay();
                    ;
                    newAppointment.HourAppointment = finalHour;
                    newAppointment.IdCabinNavigation = cabi;
                    newAppointment.IdCitizen = Int32.Parse(txtDuiCitizen.Text);

                    // SAVING DISEASES
                    if (cbx1.Checked)
                    {
                        Disease newDisease = new Disease();

                        newDisease.Diseases = cbx1.Text;
                        newDisease.DuiCitizen = Int32.Parse(txtDuiCitizen.Text);
                        db.Add(newDisease);
                    }

                    if (cbx2.Checked)
                    {
                        Disease newDisease2 = new Disease();
                        newDisease2.Diseases = cbx2.Text;
                        newDisease2.DuiCitizen = Int32.Parse(txtDuiCitizen.Text);
                        db.Add(newDisease2);
                    }

                    if (cbx3.Checked)
                    {
                        Disease newDisease3 = new Disease();
                        newDisease3.Diseases = cbx3.Text;
                        newDisease3.DuiCitizen = Int32.Parse(txtDuiCitizen.Text);
                        db.Add(newDisease3);
                    }

                    if (cbx4.Checked)
                    {
                        Disease newDisease4 = new Disease();
                        newDisease4.Diseases = cbx4.Text;
                        newDisease4.DuiCitizen = Int32.Parse(txtDuiCitizen.Text);
                        db.Add(newDisease4);
                    }

                    if (cbx5.Checked)
                    {
                        Disease newDisease5 = new Disease();
                        newDisease5.Diseases = cbx5.Text;
                        newDisease5.DuiCitizen = Int32.Parse(txtDuiCitizen.Text);
                        db.Add(newDisease5);
                    }

                    if (cbx6.Checked)
                    {
                        Disease newDisease6 = new Disease();
                        newDisease6.Diseases = cbx6.Text;
                        newDisease6.DuiCitizen = Int32.Parse(txtDuiCitizen.Text);
                        db.Add(newDisease6);
                    }


                    // SAVING DATA INTO DB
                    db.Add(newCitizen);
                    db.Add(newAppointment);
                    db.SaveChanges();

                    MessageBox.Show("Cita registrada con exito", "Cita Registrada",
                        MessageBoxButtons.OK);
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("DUI No valido y/o datos imcompletos!", "Datos Incompletos!",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        // COMBO BOX
        private void FrmMakeAppointment_Load(object sender, EventArgs e)
        {
            var db = new FinalProjectContext();

            // COMBO BOX DEPARTMENT
            cmbDepartamentCitizen.DataSource = db.Departments.ToList();
            cmbDepartamentCitizen.DisplayMember = "Department1";
            cmbDepartamentCitizen.ValueMember = "Id";

            // COMBO BOX TYPE CITIZEN
            cmbTypeCitizen.DataSource = db.TypeCitizens.ToList();
            cmbTypeCitizen.DisplayMember = "TypeCitizen1";
            cmbTypeCitizen.ValueMember = "Id";

        }

        // CONFIGURING DEPENDABLE COMBO BOX
            private void cmbDepartamentCitizen_SelectedIndexChanged(object sender, EventArgs e)
            {
                // COMBO BOX MUNICIPALITY
                var db = new FinalProjectContext();
                int idDepa = cmbDepartamentCitizen.SelectedIndex;
                idDepa += 1;

                cmbMunicipalityCitizen.DataSource = db.Municipalities
                    .Where(mu => mu.IdDepartment == idDepa)
                    .ToList();
                cmbMunicipalityCitizen.DisplayMember = "Municipality1";
                cmbMunicipalityCitizen.ValueMember = "Id";

                // COMBO BOX CABIN 
                cmbCabinCitizen.DataSource = db.Cabins
                    .Where(mu => mu.IdDepartment == idDepa)
                    .ToList();
                cmbCabinCitizen.DisplayMember = "Addres";
                cmbCabinCitizen.ValueMember = "Id";


            }
        }
    }
