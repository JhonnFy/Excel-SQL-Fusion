﻿using System;
using System.Collections.Generic;
using System.IO;
using OfficeOpenXml;
using CapaNegocio;
using CapaDatos;

namespace CapaControlador
{
    public class CapaControladorOrigen
    {
        private readonly CapaNegocioOrigen objCapaNegocioOrigen;

        public CapaControladorOrigen()
        {
            objCapaNegocioOrigen = new CapaNegocioOrigen();
        }

        // Método para importar registros desde Excel y devolver la lista de modelos
        public List<ModeloCodigoDeBarrasOrigen> ImportarDesdeExcel(string rutaArchivo)
        {
            var listaExcel = new List<ModeloCodigoDeBarrasOrigen>();

            try
            {
                if (!File.Exists(rutaArchivo))
                    throw new FileNotFoundException("[ImportarDesdeExcel].[El Archivo no Existe]", rutaArchivo);

                using (var package = new ExcelPackage(new FileInfo(rutaArchivo)))
                {
                    ExcelWorksheet hoja = package.Workbook.Worksheets[0];
                    int filas = hoja.Dimension.End.Row;

                    for (int row = 2; row <= filas; row++)
                    {
                        var objModelo = new ModeloCodigoDeBarrasOrigen
                        {
                            Radicado = long.TryParse(hoja.Cells[row, 1].Value?.ToString(), out long radicado) ? radicado : 0,
                            Id = long.TryParse(hoja.Cells[row, 2].Value?.ToString(), out long id) ? id : 0,
                            Empleado = hoja.Cells[row, 3].Value?.ToString() ?? "",
                            Identificacion = hoja.Cells[row, 4].Value?.ToString() ?? "",
                            TipoDocumental = hoja.Cells[row, 5].Value?.ToString() ?? "",
                            CodigoDeBarrasRecepcion = hoja.Cells[row, 6].Value?.ToString() ?? "",
                            CbDocumento = hoja.Cells[row, 7].Value?.ToString() ?? "",
                            CbExpediente = hoja.Cells[row, 8].Value?.ToString() ?? "",
                            CbCaja = hoja.Cells[row, 9].Value?.ToString() ?? ""
                        };
                        listaExcel.Add(objModelo);
                    }
                }

                // Invocar Negocio para guardar en DB
                int totalGuardados = objCapaNegocioOrigen.GuardarListaOrigen(listaExcel);
                Console.WriteLine($"[CapaControladorOrigen] Total de registros guardados: {totalGuardados}");
            }
            catch (Exception ex)
            {
                // Lanzar excepción para que la IGU pueda capturarla
                throw new Exception("[ImportarDesdeExcel].[Error al guardar los registros] " + ex.Message, ex);
            }

            return listaExcel;
        }

        // Método para guardar registros directamente desde lista
        public int GuardarRegistrosOrigen(List<ModeloCodigoDeBarrasOrigen> listaExcel)
        {
            if (listaExcel == null || listaExcel.Count == 0)
                throw new ArgumentException("[GuardarRegistrosOrigen].[La lista de registros está vacía o es nula]");

            try
            {
                int totalGuardados = objCapaNegocioOrigen.GuardarListaOrigen(listaExcel);
                Console.WriteLine($"[CapaControladorOrigen] Total de registros guardados: {totalGuardados}");
                return totalGuardados;
            }
            catch (Exception ex)
            {
                throw new Exception("[GuardarRegistrosOrigen].[Error al guardar los registros] " + ex.Message, ex);
            }
        }
    }
}
