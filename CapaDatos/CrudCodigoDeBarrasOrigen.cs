﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;


namespace CapaDatos
{
    public class CrudCodigoDeBarrasOrigen
    {
        private Conexion conexion = new Conexion();
                
        public List<ModeloCodigoDeBarrasOrigen> ReadOrigen() { 
        
            var lista = new List<ModeloCodigoDeBarrasOrigen>();
            
            try
            {            
                using (var db = conexion.ObtenerConexion())
                {
                    db.Open();
                    string @read = "SELECT " +
                        "IdIdentity, Radicado, Id, Empleado, Identificacion, Tipo_Documental, Codigo_De_Barras_Recepcion, " +
                        "CB_Documento, CB_Expediente, CB_Caja FROM CodigoDeBarrasOrigen";

                    using (SqlCommand readSql = new SqlCommand(read, db))
                    using (SqlDataReader runReadSQL = readSql.ExecuteReader())
                    {
                        while (runReadSQL.Read())
                        {
                            var modelo = new ModeloCodigoDeBarrasOrigen
                            {
                                IdIdentity = runReadSQL.GetInt32(0),
                                Radicado = runReadSQL.GetInt64(1),
                                Id = runReadSQL.GetInt64(2),
                                Empleado = runReadSQL.GetString(3),
                                Identificacion = runReadSQL.GetString(4),
                                TipoDocumental = runReadSQL.GetString(5),
                                CodigoDeBarrasRecepcion = runReadSQL.GetString(6),
                                CbDocumento = runReadSQL.GetString(7),
                                CbExpediente = runReadSQL.GetString(8),
                                CbCaja = runReadSQL.GetString(9)
                            };
                            lista.Add(modelo);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al leer los orígenes de código de barras: " + ex.Message);
            }

            return lista;
        }

        public List<ModeloCodigoDeBarrasOrigen> ReadOrigenId()
        {
            var lista = new List<ModeloCodigoDeBarrasOrigen>();
            try
            {
                
            }
            catch (Exception ex)
            {
                throw new Exception("Error al leer los orígenes de código de barras por Id: " + ex.Message);
            }
            return lista;
        }

    }
}
