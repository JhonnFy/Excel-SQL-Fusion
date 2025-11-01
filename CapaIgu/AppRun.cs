﻿using CapaControlador;
using CapaDatos;
using CapaNegocio;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace CapaIgu
{
    public partial class AppRun : Form
    {
        public AppRun()
        {
            InitializeComponent();
        }

        private void AppRun_Load(object sender, EventArgs e)
        {
            Debug.WriteLine("[****].[OK].[Paso 0].[CapaIgu].[AppRun_Load].[Iniciando aplicación]");
            var conexion = new Conexion();
            var resultado = conexion.ProbarConexion();
            Debug.WriteLine("[****].[OK].[Paso 0.1].[CapaIgu].[AppRun_Load].[Conexión a la base de datos ejecutada]");

            MessageBox.Show(
                resultado.mensaje,
                resultado.estado ? "Conexión exitosa" : "Error de conexión",
                MessageBoxButtons.OK,
                resultado.estado ? MessageBoxIcon.Information : MessageBoxIcon.Error
            );
            Debug.WriteLine($"[****].[OK].[Paso 0.2].[CapaIgu].[AppRun_Load].[UI Inicializada — DataGrid y Botones en construcción]");

            this.BeginInvoke((Action)(() => this.ActiveControl = null));
            ConfigurarDataGridView();
            ConstruirBtnCreate();
            ConstruirBtnImport();
        }


        private void dataGridViewExcel_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        private void ConfigurarDataGridView()
        {
            try
            {
                Debug.WriteLine("[****].[OK].[Paso 1].[CapaIgu].[ConfigurarDataGridView iniciado]");
                dataGridViewExcel.Columns.Clear();
                dataGridViewExcel.AutoGenerateColumns = false;
                dataGridViewExcel.AllowUserToAddRows = false;
                dataGridViewExcel.ReadOnly = true;
                dataGridViewExcel.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridViewExcel.AllowUserToResizeRows = false;
                              
                dataGridViewExcel.Columns.Add("Radicado", "Radicado");
                dataGridViewExcel.Columns.Add("Id", "Id");
                dataGridViewExcel.Columns.Add("Empleado", "Empleado");
                dataGridViewExcel.Columns.Add("Identificacion", "Identificacion");
                dataGridViewExcel.Columns.Add("Tipo Documental", "Tipo Documental");
                dataGridViewExcel.Columns.Add("Codigo De Barras", "Codigo De Barras");
                dataGridViewExcel.Columns.Add("Cb Documento", "Cb Documento");
                dataGridViewExcel.Columns.Add("CB Expediente", "CB Expediente");
                dataGridViewExcel.Columns.Add("CB Caja", "CB Caja");
                dataGridViewExcel.Columns.Add("Btn_IMPORT", "");
                dataGridViewExcel.Columns.Add("Btn_CREATE", "");
                dataGridViewExcel.RowHeadersVisible = false;
                dataGridViewExcel.AllowUserToResizeColumns = false;
                dataGridViewExcel.ScrollBars = ScrollBars.Vertical;
                dataGridViewExcel.BackgroundColor = Color.White;

                dataGridViewExcel.Columns["Radicado"].Width = 80;
                dataGridViewExcel.Columns["Id"].Width = 80;
                dataGridViewExcel.Columns["Empleado"].Width = 200;
                dataGridViewExcel.Columns["Identificacion"].Width = 124;
                dataGridViewExcel.Columns["Tipo Documental"].Width = 124;
                dataGridViewExcel.Columns["Codigo De Barras"].Width = 124;
                dataGridViewExcel.Columns["Cb Documento"].Width = 124;
                dataGridViewExcel.Columns["CB Expediente"].Width = 124;
                dataGridViewExcel.Columns["CB Caja"].Width = 124;
                dataGridViewExcel.Columns["Btn_IMPORT"].Width = 124;
                dataGridViewExcel.Columns["Btn_CREATE"].Width = 124;

                foreach (DataGridViewColumn column in dataGridViewExcel.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                }

                Debug.WriteLine("[****].[OK].[Paso 1.1].[CapaIgu].[DataGridView configurado correctamente]");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("[****].[ERROR].[CapaIgu].[ConfigurarDataGridView] " + ex.Message);
                MessageBox.Show("ERROR [Capa IGU].[ConfigurarDataGridView] " + ex.Message);
            }

        }

        private void InsertarDatosDataGridView(List<ModeloCodigoDeBarrasOrigen> lista)
        {
            try
            {
                Debug.WriteLine("[****].[OK].[Paso 5].[CapaIgu].[InsertarDatosDataGridView iniciado]");
                dataGridViewExcel.Rows.Clear();

                foreach (var atributo in lista)
                {
                    dataGridViewExcel.Rows.Add(
                        atributo.Radicado,
                        atributo.Id,
                        atributo.Empleado,
                        atributo.Identificacion,
                        atributo.TipoDocumental,
                        atributo.CodigoDeBarrasRecepcion,
                        atributo.CbDocumento,
                        atributo.CbExpediente,
                        atributo.CbCaja,
                        "",
                        ""
                    );
                }
                Debug.WriteLine("[****].[OK].[Paso 6].[CapaIgu].[Datos insertados en DataGridView correctamente]");

            }
            catch (Exception ex)
            {
                Debug.WriteLine("[****].[ERROR].[CapaIgu].[InsertarDatosDataGridView] " + ex.Message);
                MessageBox.Show("ERROR [Capa IGU].[InsertarDatosDataGridView] " + ex.Message);
            }

        }


        private void ConstruirBtnCreate()
        {
            try
            {
                Debug.WriteLine("[****].[OK].[Paso 2].[CapaIgu].[ConstruirBtnCreate Iniciado]");

                Button objBtnCreate = new Button();

                objBtnCreate.Size = new Size(123, 21);
                objBtnCreate.FlatStyle = FlatStyle.Flat;
                objBtnCreate.UseVisualStyleBackColor = false;
                objBtnCreate.FlatAppearance.BorderSize = 0;
                objBtnCreate.FlatAppearance.MouseOverBackColor = Color.White;
                objBtnCreate.FlatAppearance.MouseDownBackColor = Color.White;

                string rutaImagen = @"C:\Developer\ExcelProcessorNET\Icons\btnCreate.jpg";

                if (System.IO.File.Exists(rutaImagen))
                {
                    Image original = Image.FromFile(rutaImagen);

                    objBtnCreate.BackgroundImage = new Bitmap(original, new Size(40, 20));
                    objBtnCreate.BackgroundImageLayout = ImageLayout.Center;
                    objBtnCreate.ImageAlign = ContentAlignment.MiddleCenter;
                }
                else
                {
                    Debug.WriteLine("[****].[ADVERTENCIA].[CapaIgu].[Imagen no encontrada para btnCreate]");
                    MessageBox.Show("La imagen no se encontró en la ruta especificada: " + rutaImagen);
                }


                int margenDerecha = 1;
                int margenArriba = 2;

                objBtnCreate.Location = new Point(
                     this.ClientSize.Width - objBtnCreate.Width - margenDerecha,
                     margenArriba
                );

                

                objBtnCreate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                this.Controls.Add(objBtnCreate);
                objBtnCreate.BringToFront();

                Debug.WriteLine("[****].[OK].[Paso 2.1].[CapaIgu].[Botón CREATE construido correctamente]");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("[****].[ERROR].[CapaIgu].[ConstruirBtnCreate] " + ex.Message);
                MessageBox.Show("ERROR [Capa IGU].[ConstruirBtnCreate] " + ex.Message);
            }
        }


        public void ConstruirBtnImport()
        {
            try
            {
                Debug.WriteLine("[****].[OK].[Paso 3].[CapaIgu].[ConstruirBtnImport iniciado]");
                Button objBtnImport = new Button();

                objBtnImport.Size = new Size(123, 21);
                objBtnImport.FlatStyle = FlatStyle.Flat;
                objBtnImport.UseVisualStyleBackColor = false;
                objBtnImport.FlatAppearance.BorderSize = 0;
                objBtnImport.FlatAppearance.MouseOverBackColor = Color.White;
                objBtnImport.FlatAppearance.MouseDownBackColor = Color.White;

                string rutaImagen = @"C:\Developer\ExcelProcessorNET\Icons\btnImport.jpg";

                if (System.IO.File.Exists(rutaImagen))
                {
                    Image original = Image.FromFile(rutaImagen);

                    objBtnImport.BackgroundImage = new Bitmap(original, new Size(40, 20));
                    objBtnImport.BackgroundImageLayout = ImageLayout.Center;
                    objBtnImport.ImageAlign = ContentAlignment.MiddleCenter;
                }
                else
                {
                    Debug.WriteLine("[****].[ADVERTENCIA].[CapaIgu].[Imagen no encontrada para btnImport]");
                    MessageBox.Show("La imagen no se encontró en la ruta especificada: " + rutaImagen);
                }

                int margenDerecha = 120 + 5;
                int margenArriba = 2;

                objBtnImport.Location = new Point(
                    this.ClientSize.Width - objBtnImport.Width - margenDerecha,
                    margenArriba
                );


                //Evento Click
                objBtnImport.Click += (s, e) =>
                {
                    try
                    {
                        Debug.WriteLine("[****].[OK].[Paso 4].[CapaIgu].[Click en botón Importar detectado]");
                        OpenFileDialog dialog = new OpenFileDialog
                        {
                            Filter = "Archivos de Excel (*.xlsx)|*.xlsx|Todos los archivos (*.*)|*.*",
                            Title = "Seleccionar Archivo Excel"
                        };

                        if (dialog.ShowDialog() == DialogResult.OK)
                        {
                            string rutaArchivo = dialog.FileName;
                            Debug.WriteLine($"[****].[ok].[Paso 4.1].[Archivo seleccionado: {rutaArchivo}");

                            //Enviar Al Controlador
                            var controlador = new CapaControladorOrigen();
                            var lista = controlador.ImportarDesdeExcel(rutaArchivo);
                            InsertarDatosDataGridView(lista);
                            Debug.WriteLine("[****].[OK].[Paso 4.3].[Datos recibidos y enviados al DataGridView]");
                        }

                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("[****].[ERROR].[CapaIgu].[Evento Click Import] " + ex.Message);
                        MessageBox.Show("ERROR [Capa IGU].[Evento Click] " + ex.Message);
                    }
                };


                objBtnImport.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                this.Controls.Add(objBtnImport);
                objBtnImport.BringToFront();
                Debug.WriteLine("[****].[OK].[Paso 3.1].[CapaIgu].[Botón IMPORT construido correctamente]");

            }
            catch (Exception ex)
            {
                Debug.WriteLine("[****].[ERROR].[CapaIgu].[ConstruirBtnImport] " + ex.Message);
                MessageBox.Show("ERROR [Capa IGU].[ConstruirBtnImport] " + ex.Message);
            }
        }

    }
}
