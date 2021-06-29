using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections;

namespace GraficosVacunaciones
{
    public partial class Form1 : Form
    {
        SqlConnection Connection = new SqlConnection("Server=localhost;Database=FinalProject;Trusted_Connection=True;");
        SqlCommand cmd;
        SqlDataReader dr;

        // PARA COLUMNAS PARA GRAFICO DE TIEMPO DE ESPERA
        ArrayList Range = new ArrayList();
        ArrayList CitizenNum = new ArrayList();
        // PARA COLUMNAS PARA GRAFICO DE EFECTOS
        ArrayList SideEffect = new ArrayList();
        ArrayList NumCitizen = new ArrayList();


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ChartRangeTime();
            ChartSideEffects();
            DashboardDatos();
        }

        private void ChartRangeTime()
        {
            cmd = new SqlCommand("MinutesRanges", Connection);
            cmd.CommandType = CommandType.StoredProcedure;
            Connection.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Range.Add(dr.GetString(0));
                CitizenNum.Add(dr.GetInt32(1));
            }
            chtRangeTime.Series[0].Points.DataBindXY(Range, CitizenNum);
            dr.Close();
            Connection.Close();
        }

        private void ChartSideEffects()
        {
            cmd = new SqlCommand("SideEffectShowed", Connection);
            cmd.CommandType = CommandType.StoredProcedure;
            Connection.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                SideEffect.Add(dr.GetString(0));
                NumCitizen.Add(dr.GetInt32(1));
            }
            chtSideEffects.Series[0].Points.DataBindXY(SideEffect, NumCitizen);
            dr.Close();
            Connection.Close();
        }

        private void DashboardDatos()
        {
            cmd = new SqlCommand("DashboardData", Connection);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter total1 = new SqlParameter("@totalPeople1Dossis", 0); total1.Direction = ParameterDirection.Output;
            SqlParameter total2 = new SqlParameter("@totalPeople2Dossis", 0); total2.Direction = ParameterDirection.Output;
            SqlParameter total = new SqlParameter("@totalPeopleVaccinated", 0); total.Direction = ParameterDirection.Output;

            cmd.Parameters.Add(total1);
            cmd.Parameters.Add(total2);
            cmd.Parameters.Add(total);
            Connection.Open();
            cmd.ExecuteNonQuery();
            lblTotal1.Text = cmd.Parameters["@totalPeople1Dossis"].Value.ToString();
            lblTotal2.Text = cmd.Parameters["@totalPeople2Dossis"].Value.ToString();
            lblTotal.Text = cmd.Parameters["@totalPeopleVaccinated"].Value.ToString();
            Connection.Close();
        }
    }
}
