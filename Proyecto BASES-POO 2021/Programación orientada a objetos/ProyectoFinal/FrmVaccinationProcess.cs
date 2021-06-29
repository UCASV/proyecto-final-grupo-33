<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using ProyectoFinal.SQLContext;
using System.IO;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using iTextSharp.text.pdf;
using iTextSharp.text;
using Org.BouncyCastle.Asn1.Cms;

namespace ProyectoFinal
{
    public partial class FrmVaccinationProcess : Form
    {
        // VARS FOR COUNTING THE PEOPLE THAT HAS BEEN VACCINATED 
        private int totalPeopleFirstDosis;
        private int totalPeopleSecondDosis;
        private int totalPeopleVaccinated; // TOTAL VACCINATED
        
        
        // VARS FOR HOURS
        private int timeHour;
        private int timeMinutes;
        private string finalHour;
        public FrmVaccinationProcess()
        {
            InitializeComponent();
        }

        private void FrmVaccinationProcess_Load(object sender, EventArgs e)
        {
            var db = new FinalProjectContext();

            // COMBO BOX DEPARTMENT
            cmbDepartament.DataSource = db.Departments.ToList();
            cmbDepartament.DisplayMember = "Department1";
            cmbDepartament.ValueMember = "Id";

            // COMBO BOX TYPE CITIZEN
            cmbTypeCitizen.DataSource = db.TypeCitizens.ToList();
            cmbTypeCitizen.DisplayMember = "TypeCitizen1";
            cmbTypeCitizen.ValueMember = "Id";

        }

        // SAVING CITIZEN INFO
        private void btm_record_data_Click(object sender, EventArgs e)
        {
            if (txtDui.Text.Length == 9 && txtName_Citizen.Text.Length > 0 && txtPhone.Text.Length > 0 &&
                txtAdress_Citizen.Text.Length > 0 && txtEmail_Citizen.Text.Length > 0)
            {
                DialogResult result = MessageBox.Show(
                    "Esta seguro que desea registrar el ciudadano con los datos proporcionados\n Una vez hecha, la cita no podra modificarse.",
                    "Seguridad", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    var db = new FinalProjectContext();

                    int idDepa = cmbDepartament.SelectedIndex;
                    idDepa += 1;
                    int idType = cmbTypeCitizen.SelectedIndex;
                    idType += 1;

                    Municipality municipality = (Municipality) cmbMunicipality.SelectedItem;

                    Municipality muni = db.Set<Municipality>()
                        .SingleOrDefault(m => m.Id == municipality.Id);

                    // SETTING CITIZEN INFO 
                    Citizen newCitizen = new Citizen();
                    newCitizen.Dui = Int32.Parse(txtDui.Text);
                    newCitizen.Fullname = txtName_Citizen.Text;
                    newCitizen.Email = txtEmail_Citizen.Text;
                    newCitizen.Phone = txtPhone.Text;
                    newCitizen.Addres = txtAdress_Citizen.Text;
                    newCitizen.IdDepartment = idDepa;
                    newCitizen.IdMunicipalityNavigation = muni;
                    newCitizen.IdTypeCitizen = idType;

                    // SAVING DISEASES
                    if (cbx2.Checked)
                    {
                        Disease newDisease2 = new Disease();
                        newDisease2.Diseases = cbx2.Text;
                        newDisease2.DuiCitizen = Int32.Parse(txtDui.Text);
                        db.Add(newDisease2);
                    }

                    if (cbx3.Checked)
                    {
                        Disease newDisease3 = new Disease();
                        newDisease3.Diseases = cbx3.Text;
                        newDisease3.DuiCitizen = Int32.Parse(txtDui.Text);
                        db.Add(newDisease3);
                    }

                    if (cbx4.Checked)
                    {
                        Disease newDisease4 = new Disease();
                        newDisease4.Diseases = cbx4.Text;
                        newDisease4.DuiCitizen = Int32.Parse(txtDui.Text);
                        db.Add(newDisease4);
                    }

                    if (cbx5.Checked)
                    {
                        Disease newDisease5 = new Disease();
                        newDisease5.Diseases = cbx5.Text;
                        newDisease5.DuiCitizen = Int32.Parse(txtDui.Text);
                        db.Add(newDisease5);
                    }

                    if (cbx6.Checked)
                    {
                        Disease newDisease6 = new Disease();
                        newDisease6.Diseases = cbx6.Text;
                        newDisease6.DuiCitizen = Int32.Parse(txtDui.Text);
                        db.Add(newDisease6);
                    }


                    // ADDING NEW CITIZEN AND DISEASES TO THE DATABASE 
                    db.Add(newCitizen);
                    db.SaveChanges();

                    MessageBox.Show("Ciudadano registrado con exito!", "Ciudadano registrado",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // SHOWING NEXT TAB
                    tab1.SelectedIndex = 1;

                }
            }
            else
            {
                MessageBox.Show("Datos incompletos y/o DUI invalido. Verifique los datos ingresados", "Datos Incompletos!",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        // DEPENDABLE COMBO BOX MUNICIPALITY
        private void cmbDepartament_SelectedIndexChanged(object sender, EventArgs e)
        {
            // COMBO BOX MUNICIPALITY
            var db = new FinalProjectContext();
            int idDepa = cmbDepartament.SelectedIndex;
            idDepa += 1;

            cmbMunicipality.DataSource = db.Municipalities
                .Where(mu => mu.IdDepartment == idDepa)
                .ToList();
            cmbMunicipality.DisplayMember = "Municipality1";
            cmbMunicipality.ValueMember = "Id";

            // COMBO BOX CABIN 
            cmb_vaccination_place.DataSource = db.Cabins
                .Where(mu => mu.IdDepartment == idDepa)
                .ToList();
            cmb_vaccination_place.DisplayMember = "Addres";
            cmb_vaccination_place.ValueMember = "Id";
        }

        // CLOSING WINDOW MESSAGES
        private void FrmVaccinationProcess_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Desea cerrar esta ventana y regresar a la ventana de inicio de sesion?\nSi lo hace se cerrara sesion automaticamente",
                "Registro Cita", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);

            if (result == DialogResult.Cancel)
            {
                this.Close();
            }

            this.DialogResult = DialogResult.OK;
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
        
        
        
        // SAVING APPOINTMENTO INFO AND DATAGRIDVIEW
        private void btnSaveAppointment_Click(object sender, EventArgs e)
        {
            if (txtDui.Text.Length == 9 && txtName_Citizen.Text.Length > 0 && txtPhone.Text.Length > 0 &&
                txtAdress_Citizen.Text.Length > 0 && txtEmail_Citizen.Text.Length > 0)
            {
                DialogResult result = MessageBox.Show(
                    "Esta seguro que desea registrar esta cita? Una vez hecha no podra editarse.",
                    "Seguridad", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    var db = new FinalProjectContext();

                    Cabin cabin = (Cabin) cmb_vaccination_place.SelectedItem;

                    Cabin cabi = db.Set<Cabin>()
                        .SingleOrDefault(m => m.Id == cabin.Id);

                    // TIME RANDOM
                    Random random = new Random();
                    timeHour = random.Next(9, 14);
                    timeMinutes = random.Next(10, 50);

                    finalHour = timeHour.ToString() + ":" + timeMinutes.ToString();


                    // DATE FOR THE APPOINTMENT
                    Appointment newAppointment = new Appointment();
                    newAppointment.DateAppointment = RandomDay();
                    newAppointment.HourAppointment = finalHour;
                    newAppointment.IdCabinNavigation = cabi;
                    newAppointment.IdCitizen = Int32.Parse(txtDui.Text);

                    db.Add(newAppointment);
                    db.SaveChanges();

                    MessageBox.Show("Cita registrada con exito", "Cita Registrada",
                        MessageBoxButtons.OK);

                    // ADDING INFO TO DATA GRID VIEW 
                    dataGridView1.DataSource = null;
                    var lisAppoitmet = db.Appointments.Include(r => r.IdCabinNavigation)
                        .Include(r => r.IdCitizenNavigation)
                        .Select(r => new
                            {
                                Fecha_cita = r.DateAppointment,
                                Hora_cita = r.HourAppointment,
                                Ciudadano = r.IdCitizenNavigation.Fullname,
                                Dui_Usuario = r.IdCitizenNavigation.Dui,
                                Dirección_cabina = r.IdCabinNavigation.Addres
                            }

                        ).Where(r => r.Dui_Usuario == Int32.Parse(txtDui.Text)).ToList();
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = lisAppoitmet;

                    tab1.SelectedIndex = 2;
                }
            }
            else
            {
                MessageBox.Show("Datos incompletos y/o DUI invalido. Asegurese de haber llenado el formulario de ciudadano antes\nde hacer la cita.", "Datos Incompletos!",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        
        // CHECKING IF THE CITIZEN HAS ALREADY A APPOINTMENT REGISTERED
        private void btn_check_Click(object sender, EventArgs e)
        {
            if (txt_dui_check.Text.Length == 9)
            {
                var db = new FinalProjectContext();

                // APPOINTMENT TABLE & SEARCHING SUCH AN APPOINTMENT WITH THAT DUI
                List<Appointment> appointments = db.Appointments
                    .ToList();
                List<Appointment> appointment = appointments
                    .Where(a => a.IdCitizen == Int32.Parse(txt_dui_check.Text))
                    .ToList();

                if (appointment.Count() > 0)
                {
                    dataGridView2.DataSource = null;

                    var lisAppoitmet = db.Appointments
                        .Include(r => r.IdCabinNavigation)
                        .Include(r => r.IdCitizenNavigation)
                        .Select(r => new
                            {
                                Fecha = r.DateAppointment,
                                Hora_cita = r.HourAppointment,
                                Dui_Usuario = r.IdCitizenNavigation.Dui,
                                Dirección_cabina = r.IdCabinNavigation.Addres
                            }

                        ).Where(r => r.Dui_Usuario == Int32.Parse(txt_dui_check.Text)).ToList();
                    dataGridView2.DataSource = null;
                    dataGridView2.DataSource = lisAppoitmet;
                }
                else
                {
                    DialogResult result = MessageBox.Show(
                        "No existe una cita hecha para dicho ciudadano!\nPresione 'Aceptar' para ir a la ventana de datos y registrar una cita para el ciudadano.",
                        "Sin registros", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    if (result == DialogResult.OK)
                    {
                        tab1.SelectedIndex = 0;
                    }
                }
            }
            else
            {
                DialogResult result = MessageBox.Show(
                    "No ha ingresado un DUI, ingrese uno para verificar",
                    "No datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        // SAVING INFO ABOUT CITIZEN VACUNATION 
        private void btnRegisterInfoCitizen_Click(object sender, EventArgs e)
        {
            DialogResult result1 = MessageBox.Show(
                "Esta seguro que desea guardar los datos proporcionados\n Una vez hecho, no podran modificarse.",
                "Seguridad", MessageBoxButtons.YesNo);

            if (result1 == DialogResult.Yes && txtDuiCitizen.Text.Length == 9 && txtHourArrived.Text.Length > 0 &&
                txtTimeVaccunated.Text.Length > 0)
            {
                int idCitizen = Int32.Parse(txtDuiCitizen.Text);
                
                var db = new FinalProjectContext();
                List<Appointment> appointments = db.Appointments
                    .Include(u => u.IdCabinNavigation)
                    .Include(u => u.IdCitizenNavigation)
                    .ToList();

                List<Appointment> result = appointments
                    .Where(u => u.IdCitizen == idCitizen)
                    .ToList();

                if (result.Count() == 1)
                {
                    Appointment a = (from x in db.Appointments
                        where x.IdCitizen == idCitizen
                        select x).First();

                    a.HourArrived = txtHourArrived.Text;
                    a.HourVaccunated = txtTimeVaccunated.Text;
                    
                    // RANDOM SECOND DOSIS
                    Random gen = new Random();
                    DateTime newDate = Convert.ToDateTime(a.DateAppointment);
                    newDate = newDate.AddDays(42);
                    DateTime finalDate = Convert.ToDateTime(a.DateAppointment);
                    finalDate = finalDate.AddDays(56);
                    int range = (finalDate - newDate).Days;
                    newDate = newDate.AddDays((gen.Next((range))));
                    
                    // TIME RANDOM
                    Random random = new Random();
                    timeHour = random.Next(9, 14);
                    timeMinutes = random.Next(10, 50);

                    finalHour = timeHour.ToString() + ":" + timeMinutes.ToString();
                    
                    // MAKING SECOND APPOINTMENT FOR DOSIS
                    Appointment secondAppointment = new Appointment();
                    secondAppointment.DateAppointment = newDate;
                    secondAppointment.HourAppointment = finalHour;
                    secondAppointment.IdCitizen = a.IdCitizen;
                    secondAppointment.IdCabin = a.IdCabin;
                    db.Add((secondAppointment));
                    

                    if (cbx7.Checked) 
                    {
                        SideEffect newSideEffect = new SideEffect();
                        newSideEffect.SideEffect1 = cbx7.Text;
                        newSideEffect.DuiCitizen = Int32.Parse(txtDuiCitizen.Text);
                        db.Add(newSideEffect);
                    }
                    if (cbx8.Checked)
                    {
                        SideEffect newSideEffect2 = new SideEffect();
                        newSideEffect2.SideEffect1 = cbx8.Text;
                        newSideEffect2.DuiCitizen = Int32.Parse(txtDuiCitizen.Text);
                        db.Add(newSideEffect2);
                    }
                    if (cbx9.Checked)
                    {
                        SideEffect newSideEffect3 = new SideEffect();
                        newSideEffect3.SideEffect1 = cbx9.Text;
                        newSideEffect3.DuiCitizen = Int32.Parse(txtDuiCitizen.Text);
                        db.Add(newSideEffect3);
                    }
                    if (cbx10.Checked)
                    {
                        SideEffect newSideEffect4 = new SideEffect();
                        newSideEffect4.SideEffect1 = cbx10.Text;
                        newSideEffect4.DuiCitizen = Int32.Parse(txtDuiCitizen.Text);
                        db.Add(newSideEffect4);
                    }
                    if (cbx11.Checked)
                    {
                        SideEffect newSideEffect5 = new SideEffect();
                        newSideEffect5.SideEffect1 = cbx11.Text;
                        newSideEffect5.DuiCitizen = Int32.Parse(txtDuiCitizen.Text);
                        db.Add(newSideEffect5);
                    }
                    if (cbx12.Checked)
                    {
                        SideEffect newSideEffect6 = new SideEffect();
                        newSideEffect6.SideEffect1 = cbx12.Text;
                        newSideEffect6.DuiCitizen = Int32.Parse(txtDuiCitizen.Text);
                        db.Add(newSideEffect6);
                    }
                    if (cbx13.Checked)
                    {
                        SideEffect newSideEffect7 = new SideEffect();
                        newSideEffect7.SideEffect1 = cbx13.Text;
                        newSideEffect7.DuiCitizen = Int32.Parse(txtDuiCitizen.Text);
                        db.Add(newSideEffect7);
                    }
                    if (cbx15.Checked)
                    {
                        SideEffect newSideEffect9 = new SideEffect();
                        newSideEffect9.SideEffect1 = cbx15.Text;
                        newSideEffect9.DuiCitizen = Int32.Parse(txtDuiCitizen.Text);
                        db.Add(newSideEffect9);
                    }
                    db.SaveChanges();
                    
                    // ADDING CITIZEN TO PEOPLE VACCINATED WITH FIRST DOSSIS
                    totalPeopleFirstDosis++;
                    totalPeopleVaccinated = totalPeopleFirstDosis + totalPeopleSecondDosis;
                    
                        MessageBox.Show("Informacion de ciudadano guardada correctamente", "Datos guardados",
                            MessageBoxButtons.OK);

                        lblInfoSecondDosis.Text = "La cita para la aplicacion de la segunda dosis del ciudadano " + a.IdCitizenNavigation.Fullname.ToString() +
                                                  " ha sido programada para la fecha " + newDate.ToString("d") + " a las " +
                                                  finalHour + " en la Cabina de vacunacion: " +
                                                  a.IdCabinNavigation.Addres + ". Con esta aplicacion se dara por finalizada la aplicacion de la vacuna contra Covid19.";
                    tab1.SelectedIndex = 5;
                }
                else if (result.Count() == 2)
                {
                    result[1].HourArrived = txtHourArrived.Text;
                    result[1].HourVaccunated = txtTimeVaccunated.Text;
                    
                    if (cbx7.Checked) 
                    {
                        SideEffect newSideEffect = new SideEffect();
                        newSideEffect.SideEffect1 = cbx7.Text;
                        newSideEffect.DuiCitizen = Int32.Parse(txtDuiCitizen.Text);
                        db.Add(newSideEffect);
                    }
                    if (cbx8.Checked)
                    {
                        SideEffect newSideEffect2 = new SideEffect();
                        newSideEffect2.SideEffect1 = cbx8.Text;
                        newSideEffect2.DuiCitizen = Int32.Parse(txtDuiCitizen.Text);
                        db.Add(newSideEffect2);
                    }
                    if (cbx9.Checked)
                    {
                        SideEffect newSideEffect3 = new SideEffect();
                        newSideEffect3.SideEffect1 = cbx9.Text;
                        newSideEffect3.DuiCitizen = Int32.Parse(txtDuiCitizen.Text);
                        db.Add(newSideEffect3);
                    }
                    if (cbx10.Checked)
                    {
                        SideEffect newSideEffect4 = new SideEffect();
                        newSideEffect4.SideEffect1 = cbx10.Text;
                        newSideEffect4.DuiCitizen = Int32.Parse(txtDuiCitizen.Text);
                        db.Add(newSideEffect4);
                    }
                    if (cbx11.Checked)
                    {
                        SideEffect newSideEffect5 = new SideEffect();
                        newSideEffect5.SideEffect1 = cbx11.Text;
                        newSideEffect5.DuiCitizen = Int32.Parse(txtDuiCitizen.Text);
                        db.Add(newSideEffect5);
                    }
                    if (cbx12.Checked)
                    {
                        SideEffect newSideEffect6 = new SideEffect();
                        newSideEffect6.SideEffect1 = cbx12.Text;
                        newSideEffect6.DuiCitizen = Int32.Parse(txtDuiCitizen.Text);
                        db.Add(newSideEffect6);
                    }
                    if (cbx13.Checked)
                    {
                        SideEffect newSideEffect7 = new SideEffect();
                        newSideEffect7.SideEffect1 = cbx13.Text;
                        newSideEffect7.DuiCitizen = Int32.Parse(txtDuiCitizen.Text);
                        db.Add(newSideEffect7);
                    }
                    if (cbx15.Checked)
                    {
                        SideEffect newSideEffect9 = new SideEffect();
                        newSideEffect9.SideEffect1 = cbx15.Text;
                        newSideEffect9.DuiCitizen = Int32.Parse(txtDuiCitizen.Text);
                        db.Add(newSideEffect9);
                    }
                    db.SaveChanges();
                    
                    // ADDING THE PERSON TO THE TOTAL OF PEOPLE VACCINATED SECOND DOSIS
                    totalPeopleSecondDosis++;
                    
                    
                    MessageBox.Show("Datos guardados.\nEl ciudadano ha completado el proceso de vacunacion",
                        "Proceso terminado",
                        MessageBoxButtons.OK);
                }
            }
            else
            {
                MessageBox.Show("Datos no validos y/o incompletos. Nota: Rellene los datos solicitados y revise el DUI",
                    "Datos no encontrados", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        
        // PRINT INFO 1
        private void btnPrintAppointment_Click(object sender, EventArgs e)
        {
            if (dataGridView1 != null)
            {
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "PDF Files (.pdf)|.pdf|All Files (.)|.";
                if (save.ShowDialog() == DialogResult.OK)
                {
                    string filename = save.FileName;
                    Document doc = new Document(PageSize.A3, 9, 9, 9, 9);
                    FileStream file = new FileStream(filename, FileMode.OpenOrCreate);
                    PdfWriter writer = PdfWriter.GetInstance(doc, file);
                    writer.ViewerPreferences = PdfWriter.PageModeUseThumbs;
                    writer.ViewerPreferences = PdfWriter.PageLayoutOneColumn;
                    doc.Open();
                    GenerateDocs(doc);
                    doc.Close();
                }
            }
            else
            {
                MessageBox.Show("No existen datos para imprimirse, registre una cita para imprimirla.", "Sin Datos!",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            
        }
        
        // PRINT INFO 2
        private void btnPrint_inchecktab_Click(object sender, EventArgs e)
        {
            if (dataGridView1 != null)
            {
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "PDF Files (.pdf)|.pdf|All Files (.)|.";
                if (save.ShowDialog() == DialogResult.OK)
                {
                    string filename = save.FileName;
                    Document doc = new Document(PageSize.A3, 9, 9, 9, 9);
                    FileStream file = new FileStream(filename, FileMode.OpenOrCreate);
                    PdfWriter writer = PdfWriter.GetInstance(doc, file);
                    writer.ViewerPreferences = PdfWriter.PageModeUseThumbs;
                    writer.ViewerPreferences = PdfWriter.PageLayoutOneColumn;
                    doc.Open();
                    GenerateDocs2(doc);
                    doc.Close();
                }
            }
        }
        
        // FUNCTION FOR THE PDF DOCUMENT 1
        private void GenerateDocs(Document document) 
        {
            // CREATE OBJ PdfTable WITH THE NUM DGV NUM OF ROWS
            PdfPTable pdfTable = new PdfPTable(dataGridView1.ColumnCount);
            pdfTable.DefaultCell.Padding = 3;
            pdfTable.WidthPercentage = 30;
            pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfTable.DefaultCell.BorderWidth = 1;
            pdfTable.WidthPercentage = 100;
            pdfTable.DefaultCell.BorderWidth = 1;
                

            pdfTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;


            // CREATE PDF HEADER 
            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {

                pdfTable.AddCell(dataGridView1.Columns[i].HeaderText);
                pdfTable.HorizontalAlignment = Element.ALIGN_CENTER;
                    
            }
                
            pdfTable.HeaderRows = 1;
            pdfTable.DefaultCell.BorderWidth = 1;

            // CREATE PDF BODY
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                {
                    pdfTable.AddCell(dataGridView1[j, i].Value.ToString());
                }
                pdfTable.CompleteRow();
            }

            document.Add(pdfTable);
        }
        
        // FUNCTION FOR THE PDF DOCUMENT 2
        private void GenerateDocs2(Document document) 
        {
            // CREATE OBJ PdfTable WITH THE NUM DGV NUM OF ROWS
            PdfPTable pdfTable = new PdfPTable(dataGridView2.ColumnCount);
            pdfTable.DefaultCell.Padding = 3;
            pdfTable.WidthPercentage = 30;
            pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfTable.DefaultCell.BorderWidth = 1;
            pdfTable.WidthPercentage = 100;
            pdfTable.DefaultCell.BorderWidth = 1;
                

            pdfTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;

            // CREATE PDF HEADER
            for (int i = 0; i < dataGridView2.ColumnCount; i++)
            {

                pdfTable.AddCell(dataGridView2.Columns[i].HeaderText);
                pdfTable.HorizontalAlignment = Element.ALIGN_CENTER;
                    
            }
                
            pdfTable.HeaderRows = 1;
            pdfTable.DefaultCell.BorderWidth = 1;

            // CREATE PDF BODY
            for (int i = 0; i < dataGridView2.RowCount; i++)
            {
                for (int j = 0; j < dataGridView2.ColumnCount; j++)
                {
                    pdfTable.AddCell(dataGridView2[j, i].Value.ToString());
                }
                pdfTable.CompleteRow();
            }
            document.Add(pdfTable);
        }

       
    }
    
=======
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using ProyectoFinal.SQLContext;
using System.IO;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using iTextSharp.text.pdf;
using iTextSharp.text;
using Org.BouncyCastle.Asn1.Cms;

namespace ProyectoFinal
{
    public partial class FrmVaccinationProcess : Form
    {
        // VARS FOR COUNTING THE PEOPLE THAT HAS BEEN VACCINATED 
        private int totalPeopleFirstDosis;
        private int totalPeopleSecondDosis;
        private int totalPeopleVaccinated; // TOTAL VACCINATED
        
        
        // VARS FOR HOURS
        private int timeHour;
        private int timeMinutes;
        private string finalHour;
        public FrmVaccinationProcess()
        {
            InitializeComponent();
        }

        private void FrmVaccinationProcess_Load(object sender, EventArgs e)
        {
            var db = new FinalProjectContext();

            // COMBO BOX DEPARTMENT
            cmbDepartament.DataSource = db.Departments.ToList();
            cmbDepartament.DisplayMember = "Department1";
            cmbDepartament.ValueMember = "Id";

            // COMBO BOX TYPE CITIZEN
            cmbTypeCitizen.DataSource = db.TypeCitizens.ToList();
            cmbTypeCitizen.DisplayMember = "TypeCitizen1";
            cmbTypeCitizen.ValueMember = "Id";

        }

        // SAVING CITIZEN INFO
        private void btm_record_data_Click(object sender, EventArgs e)
        {
            if (txtDui.Text.Length == 9 && txtName_Citizen.Text.Length > 0 && txtPhone.Text.Length > 0 &&
                txtAdress_Citizen.Text.Length > 0 && txtEmail_Citizen.Text.Length > 0)
            {
                DialogResult result = MessageBox.Show(
                    "Esta seguro que desea registrar el ciudadano con los datos proporcionados\n Una vez hecha, la cita no podra modificarse.",
                    "Seguridad", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    var db = new FinalProjectContext();

                    int idDepa = cmbDepartament.SelectedIndex;
                    idDepa += 1;
                    int idType = cmbTypeCitizen.SelectedIndex;
                    idType += 1;

                    Municipality municipality = (Municipality) cmbMunicipality.SelectedItem;

                    Municipality muni = db.Set<Municipality>()
                        .SingleOrDefault(m => m.Id == municipality.Id);

                    // SETTING CITIZEN INFO 
                    Citizen newCitizen = new Citizen();
                    newCitizen.Dui = Int32.Parse(txtDui.Text);
                    newCitizen.Fullname = txtName_Citizen.Text;
                    newCitizen.Email = txtEmail_Citizen.Text;
                    newCitizen.Phone = txtPhone.Text;
                    newCitizen.Addres = txtAdress_Citizen.Text;
                    newCitizen.IdDepartment = idDepa;
                    newCitizen.IdMunicipalityNavigation = muni;
                    newCitizen.IdTypeCitizen = idType;

                    // SAVING DISEASES
                    if (cbx2.Checked)
                    {
                        Disease newDisease2 = new Disease();
                        newDisease2.Diseases = cbx2.Text;
                        newDisease2.DuiCitizen = Int32.Parse(txtDui.Text);
                        db.Add(newDisease2);
                    }

                    if (cbx3.Checked)
                    {
                        Disease newDisease3 = new Disease();
                        newDisease3.Diseases = cbx3.Text;
                        newDisease3.DuiCitizen = Int32.Parse(txtDui.Text);
                        db.Add(newDisease3);
                    }

                    if (cbx4.Checked)
                    {
                        Disease newDisease4 = new Disease();
                        newDisease4.Diseases = cbx4.Text;
                        newDisease4.DuiCitizen = Int32.Parse(txtDui.Text);
                        db.Add(newDisease4);
                    }

                    if (cbx5.Checked)
                    {
                        Disease newDisease5 = new Disease();
                        newDisease5.Diseases = cbx5.Text;
                        newDisease5.DuiCitizen = Int32.Parse(txtDui.Text);
                        db.Add(newDisease5);
                    }

                    if (cbx6.Checked)
                    {
                        Disease newDisease6 = new Disease();
                        newDisease6.Diseases = cbx6.Text;
                        newDisease6.DuiCitizen = Int32.Parse(txtDui.Text);
                        db.Add(newDisease6);
                    }


                    // ADDING NEW CITIZEN AND DISEASES TO THE DATABASE 
                    db.Add(newCitizen);
                    db.SaveChanges();

                    MessageBox.Show("Ciudadano registrado con exito!", "Ciudadano registrado",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // SHOWING NEXT TAB
                    tab1.SelectedIndex = 1;

                }
            }
        }

        // DEPENDABLE COMBO BOX MUNICIPALITY
        private void cmbDepartament_SelectedIndexChanged(object sender, EventArgs e)
        {
            // COMBO BOX MUNICIPALITY
            var db = new FinalProjectContext();
            int idDepa = cmbDepartament.SelectedIndex;
            idDepa += 1;

            cmbMunicipality.DataSource = db.Municipalities
                .Where(mu => mu.IdDepartment == idDepa)
                .ToList();
            cmbMunicipality.DisplayMember = "Municipality1";
            cmbMunicipality.ValueMember = "Id";

            // COMBO BOX CABIN 
            cmb_vaccination_place.DataSource = db.Cabins
                .Where(mu => mu.IdDepartment == idDepa)
                .ToList();
            cmb_vaccination_place.DisplayMember = "Addres";
            cmb_vaccination_place.ValueMember = "Id";
        }

        // CLOSING WINDOW MESSAGES
        private void FrmVaccinationProcess_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Desea cerrar esta ventana y regresar a la ventana de inicio de sesion?\nSi lo hace se cerrara sesion automaticamente",
                "Registro Cita", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);

            if (result == DialogResult.Cancel)
            {
                this.Close();
            }

            this.DialogResult = DialogResult.OK;
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
        
        
        
        // SAVING APPOINTMENTO INFO AND DATAGRIDVIEW
        private void btnSaveAppointment_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Esta seguro que desea registrar esta cita? Una vez hecha no podra editarse.",
                "Seguridad", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                var db = new FinalProjectContext();

                Cabin cabin = (Cabin) cmb_vaccination_place.SelectedItem;

                Cabin cabi = db.Set<Cabin>()
                    .SingleOrDefault(m => m.Id == cabin.Id);
                
                // TIME RANDOM
                Random random = new Random();
                timeHour = random.Next(9, 14);
                timeMinutes = random.Next(10, 50);

                finalHour = timeHour.ToString() + ":" + timeMinutes.ToString();
                
                
                // DATE FOR THE APPOINTMENT
                Appointment newAppointment = new Appointment();
                newAppointment.DateAppointment = RandomDay();
                newAppointment.HourAppointment = finalHour;
                newAppointment.IdCabinNavigation = cabi; 
                newAppointment.IdCitizen = Int32.Parse(txtDui.Text); 

                db.Add(newAppointment);
                db.SaveChanges();

                MessageBox.Show("Cita registrada con exito", "Cita Registrada",
                    MessageBoxButtons.OK);

                // ADDING INFO TO DATA GRID VIEW 
                dataGridView1.DataSource = null;
                var lisAppoitmet = db.Appointments.Include(r => r.IdCabinNavigation)
                    .Include(r => r.IdCitizenNavigation)
                    .Select(r => new
                        {
                            Fecha_cita = r.DateAppointment, 
                            Hora_cita = r.HourAppointment,
                            Ciudadano = r.IdCitizenNavigation.Fullname,
                            Dui_Usuario = r.IdCitizenNavigation.Dui,
                            Dirección_cabina = r.IdCabinNavigation.Addres
                        }

                    ).Where(r => r.Dui_Usuario == Int32.Parse(txtDui.Text)).ToList();
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = lisAppoitmet;

                tab1.SelectedIndex = 2;
            }
        }
        
        // CHECKING IF THE CITIZEN HAS ALREADY A APPOINTMENT REGISTERED
        private void btn_check_Click(object sender, EventArgs e)
        {
            var db = new FinalProjectContext();

            // APPOINTMENT TABLE & SEARCHING SUCH AN APPOINTMENT WITH THAT DUI
            List<Appointment> appointments = db.Appointments
                .ToList();
            List<Appointment> appointment = appointments
                .Where(a => a.IdCitizen == Int32.Parse(txt_dui_check.Text))
                .ToList();

            if (appointment.Count() > 0)
            {
                dataGridView2.DataSource = null;

                var lisAppoitmet = db.Appointments
                    .Include(r => r.IdCabinNavigation)
                    .Include(r => r.IdCitizenNavigation)
                    .Select(r => new
                        {
                            Fecha = r.DateAppointment,
                            Hora_cita = r.HourAppointment,
                            Dui_Usuario = r.IdCitizenNavigation.Dui,
                            Dirección_cabina = r.IdCabinNavigation.Addres
                        }

                    ).Where(r => r.Dui_Usuario == Int32.Parse(txt_dui_check.Text)).ToList();
                dataGridView2.DataSource = null;
                dataGridView2.DataSource = lisAppoitmet;
            }
            else
            {
                DialogResult result = MessageBox.Show(
                    "No existe una cita hecha para dicho ciudadano!\nPresione 'Aceptar' para ir a la ventana de datos y registrar una cita para el ciudadano.",
                    "Sin registros", MessageBoxButtons.OK);

                if (result == DialogResult.OK)
                {
                    tab1.SelectedIndex = 0;
                }
            }
        }

        // SAVING INFO ABOUT CITIZEN VACUNATION 
        private void btnRegisterInfoCitizen_Click(object sender, EventArgs e)
        {
            DialogResult result1 = MessageBox.Show(
                "Esta seguro que desea guardar los datos proporcionados\n Una vez hecho, no podran modificarse.",
                "Seguridad", MessageBoxButtons.YesNo);

            if (result1 == DialogResult.Yes && txtDuiCitizen.Text.Length == 9 && txtHourArrived.Text.Length > 0 &&
                txtTimeVaccunated.Text.Length > 0)
            {
                int idCitizen = Int32.Parse(txtDuiCitizen.Text);
                
                var db = new FinalProjectContext();
                List<Appointment> appointments = db.Appointments
                    .Include(u => u.IdCabinNavigation)
                    .Include(u => u.IdCitizenNavigation)
                    .ToList();

                List<Appointment> result = appointments
                    .Where(u => u.IdCitizen == idCitizen)
                    .ToList();

                if (result.Count() == 1)
                {
                    Appointment a = (from x in db.Appointments
                        where x.IdCitizen == idCitizen
                        select x).First();

                    a.HourArrived = txtHourArrived.Text;
                    a.HourVaccunated = txtTimeVaccunated.Text;
                    
                    // RANDOM SECOND DOSIS
                    Random gen = new Random();
                    DateTime newDate = Convert.ToDateTime(a.DateAppointment);
                    newDate = newDate.AddDays(42);
                    DateTime finalDate = Convert.ToDateTime(a.DateAppointment);
                    finalDate = finalDate.AddDays(56);
                    int range = (finalDate - newDate).Days;
                    newDate = newDate.AddDays((gen.Next((range))));
                    
                    // TIME RANDOM
                    Random random = new Random();
                    timeHour = random.Next(9, 14);
                    timeMinutes = random.Next(10, 50);

                    finalHour = timeHour.ToString() + ":" + timeMinutes.ToString();
                    
                    // MAKING SECOND APPOINTMENT FOR DOSIS
                    Appointment secondAppointment = new Appointment();
                    secondAppointment.DateAppointment = newDate;
                    secondAppointment.HourAppointment = finalHour;
                    secondAppointment.IdCitizen = a.IdCitizen;
                    secondAppointment.IdCabin = a.IdCabin;
                    db.Add((secondAppointment));
                    

                    if (cbx7.Checked) 
                    {
                        SideEffect newSideEffect = new SideEffect();
                        newSideEffect.SideEffect1 = cbx7.Text;
                        newSideEffect.DuiCitizen = Int32.Parse(txtDuiCitizen.Text);
                        db.Add(newSideEffect);
                    }
                    if (cbx8.Checked)
                    {
                        SideEffect newSideEffect2 = new SideEffect();
                        newSideEffect2.SideEffect1 = cbx8.Text;
                        newSideEffect2.DuiCitizen = Int32.Parse(txtDuiCitizen.Text);
                        db.Add(newSideEffect2);
                    }
                    if (cbx9.Checked)
                    {
                        SideEffect newSideEffect3 = new SideEffect();
                        newSideEffect3.SideEffect1 = cbx9.Text;
                        newSideEffect3.DuiCitizen = Int32.Parse(txtDuiCitizen.Text);
                        db.Add(newSideEffect3);
                    }
                    if (cbx10.Checked)
                    {
                        SideEffect newSideEffect4 = new SideEffect();
                        newSideEffect4.SideEffect1 = cbx10.Text;
                        newSideEffect4.DuiCitizen = Int32.Parse(txtDuiCitizen.Text);
                        db.Add(newSideEffect4);
                    }
                    if (cbx11.Checked)
                    {
                        SideEffect newSideEffect5 = new SideEffect();
                        newSideEffect5.SideEffect1 = cbx11.Text;
                        newSideEffect5.DuiCitizen = Int32.Parse(txtDuiCitizen.Text);
                        db.Add(newSideEffect5);
                    }
                    if (cbx12.Checked)
                    {
                        SideEffect newSideEffect6 = new SideEffect();
                        newSideEffect6.SideEffect1 = cbx12.Text;
                        newSideEffect6.DuiCitizen = Int32.Parse(txtDuiCitizen.Text);
                        db.Add(newSideEffect6);
                    }
                    if (cbx13.Checked)
                    {
                        SideEffect newSideEffect7 = new SideEffect();
                        newSideEffect7.SideEffect1 = cbx13.Text;
                        newSideEffect7.DuiCitizen = Int32.Parse(txtDuiCitizen.Text);
                        db.Add(newSideEffect7);
                    }
                    if (cbx15.Checked)
                    {
                        SideEffect newSideEffect9 = new SideEffect();
                        newSideEffect9.SideEffect1 = cbx15.Text;
                        newSideEffect9.DuiCitizen = Int32.Parse(txtDuiCitizen.Text);
                        db.Add(newSideEffect9);
                    }
                    db.SaveChanges();
                    
                    // ADDING CITIZEN TO PEOPLE VACCINATED WITH FIRST DOSSIS
                    totalPeopleFirstDosis++;
                    totalPeopleVaccinated = totalPeopleFirstDosis + totalPeopleSecondDosis;
                    
                        MessageBox.Show("Informacion de ciudadano guardada correctamente", "Datos guardados",
                            MessageBoxButtons.OK);

                        lblInfoSecondDosis.Text = "La cita para la aplicacion de la segunda dosis del ciudadano " + a.IdCitizenNavigation.Fullname.ToString() +
                                                  " ha sido programada para la fecha " + newDate.ToString("d") + " a las " +
                                                  finalHour + " en la Cabina de vacunacion: " +
                                                  a.IdCabinNavigation.Addres + ". Con esta aplicacion se dara por finalizada la aplicacion de la vacuna contra Covid19.";
                    tab1.SelectedIndex = 5;
                }
                else if (result.Count() == 2)
                {
                    result[1].HourArrived = txtHourArrived.Text;
                    result[1].HourVaccunated = txtTimeVaccunated.Text;
                    
                    if (cbx7.Checked) 
                    {
                        SideEffect newSideEffect = new SideEffect();
                        newSideEffect.SideEffect1 = cbx7.Text;
                        newSideEffect.DuiCitizen = Int32.Parse(txtDuiCitizen.Text);
                        db.Add(newSideEffect);
                    }
                    if (cbx8.Checked)
                    {
                        SideEffect newSideEffect2 = new SideEffect();
                        newSideEffect2.SideEffect1 = cbx8.Text;
                        newSideEffect2.DuiCitizen = Int32.Parse(txtDuiCitizen.Text);
                        db.Add(newSideEffect2);
                    }
                    if (cbx9.Checked)
                    {
                        SideEffect newSideEffect3 = new SideEffect();
                        newSideEffect3.SideEffect1 = cbx9.Text;
                        newSideEffect3.DuiCitizen = Int32.Parse(txtDuiCitizen.Text);
                        db.Add(newSideEffect3);
                    }
                    if (cbx10.Checked)
                    {
                        SideEffect newSideEffect4 = new SideEffect();
                        newSideEffect4.SideEffect1 = cbx10.Text;
                        newSideEffect4.DuiCitizen = Int32.Parse(txtDuiCitizen.Text);
                        db.Add(newSideEffect4);
                    }
                    if (cbx11.Checked)
                    {
                        SideEffect newSideEffect5 = new SideEffect();
                        newSideEffect5.SideEffect1 = cbx11.Text;
                        newSideEffect5.DuiCitizen = Int32.Parse(txtDuiCitizen.Text);
                        db.Add(newSideEffect5);
                    }
                    if (cbx12.Checked)
                    {
                        SideEffect newSideEffect6 = new SideEffect();
                        newSideEffect6.SideEffect1 = cbx12.Text;
                        newSideEffect6.DuiCitizen = Int32.Parse(txtDuiCitizen.Text);
                        db.Add(newSideEffect6);
                    }
                    if (cbx13.Checked)
                    {
                        SideEffect newSideEffect7 = new SideEffect();
                        newSideEffect7.SideEffect1 = cbx13.Text;
                        newSideEffect7.DuiCitizen = Int32.Parse(txtDuiCitizen.Text);
                        db.Add(newSideEffect7);
                    }
                    if (cbx15.Checked)
                    {
                        SideEffect newSideEffect9 = new SideEffect();
                        newSideEffect9.SideEffect1 = cbx15.Text;
                        newSideEffect9.DuiCitizen = Int32.Parse(txtDuiCitizen.Text);
                        db.Add(newSideEffect9);
                    }
                    db.SaveChanges();
                    
                    // ADDING THE PERSON TO THE TOTAL OF PEOPLE VACCINATED SECOND DOSIS
                    totalPeopleSecondDosis++;
                    
                    
                    MessageBox.Show("Datos guardados.\nEl ciudadano ha completado el proceso de vacunacion",
                        "Proceso terminado",
                        MessageBoxButtons.OK);
                }
            }
            else
            {
                MessageBox.Show("Datos no validos. Nota: Rellene los datos solicitados y revise el DUI",
                    "Datos no encontrados", MessageBoxButtons.OK);
            }

        }
        
        // PRINT INFO 1
        private void btnPrintAppointment_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "PDF Files (.pdf)|.pdf|All Files (.)|.";
            if (save.ShowDialog() == DialogResult.OK)
            {
                string filename = save.FileName;
                Document doc = new Document(PageSize.A3, 9, 9, 9, 9);
                FileStream file = new FileStream(filename, FileMode.OpenOrCreate);
                PdfWriter writer = PdfWriter.GetInstance(doc, file);
                writer.ViewerPreferences = PdfWriter.PageModeUseThumbs;
                writer.ViewerPreferences = PdfWriter.PageLayoutOneColumn;
                doc.Open();
                GenerateDocs(doc);
                doc.Close();
            }
        }
        
        // PRINT INFO 2
        private void btnPrint_inchecktab_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "PDF Files (.pdf)|.pdf|All Files (.)|.";
            if (save.ShowDialog() == DialogResult.OK)
            {
                string filename = save.FileName;
                Document doc = new Document(PageSize.A3, 9, 9, 9, 9);
                FileStream file = new FileStream(filename, FileMode.OpenOrCreate);
                PdfWriter writer = PdfWriter.GetInstance(doc, file);
                writer.ViewerPreferences = PdfWriter.PageModeUseThumbs;
                writer.ViewerPreferences = PdfWriter.PageLayoutOneColumn;
                doc.Open();
                GenerateDocs2(doc);
                doc.Close();
            }
        }
        
        // FUNCTION FOR THE PDF DOCUMENT 1
        private void GenerateDocs(Document document) 
        {
            // CREATE OBJ PdfTable WITH THE NUM DGV NUM OF ROWS
            PdfPTable pdfTable = new PdfPTable(dataGridView1.ColumnCount);
            pdfTable.DefaultCell.Padding = 3;
            pdfTable.WidthPercentage = 30;
            pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfTable.DefaultCell.BorderWidth = 1;
            pdfTable.WidthPercentage = 100;
            pdfTable.DefaultCell.BorderWidth = 1;
                

            pdfTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;


            // CREATE PDF HEADER 
            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {

                pdfTable.AddCell(dataGridView1.Columns[i].HeaderText);
                pdfTable.HorizontalAlignment = Element.ALIGN_CENTER;
                    
            }
                
            pdfTable.HeaderRows = 1;
            pdfTable.DefaultCell.BorderWidth = 1;

            // CREATE PDF BODY
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                {
                    pdfTable.AddCell(dataGridView1[j, i].Value.ToString());
                }
                pdfTable.CompleteRow();
            }

            document.Add(pdfTable);
        }
        
        // FUNCTION FOR THE PDF DOCUMENT 2
        private void GenerateDocs2(Document document) 
        {
            // CREATE OBJ PdfTable WITH THE NUM DGV NUM OF ROWS
            PdfPTable pdfTable = new PdfPTable(dataGridView2.ColumnCount);
            pdfTable.DefaultCell.Padding = 3;
            pdfTable.WidthPercentage = 30;
            pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfTable.DefaultCell.BorderWidth = 1;
            pdfTable.WidthPercentage = 100;
            pdfTable.DefaultCell.BorderWidth = 1;
                

            pdfTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;

            // CREATE PDF HEADER
            for (int i = 0; i < dataGridView2.ColumnCount; i++)
            {

                pdfTable.AddCell(dataGridView2.Columns[i].HeaderText);
                pdfTable.HorizontalAlignment = Element.ALIGN_CENTER;
                    
            }
                
            pdfTable.HeaderRows = 1;
            pdfTable.DefaultCell.BorderWidth = 1;

            // CREATE PDF BODY
            for (int i = 0; i < dataGridView2.RowCount; i++)
            {
                for (int j = 0; j < dataGridView2.ColumnCount; j++)
                {
                    pdfTable.AddCell(dataGridView2[j, i].Value.ToString());
                }
                pdfTable.CompleteRow();
            }
            document.Add(pdfTable);
        }

       
    }
    
>>>>>>> e196b46f1e90476656035405f5aaf321c39d98dc
}