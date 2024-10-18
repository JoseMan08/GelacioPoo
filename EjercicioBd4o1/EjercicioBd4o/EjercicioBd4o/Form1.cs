using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Db4objects.Db4o;
using Db4objects.Db4o.Query;

namespace EjercicioBd4o
{
    public partial class Form1 : Form
    {
        //IObjectContainer BD = Db4oFactory.OpenFile("SuperBD.yap");
        //Pelicula unapeli = new Pelicula();
        //DVD copiadvd = new DVD();

        public Form1()
        {
            InitializeComponent();
            this.tabControl1.TabPages.Remove(this.tabPage2);//Oculta la pestaña DVD
            //this.tabControl1.TabPages.Remove(this.tabPage3);//Oculta la pestaña Alquiler
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.tabControl1.TabPages.Remove(this.tabPage1);
            this.tabControl1.TabPages.Remove(this.tabPage2);
            this.tabControl1.TabPages.Add(this.tabPage1);//Muestra la pestaña Cliente
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            this.tabControl1.TabPages.Remove(this.tabPage1);
            this.tabControl1.TabPages.Remove(this.tabPage2);
            this.tabControl1.TabPages.Add(this.tabPage2);//Muestra la pestaña Cita
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Form2 a = new Form2();
            a.ShowDialog();
        }

        private void btnAgregar_Click_1(object sender, EventArgs e)
        {
            if (txtId_C.Text != string.Empty && txtRFC.Text != string.Empty && txtNombre.Text != string.Empty && txtApellidos.Text != string.Empty && txtDireccion.Text != string.Empty)
            {
                IObjectContainer BD = Db4oFactory.OpenFile("SuperBD.yap");

                Cliente movie = new Cliente();

                movie.Id_C = txtId_C.Text;
                movie.RFC = txtRFC.Text;
                movie.Nombre = txtNombre.Text;
                movie.Apellidos = txtApellidos.Text;
                movie.Direccion = txtDireccion.Text;
                movie.Telefono = txtTelefono.Text;


                try
                {
                    BD.Store(movie);
                    BD.Commit();
                }
                finally
                {
                    BD.Close();
                }
                MessageBox.Show("EXITO!!! Se Guardo");
                txtId_C.Clear();
                txtRFC.Clear();
                txtNombre.Clear();
                txtApellidos.Clear();
                txtDireccion.Clear();
                txtTelefono.Clear();
            }
            else
                MessageBox.Show("Hay campos Vacios");
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (txtId_C.Text != string.Empty)
            {
                IObjectContainer BD = Db4oFactory.OpenFile("SuperBD.yap");
                string Id_C = txtId_C.Text;
                Cliente c = new Cliente();
                c.Id_C = txtId_C.Text;
                IList<Cliente> resultados = BD.Query<Cliente>(a => a.Id_C == Id_C);
                if (resultados.Count > 0)
                {
                    foreach (Cliente a in resultados)
                    {
                        txtId_C.Text = a.Id_C;
                        txtRFC.Text = a.RFC;
                        txtNombre.Text = a.Nombre;
                        txtApellidos.Text = a.Apellidos;
                        txtDireccion.Text = a.Direccion;
                        txtTelefono.Text = a.Telefono;
                        btnGuardarp.Visible = true;
                        btncancelm.Visible = true;
                    }
                }
                else
                    MessageBox.Show("No Existe");
                //TxtId_Cp.Clear();
                BD.Close();

            }
            else
                MessageBox.Show("Ingrese la clave");
        }

        private void btnGuardarp_Click(object sender, EventArgs e)
        {
            IObjectContainer BD = Db4oFactory.OpenFile("SuperBD.yap");
            Cliente cli = new Cliente();
            cli.Id_C = txtId_C.Text;
            IList<Cliente> resultados = BD.Query<Cliente>(x => x.Id_C == txtId_C.Text);
            if (resultados.Count > 0)
            {
                IObjectSet result = BD.QueryByExample(cli);
                Cliente v = (Cliente)result.Next();
                //vie.Clave = TxtClavep.Text;
                v.Id_C = txtId_C.Text;
                v.RFC = txtRFC.Text;
                v.Nombre = txtNombre.Text;
                v.Apellidos = txtApellidos.Text;
                v.Direccion = txtDireccion.Text;
                v.Telefono = txtTelefono.Text;


                BD.Store(v);
                BD.Commit();
                MessageBox.Show("Exito!!! Se Modifico");
                txtId_C.Clear();
                txtRFC.Clear();
                txtNombre.Clear();
                txtApellidos.Clear();
                txtDireccion.Clear();
                txtTelefono.Clear();
            }
            else
                MessageBox.Show("Error!!!  No se Modifico");
            BD.Close();
            txtId_C.Clear();
            txtRFC.Clear();
            txtNombre.Clear();
            txtApellidos.Clear();
            txtDireccion.Clear();
            txtTelefono.Clear();
            btnGuardarp.Visible = false;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (txtId_C.Text != string.Empty)
            {
                IObjectContainer BD = Db4oFactory.OpenFile("SuperBD.yap");
                string nom = txtId_C.Text;
                try
                {
                    IList<Cliente> consulta = BD.Query<Cliente>(z => z.Id_C == nom);
                    foreach (Cliente item in consulta)
                    {
                        BD.Delete(item);
                        MessageBox.Show("Registro eliminado");
                        txtId_C.Clear();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                    MessageBox.Show("No hay registros que coincidan");
                }
                finally
                {
                    BD.Close();
                }
            }
            else
                MessageBox.Show("Inserta la clave");
        }

        private void btnConsultaG_Click(object sender, EventArgs e)
        {
            IObjectContainer BD = Db4oFactory.OpenFile("SuperBD.yap");
            try
            {
                IList<Cliente> consulta = BD.Query<Cliente>();
                MessageBox.Show("Cantidad de registros encontrados: " + consulta.Count);

                // Limpiar filas y columnas antes de agregar nuevos datos
                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();

                // Definir columnas si es necesario
                if (consulta.Count > 0)
                {
                    // Crear columnas solo si no existen
                    if (dataGridView1.Columns.Count == 0)
                    {
                        dataGridView1.Columns.Add("Id_C", "ID");
                        dataGridView1.Columns.Add("RFC", "RFC");
                        dataGridView1.Columns.Add("Nombre", "Nombre");
                        dataGridView1.Columns.Add("Apellidos", "Apellidos");
                        dataGridView1.Columns.Add("Direccion", "Dirección");
                        dataGridView1.Columns.Add("Telefono", "Teléfono");
                        
                    }

                    // Agregar los registros al DataGridView
                    foreach (var cliente in consulta)
                    {
                        dataGridView1.Rows.Add(cliente.Id_C, cliente.RFC, cliente.Nombre, cliente.Apellidos,
                            cliente.Direccion, cliente.Telefono);
                    }
                }
                else
                {
                    MessageBox.Show("No se encontraron registros.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                BD.Close();
            }
        }



        private void btnBuscarp_Click(object sender, EventArgs e)
        {
            if (txtbuscar.Text != string.Empty)
            {
                IObjectContainer BD = Db4oEmbedded.OpenFile("SuperBD.yap");
                Cliente cl = new Cliente();
                cl.Id_C = txtbuscar.Text;
                IList<Cliente> resultados = BD.Query<Cliente>(x => x.Id_C == txtbuscar.Text);

                if (resultados.Count > 0)
                {

                    foreach (Cliente al in resultados)
                    {
                        MessageBox.Show("Id_C " + al.Id_C + "\n" + "RFC " + al.RFC + "\n" +
                            "Nombre " + al.Nombre + "\n" + "Apellidos: " + al.Apellidos + "\n" +
                            "Direccion " + al.Direccion + "\n" + "Telefono " + al.Telefono + "\n");
                    }

                }
                else
                    MessageBox.Show("No existe!! Inserta otra clave");
                BD.Close();
                txtbuscar.Clear();
            }
            else
                MessageBox.Show("Por favor ingrese la clave");
        }

        private void btnagregarD_Click(object sender, EventArgs e)
        {
            if (txtId_E.Text != string.Empty && txtPuesto.Text != string.Empty && txtNombreE.Text != string.Empty
                && txtApellidosE.Text != string.Empty && txtDireccionE.Text != string.Empty && txtTelefonoE.Text != string.Empty)
            {
                IObjectContainer BD = Db4oFactory.OpenFile("SuperBD.yap");

                Empleado mo = new Empleado();

                mo.Id_E = txtId_E.Text;
                mo.Puesto = txtPuesto.Text;
                mo.Nombre = txtNombreE.Text;
                mo.Apellidos = txtApellidosE.Text;
                mo.Direccion = txtDireccionE.Text;
                mo.Telefono = txtTelefonoE.Text;

                try
                {
                    BD.Store(mo);
                    BD.Commit();
                }
                finally
                {
                    BD.Close();
                }
                MessageBox.Show("EXITO!!! Se Guardo");
                txtId_E.Clear();
                txtPuesto.Clear();
                txtNombreE.Clear();
                txtApellidosE.Clear();
                txtDireccionE.Clear();
                txtTelefonoE.Clear();


            }
            else
                MessageBox.Show("Hay campos Vacios");
        }

        private void btnModificarD_Click(object sender, EventArgs e)
        {
            if (txtId_E.Text != string.Empty)
            {
                IObjectContainer BD = Db4oFactory.OpenFile("SuperBD.yap");
                string Codigo = txtId_E.Text;
                Empleado p = new Empleado();
                p.Id_E = txtId_E.Text;
                IList<Empleado> resultados = BD.Query<Empleado>(a => a.Id_E == txtId_E.Text);
                if (resultados.Count > 0)
                {
                    foreach (Empleado a in resultados)
                    {
                        txtId_E.Text = a.Id_E;
                        txtPuesto.Text = a.Puesto;
                        txtNombreE.Text = a.Nombre;
                        txtApellidosE.Text = a.Apellidos;
                        txtDireccionE.Text = a.Direccion;
                        txtTelefonoE.Text = a.Telefono;
                        
                        btnGuardarD.Visible = true;
                        btncanceld.Visible = true;
                    }
                }
                else
                    MessageBox.Show("No Existe");
                //TxtClavep.Clear();
                BD.Close();
            }
            else
                MessageBox.Show("Ingrese el Codigo");
        }

        private void btnGuardarD_Click(object sender, EventArgs e)
        {
            IObjectContainer BD = Db4oFactory.OpenFile("SuperBD.yap");
            Empleado dv = new Empleado();
            dv.Id_E = txtId_E.Text;
            IList<Empleado> resultados = BD.Query<Empleado>(x => x.Id_E == txtId_E.Text);
            if (resultados.Count > 0)
            {
                IObjectSet result = BD.QueryByExample(dv);
                Empleado vie = (Empleado)result.Next();
                vie.Id_E = txtId_E.Text;
                vie.Puesto = txtPuesto.Text;
                vie.Nombre = txtNombreE.Text;
                vie.Apellidos = txtApellidosE.Text;
                vie.Direccion = txtDireccionE.Text;
                vie.Telefono = txtTelefonoE.Text;
                BD.Store(vie);
                BD.Commit();
                MessageBox.Show("Exito!!! Se Modifico");
                txtId_E.Clear();
                txtPuesto.Clear();
                txtNombreE.Clear();
                txtApellidosE.Clear();
                txtDireccionE.Clear();
                txtTelefonoE.Clear();
            }
            else
                MessageBox.Show("Error!!!  No se Modifico");
            BD.Close();
            txtId_E.Clear();
            txtPuesto.Clear();
            txtNombreE .Clear();
            txtApellidosE .Clear();
            txtDireccionE .Clear();
            txtTelefonoE .Clear();
            btnGuardarD.Visible = false;
        }

        private void btneliminarD_Click(object sender, EventArgs e)
        {
            if (txtId_E.Text != string.Empty)
            {
                IObjectContainer BD = Db4oFactory.OpenFile("SuperBD.yap");
                string cod = txtId_E.Text;
                try
                {
                    IList<Empleado> consulta = BD.Query<Empleado>(z => z.Id_E == cod);
                    foreach (Empleado item in consulta)
                    {
                        BD.Delete(item);
                        MessageBox.Show("EL Registro se elimino");
                        txtId_E.Clear();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                    MessageBox.Show("No hay registros que coincidan");
                }
                finally
                {
                    BD.Close();
                }
            }
            else
                MessageBox.Show("Inserta el Codigo");
        }

        private void btnConsultaD_Click(object sender, EventArgs e)
        {
            IObjectContainer BD = Db4oFactory.OpenFile("SuperBD.yap");
            try
            {
                IList<Empleado> consulta = BD.Query<Empleado>();
                MessageBox.Show("Cantidad de registros encontrados: " + consulta.Count);

                // Limpiar filas y columnas antes de agregar nuevos datos
                dataGridView2.Rows.Clear();
                dataGridView2.Columns.Clear();

                // Definir columnas si es necesario
                if (consulta.Count > 0)
                {
                    // Crear columnas solo si no existen
                    if (dataGridView2.Columns.Count == 0)
                    {
                        dataGridView2.Columns.Add("Id_E", "ID");
                        dataGridView2.Columns.Add("Puesto", "Puesto");
                        dataGridView2.Columns.Add("Nombre", "Nombre");
                        dataGridView2.Columns.Add("Apellidos", "Apellidos");
                        dataGridView2.Columns.Add("Direccion", "Direccion");
                        dataGridView2.Columns.Add("Telefono", "Telefono");

                    }

                    // Agregar los registros al DataGridView
                    foreach (var cita in consulta)
                    {
                        dataGridView2.Rows.Add(cita.Id_E, cita.Puesto, cita.Nombre, cita.Apellidos, cita.Direccion, cita.Telefono);
                    }
                }
                else
                {
                    MessageBox.Show("No se encontraron registros.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                BD.Close();
            }
        }


        private void btnbuscarD_Click(object sender, EventArgs e)
        {
            if (txtId_E1.Text != string.Empty)
            {
                IObjectContainer BD = Db4oEmbedded.OpenFile("SuperBD.yap");
                Empleado ve = new Empleado();
                ve.Id_E = txtId_E1.Text;
                IList<Empleado> resultados = BD.Query<Empleado>(x => x.Id_E == txtId_E1.Text);

                if (resultados.Count > 0)
                {

                    foreach (Empleado al in resultados)
                    {
                        MessageBox.Show("Id_E " + al.Id_E + "\n" + "Puesto " + al.Puesto + "\n"
                            + "Nombre " + al.Nombre + "\n" + "Apellidos " + al.Apellidos + "\n"
                            + "Direccion " + al.Direccion + "\n" + "Telefono " + al.Telefono + "\n");
                    }

                }
                else
                    MessageBox.Show("No existe!! Inserta otro codigo");
                BD.Close();
                txtId_E1.Clear();
            }
            else
                MessageBox.Show("Por favor ingrese el codigo");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            btncancelm.Visible = false;
            btnGuardarp.Visible = false;
            txtId_C.Clear();
            txtRFC.Clear();
            txtNombre.Clear();
            txtApellidos.Clear();
            txtDireccion.Clear();
            txtTelefono.Clear();
        }

        private void btncanceld_Click(object sender, EventArgs e)
        {
            btncanceld.Visible = false;
            btnGuardarD.Visible = false;
            txtId_E1.Clear();
            txtPuesto.Clear();
            txtNombreE.Clear();
            txtApellidosE.Clear();
            txtDireccionE.Clear();
            txtTelefonoE.Clear();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'fensterDataSet1.Cliente' Puede moverla o quitarla según sea necesario.
            this.clienteTableAdapter.Fill(this.farmaciaDataSet.Cliente);
            // TODO: esta línea de código carga datos en la tabla 'fensterDataSet1.Cita' Puede moverla o quitarla según sea necesario.
            this.empleadoTableAdapter.Fill(this.farmaciaDataSet.Empleado);

        }
        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtFecha_TextChanged(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void label21_Click(object sender, EventArgs e)
        {

        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtId_Ct1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void toolStripDropDownButton1_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void fensterDataSet1BindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void txtTelefonoE_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
