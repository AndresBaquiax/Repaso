using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Repaso
{
    public partial class Form1 : Form
    {
        List<Empleado> empleados = new List<Empleado>();
        List<Asistencia> asistencias = new List<Asistencia>();
        List<Sueldo> sueldos = new List<Sueldo>();
        public Form1()
        {
            InitializeComponent();
        }
        private void cargarEmpleado()
        {
            FileStream stream = new FileStream("Empleado.txt", FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(stream);

            while (reader.Peek() > -1)
            {
                Empleado empleado = new Empleado();
                empleado.numeroEmpleado = Convert.ToInt16(reader.ReadLine());
                empleado.nombreEmpleado = reader.ReadLine();
                empleado.sueldoHora = Convert.ToDecimal(reader.ReadLine());
                empleados.Add(empleado);
            }
            reader.Close();
        }
        private void cargarAsistencia()
        {
            FileStream stream = new FileStream("Asistencia.txt", FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(stream);

            while (reader.Peek() > -1)
            {
                Asistencia asistencia = new Asistencia();
                asistencia.numeroEmpleado = Convert.ToInt16(reader.ReadLine());
                asistencia.horasMes = Convert.ToInt16(reader.ReadLine());
                asistencia.mes = reader.ReadLine();
                asistencias.Add(asistencia);
            }
            reader.Close();
        }
        private void cargarGrid()
        {
            dataGridView1.DataSource = null;
            dataGridView1.Refresh();
            dataGridView1.DataSource = empleados;
            dataGridView1.Refresh();

            dataGridView2.DataSource = null;
            dataGridView2.Refresh();
            dataGridView2.DataSource = asistencias;
            dataGridView2.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cargarAsistencia();
            cargarEmpleado();
            cargarGrid();
        }

        private void buttonCalcular_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < empleados.Count; i++)
            {
                for (int j = 0; j < asistencias.Count; j++)
                {
                    if (empleados[i].numeroEmpleado == asistencias[j].numeroEmpleado)
                    {
                        Sueldo sueldo = new Sueldo();
                        sueldo.numeroEmpleado = empleados[i].numeroEmpleado;
                        sueldo.nombre = empleados[i].nombreEmpleado;
                        sueldo.sueldoMes = empleados[i].sueldoHora * asistencias[j].horasMes;
                        sueldo.mes = asistencias[j].mes;
                        sueldos.Add(sueldo);
                    }
                }
            }
            dataGridView3.DataSource = sueldos;
            dataGridView3.Refresh();
        }
    }
}
