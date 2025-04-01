using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.Data;
using System.Data.SqlClient;
using APIformbuilder.Models;
using Microsoft.AspNetCore.Cors;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Win32;
using Microsoft.AspNetCore.Authorization;
using System.IO.Pipelines;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Xml.Linq;

namespace APIformbuilder.Controllers
{
    [EnableCors("ReglasCorse")]
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]




    public class ConfigFormController : ControllerBase
    {
        private readonly string cadenaSQL;
        public ConfigFormController(IConfiguration config)
        {
            cadenaSQL = config.GetConnectionString("CadenaSQL");
        }

       
        public class Nomenclador
        {
            public string Nombre { get; set; }
            public string Codigo { get; set; }
        }

        //***************LISTAR FORMULARIOS MENU**************
        [HttpGet]
        [Route("ListaFormulariosMenu")]
        public IActionResult ListaFormMenu()
        {
            List<ConfigFormMenu> lista = new List<ConfigFormMenu>();
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("ObtenerFormulariosMenu", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new ConfigFormMenu()
                            {
                                IdConfigForm = Convert.ToInt32(rd["ID"]),
                                Titulo = rd["Titulo_Formulario"].ToString(),
                            });
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { lista });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }
        //***************Buscar  Articulos**************
        [HttpGet]
        [Route("ListarCartas")]
        public IActionResult ListarCartas(int pInter)
        {
            List<ListarCarta> lista = new List<ListarCarta>();
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("ListarCarta", conexion);
                    cmd.Parameters.AddWithValue("@pInter", pInter);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new ListarCarta()
                            {
                                deposito = rd["deposito"].ToString(),
                                carta = rd["carta"].ToString()

                            }); ; ;
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { lista });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }
        //***************Buscar  Articulos**************
        [HttpGet]
        [Route("BuscarArticulos")]
        public IActionResult BuscarArticulos(int pTipo, string pNombre)
        {
            List<ListarArticulosTodos> lista = new List<ListarArticulosTodos>();
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("BuscaArticulosNombre", conexion);
                    cmd.Parameters.AddWithValue("@pTipo", pTipo);
                    cmd.Parameters.AddWithValue("@pNombre", pNombre);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new ListarArticulosTodos()
                            {
                                articulosid = Convert.ToInt32(rd["articulosid"]),
                                codigo = rd["codigo"].ToString(),
                                nombre = rd["nombre"].ToString(),
                                precio = Convert.ToDecimal(rd["precio"]),
                                nroregistro = rd["nroregistro"].ToString(),
                                tipomedicamentos = rd["tipomedicamentos"].ToString(),
                                sector = rd["sector"].ToString(),
                                stockminimo = rd["stockminimo"].ToString(),
                                stockmedio = rd["stockmedio"].ToString(),
                                stockmaximo = rd["stockmaximo"].ToString(),
                                troquel = rd["troquel"].ToString(),
                                codbarra = rd["codbarra"].ToString()

                            }); ; ;
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { lista });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }

        //***************Buscar  Articulos**************
        [HttpGet]
        [Route("AlertaMedicamentos")]
        public IActionResult AlertaMedicamentos(int pTipo, int pCodigo, int pInstitucion)
        {
            List<AlertaMedicamento> lista = new List<AlertaMedicamento>();
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("ListarAlertas", conexion);
                    cmd.Parameters.AddWithValue("@pCodigo", pCodigo);
                    cmd.Parameters.AddWithValue("@pTipo", pTipo);
                    cmd.Parameters.AddWithValue("@pInstitucion", pInstitucion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new AlertaMedicamento()
                            {
                                articulosid = rd["articuloid"].ToString(),
                                nombre = rd["nombre"].ToString()

                            });
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { lista });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }
        //***************Buscar  Articulos**************
        [HttpGet]
        [Route("BuscarArticulosCodigo")]
        public IActionResult BuscarArticulosCodigo(int pTipo, string pCodigo)
        {
            List<ListarArticulosTodos> lista = new List<ListarArticulosTodos>();
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("BuscaArticulosCodigo", conexion);
                    cmd.Parameters.AddWithValue("@pTipo", pTipo);
                    cmd.Parameters.AddWithValue("@pCodigo", pCodigo);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new ListarArticulosTodos()
                            {
                                articulosid = Convert.ToInt32(rd["articulosid"]),
                                codigo = rd["codigo"].ToString(),
                                nombre = rd["nombre"].ToString(),
                                precio = Convert.ToDecimal(rd["precio"]),
                                nroregistro = rd["nroregistro"].ToString(),
                                tipomedicamentos = rd["tipomedicamentos"].ToString(),
                                sector = rd["sector"].ToString(),
                                stockminimo = rd["stockminimo"].ToString(),
                                stockmedio = rd["stockmedio"].ToString(),
                                stockmaximo = rd["stockmaximo"].ToString(),
                                troquel = rd["troquel"].ToString(),
                                codbarra = rd["codbarra"].ToString(),
                                Relacion = rd["Relacion"].ToString()

                            }); ; ;
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { lista });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }

        //***************Buscar  Articulos**************
        [HttpGet]
        [Route("TraeComprobante")]
        public IActionResult TraeComprobante()
        {
            List<TraeComprobante> lista = new List<TraeComprobante>();
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("TraeComprobante", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new TraeComprobante()
                            {
                                NewID = Convert.ToInt32(rd["NewID"])


                            }); ; ;
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { lista });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }
        //**************Verifico si existe codigo de articulo**********
        [HttpGet]
        [Route("VerificoCodigoArticulo")]
        public IActionResult VerificoCodigoArticulo(string pcodigo, int pinstitucion)
        {
            List<TraeComprobante> lista = new List<TraeComprobante>();
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("VerificoCodigoArticulos", conexion);
                    cmd.Parameters.AddWithValue("@pcodigo", pcodigo);
                    cmd.Parameters.AddWithValue("@pinstitucion", pinstitucion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new TraeComprobante()
                            {
                                NewID = Convert.ToInt32(rd["codigo"])


                            }); ; ;
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { lista });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }
        //***************LISTAR Consumo**************
        [HttpGet]
        [Route("ListarConsumo")]
        public IActionResult ListarConsumo(int pTipo)
        {
            List<ListarConsumo> lista = new List<ListarConsumo>();
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("ListarConsumo", conexion);
                    cmd.Parameters.AddWithValue("@pTipo", pTipo);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new ListarConsumo()
                            {
                                consumoid = Convert.ToInt32(rd["consumoid"]),
                                codigoarticulo = rd["codigoarticulo"].ToString(),
                                nomarticulo = rd["nomarticulo"].ToString(),
                                NInternado = rd["NInternado"].ToString(),
                                fecha = rd["fecha"].ToString(),
                                dosis = rd["dosis"].ToString(),
                                //detalleInternado = rd["detalleInternado"].ToString(),
                                cantidad = rd["cantidad"].ToString(),
                                precio = rd["precio"].ToString(),
                                lote = rd["lote"].ToString(),
                                fecven = rd["fecven"].ToString()


                            }); ; ;
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { lista });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }
        //***************LISTAR Movimientoa**************
        [HttpGet]
        [Route("ListarMov")]
        public IActionResult ListarMov(int pInstitucion)
        {
            List<ListarMov> lista = new List<ListarMov>();
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("ListarMov", conexion);
                    cmd.Parameters.AddWithValue("@pInstitucion", pInstitucion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new ListarMov()
                            {
                                Identificador = Convert.ToInt32(rd["Identificador"]),
                                fecha = rd["fecha"].ToString(),
                                codigo = rd["codigo"].ToString(),
                                nombre = rd["nombre"].ToString(),
                                FechaVencimiento = rd["FechaVencimiento"].ToString(),
                                lote = rd["lote"].ToString(),
                                sector = rd["sector"].ToString(),
                                Movimiento = rd["Movimiento"].ToString(),
                                cantidad = rd["cantidad"].ToString(),



                            }); ; ;
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { lista });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }
        //***************LISTAR Deuda Paciente**************
        [HttpGet]
        [Route("ListarDeudaPaciente")]
        public IActionResult ListarDeudaPaciente(int pInstitucion, int pInter, int pTipo)
        {
            List<ListarDeudaPaciente> lista = new List<ListarDeudaPaciente>();
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("ListarDedudaPaciente", conexion);
                    cmd.Parameters.AddWithValue("@pInstitucion", pInstitucion);
                    cmd.Parameters.AddWithValue("@pInter", pInter);
                    cmd.Parameters.AddWithValue("@pTipo", pTipo);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new ListarDeudaPaciente()
                            {

                                
                                codigo = rd["codigo"].ToString(),
                                nombre = rd["nombre"].ToString(),
                                tipo = rd["tipo"].ToString(),
                                PU = rd["PU"].ToString(),
                                cantidad = rd["cantidad_Consumida"].ToString(),
                                Cantidad_Devuelta = rd["Cantidad_Devuelta"].ToString(),
                                faltante = rd["faltante"].ToString(),
                                deuda = rd["deuda"].ToString(),



                            }); ; ;
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { lista });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }
        //***************LISTAR Deuda Paciente**************
        [HttpGet]
        [Route("ListarDeudaPacienteMed")]
        public IActionResult ListarDeudaPacienteMed(int pInstitucion, int pInter, int pTipo)
        {
            List<ListarDeudaPacienteMed> lista = new List<ListarDeudaPacienteMed>();
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("ListarDedudaPaciente", conexion);
                    cmd.Parameters.AddWithValue("@pInstitucion", pInstitucion);
                    cmd.Parameters.AddWithValue("@pInter", pInter);
                    cmd.Parameters.AddWithValue("@pTipo", pTipo);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new ListarDeudaPacienteMed()
                            {

                                fecha = rd["fecha"].ToString(),
                                nombre = rd["nombre"].ToString(),
                                tipo = rd["tipo"].ToString(),
                                cantidad = rd["cantidad_Consumida"].ToString()
                               



                            }); ; ;
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { lista });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }
        //*******Deuda General
        [HttpGet]
        [Route("ListarDeudaGeneral")]
        public IActionResult ListarDeudaGeneral(int pInstitucion, string desde, string hasta)
        {
            List<ListarDeudaGeneralNN> lista = new List<ListarDeudaGeneralNN>();
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("ListadoDeudaGeneralNuevo", conexion);
                    cmd.Parameters.AddWithValue("@pInstitucion", pInstitucion);
                    cmd.Parameters.AddWithValue("@desde", desde);
                    cmd.Parameters.AddWithValue("@hasta", hasta);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new ListarDeudaGeneralNN()
                            {
                                Internado = rd["Internado"].ToString(),
                                dni = rd["dni"].ToString(),
                                paciente = rd["paciente"].ToString(),
                                telefono = rd["telefono"].ToString(),
                                Obrasocial = rd["Obrasocial"].ToString(),
                                tipointernacion = rd["tipointernacion"].ToString(),
                                FechaIngreso = rd["FechaIngreso"].ToString(),
                                FechaAlta = rd["FechaAlta"].ToString(),
                                total = rd["total"].ToString(),
                                Deuda = rd["Deuda"].ToString(),
                                carta = rd["carta"].ToString()



                            }); ; ;
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { lista });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }

        [HttpGet]
        [Route("ListarDeudaGeneral_Consumo")]
        public IActionResult ListarDeudaGeneral_Consumo(int pInstitucion, string desde, string hasta)
        {
            List<ListarDeudaGeneral_Consumo> lista = new List<ListarDeudaGeneral_Consumo>();
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("ListadoDeudaGeneral", conexion);
                    cmd.Parameters.AddWithValue("@pInstitucion", pInstitucion);
                    cmd.Parameters.AddWithValue("@desde", desde);
                    cmd.Parameters.AddWithValue("@hasta", hasta);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new ListarDeudaGeneral_Consumo()
                            {
                                Internado = rd["Internado"].ToString(),
                                paciente = rd["paciente"].ToString(),
                                Obrasocial = rd["Obrasocial"].ToString(),
                                //FechaIngreso = rd["FechaIngreso"].ToString(),
                                //FechaAlta = rd["FechaAlta"].ToString(),
                                total = rd["total"].ToString(),
                                Medicamento = rd["Medicamento"].ToString(),
                                Descartable = rd["Descartable"].ToString(),
                                fac = rd["fac"].ToString(),
                                pag = rd["pag"].ToString(),
                                devolucion = rd["devolucion"].ToString(),
                                Deuda = rd["Deuda"].ToString(),



                            }); ; ;
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { lista });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }
        [HttpGet]
        [Route("InformeGimbernart")]
        public IActionResult InformeGimbernart( string FechaDesde, string FechaHasta, int pInstitucion)
        {
            List<InformesGimbernart> lista = new List<InformesGimbernart>();
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("Informe_Ginbernat", conexion);
                    cmd.Parameters.AddWithValue("@FechaDesde", FechaDesde);
                    cmd.Parameters.AddWithValue("@FechaHasta", FechaHasta);
                    cmd.Parameters.AddWithValue("@pInstitucion", pInstitucion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new InformesGimbernart()
                            {
                                Internacion = rd["InternacionID"].ToString(),
                                Paciente = rd["NombrePaciente"].ToString(),
                                Obra_Social = rd["ObraSocial"].ToString(),
                                Tipo_Internacion = rd["TipoInternacion"].ToString(),
                                Fecha_Ingreso = rd["FechaIngreso"].ToString(),
                                Hora_Int = rd["FechaIngreso"].ToString(),
                                Fecha_Alta = rd["FechaAlta"].ToString(),
                                Hora_Alta = rd["HoraAlta"].ToString(),
                                Tipo_Alta = rd["TipoAlata"].ToString(),
                                Diagnostico = rd["Diagnostico"].ToString()
                                



                            }); ; ;
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { lista });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }
        //***************LISTAR Consumo**************
        [HttpGet]
        [Route("ListarDevolucion")]
        public IActionResult ListarDevolucion(int pTipo)
        {
            List<ListarDevolucion> lista = new List<ListarDevolucion>();
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("ListarDevolucion", conexion);
                    cmd.Parameters.AddWithValue("@pTipo", pTipo);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new ListarDevolucion()
                            {
                                consumoid = Convert.ToInt32(rd["consumoid"]),
                                codigoarticulo = rd["codigoarticulo"].ToString(),
                                nomarticulo = rd["nomarticulo"].ToString(),
                                NInternado = rd["NInternado"].ToString(),
                                fecha = rd["fecha"].ToString(),
                                total = rd["total"].ToString(),
                                //detalleInternado = rd["detalleInternado"].ToString(),
                                cantidad = rd["cantidad"].ToString(),
                                precio = rd["precio"].ToString(),
                                Lote = rd["Lote"].ToString(),
                                FecVen = rd["FecVen"].ToString()


                            }); ; ;
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { lista });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }
        //***************LISTAR Consumo Filtrado**************
        [HttpGet]
        [Route("ListarDevolucionFiltrado")]
        public IActionResult ListarDevolucionFiltrado(string pFechaDesde, string pFechaHasta, int Usuario, int pInstitucion)
        {
            List<ListarDevolucionListadoN> lista = new List<ListarDevolucionListadoN>();
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("ObtenerConsumosFiltrados", conexion);
                    cmd.Parameters.AddWithValue("@pFechaDesde", pFechaDesde);
                    cmd.Parameters.AddWithValue("@pFechaHasta", pFechaHasta);
                    cmd.Parameters.AddWithValue("@Usuario", Usuario);
                    cmd.Parameters.AddWithValue("@pInstitucion", pInstitucion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new ListarDevolucionListadoN()
                            {

                                fecha = rd["fecha"].ToString(),
                                NInternado = rd["NInternado"].ToString(),
                                codigo = rd["codigo"].ToString(),
                                nombre = rd["nombre"].ToString(),
                                detalleInternado = rd["detalleInternado"].ToString(),
                                Sector = rd["SectorNombre"].ToString(),
                                tipo = rd["tipo"].ToString(),
                                tipomov = rd["tipomov"].ToString(),
                                Usuario = rd["nombreUsuario"].ToString(),
                                cantidad = rd["cantidad"].ToString(),
                                preciocosto = rd["preciocosto"].ToString(),
                                precio = rd["precio"].ToString(),
                                total = rd["total"].ToString(),



                            }); ; ;
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { lista });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }
        //***************LISTAR Consumo Filtrado**************
        [HttpGet]
        [Route("ListarConsumoFiltrado")]
        public IActionResult ListarConsumoFiltrado(string pFechaDesde, string pFechaHasta, int Usuario, int pInstitucion)
        {
            List<ListarDevolucionListadoN> lista = new List<ListarDevolucionListadoN>();
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("ObtenerConsumosFiltradosInfo", conexion);
                    cmd.Parameters.AddWithValue("@pFechaDesde", pFechaDesde);
                    cmd.Parameters.AddWithValue("@pFechaHasta", pFechaHasta);
                    cmd.Parameters.AddWithValue("@Usuario", Usuario);
                    cmd.Parameters.AddWithValue("@pInstitucion", pInstitucion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new ListarDevolucionListadoN()
                            {

                                fecha = rd["fecha"].ToString(),
                                NInternado = rd["NInternado"].ToString(),
                                codigo = rd["codigo"].ToString(),
                                nombre = rd["nombre"].ToString(),
                                tipo = rd["tipo"].ToString(),
                                tipomov = rd["tipomov"].ToString(),
                                preciocosto = rd["preciocosto"].ToString(),

                                detalleInternado = rd["detalleInternado"].ToString(),
                                Sector = rd["SectorNombre"].ToString(),
                                Usuario = rd["nombreUsuario"].ToString(),
                              
                                cantidad = rd["cantidad"].ToString(),
                                precio = rd["precio"].ToString(),
                                total = rd["total"].ToString(),
                                carta = rd["carta"].ToString(),



                            }); ; ;
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { lista });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }
        //***************LISTAR Consumo Filtrado**************
        [HttpGet]
        [Route("ObtenerEconoomatoFiltradosInfo")]
        public IActionResult ObtenerEconoomatoFiltradosInfo(string pFechaDesde, string pFechaHasta, int Usuario, int pInstitucion)
        {
            List<ListarEconomatoListadoN> lista = new List<ListarEconomatoListadoN>();
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("ObtenerEconoomatoFiltradosInfo", conexion);
                    cmd.Parameters.AddWithValue("@pFechaDesde", pFechaDesde);
                    cmd.Parameters.AddWithValue("@pFechaHasta", pFechaHasta);
                    cmd.Parameters.AddWithValue("@Usuario", Usuario);
                    cmd.Parameters.AddWithValue("@pInstitucion", pInstitucion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new ListarEconomatoListadoN()
                            {

                                fecha = rd["fecha"].ToString(),
                               
                                codigo = rd["codigo"].ToString(),
                                nombre = rd["nombre"].ToString(),
                                tipo = rd["tipo"].ToString(),
                                preciocosto = rd["preciocosto"].ToString(),

                               
                                
                                Usuario = rd["nombreUsuario"].ToString(),

                                cantidad = rd["cantidad"].ToString(),
                                precio = rd["precio"].ToString(),
                                total = rd["total"].ToString(),
                               



                            }); ; ;
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { lista });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }
        //***************LISTAR Devolucion Id**************
        [HttpGet]
        [Route("ListarDevolucionId")]
        public IActionResult ListarDevolucionId(int pTipo, int pId)
        {
            List<ListarDevolucionId> lista = new List<ListarDevolucionId>();
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("ListarDevolucionId", conexion);
                    cmd.Parameters.AddWithValue("@pTipo", pTipo);
                    cmd.Parameters.AddWithValue("@pId", pId);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new ListarDevolucionId()
                            {
                                consumoid = Convert.ToInt32(rd["consumoid"]),
                                codigo = rd["codigoarticulo"].ToString(),
                                nombre = rd["nomarticulo"].ToString().Trim(),
                                NInternado = rd["NInternado"].ToString(),
                                fecha = rd["fecha"].ToString(),
                                total = rd["total"].ToString(),
                                tipo = rd["tipom"].ToString(),
                                Devolucion = rd["Devolucion"].ToString().Trim(),
                                cantidad = rd["cantidad"].ToString(),
                                precio = rd["precio"].ToString(),
                                fecven = rd["fecven"].ToString(),
                                lote = rd["lote"].ToString()


                            }); ; ;
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { lista });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }
        //***************LISTAR Consumo Id**************
        [HttpGet]
        [Route("ListarConsumoId")]
        public IActionResult ListarConsumoId(int pTipo, int pId)
        {
            List<ListarConsumoId> lista = new List<ListarConsumoId>();
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("ListarConsumoId", conexion);
                    cmd.Parameters.AddWithValue("@pTipo", pTipo);
                    cmd.Parameters.AddWithValue("@pId", pId);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new ListarConsumoId()
                            {
                                consumoid = Convert.ToInt32(rd["consumoid"]),
                                codigo = rd["codigoarticulo"].ToString(),
                                nombre = rd["nomarticulo"].ToString(),
                                NInternado = rd["NInternado"].ToString(),
                                fecha = rd["fecha"].ToString(),
                                total = rd["total"].ToString(),
                                tipo = rd["tipom"].ToString(),
                                cantidad = rd["cantidad"].ToString(),
                                precio = rd["precio"].ToString(),
                                lote = rd["lote"].ToString(),
                                fecven = rd["fecven"].ToString()


                            }); ; ;
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { lista });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }

        //***************LISTAR Consumo Id**************ListarReceta
        [HttpGet]
        [Route("ListarRecetas")]
        public IActionResult ListarRecetas(int pTipo, int pId)
        {
            List<ListarRecetas> lista = new List<ListarRecetas>();
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("ListarReceta", conexion);
                    cmd.Parameters.AddWithValue("@pTipo", pTipo);
                    cmd.Parameters.AddWithValue("@pId", pId);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new ListarRecetas()
                            {

                                CodigoBarra = rd["CodigoBarra"].ToString(),
                                Troquel = rd["Troquel"].ToString(),
                                Nombre = rd["Nombre"].ToString()


                            }); ; ;
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { lista });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }

        [HttpGet]
        [Route("ListarDatosInformes")]
        public IActionResult ListarDatosInformes(int pI, int pN)
        {
            List<ListaDatosInforme> lista = new List<ListaDatosInforme>();
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("ListarDatosInforme", conexion);
                    cmd.Parameters.AddWithValue("@pI", pI);
                    cmd.Parameters.AddWithValue("@pN", pN);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new ListaDatosInforme()
                            {

                                fecha = rd["fecha"].ToString(),
                                detalleInternado = rd["detalleInternado"].ToString(),
                                sector = rd["sector"].ToString()


                            }); ; ;
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { lista });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }
        [HttpGet]
        [Route("ListarDatosInternados")]
        public IActionResult ListarDatosInternados(int pI, int pN)
        {
            List<ListaDatosInforme> lista = new List<ListaDatosInforme>();
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("ListarDatosInternado", conexion);
                    cmd.Parameters.AddWithValue("@pI", pI);
                    cmd.Parameters.AddWithValue("@pN", pN);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new ListaDatosInforme()
                            {

                                fecha = rd["fecha"].ToString(),
                                detalleInternado = rd["detalleInternado"].ToString(),
                                sector = rd["sector"].ToString()


                            }); ; ;
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { lista });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }
        [HttpGet]
        [Route("ListarDatosEconomato")]
        public IActionResult ListarDatosEconomato(int pI, string pN)
        {
            List<ListaDatosEconomato> lista = new List<ListaDatosEconomato>();
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("ListarDatosEconomato", conexion);
                    cmd.Parameters.AddWithValue("@pI", pI);
                    cmd.Parameters.AddWithValue("@pN", pN);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new ListaDatosEconomato()
                            {

                                fecha = rd["fecha"].ToString(),
                                proveedor = rd["proveedor"].ToString(),
                                nompres = rd["nompres"].ToString()


                            }); ; ;
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { lista });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }
        //***************LISTAR Consumo**************
        [HttpGet]
        [Route("ListarTipoMovimiento")]
        public IActionResult ListarTipoMovimiento(int pTipo, int pId)
        {
            List<TipoMovimientos> lista = new List<TipoMovimientos>();
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("TipoMovimientos", conexion);
                    cmd.Parameters.AddWithValue("@pId", pId);
                    cmd.Parameters.AddWithValue("@pTipo", pTipo);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new TipoMovimientos()
                            {
                                idMovimientoStock = rd["idMovimientoStock"].ToString()


                            }); ; ;
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { lista });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }

        //***************LISTAR Consumo Ingreso**************
        [HttpGet]
        [Route("ListarConsumoIngreso")]
        public IActionResult ListarConsumoIngreso(int pTipo)
        {
            List<ListarConsumoIngreso> lista = new List<ListarConsumoIngreso>();
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("ListarConsumoIngreso", conexion);
                    cmd.Parameters.AddWithValue("@pTipo", pTipo);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new ListarConsumoIngreso()
                            {
                                consumoid = Convert.ToInt32(rd["consumoid"]),
                                codigo = rd["codigoarticulo"].ToString(),
                                nombre = rd["nomarticulo"].ToString(),
                                fecha = rd["fecha"].ToString(),
                                comprobante = rd["comprobante"].ToString(),
                                cantidad = rd["cantidad"].ToString(),
                                precio = rd["precio"].ToString(),



                            }); ; ;
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { lista });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }

        //***************LISTAR Consumo Ingreso**************
        [HttpGet]
        [Route("ListarVencidos")]
        public IActionResult ListarVencidos(int pTipo, int pInstitucion, string pFechaDesde, string pFechaHasta, string pdias)
        {
            List<ListarVencidos> lista = new List<ListarVencidos>();
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("ListarVencidos", conexion);
                    cmd.Parameters.AddWithValue("@pTipo", pTipo);
                    cmd.Parameters.AddWithValue("@pInstitucion", pInstitucion);
                    cmd.Parameters.AddWithValue("@pFechaDesde", pFechaDesde);
                    cmd.Parameters.AddWithValue("@pFechaHasta", pFechaHasta);
                    cmd.Parameters.AddWithValue("@pdias", pdias);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new ListarVencidos()
                            {
                                articuloid = rd["articuloid"].ToString(),
                                nombre = rd["nombre"].ToString(),
                                fecven = rd["fecven"].ToString(),
                                lote = rd["lote"].ToString(),
                                ingreso = rd["ingreso"].ToString(),
                                cantidad = rd["cantidad"].ToString(),
                                salida = rd["salida"].ToString(),



                            }); ; ;
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { lista });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }
        //***************LISTAR Consumo Ingreso**************
        [HttpGet]
        [Route("ListarIndicaciones")]
        public IActionResult ListarIndicaciones(int pInstitucion, string pInt)
        {
            List<ListarIndicaciones> lista = new List<ListarIndicaciones>();
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("ListarIndicaciones", conexion);
                    cmd.Parameters.AddWithValue("@pInstitucion", pInstitucion);
                    cmd.Parameters.AddWithValue("@pInt", pInt);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new ListarIndicaciones()
                            {
                                
                                articulosid = rd["articulosid"].ToString().Trim(),
                                codigo = rd["codigo"].ToString().Trim(),
                                nombre = rd["nombre"].ToString().Trim(),
                                fecha = rd["fecha"].ToString().Trim(),
                                nro = rd["nro"].ToString().Trim(),
                                precio = rd["precio"].ToString().Trim(),
                                dosis = rd["dosis"].ToString().Trim(),
                                cantidad = rd["cantidad"].ToString().Trim(),
                                unico = rd["unico"].ToString().Trim()




                            }); ; ;
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { lista });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }
        //***************LISTAR Consumo Ingreso**************
        [HttpGet]
        [Route("ListarPagos")]
        public IActionResult ListarPagos()
        {
            List<ListarPagos> lista = new List<ListarPagos>();
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("ListarPagos", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new ListarPagos()
                            {

                                pagoid = rd["pagoid"].ToString().Trim(),
                                cantidad = rd["cantidad"].ToString().Trim(),
                                contribuyente = rd["contribuyente"].ToString().Trim(),
                                calle = rd["calle"].ToString().Trim(),
                                barrio = rd["barrio"].ToString().Trim(),
                                manzana = rd["manzana"].ToString().Trim(),
                                lote = rd["lote"].ToString().Trim(),
                                zona = rd["zona"].ToString().Trim(),
                                mtslineales = rd["mtslineales"].ToString().Trim(),
                                form = rd["form"].ToString().Trim(),
                                uc = rd["uc"].ToString().Trim(),
                                monto = rd["monto"].ToString().Trim(),
                                numrecibo = rd["numrecibo"].ToString().Trim(),
                                fechapago = rd["fechapago"].ToString().Trim(),
                                fechavencimiento = rd["fechavencimiento"].ToString().Trim()




                            }); ; ;
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { lista });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }
        //***************LISTAR Consumo Ingreso**************
        [HttpGet]
        [Route("ListadoPagos")]
        public IActionResult ListadoPagos(string pDesde, string pHasta)
        {
            List<ListarPagos> lista = new List<ListarPagos>();
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("ListadoPagos", conexion);
                    cmd.Parameters.AddWithValue("pFechaDesde", pDesde);
                    cmd.Parameters.AddWithValue("@pFechaHasta", pHasta);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new ListarPagos()
                            {

                                pagoid = rd["pagoid"].ToString().Trim(),
                                cantidad = rd["cantidad"].ToString().Trim(),
                                contribuyente = rd["contribuyente"].ToString().Trim(),
                                calle = rd["calle"].ToString().Trim(),
                                barrio = rd["barrio"].ToString().Trim(),
                                manzana = rd["manzana"].ToString().Trim(),
                                lote = rd["lote"].ToString().Trim(),
                                zona = rd["zona"].ToString().Trim(),
                                mtslineales = rd["mtslineales"].ToString().Trim(),
                                form = rd["form"].ToString().Trim(),
                                uc = rd["uc"].ToString().Trim(),
                                monto = rd["monto"].ToString().Trim(),
                                numrecibo = rd["numrecibo"].ToString().Trim(),
                                fechapago = rd["fechapago"].ToString().Trim(),
                                fechavencimiento = rd["fechavencimiento"].ToString().Trim()




                            }); ; ;
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { lista });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }
        //***************LISTAR Consumo Ingreso**************
        [HttpGet]
        [Route("ListarConsumoIngresoRemito")]
        public IActionResult ListarConsumoIngresoRemito(int pTipo, string pId)
        {
            List<ListarConsumoIngresoP> lista = new List<ListarConsumoIngresoP>();
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("ListarConsumoIngresoRemito", conexion);
                    cmd.Parameters.AddWithValue("@pTipo", pTipo);
                    cmd.Parameters.AddWithValue("@pId", pId);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new ListarConsumoIngresoP()
                            {
                                consumoid = Convert.ToInt32(rd["consumoid"]),
                                codigo = rd["codigoarticulo"].ToString(),
                                nombre = rd["nomarticulo"].ToString(),
                                fecha = rd["fecha"].ToString(),
                                comprobante = rd["comprobante"].ToString(),
                                cantidad = rd["cantidad"].ToString(),
                                precio = rd["precio"].ToString(),
                                tipo = rd["tipom"].ToString(),
                                preciocosto = rd["preciocosto"].ToString(),
                                total = rd["total"].ToString(),
                                lote = rd["lote"].ToString(),
                                fecven = rd["fecven"].ToString()



                            }); ; ;
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { lista });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }

        //***************LISTAR ArticulosTODOS**************
        [HttpGet]
        [Route("ListarArticulosTodos")]
        public IActionResult ListarArticulosTodos(int pTipo)
        {
            List<ListarArticulosTodos2> lista = new List<ListarArticulosTodos2>();
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("ListarArticulosTodos", conexion);
                    cmd.Parameters.AddWithValue("@pTipo", pTipo);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new ListarArticulosTodos2()
                            {
                                articulosid = Convert.ToInt32(rd["articulosid"]),
                                codigo = rd["codigo"].ToString(),
                                nombre = rd["nombre"].ToString(),
                                precio = Convert.ToDecimal(rd["precio"]),
                                nroregistro = rd["nroregistro"].ToString(),
                                tipomedicamentos = rd["tipomedicamentos"].ToString(),
                                sector = rd["sector"].ToString(),
                                stockminimo = rd["stockminimo"].ToString(),
                                stockmedio = rd["stockmedio"].ToString(),
                                stockmaximo = rd["stockmaximo"].ToString(),
                                troquel = rd["troquel"].ToString(),
                                codbarra = rd["codbarra"].ToString(),
                                dosis = rd["dosis"].ToString(),
                                forma = rd["forma"].ToString(),
                                tamano = rd["tamano"].ToString(),
                                categoria = rd["categoria"].ToString(),
                                traza = Convert.ToInt32(rd["traza"]),
                                stockcero = Convert.ToInt32(rd["stockcero"])

                            });
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { lista });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }
        //*****************************************************************************************************
        //***************LISTAR Articulos**************
        [HttpGet]
        [Route("ListarArticulos")]
        public IActionResult ListarArticulos(int pTipo)
        {
            List<ListarArticulos> lista = new List<ListarArticulos>();
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("ListarArticulos", conexion);
                    cmd.Parameters.AddWithValue("@pTipo", pTipo);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new ListarArticulos()
                            {
                                articulosid = Convert.ToInt32(rd["articulosid"]),
                                codigo = rd["codigo"].ToString(),
                                nombre = rd["nombre"].ToString(),
                                precio = Convert.ToDecimal(rd["precio"]),
                                nroregistro = rd["nroregistro"].ToString()
                                // institucionid = Convert.ToInt32(rd["institucionid"])

                            }); ; ;
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { lista });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }

        //*****************************************************************************************************
        //***************LISTAR Articulos**************
        [HttpGet]
        [Route("ListarArticulosDev")]
        public IActionResult ListarArticulosDev(int pTipo, int pInt)
        {
            List<ListarArticulos> lista = new List<ListarArticulos>();
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("ListarArticulosDev", conexion);
                    cmd.Parameters.AddWithValue("@pTipo", pTipo);
                    cmd.Parameters.AddWithValue("@pInt", pInt);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new ListarArticulos()
                            {
                                articulosid = Convert.ToInt32(rd["articulosid"]),
                                codigo = rd["codigo"].ToString(),
                                nombre = rd["nombre"].ToString(),
                                precio = Convert.ToDecimal(rd["precio"]),
                                nroregistro = rd["nroregistro"].ToString()
                                // institucionid = Convert.ToInt32(rd["institucionid"])

                            }); ; ;
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { lista });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }

        //*****************************************************************************************************
        //***************LISTAR AlfaBeta**************
        [HttpGet]
        [Route("ListaAlfaBeta")]
        public IActionResult ListaAlfaBeta(int pTipo)
        {
            List<ListaAlfaBeta> lista = new List<ListaAlfaBeta>();
            try
            {


                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("ListaAlfaBeta", conexion);
                    cmd.Parameters.AddWithValue("@pTipo", pTipo);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new ListaAlfaBeta()
                            {
                                troquel = rd["troquel"].ToString(),
                                CodBarras = rd["CodBarras"].ToString(),
                                NroRegistro = rd["NroRegistro"].ToString(),
                                nombre = rd["nombre"].ToString(),
                                presentacion = rd["presentacion"].ToString(),
                                Unidades = rd["Unidades"].ToString(),
                                IdMonodroga = rd["IdMonodroga"].ToString(),
                                CodLab = rd["CodLab"].ToString(),
                                Laboratorio = rd["Laboratorio"].ToString(),
                                Monodroga = rd["Monodroga"].ToString(),
                                precio = rd["precio"].ToString()


                            });
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { lista });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }
        //---------------------------Lista Alfa Beta por nombre -------------------------------------
        //-----------------------------------------------------------------------------------------------------

        [HttpGet]
        [Route("BuscaAlfaBetaNombre")]
        public IActionResult BuscaAlfaBetaNombre(string pNombre)
        {
            List<ListaAlfaBeta> lista = new List<ListaAlfaBeta>();
            try
            {


                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("BuscaAlfaBetaNombre", conexion);
                    cmd.Parameters.AddWithValue("@pNombre", pNombre.Trim());
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new ListaAlfaBeta()
                            {
                                troquel = rd["troquel"].ToString(),
                                CodBarras = rd["CodBarras"].ToString(),
                                NroRegistro = rd["NroRegistro"].ToString(),
                                nombre = rd["nombre"].ToString(),
                                presentacion = rd["presentacion"].ToString(),
                                Unidades = rd["Unidades"].ToString(),
                                IdMonodroga = rd["IdMonodroga"].ToString(),
                                CodLab = rd["CodLab"].ToString(),
                                Laboratorio = rd["Laboratorio"].ToString(),
                                Monodroga = rd["Monodroga"].ToString(),
                                precio = rd["precio"].ToString(),
                                vigencia = rd["vigencia"].ToString()


                            });
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { lista });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }
        [HttpGet]
        [Route("BuscadorMedicamento")]
        public IActionResult BuscadorMedicamento(string pNombre, int pTipo)
        {
            //var procedimiento = "BuscadorMedicamento";
            //if(pTipo == 2)
            //    procedimiento = "BuscaAlfaBetaNombre";

            List<BuscadorMedicamentos> lista = new List<BuscadorMedicamentos>();
            try
            {


                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();

                    // var cmd = new SqlCommand("BuscaAlfaBetaNombre", conexion);
                    var cmd = new SqlCommand("BuscadorMedicamento", conexion);
                    cmd.Parameters.AddWithValue("@pNombre", pNombre.Trim());
                    cmd.Parameters.AddWithValue("@pTipo", pTipo);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new BuscadorMedicamentos()
                            {
                                troquel = rd["troquel"].ToString(),
                                CodBarras = rd["CodBarras"].ToString(),
                                NroRegistro = rd["NroRegistro"].ToString(),
                                nombre = rd["nombre"].ToString(),
                                presentacion = rd["presentacion"].ToString(),
                                Unidades = rd["Unidades"].ToString(),
                                IdMonodroga = rd["IdMonodroga"].ToString(),
                                CodLab = rd["CodLab"].ToString(),
                                Laboratorio = rd["Laboratorio"].ToString(),
                                Monodroga = rd["Monodroga"].ToString(),
                                precio = rd["precio"].ToString(),
                                vigencia = rd["vigencia"].ToString(),
                                accion = rd["accion"].ToString()


                            });
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { lista });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }

        //***************LISTAR ListaMedicamentos Clinicas**************
        [HttpGet]
        [Route("ListarMedicamentosClinicas")]
        public IActionResult ListarMedicamentosClinicas(int pIdClinica)
        {
            List<Listarmedicamentosclinicas> lista = new List<Listarmedicamentosclinicas>();
            try
            {



                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("ListarMedicamentosClinicas", conexion);
                    cmd.Parameters.AddWithValue("@pIdClinica", pIdClinica);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new Listarmedicamentosclinicas()
                            {
                                // IdArticulo = rd["IdArticulo"].ToString(),
                                // Nombre = rd["Nombre"].ToString(),
                                // Codigo = rd["Codigo"].ToString(),
                                //Tipo_Articulo = rd["Tipo_Articulo"].ToString(),
                                //Precio_Venta = rd["Precio_Venta"].ToString(),
                                //Precio_Costo = rd["Precio_Costo"].ToString(),
                                //Modifica_Manual = rd["Modifica_Manual"].ToString(),
                                //Stock_Maximo = rd["Stock_Maximo"].ToString(),
                                //Stock_Medio = rd["Stock_Medio"].ToString(),
                                //Stock_Minimo = rd["Stock_Minimo"].ToString(),
                                //Troquel = rd["Troquel"].ToString(),
                                //Barra = rd["Barra"].ToString(),
                                //Descartable_art = rd["Descartable_art"].ToString(),
                                //Urgencia = rd["Urgencia"].ToString(),
                                //Gastosnn = rd["Gastosnn"].ToString(),
                                //SinCargo = rd["SinCargo"].ToString(),
                                //Afacturar = rd["Afacturar"].ToString(),
                                //SinCargoIn = rd["SinCargoIn"].ToString(),
                                //AfacturarIn  = rd["AfacturarIn"].ToString(),
                                //Anulado = rd["Anulado"].ToString(),
                                //IdInstitucion = rd["IdInstitucion"].ToString(),

                                Id_ConfigForm = Convert.ToInt32(rd["Id_ConfigForm"]),
                                Id_Field = Convert.ToInt32(rd["Id_Field"]),
                                Id_Answer = Convert.ToInt32(rd["Id_Answer"]),
                                nombre = rd["nombre"].ToString(),
                                valor = rd["valor"].ToString(),
                                identificador_fila = Convert.ToInt32(rd["identificador_fila"]),




                            });
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { lista });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }

        //***************LISTAR ListaMedicamentos**************
        [HttpGet]
        [Route("ListaMedicamentos")]
        public IActionResult ListaMedicamentos(int pIdClinica, string pIdUsuario, string pIdPass)
        {
            List<ListaAlfaBeta> lista = new List<ListaAlfaBeta>();
            try
            {



                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("ListarMedicamentos", conexion);
                    cmd.Parameters.AddWithValue("@pIdClinica", pIdClinica);
                    cmd.Parameters.AddWithValue("@pIdUsuario", pIdUsuario);
                    cmd.Parameters.AddWithValue("@pIdPass", pIdPass);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new ListaAlfaBeta()
                            {
                                troquel = rd["troquel"].ToString(),
                                CodBarras = rd["CodBarras"].ToString(),
                                NroRegistro = rd["NroRegistro"].ToString(),
                                nombre = rd["nombre"].ToString(),
                                presentacion = rd["presentacion"].ToString(),
                                Unidades = rd["Unidades"].ToString(),
                                IdMonodroga = rd["IdMonodroga"].ToString(),
                                CodLab = rd["CodLab"].ToString(),
                                precio = rd["precio"].ToString()


                            });
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { lista });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }
        //*****************************************************************************************************
        //***************LISTAR CAMPOS STRING**************
        [HttpGet]
        [Route("ListaCamposString")]
        public IActionResult ListaCamposString(int pTipo)
        {
            List<ListaCamposString> lista = new List<ListaCamposString>();
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("ListarCampos", conexion);
                    cmd.Parameters.AddWithValue("@pTipo", pTipo);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new ListaCamposString()
                            {
                                string_id = Convert.ToInt32(rd["string_id"]),
                                default_value = rd["default_value"].ToString(),
                                value_list = rd["value_list"].ToString(),
                                mask_library = rd["mask_library"].ToString(),
                                assumed_value = rd["assumed_value"].ToString(),
                                length = Convert.ToInt32(rd["length"])
                            });
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { lista });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }
        //***************LISTAR CAMPOS Combo**************
        [HttpGet]
        [Route("ListaField")]
        public IActionResult ListaField(int pTipo)
        {
            List<ListaField> lista = new List<ListaField>();
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("ListaField", conexion);
                    cmd.Parameters.AddWithValue("@pTipo", pTipo);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new ListaField()
                            {
                                Id_Field = Convert.ToInt32(rd["Id_Field"]),
                                nombre = rd["nombre"].ToString(),
                                opciones = rd["opciones"].ToString(),
                                posi = rd["posi"].ToString(),
                                ver = rd["ver"].ToString(),
                                urlapi = rd["urlapi"].ToString(),
                                orden = rd["orden"].ToString()

                            });
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { lista });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }
        //***************LISTAR Combo**************
        [HttpGet]
        [Route("ListaCombo")]
        public IActionResult ListaCombo(int pTipo, int pId)
        {
            List<ListarCombo> lista = new List<ListarCombo>();
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("ListarCombo", conexion);
                    cmd.Parameters.AddWithValue("@pTipo", pTipo);
                    cmd.Parameters.AddWithValue("@pId", pId);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new ListarCombo()
                            {
                                codigo = Convert.ToInt32(rd["codigo"]),
                                nombre = rd["nombre"].ToString(),
                                campo = rd["urlapi"].ToString().Trim()

                            });
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { lista });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }
        //***************LISTAR Grafico**************
        [HttpGet]
        [Route("ListaGrafico")]
        public IActionResult ListaGrafico(int pTipo, int pInstitucion)
        {
            List<ListarGrafico> lista = new List<ListarGrafico>();
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("GraficoConsumo10", conexion);
                    cmd.Parameters.AddWithValue("@pTipo", pTipo);
                    cmd.Parameters.AddWithValue("@pInstitucion", pInstitucion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new ListarGrafico()
                            {
                                nombre = rd["nombre"].ToString(),
                                cantidad = rd["cantidad"].ToString()

                            });
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { lista });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }
        //***************LISTAR Proveedores**************
        [HttpGet]
        [Route("ListaProveedores")]
        public IActionResult ListaProveedores(int pTipo, string pCodigo, int pInstitucion)
        {
            List<ListadoProveedor> lista = new List<ListadoProveedor>();
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("ListarProveedor", conexion);
                    cmd.Parameters.AddWithValue("@pTipo", pTipo);
                    cmd.Parameters.AddWithValue("@pCodigo", pCodigo);
                    cmd.Parameters.AddWithValue("@pInstitucion", pInstitucion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new ListadoProveedor()
                            {
                                codigo = rd["codigo"].ToString(),
                                nombre = rd["nombre"].ToString()


                            });
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { lista });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }
        //***************LISTAR Proveedores TODOS**************
        [HttpGet]
        [Route("ListaProveedoresTodos")]
        public IActionResult ListaProveedoresTodos(int pTipo, int pId)
        {
            List<ListadoProveedores> lista = new List<ListadoProveedores>();
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("ListaProveedores", conexion);
                    cmd.Parameters.AddWithValue("@pTipo", pTipo);
                    cmd.Parameters.AddWithValue("@pId", pId);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new ListadoProveedores()
                            {
                                proveedorid = Convert.ToInt32(rd["proveedorid"]),
                                nombre = rd["nombre"].ToString(),
                                nombrecontacto = rd["nombrecontacto"].ToString(),
                                telefono = rd["telefono"].ToString(),
                                correo = rd["correo"].ToString()



                            });
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { lista });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }
        //*****************************************************************************************************
        //***************LISTAR CAMPOS STRING**************
        [HttpGet]
        [Route("ListaCamposText")]
        public IActionResult ListaCamposText(int pTipo)
        {
            List<ListaCamposText> lista = new List<ListaCamposText>();
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("ListarText", conexion);
                    cmd.Parameters.AddWithValue("@pTipo", pTipo);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new ListaCamposText()
                            {
                                text_id = Convert.ToInt32(rd["text_id"]),
                                text_value = rd["text_value"].ToString(),
                                default_value = rd["default_value"].ToString(),
                                value_list = rd["value_list"].ToString(),
                                assumed_value = rd["assumed_value"].ToString(),
                                editable = Convert.ToInt32(rd["editable"])
                            });
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { lista });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }
        //***************LISTAR CAMPOS Date**************
        [HttpGet]
        [Route("ListaCamposDate")]
        public IActionResult ListaCamposDate(int pTipo)
        {
            List<ListaCamposDate> lista = new List<ListaCamposDate>();
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("ListarDate", conexion);
                    cmd.Parameters.AddWithValue("@pTipo", pTipo);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new ListaCamposDate()
                            {
                                date_id = Convert.ToInt32(rd["date_id"]),
                                enable_format = Convert.ToInt32(rd["enable_format"]),
                                use_date_format = Convert.ToInt32(rd["use_date_format"]),
                                date_format = rd["date_format"].ToString(),
                                value_list = rd["value_list"].ToString(),
                                use_range = Convert.ToInt32(rd["use_range"]),
                                upper_bound = Convert.ToInt32(rd["upper_bound"]),
                                upper_date = Convert.ToDateTime(rd["upper_date"]),
                                lower_bound = Convert.ToInt32(rd["lower_bound"]),
                                lower_date = Convert.ToDateTime(rd["lower_date"]),
                                assume_value = Convert.ToDateTime(rd["assume_value"])

                            });
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { lista });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }
        //***************LISTAR CAMPOS Date**************
        [HttpGet]
        [Route("ListaCamposDateTime")]
        public IActionResult ListaCamposDateTime(int pTipo)
        {
            List<ListaCamposDateTime> lista = new List<ListaCamposDateTime>();
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("ListarDateTime", conexion);
                    cmd.Parameters.AddWithValue("@pTipo", pTipo);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new ListaCamposDateTime()
                            {
                                datetime_id = Convert.ToInt32(rd["datetime_id"]),
                                enable_format = Convert.ToInt32(rd["enable_format"]),
                                date_format = rd["date_format"].ToString(),
                                use_date_format = Convert.ToInt32(rd["use_date_format"]),
                                time_format = rd["time_format"].ToString(),
                                value_list = Convert.ToDateTime(rd["value_list"]),
                                use_range = Convert.ToInt32(rd["use_range"]),
                                upper_bound = Convert.ToInt32(rd["upper_bound"]),
                                upper_date = Convert.ToDateTime(rd["upper_date"]),
                                upper_time = rd["upper_time"].ToString(),
                                lower_bound = Convert.ToInt32(rd["lower_bound"]),
                                lower_date = Convert.ToDateTime(rd["lower_date"]),
                                lower_time = rd["lower_time"].ToString(),
                                assume_value = rd["assume_value"].ToString(),
                                use_time_format = Convert.ToInt32(rd["use_time_format"]),
                            });
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { lista });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }
        //*****************************************************************************************************
        //***************LISTAR CAMPOS Integer**************
        [HttpGet]
        [Route("ListaCamposInteger")]
        public IActionResult ListaCamposInteger(int pTipo)
        {
            List<ListaCamposInteger> lista = new List<ListaCamposInteger>();
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("ListarCamposInteger", conexion);
                    cmd.Parameters.AddWithValue("@pTipo", pTipo);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new ListaCamposInteger()
                            {
                                integer_id = Convert.ToInt32(rd["integer_id"]),
                                default_value = Convert.ToInt32(rd["default_value"]),
                                value_list = rd["value_list"].ToString(),
                                min_configuration = rd["min_configuration"].ToString(),
                                min_value = Convert.ToInt32(rd["min_value"]),
                                max_configuration = rd["max_configuration"].ToString(),
                                max_value = Convert.ToInt32(rd["max_value"]),
                                assumed_value = Convert.ToInt32(rd["assumed_value"])

                            });
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { lista });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }
        //*****************************************************************************************************
        //***************LISTAR CAMPOS Double**************
        [HttpGet]
        [Route("ListaCamposDoble")]
        public IActionResult ListaCamposDouble(int pTipo)
        {
            List<ListaCamposDouble> lista = new List<ListaCamposDouble>();
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("ListarCamposDouble", conexion);
                    cmd.Parameters.AddWithValue("@pTipo", pTipo);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new ListaCamposDouble()
                            {
                                double_id = Convert.ToInt32(rd["double_id"]),
                                default_value = Convert.ToInt32(rd["default_value"]),
                                value_list = rd["value_list"].ToString(),
                                min_configuration = rd["min_configuration"].ToString(),
                                min_value = Convert.ToInt32(rd["min_value"]),
                                max_configuration = rd["max_configuration"].ToString(),
                                max_value = Convert.ToInt32(rd["max_value"]),
                                assumed_value = Convert.ToInt32(rd["assumed_value"])

                            });
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { lista });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }
        //***************LISTAR CAMPOS Double**************
        [HttpGet]
        [Route("ListaCamposBoolean")]
        public IActionResult ListaCamposBoolean(int pTipo)
        {
            List<ListaCamposBoolean> lista = new List<ListaCamposBoolean>();
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("ListarCamposBoolean", conexion);
                    cmd.Parameters.AddWithValue("@pTipo", pTipo);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new ListaCamposBoolean()
                            {
                                boolean_id = Convert.ToInt32(rd["boolean_id"]),
                                true_value = Convert.ToInt32(rd["true_value"]),
                                false_value = Convert.ToInt32(rd["false_value"]),
                                assumed_value = rd["assumed_value"].ToString()


                            });
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { lista });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }
        //***************LISTAR CAMPOS Double**************
        [HttpGet]
        [Route("ListaCamposLabel")]
        public IActionResult ListaCamposLabel(int pTipo)
        {
            List<ListaCamposLabel> lista = new List<ListaCamposLabel>();
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("ListarCamposLabel", conexion);
                    cmd.Parameters.AddWithValue("@pTipo", pTipo);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new ListaCamposLabel()
                            {
                                label_id = Convert.ToInt32(rd["label_id"]),
                                text_value = rd["text_value"].ToString(),
                                default_value = rd["default_value"].ToString(),
                                assumed_value = rd["assumed_value"].ToString()


                            });
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { lista });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }
        //***************LISTAR GRILLAS**************
        [HttpGet]
        [Route("ListaGrillas")]
        public IActionResult ListaGrillas(int pTipo)
        {
            List<ListaGrillas> lista = new List<ListaGrillas>();
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("ListarGrillas", conexion);
                    cmd.Parameters.AddWithValue("@pTipo", pTipo);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new ListaGrillas()
                            {
                                idConfigForm = Convert.ToInt32(rd["grillaid"]),
                                urlmodi = rd["urlmodi"].ToString().Trim(),
                                metodo = rd["metodo"].ToString().Trim()


                            });
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { lista });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }
        //***************LISTAR BOTONES**************
        [HttpGet]
        [Route("ListaBotones")]
        public IActionResult ListaBotones(int pTipo)
        {
            List<ListaBotones> lista = new List<ListaBotones>();
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("ListarBotones", conexion);
                    cmd.Parameters.AddWithValue("@pTipo", pTipo);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new ListaBotones()
                            {
                                idConfigForm = Convert.ToInt32(rd["botonesid"]),
                                metodo = rd["metodo"].ToString().Trim(),
                                texto = rd["texto"].ToString().Trim(),
                                color = rd["color"].ToString().Trim(),
                                icono = rd["icono"].ToString().Trim()


                            });
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { lista });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }
        //-----------------------------------------------------
        //------------------------------------------------------
        //-------Agregar Tablas---------------------------
        [HttpPost]
        [Route("AgregarTablas/{formulario}/{Parametros}/{valores}")]
        public IActionResult AgregarTablas(string formulario, string Parametros, string valores)
        {
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("GrabarTablas", conexion);
                    cmd.Parameters.AddWithValue("@formulario", formulario);
                    cmd.Parameters.AddWithValue("@Parametros", Parametros);
                    cmd.Parameters.AddWithValue("@valores", valores);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                return StatusCode(StatusCodes.Status200OK, new { message = "Se Grabaron Exitosamente lod datos" });
            }
            catch (Exception erx)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = erx.Message });
            }

        }
        [HttpPost]
        [Route("VerificaCarta/{pInter}/{pDeposito}/{pCarta}")]
        public IActionResult VerificaCarta(int pInter, string pDeposito, string pCarta)
        {
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("VerificoCarta", conexion);
                    cmd.Parameters.AddWithValue("@pInter", pInter);
                    cmd.Parameters.AddWithValue("@pDeposito", pDeposito);
                    cmd.Parameters.AddWithValue("@pCarta", pCarta);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                return StatusCode(StatusCodes.Status200OK, new { message = "Se Grabaron Exitosamente lod datos" });
            }
            catch (Exception erx)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = erx.Message });
            }

        }
        [HttpPost]
        [Route("VerificaAFacturar/{pInter}")]
        public IActionResult VerificaAFacturar(int pInter)
        {
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("VerificoAFacturar", conexion);
                    cmd.Parameters.AddWithValue("@pInter", pInter);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                return StatusCode(StatusCodes.Status200OK, new { message = "Se Grabaron Exitosamente lod datos" });
            }
            catch (Exception erx)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = erx.Message });
            }

        }
        //-------Agregar AlertaCorreo---------------------------
        [HttpPost]
        [Route("AgregarAlertaCorreo")]
        public IActionResult AgregarAlertaCorreo(string pnombre, string pcorreo, string pcodigo, int pinstitucionid)
        {
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("AgregarAlertacorreo", conexion);
                    cmd.Parameters.AddWithValue("@pnombre", pnombre);
                    cmd.Parameters.AddWithValue("@pcorreo", pcorreo);
                    cmd.Parameters.AddWithValue("@pcodigo", pcodigo);
                    cmd.Parameters.AddWithValue("@pinstitucionid", pinstitucionid);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                return StatusCode(StatusCodes.Status200OK, new { message = "Se Grabaron Exitosamente lod datos" });
            }
            catch (Exception erx)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = erx.Message });
            }
        }
        //-------Agregar Indicaciones---------------------------

        [HttpPost]
        [Route("AgregarIndicaciones")]
        public IActionResult AgregarIndicaciones(string pcodigo, string pfecha, string pcantidad, int pinstitucion, string idinternado, string dosis)
        {
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("AgregarIndicaciones", conexion);
                    cmd.Parameters.AddWithValue("@pcodigo", pcodigo);
                    cmd.Parameters.AddWithValue("@pfecha", pfecha);
                    cmd.Parameters.AddWithValue("@pcantidad", pcantidad);
                    cmd.Parameters.AddWithValue("@pinstitucion", pinstitucion);
                    cmd.Parameters.AddWithValue("@idinternado", idinternado);
                    cmd.Parameters.AddWithValue("@dosis", dosis);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                return StatusCode(StatusCodes.Status200OK, new { message = "Se Grabaron Exitosamente los datos" });
            }
            catch (Exception erx)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = erx.Message });
            }
        }
        //-------Agregar Tablas---------------------------
        [HttpPost]
        [Route("AgregarRelacionP")]
        public IActionResult AgregarRelacionP(int pTipo, string pCodigo, string pProveedor, int pInstitucion)
        {
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("GrabarRelacionProveedores", conexion);
                    cmd.Parameters.AddWithValue("@pTipo", pTipo);
                    cmd.Parameters.AddWithValue("@pCodigo", pCodigo);
                    cmd.Parameters.AddWithValue("@pProveedor", pProveedor);
                    cmd.Parameters.AddWithValue("@pInstitucion", pInstitucion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                return StatusCode(StatusCodes.Status200OK, new { message = "Se Grabaron Exitosamente lod datos" });
            }
            catch (Exception erx)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = erx.Message });
            }
        }
        //-------Agregar Tablas---------------------------
        [HttpPost]
        [Route("AgregarAlertas")]
        public IActionResult AgregarAlertas(int pTipo, string pCodigo, string pNombre, int pInstitucion)
        {
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("AgregarAlerta", conexion);
                    cmd.Parameters.AddWithValue("@pTipo", pTipo);
                    cmd.Parameters.AddWithValue("@pCodigo", pCodigo);
                    cmd.Parameters.AddWithValue("@pNombre", pNombre);
                    cmd.Parameters.AddWithValue("@pInstitucion", pInstitucion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                return StatusCode(StatusCodes.Status200OK, new { message = "Se Grabaron Exitosamente lod datos" });
            }
            catch (Exception erx)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = erx.Message });
            }
        }

        //***************LISTAR CAMPOS Double**************
        [HttpGet]
        [Route("ListaCamposTime")]
        public IActionResult ListaTime(int pTipo)
        {
            List<ListaCamposTime> lista = new List<ListaCamposTime>();
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("ListaCamposTime", conexion);
                    cmd.Parameters.AddWithValue("@pTipo", pTipo);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new ListaCamposTime()
                            {
                                time_id = Convert.ToInt32(rd["time_id"]),
                                enable_format = Convert.ToInt32(rd["enable_format"]),
                                use_time_format = Convert.ToInt32(rd["use_time_format"]),
                                time_format = rd["time_format"].ToString(),
                                value_list = rd["value_list"].ToString(),
                                use_range = Convert.ToInt32(rd["use_range"]),
                                upper_bound = Convert.ToInt32(rd["upper_bound"]),
                                upper_time = rd["upper_time"].ToString(),
                                lower_bound = Convert.ToInt32(rd["lower_bound"]),
                                lower_time = rd["lower_time"].ToString(),
                                assume_value = rd["assume_value"].ToString()
                            });
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { lista });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }
        //***************LISTAR Tipos**************
        [HttpGet]
        [Route("ListaTipos")]
        public IActionResult ListaTipos(int pTipo, int @pId)
        {
            List<ListarTipos> lista = new List<ListarTipos>();
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("ListarTipos", conexion);
                    cmd.Parameters.AddWithValue("@pTipo", pTipo);
                    cmd.Parameters.AddWithValue("@pId", pId);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new ListarTipos()
                            {
                                identificador = Convert.ToInt32(rd["identificador"]),
                                nombre = rd["nombre"].ToString()
                            });
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { lista });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }
        //***************LISTAR Tipos Registro**************
        [HttpGet]
        [Route("ListaTiposRegistro")]
        public IActionResult ListaTiposRegistro(int pTipo, int @pId)
        {
            List<ListarTipos> lista = new List<ListarTipos>();
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("ListarTipos_Registro", conexion);
                    cmd.Parameters.AddWithValue("@pTipo", pTipo);
                    cmd.Parameters.AddWithValue("@pId", pId);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new ListarTipos()
                            {
                                identificador = Convert.ToInt32(rd["identificador"]),
                                nombre = rd["nombre"].ToString()
                            });
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { lista });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }
        //***************LISTAR Tipos Registro**************
        [HttpGet]
        [Route("ListaArticulosStock")]
        public IActionResult ListaArticulosStock(int pTipo)
        {
            List<ListarArticulosStock> lista = new List<ListarArticulosStock>();
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("ListarArticulosStock", conexion);
                    cmd.Parameters.AddWithValue("@pTipo", pTipo);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new ListarArticulosStock()
                            {
                                Identificador = Convert.ToInt32(rd["Identificador"]),
                                codigo = rd["codigo"].ToString(),
                                nombre = rd["nombre"].ToString(),
                                FechaVencimiento = rd["FechaVencimiento"].ToString(),
                                lote = rd["lote"].ToString(),
                                sector = rd["sector"].ToString(),
                                nombresector = rd["nombresector"].ToString(),
                                stock_restante = rd["stock_restante"].ToString()



                            });
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { lista });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }
        //***************LISTAR Tipos Registro**************
        [HttpGet]
        [Route("ListaArticulosStockCritico")]
        public IActionResult ListaArticulosStockCritico(int pTipo)
        {
            List<ListarArticulosStockCritico> lista = new List<ListarArticulosStockCritico>();
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("ListarArticulosStockCritico", conexion);
                    cmd.Parameters.AddWithValue("@pTipo", pTipo);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new ListarArticulosStockCritico()
                            {
                                Identificador = Convert.ToInt32(rd["Identificador"]),
                                codigo = rd["codigo"].ToString(),
                                nombre = rd["nombre"].ToString(),
                                stock_minimo = rd["stock_minimo"].ToString(),
                                stock = rd["stock"].ToString()



                            });
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { lista });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }
        //***************LISTAR Tipos Registro**************
        [HttpGet]
        [Route("ListaStockPorArticulos")]
        public IActionResult ListaStockPorArticulos(int @pInstitucion, int @pCodigo, int @pSector)
        {
            List<ListarStockPorArticulos> lista = new List<ListarStockPorArticulos>();
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("ListarStockPorArticulos", conexion);
                    cmd.Parameters.AddWithValue("@pInstitucion", pInstitucion);
                    cmd.Parameters.AddWithValue("@pCodigo", pCodigo);
                    cmd.Parameters.AddWithValue("@pSector", pSector);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new ListarStockPorArticulos()
                            {
                                articuloid = Convert.ToInt32(rd["articuloid"]),
                                cantidad = rd["cantidad"].ToString(),
                                fecven = rd["fecven"].ToString(),
                                lote = rd["lote"].ToString(),
                                egreso = rd["egreso"].ToString(),
                                stock_restante = rd["stock_restante"].ToString()

                            });
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { lista });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }
        //***************Modifica Tbala**************
        [HttpPut]
        [Route("ModificarTabla/{formulario:int}/{Parametros}/{valores}")]
        public IActionResult ModificarTabla(int formulario, string Parametros, string valores)
        {
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("ModificarTablas", conexion);
                    cmd.Parameters.AddWithValue("@formulario", formulario);
                    cmd.Parameters.AddWithValue("@Parametros", Parametros);
                    cmd.Parameters.AddWithValue("@valores", valores);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();


                }
                return StatusCode(StatusCodes.Status200OK, new { message = "Modificado" });
            }
            catch (Exception erx)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = erx.Message });
            }
        }
        //***************Eliminar Tbala**************
        [HttpPut]
        [Route("EliminarTabla/{formulario:int}/{Parametros}/{valores}")]
        public IActionResult EliminarTabla(int formulario, string Parametros, string valores)
        {
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("EliminarTablas", conexion);
                    cmd.Parameters.AddWithValue("@formulario", formulario);
                    cmd.Parameters.AddWithValue("@Parametros", Parametros);
                    cmd.Parameters.AddWithValue("@valores", valores);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();


                }
                return StatusCode(StatusCodes.Status200OK, new { message = "Eliminardo" });
            }
            catch (Exception erx)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = erx.Message });
            }
        }
        //***************ELIMINA  CAMPOS**************
        [HttpPut]
        [Route("EliminaCampos/{pTipo:int}/{pId:int}")]
        public IActionResult EliminaCampos(int pTipo, int pId)//
        {
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("EliminaCampos", conexion);
                    cmd.Parameters.AddWithValue("@pTipo", pTipo);
                    cmd.Parameters.AddWithValue("@pId", pId);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();


                }
                return StatusCode(StatusCodes.Status200OK, new { message = "eliminado" });
            }
            catch (Exception erx)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = erx.Message });
            }
        }
        //***************ELIMINA  Economato**************
        [HttpPut]
        [Route("EliminaEconomato")]
        public IActionResult EliminaEconomato(int pComprobante, int pIdarticulo, string pLote, string pFecven, int IdInstitucion)
        {
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("EliminaEconomato", conexion);
                    cmd.Parameters.AddWithValue("@pComprobante", pComprobante);
                    cmd.Parameters.AddWithValue("@pIdarticulo", pIdarticulo);
                    cmd.Parameters.AddWithValue("@pLote", pLote);
                    cmd.Parameters.AddWithValue("@pFecven", pFecven);
                    cmd.Parameters.AddWithValue("@IdInstitucion", IdInstitucion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();


                }
                return StatusCode(StatusCodes.Status200OK, new { message = "eliminado" });
            }
            catch (Exception erx)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = erx.Message });
            }
        }
        //***************ELIMINA  Economato**************
        [HttpPut]
        [Route("ModificarCantidad")]
        public IActionResult ModificarCantidad(int pId, int pCantidad, int IdInstitucion)
        {
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("ModificarCantidad", conexion);
                    cmd.Parameters.AddWithValue("@pId", pId);
                    cmd.Parameters.AddWithValue("@pCantidad", pCantidad);
                    cmd.Parameters.AddWithValue("@IdInstitucion", IdInstitucion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();


                }
                return StatusCode(StatusCodes.Status200OK, new { message = "eliminado" });
            }
            catch (Exception erx)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = erx.Message });
            }
        }
        //***************ELIMINA  INDICACIONES**************
        [HttpPut]
        [Route("EliminaIndicaciones")]
        public IActionResult EliminaIndicaciones(int pInstitucion, int pId, int pIdInt)
        {
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("EliminaIndicaciones", conexion);
                    cmd.Parameters.AddWithValue("@pInstitucion", pInstitucion);
                    cmd.Parameters.AddWithValue("@pId", pId);
                    cmd.Parameters.AddWithValue("@pIdInt", pIdInt);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();


                }
                return StatusCode(StatusCodes.Status200OK, new { message = "eliminado" });
            }
            catch (Exception erx)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = erx.Message });
            }
        }

        //*****************************************************************************************************
        //***************MODIFICA  CAMPOS Integer**************
        [HttpPut]
        [Route("ModificaCamposInteger/{pTipo:int}/{pId:int}/{pdefault_value}/{pvalue_list}/{pmin_configuration}/{pmin_value:int}/{pmax_configuration}/{pmax_value:int}/{passumed_value:int}")]
        public IActionResult ModificaCamposInteger(int pTipo, int pId, int pdefault_value, string pvalue_list, string pmin_configuration, int pmin_value, string pmax_configuration, int pmax_value, int passumed_value)
        {
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("ModificaCamposInteger", conexion);
                    cmd.Parameters.AddWithValue("@pTipo", pTipo);
                    cmd.Parameters.AddWithValue("@pId", pId);
                    cmd.Parameters.AddWithValue("@pdefault_value", pdefault_value);
                    cmd.Parameters.AddWithValue("@pvalue_list", pvalue_list);
                    cmd.Parameters.AddWithValue("@pmin_configuration", pmin_configuration);
                    cmd.Parameters.AddWithValue("@pmin_value", pmin_value);
                    cmd.Parameters.AddWithValue("@pmax_configuration", pmax_configuration);
                    cmd.Parameters.AddWithValue("@pmax_value", pmax_value);
                    cmd.Parameters.AddWithValue("@passumed_value", passumed_value);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();


                }
                return StatusCode(StatusCodes.Status200OK, new { message = "Modificacion Exitosa" });
            }
            catch (Exception erx)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = erx.Message });
            }
        }
        //***************MODIFICA  Gield**************
        [HttpPut]
        [Route("ModificaField/{pTipo:int}/{pId_Field:int}/{pId_ConfigForm:int}/{pNombre}/{pOpcion}/{pOrden}/{pselectedItem}/{pselectedItemPosi}")]
        public IActionResult ModificaField(int pTipo, int pId_Field, int pId_ConfigForm, string pNombre, string pOpcion, string pOrden, string pselectedItem, string pselectedItemPosi)
        {
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("ModificarField", conexion);
                    cmd.Parameters.AddWithValue("@pTipo", pTipo);
                    cmd.Parameters.AddWithValue("@pId_Field", pId_Field);
                    cmd.Parameters.AddWithValue("@pId_ConfigForm", pId_ConfigForm);
                    cmd.Parameters.AddWithValue("@pNombre", pNombre);
                    cmd.Parameters.AddWithValue("@pOpcion", pOpcion);
                    cmd.Parameters.AddWithValue("@pOrden", pOrden);
                    cmd.Parameters.AddWithValue("@pselectedItem", pselectedItem);
                    cmd.Parameters.AddWithValue("@pselectedItemPosi", pselectedItemPosi);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();


                }
                return StatusCode(StatusCodes.Status200OK, new { message = "Modificacion Exitosa" });
            }
            catch (Exception erx)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = erx.Message });
            }
        }
        //*****************************************************************************************************
        //***************AGREGAR  CAMPOS Integer**************
        [HttpPost]
        [Route("AgregarCamposInteger/{pTipo:int}/{pId:int}/{pdefault_value:int}/{pvalue_list}/{pmin_configuration}/{pmin_value:int}/{pmax_configuration}/{pmax_value:int}/{passumed_value:int}")]
        public IActionResult AgregarCamposInteger(int pTipo, int pId, int pdefault_value, string pvalue_list, string pmin_configuration, int pmin_value, string pmax_configuration, int pmax_value, int passumed_value)
        {
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("AgregarCamposInteger", conexion);
                    cmd.Parameters.AddWithValue("@pTipo", pTipo);
                    cmd.Parameters.AddWithValue("@pId", pId);
                    cmd.Parameters.AddWithValue("@pdefault_value", pdefault_value);
                    cmd.Parameters.AddWithValue("@pvalue_list", pvalue_list);
                    cmd.Parameters.AddWithValue("@pmin_configuration", pmin_configuration);
                    cmd.Parameters.AddWithValue("@pmin_value", pmin_value);
                    cmd.Parameters.AddWithValue("@pmax_configuration", pmax_configuration);
                    cmd.Parameters.AddWithValue("@pmax_value", pmax_value);
                    cmd.Parameters.AddWithValue("@passumed_value", passumed_value);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();


                }
                return StatusCode(StatusCodes.Status200OK, new { message = "Se Graboron Exitosamente lod datos" });
            }
            catch (Exception erx)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = erx.Message });
            }
        }
        //*****************************************************************************************************
        //***************AGREGAR  Consumo**************
        [HttpPost]
        [AllowAnonymous]
        [Route("AgregarConsumo/{NInternado:int}/{articuloid:int}/{comprobante}/{receta}/{dosis}/{fecha}/{detalleInternado}/{cantidad:int}/{sector:int}/{usuario:int}/{tipo:int}/{lote}/{fecven}")]
        public IActionResult AgregarConsumo(int NInternado, int articuloid, string comprobante, string receta, string dosis, string fecha, string detalleInternado, int cantidad, int sector, int usuario, int tipo, string lote, string fecven)
        {
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("GrabarConsumo", conexion);
                    cmd.Parameters.AddWithValue("@NInternado", NInternado);
                    cmd.Parameters.AddWithValue("@articuloid", articuloid);
                    cmd.Parameters.AddWithValue("@comprobante", comprobante);
                    cmd.Parameters.AddWithValue("@receta", receta);
                    cmd.Parameters.AddWithValue("@fecha", fecha);
                    cmd.Parameters.AddWithValue("@dosis", dosis);
                    cmd.Parameters.AddWithValue("@detalleInternado", detalleInternado);
                    cmd.Parameters.AddWithValue("@cantidad", cantidad);
                    cmd.Parameters.AddWithValue("@sector", sector);
                    cmd.Parameters.AddWithValue("@usuario", usuario);
                    cmd.Parameters.AddWithValue("@tipo", tipo);
                    cmd.Parameters.AddWithValue("@lote", lote);
                    cmd.Parameters.AddWithValue("@fecven", fecven);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();


                }
                return StatusCode(StatusCodes.Status200OK, new { message = "Se Grabaron Exitosamente los datos" });
            }
            catch (Exception erx)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = erx.Message });
            }
        }
        //***************AGREGAR  Consumo**************
        [HttpPost]
        [AllowAnonymous]
        [Route("AgregarPedido")]
        public IActionResult AgregarPedido(int pCantidad, string pContribuyente, string pCalle, string pBarrio, string pManzana, string pLote, string pZona, int pMtslineales, int pForm, int pUc, string pMonto, string pNumrecibo, string pFechapago, string pFechavencimiento)
        {
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("GrabarPagos", conexion);
                    cmd.Parameters.AddWithValue("@pCantidad", pCantidad);
                    cmd.Parameters.AddWithValue("@pContribuyente", pContribuyente);
                    cmd.Parameters.AddWithValue("@pCalle", pCalle);
                    cmd.Parameters.AddWithValue("@pBarrio", pBarrio);
                    cmd.Parameters.AddWithValue("@pManzana", pManzana);
                    cmd.Parameters.AddWithValue("@pLote", pLote);
                    cmd.Parameters.AddWithValue("@pZona", pZona);
                    cmd.Parameters.AddWithValue("@pMtslineales", pMtslineales);
                    cmd.Parameters.AddWithValue("@pForm", pForm);
                    cmd.Parameters.AddWithValue("@pUc", pUc);
                    cmd.Parameters.AddWithValue("@pMonto", pMonto);
                    cmd.Parameters.AddWithValue("@pNumrecibo", pNumrecibo);
                    cmd.Parameters.AddWithValue("@pFechapago", pFechapago);
                    cmd.Parameters.AddWithValue("@pFechavencimiento", pFechavencimiento);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();


                }
                return StatusCode(StatusCodes.Status200OK, new { message = "Se Grabaron Exitosamente los datos" });
            }
            catch (Exception erx)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = erx.Message });
            }
        }
        //***************AGREGAR  Consumo**************
        [HttpPut]
        [AllowAnonymous]
        [Route("ModificarPedido")]
        public IActionResult ModificarPedido(int pId, int pCantidad, string pContribuyente, string pCalle, string pBarrio, string pManzana, string pLote, string pZona, int pMtslineales, int pForm, int pUc, string pMonto, string pNumrecibo, string pFechapago, string pFechavencimiento)
        {
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("ModificarPagos", conexion);
                    cmd.Parameters.AddWithValue("@pId", pId);
                    cmd.Parameters.AddWithValue("@pCantidad", pCantidad);
                    cmd.Parameters.AddWithValue("@pContribuyente", pContribuyente);
                    cmd.Parameters.AddWithValue("@pCalle", pCalle);
                    cmd.Parameters.AddWithValue("@pBarrio", pBarrio);
                    cmd.Parameters.AddWithValue("@pManzana", pManzana);
                    cmd.Parameters.AddWithValue("@pLote", pLote);
                    cmd.Parameters.AddWithValue("@pZona", pZona);
                    cmd.Parameters.AddWithValue("@pMtslineales", pMtslineales);
                    cmd.Parameters.AddWithValue("@pForm", pForm);
                    cmd.Parameters.AddWithValue("@pUc", pUc);
                    cmd.Parameters.AddWithValue("@pMonto", pMonto);
                    cmd.Parameters.AddWithValue("@pNumrecibo", pNumrecibo);
                    cmd.Parameters.AddWithValue("@pFechapago", pFechapago);
                    cmd.Parameters.AddWithValue("@pFechavencimiento", pFechavencimiento);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();


                }
                return StatusCode(StatusCodes.Status200OK, new { message = "Se Modificaron Exitosamente los datos" });
            }
            catch (Exception erx)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = erx.Message });
            }
        }
        //*****************************************************************************************************
        //***************AGREGAR  Movimiento**************
        [HttpPost]
        [Route("AgregarMov/{NInternado:int}/{articuloid:int}/{comprobante}/{receta}/{dosis}/{fecha}/{detalleInternado}/{cantidad:int}/{sector:int}/{usuario:int}/{tipo:int}/{lote}/{fecven}")]
        public IActionResult AgregarMov(int NInternado, int articuloid, string comprobante, string receta, string dosis, string fecha, string detalleInternado, int cantidad, int sector, int usuario, int tipo, string lote, string fecven)
        {
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("GrabarMov", conexion);
                    cmd.Parameters.AddWithValue("@NInternado", NInternado);
                    cmd.Parameters.AddWithValue("@articuloid", articuloid);
                    cmd.Parameters.AddWithValue("@comprobante", comprobante);
                    cmd.Parameters.AddWithValue("@receta", receta);
                    cmd.Parameters.AddWithValue("@fecha", fecha);
                    cmd.Parameters.AddWithValue("@dosis", dosis);
                    cmd.Parameters.AddWithValue("@detalleInternado", detalleInternado);
                    cmd.Parameters.AddWithValue("@cantidad", cantidad);
                    cmd.Parameters.AddWithValue("@sector", sector);
                    cmd.Parameters.AddWithValue("@usuario", usuario);
                    cmd.Parameters.AddWithValue("@tipo", tipo);
                    cmd.Parameters.AddWithValue("@lote", lote);
                    cmd.Parameters.AddWithValue("@fecven", fecven);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();


                }
                return StatusCode(StatusCodes.Status200OK, new { message = "Se Grabaron Exitosamente los datos" });
            }
            catch (Exception erx)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = erx.Message });
            }
        }
        //*****************************************************************************************************
        //***************AGREGAR  Consumo**************
        [HttpPost]
        [Route("AgregarEconomato/{NInternado:int}/{articuloid:int}/{comprobante}/{receta}/{dosis}/{fecha}/{detalleInternado}/{cantidad:int}/{sector:int}/{usuario:int}/{tipo:int}/{pProveedor:int}/{FecVen}/{Lote}/{precio}/{preciocosto}")]
        public IActionResult AgregarEconomato(int NInternado, int articuloid, string comprobante, string receta, string dosis, string fecha, string detalleInternado, int cantidad, int sector, int usuario, int tipo, int pProveedor, string FecVen, string Lote, string precio, string preciocosto)
        {
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("GrabarEconomato", conexion);
                    cmd.Parameters.AddWithValue("@NInternado", NInternado);
                    cmd.Parameters.AddWithValue("@articuloid", articuloid);
                    cmd.Parameters.AddWithValue("@comprobante", comprobante);
                    cmd.Parameters.AddWithValue("@receta", receta);
                    cmd.Parameters.AddWithValue("@fecha", fecha);
                    cmd.Parameters.AddWithValue("@dosis", dosis);
                    cmd.Parameters.AddWithValue("@detalleInternado", detalleInternado);
                    cmd.Parameters.AddWithValue("@cantidad", cantidad);
                    cmd.Parameters.AddWithValue("@sector", sector);
                    cmd.Parameters.AddWithValue("@usuario", usuario);
                    cmd.Parameters.AddWithValue("@tipo", tipo);
                    cmd.Parameters.AddWithValue("@pProveedor", pProveedor);
                    cmd.Parameters.AddWithValue("@FecVen", FecVen);
                    cmd.Parameters.AddWithValue("@Lote", Lote);
                    cmd.Parameters.AddWithValue("@precio", precio);
                    cmd.Parameters.AddWithValue("@preciocosto", preciocosto);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();


                }
                return StatusCode(StatusCodes.Status200OK, new { message = "Se Grabaron Exitosamente los datos" });
            }
            catch (Exception erx)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = erx.Message });
            }
        }
        //*****************************************************************************************************
        //***************AGREGAR  CAMPOS Integer**************
        [HttpPost]
        //[Route("AgregarArticulos/{pcodigo}/{pnombre}/{pprecio:decimal}/{pnroregistro}/{idtipo :int}/{idsector :int}/{stockminimo :int}/{stockmedio :int}/{stockmaximo :int}/{pusuario :int}/{pinstitucionid :int}")]
        [Route("AgregarArticulos")]
        public IActionResult AgregarArticulos(string pcodigo, string pnombre, decimal pprecio, string pnroregistro, int idtipo, int idsector, int stockminimo, int stockmedio, int stockmaximo, int pusuario, int pinstitucionid, int pDosis, int pForma, int pTamano, int pCategoria, int pTraza, int pStockCero)
        {
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("AgregarArticulos", conexion);
                    cmd.Parameters.AddWithValue("@pcodigo", pcodigo);
                    cmd.Parameters.AddWithValue("@pnombre", pnombre);
                    cmd.Parameters.AddWithValue("@pprecio", pprecio);
                    cmd.Parameters.AddWithValue("@pnroregistro", pnroregistro);
                    cmd.Parameters.AddWithValue("@pusuario", pusuario);
                    cmd.Parameters.AddWithValue("@pinstitucionid", pinstitucionid);
                    cmd.Parameters.AddWithValue("@idtipo", idtipo);
                    cmd.Parameters.AddWithValue("@idsector", idsector);
                    cmd.Parameters.AddWithValue("@stockminimo", stockminimo);
                    cmd.Parameters.AddWithValue("@stockmedio", stockmedio);
                    cmd.Parameters.AddWithValue("@stockmaximo", stockmaximo);
                    cmd.Parameters.AddWithValue("@pDosis", pDosis);
                    cmd.Parameters.AddWithValue("@pForma", pForma);
                    cmd.Parameters.AddWithValue("@pTamano", pTamano);
                    cmd.Parameters.AddWithValue("@pCategoria", pCategoria);
                    cmd.Parameters.AddWithValue("@pTraza", pTraza);
                    cmd.Parameters.AddWithValue("@pStockCero", pStockCero);

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();


                }
                return StatusCode(StatusCodes.Status200OK, new { message = "Se Graboron Exitosamente lod datos" });
            }
            catch (Exception erx)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = erx.Message });
            }
        }
        //*****************************************************************************************************
        //***************AGREGAR  CAMPOS Integer**************
        [HttpPut]
        [AllowAnonymous]
        //[Route("ModificaArticulos/{pcodigo}/{pnombre}/{pprecio:decimal}/{pnroregistro}/{idtipo :int}/{idsector :int}/{stockminimo :int}/{stockmedio :int}/{stockmaximo :int}/{pusuario :int}/{pinstitucionid :int}")]
        [Route("ModificaArticulos")]
        public IActionResult ModificaArticulos(int pId, string pcodigo, string pnombre, decimal pprecio, string pnroregistro, int pTroquel, string pCodbarra, int idtipo, int idsector, int stockminimo, int stockmedio, int stockmaximo, int pDosis, int pForma, int pTamano, int pCategoria, int pTraza, int pStockCero)
        {
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("ModificarArticulos", conexion);
                    cmd.Parameters.AddWithValue("@pId", pId);
                    cmd.Parameters.AddWithValue("@pcodigo", pcodigo);
                    cmd.Parameters.AddWithValue("@pnombre", pnombre);
                    cmd.Parameters.AddWithValue("@pprecio", pprecio);
                    cmd.Parameters.AddWithValue("@pnroregistro", pnroregistro);
                    cmd.Parameters.AddWithValue("@pTroquel", pTroquel);
                    cmd.Parameters.AddWithValue("@pCodbarra", pCodbarra);
                    cmd.Parameters.AddWithValue("@idtipo", idtipo);
                    cmd.Parameters.AddWithValue("@idsector", idsector);
                    cmd.Parameters.AddWithValue("@stockminimo", stockminimo);
                    cmd.Parameters.AddWithValue("@stockmedio", stockmedio);
                    cmd.Parameters.AddWithValue("@stockmaximo", stockmaximo);
                    cmd.Parameters.AddWithValue("@pDosis", pDosis);
                    cmd.Parameters.AddWithValue("@pForma", pForma);
                    cmd.Parameters.AddWithValue("@pTamano", pTamano);
                    cmd.Parameters.AddWithValue("@pCategoria", pCategoria);
                    cmd.Parameters.AddWithValue("@pTraza", pTraza);
                    cmd.Parameters.AddWithValue("@pStockCero", pStockCero);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();


                }
                return StatusCode(StatusCodes.Status200OK, new { message = "Se Graboron Exitosamente lod datos" });
            }
            catch (Exception erx)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = erx.Message });
            }
        }
        //***************AGREGAR  CAMPOS Date**************
        [HttpPost]
        [Route("AgregarCamposDate/{pTipo:int}/{pdate_id:int}/{penable_format}/{puse_date_format}/{pdate_format}/{pvalue_list}/{puse_range}/{pupper_bound}/{pupper_date}/{plower_bound}/{plower_date}/{passume_value}")]
        public IActionResult AgregarCamposDate(int pTipo, int pdate_id, string penable_format, string puse_date_format, string pdate_format, string pvalue_list, string puse_range, string pupper_bound, string pupper_date, string plower_bound, string plower_date, string passume_value)
        {
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("AgregarCamposDate", conexion);
                    cmd.Parameters.AddWithValue("@pTipo", pTipo);
                    cmd.Parameters.AddWithValue("@pdate_id", pdate_id);
                    cmd.Parameters.AddWithValue("@penable_format", penable_format);
                    cmd.Parameters.AddWithValue("@puse_date_format", puse_date_format);
                    cmd.Parameters.AddWithValue("@pdate_format", pdate_format);
                    cmd.Parameters.AddWithValue("@pvalue_list", pvalue_list);
                    cmd.Parameters.AddWithValue("@puse_range", puse_range);
                    cmd.Parameters.AddWithValue("@pupper_bound", pupper_bound);
                    cmd.Parameters.AddWithValue("@pupper_date", pupper_date);
                    cmd.Parameters.AddWithValue("@plower_bound", plower_bound);
                    cmd.Parameters.AddWithValue("@plower_date", plower_date);
                    cmd.Parameters.AddWithValue("@passume_value", passume_value);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();


                }
                return StatusCode(StatusCodes.Status200OK, new { message = "Se Grabaron Exitosamente lod datos" });
            }
            catch (Exception erx)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = erx.Message });
            }
        }
        //***************Modificar  CAMPOS Date**************
        [HttpPut]
        [Route("ModificarCamposDate/{pTipo:int}/{pdate_id:int}/{penable_format}/{puse_date_format}/{pdate_format}/{pvalue_list}/{puse_range}/{pupper_bound}/{pupper_date}/{plower_bound}/{plower_date}/{passume_value}")]
        public IActionResult ModificarCamposDate(int pTipo, int pdate_id, string penable_format, string puse_date_format, string pdate_format, string pvalue_list, string puse_range, string pupper_bound, string pupper_date, string plower_bound, string plower_date, string passume_value)
        {
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("ModificarCamposDate_SP", conexion);
                    cmd.Parameters.AddWithValue("@pTipo", pTipo);
                    cmd.Parameters.AddWithValue("@pdate_id", pdate_id);
                    cmd.Parameters.AddWithValue("@penable_format", penable_format);
                    cmd.Parameters.AddWithValue("@puse_date_format", puse_date_format);
                    cmd.Parameters.AddWithValue("@pdate_format", pdate_format);
                    cmd.Parameters.AddWithValue("@pvalue_list", pvalue_list);
                    cmd.Parameters.AddWithValue("@puse_range", puse_range);
                    cmd.Parameters.AddWithValue("@pupper_bound", pupper_bound);
                    cmd.Parameters.AddWithValue("@pupper_date", pupper_date);
                    cmd.Parameters.AddWithValue("@plower_bound", plower_bound);
                    cmd.Parameters.AddWithValue("@plower_date", plower_date);
                    cmd.Parameters.AddWithValue("@passume_value", passume_value);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();


                }
                return StatusCode(StatusCodes.Status200OK, new { message = "Se Grabaron Exitosamente los datos" });
            }
            catch (Exception erx)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = erx.Message });
            }
        }
        //***************Modificar  CAMPOS DateTime**************
        [HttpPut]
        [Route("ModificarCamposDateTime/{pTipo:int}/{pdatetime_id:int}/{penable_format}/{puse_date_format}/{pdate_format}/{puse_time_format}/{ptime_format}/{pvalue_list}/{puse_range}/{pupper_bound}/{pupper_date}/{pupper_time}/{plower_bound}/{plower_date}/{plower_time}/{passume_value}")]
        public IActionResult ModificarCamposDateTime(int pTipo, int pdatetime_id, string penable_format, string puse_date_format, string pdate_format, string puse_time_format, string ptime_format, string pvalue_list, string puse_range, string pupper_bound, string pupper_date, string pupper_time, string plower_bound, string plower_date, string plower_time, string passume_value)
        {
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("ModificarCamposDateTime", conexion);
                    cmd.Parameters.AddWithValue("@pTipo", pTipo);
                    cmd.Parameters.AddWithValue("@pdatetime_id", pdatetime_id);
                    cmd.Parameters.AddWithValue("@penable_format", penable_format);
                    cmd.Parameters.AddWithValue("@puse_date_format", puse_date_format);
                    cmd.Parameters.AddWithValue("@pdate_format", pdate_format);
                    cmd.Parameters.AddWithValue("@puse_time_format", puse_time_format);
                    cmd.Parameters.AddWithValue("@ptime_format", ptime_format);
                    cmd.Parameters.AddWithValue("@pvalue_list", pvalue_list);
                    cmd.Parameters.AddWithValue("@puse_range", puse_range);
                    cmd.Parameters.AddWithValue("@pupper_bound", pupper_bound);
                    cmd.Parameters.AddWithValue("@pupper_date", pupper_date);
                    cmd.Parameters.AddWithValue("@pupper_time", pupper_time);
                    cmd.Parameters.AddWithValue("@plower_bound", plower_bound);
                    cmd.Parameters.AddWithValue("@plower_date", plower_date);
                    cmd.Parameters.AddWithValue("@plower_time", plower_time);
                    cmd.Parameters.AddWithValue("@passume_value", passume_value);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();


                }
                return StatusCode(StatusCodes.Status200OK, new { message = "Se Grabaron Exitosamente los datos" });
            }
            catch (Exception erx)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = erx.Message });
            }
        }
        //***************Modificar  CAMPOS DateTime**************
        [HttpPost]
        [Route("AgregarCamposDateTime/{pTipo:int}/{pdatetime_id:int}/{penable_format}/{puse_date_format}/{pdate_format}/{puse_time_format}/{ptime_format}/{pvalue_list}/{puse_range}/{pupper_bound}/{pupper_date}/{pupper_time}/{plower_bound}/{plower_date}/{plower_time}/{passume_value}")]
        public IActionResult AgregarCamposDateTime(int pTipo, int pdatetime_id, string penable_format, string puse_date_format, string pdate_format, string puse_time_format, string ptime_format, string pvalue_list, string puse_range, string pupper_bound, string pupper_date, string pupper_time, string plower_bound, string plower_date, string plower_time, string passume_value)
        {
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("AgregarCamposDateTime", conexion);
                    cmd.Parameters.AddWithValue("@pTipo", pTipo);
                    cmd.Parameters.AddWithValue("@pdatetime_id", pdatetime_id);
                    cmd.Parameters.AddWithValue("@penable_format", penable_format);
                    cmd.Parameters.AddWithValue("@puse_date_format", puse_date_format);
                    cmd.Parameters.AddWithValue("@pdate_format", pdate_format);
                    cmd.Parameters.AddWithValue("@puse_time_format", puse_time_format);
                    cmd.Parameters.AddWithValue("@ptime_format", ptime_format);
                    cmd.Parameters.AddWithValue("@pvalue_list", pvalue_list);
                    cmd.Parameters.AddWithValue("@puse_range", puse_range);
                    cmd.Parameters.AddWithValue("@pupper_bound", pupper_bound);
                    cmd.Parameters.AddWithValue("@pupper_date", pupper_date);
                    cmd.Parameters.AddWithValue("@pupper_time", pupper_time);
                    cmd.Parameters.AddWithValue("@plower_bound", plower_bound);
                    cmd.Parameters.AddWithValue("@plower_date", plower_date);
                    cmd.Parameters.AddWithValue("@plower_time", plower_time);
                    cmd.Parameters.AddWithValue("@passume_value", passume_value);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();


                }
                return StatusCode(StatusCodes.Status200OK, new { message = "Se Grabaron Exitosamente los datos" });
            }
            catch (Exception erx)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = erx.Message });
            }
        }
        //*****************************************************************************************************
        //***************MODIFICA  CAMPOS**************
        [HttpPut]
        [Route("ModificaCampos/{pTipo:int}/{pId:int}/{pdefault_value}/{pvalue_list}/{pmask_library}/{passumed_value}/{plength:int}")]
        public IActionResult ModificaCampos(int pTipo, int pId, string pdefault_value, string pvalue_list, string pmask_library, string passumed_value, int plength)
        {
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("ModificaCamposString", conexion);
                    cmd.Parameters.AddWithValue("@pTipo", pTipo);
                    cmd.Parameters.AddWithValue("@pId", pId);
                    cmd.Parameters.AddWithValue("@pdefault_value", pdefault_value);
                    cmd.Parameters.AddWithValue("@pvalue_list", pvalue_list);
                    cmd.Parameters.AddWithValue("@pmask_library", pmask_library);
                    cmd.Parameters.AddWithValue("@passumed_value", passumed_value);
                    cmd.Parameters.AddWithValue("@plength", plength);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();


                }
                return StatusCode(StatusCodes.Status200OK, new { message = "Modificacion Exitosa" });
            }
            catch (Exception erx)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = erx.Message });
            }
        }
        //*****************************************************************************************************
        //********************************************************
        //***************Agregar  CAMPOS**************
        [HttpPost]
        [Route("AgregarCampos/{pTipo:int}/{pId:int}/{pdefault_value}/{pvalue_list}/{pmask_library}/{passumed_value}/{plength:int}")]
        public IActionResult AgregarCampos(int pTipo, int pId, string pdefault_value, string pvalue_list, string pmask_library, string passumed_value, int plength)
        {
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("AgregarCamposString", conexion);
                    cmd.Parameters.AddWithValue("@pTipo", pTipo);
                    cmd.Parameters.AddWithValue("@pId", pId);
                    cmd.Parameters.AddWithValue("@pdefault_value", pdefault_value);
                    cmd.Parameters.AddWithValue("@pvalue_list", pvalue_list);
                    cmd.Parameters.AddWithValue("@pmask_library", pmask_library);
                    cmd.Parameters.AddWithValue("@passumed_value", passumed_value);
                    cmd.Parameters.AddWithValue("@plength", plength);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();


                }
                return StatusCode(StatusCodes.Status200OK, new { message = "Guardado Exitosa" });
            }
            catch (Exception erx)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = erx.Message });
            }
        }


        //***************LISTAR FORMULARIOS CRUD**************
        [HttpGet]
        [Route("ListaFormulariosCRUD")]
        public IActionResult ListaFormCRUD()
        {
            List<ConfigFormCRUD> lista = new List<ConfigFormCRUD>();
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("ObtenerFormulariosCRUD", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new ConfigFormCRUD()
                            {
                                IdConfigForm = Convert.ToInt32(rd["Id_ConfigForm"]),
                                Titulo = rd["titulo"].ToString(),
                                Descripcion = rd["descripcion"].ToString()
                            });
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { lista });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }
        //*****************************************************************************************************

        //***************MARCAR FORMULARIO COMO ELIMINADO**************
        [HttpPut]
        [Route("EliminarModulo/{IdConfigForm:int}")]
        public IActionResult EliminarModulo(int IdConfigForm)//Actualizar el campo fecha_eliminacion
        {
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("MarcarFormularioComoEliminado", conexion);
                    cmd.Parameters.AddWithValue("@Id_Formulario", IdConfigForm);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();


                }
                return StatusCode(StatusCodes.Status200OK, new { message = "eliminado" });
            }
            catch (Exception erx)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = erx.Message });
            }
        }
        //*****************************************************************************************************

        //***************MOSTRAR FORMULARIO COMPLT**************
        [HttpGet]
        [Route("MostrarFormularioCompleto/{IdConfigForm:int}")]
        public IActionResult MostrarFormularios(int IdConfigForm)//Mostrar formulario completo segun su id
        {
            ConfigFormCRUD formulariodata = new ConfigFormCRUD();
            List<Field> campodata = new List<Field>();

            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("ObtenerFormularioYCampos", conexion);
                    cmd.Parameters.AddWithValue("@Id_Formulario", IdConfigForm);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // cmd.ExecuteNonQuery();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            formulariodata.IdConfigForm = reader.GetInt32(reader.GetOrdinal("Id_ConfigForm"));
                            formulariodata.Titulo = reader.GetString(reader.GetOrdinal("TituloFormulario"));
                            formulariodata.Descripcion = reader.GetString(reader.GetOrdinal("DescripcionFormulario"));
                            //formulariodata.FechaCreacionFormulario = reader.GetDateTime(reader.GetOrdinal("FechaCreacionFormulario"));
                            //formulariodata.FechaModificacionFormulario = reader.GetDateTime(reader.GetOrdinal("FechaModificacionFormulario"));

                        }
                        reader.NextResult();//ir al siguiente resultado (field)
                        while (reader.Read())
                        {
                            Field campo = new Field();
                            campo.Id_Field = reader.GetInt32(reader.GetOrdinal("Id_Field"));
                            campo.nombre = reader.GetString(reader.GetOrdinal("NombreCampo"));
                            campo.orden = reader.GetInt32(reader.GetOrdinal("OrdenCampo"));
                            campo.etiqueta = reader.GetString(reader.GetOrdinal("EtiquetaCampo"));
                            campo.tipo = reader.GetString(reader.GetOrdinal("TipoCampo"));
                            campo.requerido = reader.GetInt32(reader.GetOrdinal("RequeridoCampo"));
                            campo.marcador = reader.GetString(reader.GetOrdinal("MarcadorCampo"));
                            campo.opciones = reader.GetString(reader.GetOrdinal("OpcionesCampo"));
                            campo.visible = reader.GetInt32(reader.GetOrdinal("VisibleCampo"));
                            campo.clase = reader.GetString(reader.GetOrdinal("ClaseCampo"));
                            campo.estado = reader.GetInt32(reader.GetOrdinal("EstadoCampo"));
                            campo.posi = reader.GetString(reader.GetOrdinal("posi"));
                            campo.urlapi = reader.GetString(reader.GetOrdinal("urlapi"));

                            campodata.Add(campo);
                        }

                    }


                }
                //formulariodata.Campos = campodata;
                //return formulariodata;
                return StatusCode(StatusCodes.Status200OK, new { datosForm = formulariodata, datosField = campodata });
            }
            catch (Exception erx)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = erx.Message });
            }
        }
        //*****************************************************************************************************

        //***************GUARDAR FORMULARIO COMPLT**************

        [HttpPost]
        [Route("GuardarFormularioCreado")]
        public IActionResult GuardarFormularioCampos(ConfigForm Field)
        {
            using (var conexion = new SqlConnection(cadenaSQL))
            {
                conexion.Open();
                using (var transaction = conexion.BeginTransaction())
                {
                    try
                    {
                        var insertConfigFormSql = "INSERT INTO ConfigForm (Titulo, Descripcion, Fecha_Creacion) VALUES (@Titulo, @Descripcion, @Fecha_Creacion); SELECT SCOPE_IDENTITY();";
                        int configFormId = conexion.QuerySingle<int>(insertConfigFormSql, new
                        {
                            Titulo = Field.Titulo,
                            Descripcion = Field.Descripcion,
                            Fecha_Creacion = DateTime.Now
                        }, transaction);

                        foreach (var fieldInput in Field.Campos)
                        {
                            var insertFieldSql = "INSERT INTO Field (nombre, orden, etiqueta, tipo, requerido, marcador, opciones, visible, clase, estado, Id_ConfigForm, fecha_eliminacion) VALUES (@nombre, @orden, @etiqueta, @tipo, @requerido, @marcador, @opciones, @visible, @clase, @estado, @Id_ConfigForm, @fecha_eliminacion);";
                            conexion.Execute(insertFieldSql, new
                            {
                                nombre = fieldInput.nombre,
                                orden = fieldInput.orden,
                                etiqueta = fieldInput.etiqueta,
                                tipo = fieldInput.tipo,
                                requerido = fieldInput.requerido,
                                marcador = fieldInput.marcador,
                                opciones = fieldInput.opciones,
                                visible = fieldInput.visible,
                                clase = fieldInput.clase,
                                estado = fieldInput.estado,
                                Id_ConfigForm = configFormId,
                                fecha_eliminacion = fieldInput.fecha_eliminacion
                            }, transaction);
                        }

                        transaction.Commit();

                        // Devuelve el ID generado como respuesta HTTP 200 (éxito)
                        return Ok(new { Id = configFormId });
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
        //***************AgregarContenido**************

        [HttpPost]
        [Route("AgregarContenido/{pIdConfigForm:int}/{pidentificador_fila:int}")]
        public IActionResult AgregarContenido(int pIdConfigForm, int pidentificador_fila)
        {
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("AgregarCContenido", conexion);
                    cmd.Parameters.AddWithValue("@pIdConfigForm", pIdConfigForm);
                    cmd.Parameters.AddWithValue("@pidentificador_fila", pidentificador_fila);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();


                }
                return StatusCode(StatusCodes.Status200OK, new { message = "Guardado Exitosa" });
            }
            catch (Exception erx)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = erx.Message });
            }
        }
        //***************GUARDAR FORMULARIO COMPLT**************

        [HttpPost]
        [Route("AgregarFormularioCreado")]
        public IActionResult AgregarFormularioCreado(ConfigForm Field)
        {
            using (var conexion = new SqlConnection(cadenaSQL))
            {
                conexion.Open();
                using (var transaction = conexion.BeginTransaction())
                {
                    try
                    {

                        int configFormId = Field.IdConfigForm;


                        foreach (var fieldInput in Field.Campos)
                        {
                            var insertFieldSql = "INSERT INTO Field (nombre, orden, etiqueta, tipo, requerido, marcador, opciones, visible, clase, estado, Id_ConfigForm, fecha_eliminacion) VALUES (@nombre, @orden, @etiqueta, @tipo, @requerido, @marcador, @opciones, @visible, @clase, @estado, @Id_ConfigForm, @fecha_eliminacion);";
                            conexion.Execute(insertFieldSql, new
                            {
                                // configFormId = fieldInput.Id_ConfigForm,
                                nombre = fieldInput.nombre,
                                orden = fieldInput.orden,
                                etiqueta = fieldInput.etiqueta,
                                tipo = fieldInput.tipo,
                                requerido = fieldInput.requerido,
                                marcador = fieldInput.marcador,
                                opciones = fieldInput.opciones,
                                visible = fieldInput.visible,
                                clase = fieldInput.clase,
                                estado = fieldInput.estado,
                                Id_ConfigForm = fieldInput.Id_ConfigForm,
                                fecha_eliminacion = fieldInput.fecha_eliminacion
                            }, transaction); ;
                        }

                        transaction.Commit();

                        // Devuelve el ID generado como respuesta HTTP 200 (éxito)
                        return Ok(new { Id = 1 });
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
        //***************LISTAR RESPUESTAS**************
        [HttpGet]
        [Route("ListaRespuestas/{IdConfigForm:int}")]
        public IActionResult ListaRespuesta(int IdConfigForm)
        {
            List<RespuestasLista> lista = new List<RespuestasLista>();
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("ListarRespuestas", conexion);
                    cmd.Parameters.AddWithValue("@FormularioID", IdConfigForm);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new RespuestasLista()
                            {
                                Id_ConfigForm = Convert.ToInt32(rd["Id_ConfigForm"]),
                                Id_Field = Convert.ToInt32(rd["Id_Field"]),
                                Id_Answer = Convert.ToInt32(rd["Id_Answer"]),
                                nombre = rd["nombre"].ToString(),
                                //titulo = rd["titulo"].ToString(),
                                valor = rd["valor"].ToString(),
                                identificador_fila = Convert.ToInt32(rd["identificador_fila"])

                            });
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { lista });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }
        //*****************************************************************************************************

        //***************GUARDAR RESPUESTA***************************************


        [HttpPost]
        [Route("Respuestas")]

        public async Task<IActionResult> GuardarRespuesta([FromBody] AnswerModel Answer)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(cadenaSQL))
                {
                    await connection.OpenAsync();

                    string query = "INSERT INTO Answer (Id_ConfigForm, Id_Field, valor, fecha_creacion, fecha_modificacion) VALUES (@Id_ConfigForm, @Id_Field, @valor, @fecha_creacion, @fecha_modificacion)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id_ConfigForm", Answer.Id_ConfigForm);
                        command.Parameters.AddWithValue("@Id_Field", Answer.Id_Field);
                        command.Parameters.AddWithValue("@valor", Answer.valor);
                        command.Parameters.AddWithValue("@fecha_creacion", DateTime.Now);
                        command.Parameters.AddWithValue("@fecha_modificacion", DateTime.Now);

                        await command.ExecuteNonQueryAsync();
                    }
                }

                return Ok("Respuesta del formulario guardada exitosamente.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al guardar la respuesta: {ex.Message}");
            }
        }
        //***************EDITAR RESPUESTA***************************************
        [HttpPost]
        [Route("Respuestas/Editar")]
        public IActionResult ActualizarDatos([FromBody] List<AnswerModel> actualizaciones)
        {
            try
            {
                // Obtén la cadena de conexión a la base de datos desde la configuración
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();

                    // Crea una tabla de valores para los datos de actualización
                    DataTable actualizacionesTable = new DataTable();
                    actualizacionesTable.Columns.Add("Id_Answer", typeof(int));
                    actualizacionesTable.Columns.Add("valor", typeof(string));

                    foreach (var actualizacion in actualizaciones)
                    {
                        actualizacionesTable.Rows.Add(actualizacion.Id_Answer, actualizacion.valor);
                    }

                    using (SqlCommand cmd = new SqlCommand("EditarRegistrosAnswer", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Asigna el parámetro del procedimiento almacenado
                        SqlParameter parameter = cmd.Parameters.AddWithValue("@Actualizaciones", actualizacionesTable);
                        parameter.SqlDbType = SqlDbType.Structured;

                        cmd.ExecuteNonQuery();
                    }
                }

                return Ok("Registros actualizados exitosamente");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
        //*************************************************************
        //***************GUARDAR RESPUESTA***************************************
        [HttpPost]
        [Route("Respuestas/Guardar")]
        public ActionResult<int> GuardarRespuesta([FromBody] List<NuevaRespuestasGuardar> registros)
        {
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();

                    // Obtener el próximo identificador_fila una vez
                    int proximoIdentificador = ObtenerProximoIdentificador(conexion);

                    foreach (var registro in registros)
                    {
                        // Construye la consulta SQL para insertar un registro en la tabla Answer
                        string query = "INSERT INTO Answer (Id_ConfigForm, Id_Field, valor, fecha_creacion, identificador_fila) " +
                                       "VALUES (@Id_ConfigForm, @Id_Field, @valor, GETDATE(), @IdentificadorFila);";

                        using (SqlCommand cmd = new SqlCommand(query, conexion))
                        {
                            cmd.Parameters.AddWithValue("@Id_ConfigForm", registro.Id_ConfigForm);
                            cmd.Parameters.AddWithValue("@Id_Field", registro.Id_Field);
                            cmd.Parameters.AddWithValue("@valor", registro.valor);

                            // Utiliza el mismo valor de identificador_fila para todos los registros
                            cmd.Parameters.AddWithValue("@IdentificadorFila", proximoIdentificador);

                            cmd.ExecuteNonQuery();
                        }
                    }

                    return Ok(registros.Count);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private int ObtenerProximoIdentificador(SqlConnection conexion)
        {
            // Obtener el próximo identificador_fila
            string query = "SELECT ISNULL(MAX(identificador_fila), 0) + 1 FROM Answer;";

            using (SqlCommand cmd = new SqlCommand(query, conexion))
            {
                object resultado = cmd.ExecuteScalar();
                return resultado is DBNull ? 1 : (int)resultado;
            }
        }
        //----------------------------
        //-------Listar Datos ----
        //----------------------------
        [HttpGet]
        [Route("ListarDatos/{IdConfigForm:int}/{dato}")]

        public IActionResult ListarDatos(int IdConfigForm, string dato)
        {
            List<RespuestasLista> lista = new List<RespuestasLista>();
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("ListarDatos", conexion);
                    cmd.Parameters.AddWithValue("@FormularioID", IdConfigForm);
                    cmd.Parameters.AddWithValue("@dato", dato);

                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new RespuestasLista()
                            {
                                Id_ConfigForm = Convert.ToInt32(rd["Id_ConfigForm"]),
                                Id_Field = Convert.ToInt32(rd["Id_Field"]),
                                Id_Answer = Convert.ToInt32(rd["Id_Answer"]),
                                nombre = rd["nombre"].ToString(),
                                valor = rd["valor"].ToString(),
                                identificador_fila = Convert.ToInt32(rd["identificador_fila"])
                            });
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { lista });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }

        //----------------------------
        //----------------------------
        //----------------------------

        //--------------------------
        //-------BUSCAR

        [HttpGet]
        [Route("Buscar/{IdConfigForm:int}/{dato}")]

        public IActionResult Buscar(int IdConfigForm, string dato)
        {
            List<RespuestasLista> lista = new List<RespuestasLista>();
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("Buscar", conexion);
                    cmd.Parameters.AddWithValue("@FormularioID", IdConfigForm);
                    cmd.Parameters.AddWithValue("@dato", dato);

                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new RespuestasLista()
                            {
                                Id_ConfigForm = Convert.ToInt32(rd["Id_ConfigForm"]),
                                Id_Field = Convert.ToInt32(rd["Id_Field"]),
                                Id_Answer = Convert.ToInt32(rd["Id_Answer"]),
                                nombre = rd["nombre"].ToString(),
                                valor = rd["valor"].ToString(),
                                identificador_fila = Convert.ToInt32(rd["identificador_fila"])
                            });
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { lista });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }
        //------------------------------------------	
        //******************************************************************
        //****LISTAR RESPUESTAS X IDENTIFICADOR DE FILA
        [HttpGet]
        [Route("ListaRespuestasIdentificadorFila/{IdConfigForm:int}/{identificador_fila:int}")]

        public IActionResult ListaRespuestaIdentificadorFila(int IdConfigForm, int identificador_fila)
        {
            List<RespuestasLista> lista = new List<RespuestasLista>();
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("ListarRespuestasPorIdentificadorFila", conexion);
                    cmd.Parameters.AddWithValue("@FormularioID", IdConfigForm);
                    cmd.Parameters.AddWithValue("@IdentificadorFila", identificador_fila);

                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new RespuestasLista()
                            {
                                Id_ConfigForm = Convert.ToInt32(rd["Id_ConfigForm"]),
                                Id_Field = Convert.ToInt32(rd["Id_Field"]),
                                Id_Answer = Convert.ToInt32(rd["Id_Answer"]),
                                nombre = rd["nombre"].ToString(),
                                valor = rd["valor"].ToString(),
                                identificador_fila = Convert.ToInt32(rd["identificador_fila"])
                            });
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { lista });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }
        //***************ELIMINAR RESPUESTA***************************************
        [HttpPut]
        [Route("Respuestas/Eliminar/{id_fila}")]
        public async Task<IActionResult> EliminarRespuesta(int id_fila, AnswerErase Answer)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(cadenaSQL))
                {
                    await connection.OpenAsync();

                    string query = "UPDATE Answer SET fecha_eliminacion = @fecha_eliminacion WHERE identificador_fila = @id_fila";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id_fila", id_fila);
                        command.Parameters.AddWithValue("@fecha_eliminacion", DateTime.Now);

                        await command.ExecuteNonQueryAsync();
                    }
                }

                return Ok("Respuesta del formulario eliminado exitosamente.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al editar la respuesta: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("EditarFormulario")]
        public IActionResult EditarFormulario(int id, ConfigForm Field)
        {
            using (var conexion = new SqlConnection(cadenaSQL))
            {
                conexion.Open();
                using (var transaction = conexion.BeginTransaction())
                {
                    try
                    {
                        // Verifica si el formulario con el ID proporcionado existe
                        var existingForm = conexion.QuerySingleOrDefault<ConfigForm>("SELECT * FROM ConfigForm WHERE Id_ConfigForm = @Id", new { Id = id }, transaction);

                        if (existingForm == null)
                        {
                            // El formulario no existe, devolver un error
                            return NotFound("Formulario no encontrado");
                        }

                        // Actualiza los datos del formulario
                        DateTime fechaModificacion = DateTime.Now;
                        var updateConfigFormSql = "UPDATE ConfigForm SET Titulo = @Titulo, Descripcion = @Descripcion, fecha_modificacion = @fecha_modificacion WHERE Id_ConfigForm = @Id";
                        conexion.Execute(updateConfigFormSql, new
                        {
                            Id = id,
                            Titulo = Field.Titulo,
                            Descripcion = Field.Descripcion,
                            fecha_modificacion = fechaModificacion
                        }, transaction);



                        // Inserta los nuevos campos
                        foreach (var fieldInput in Field.Campos)
                        {
                            var insertFieldSql = "INSERT INTO Field (nombre, orden, etiqueta, tipo, requerido, marcador, opciones, visible, clase, estado, Id_ConfigForm, fecha_eliminacion) " +
                                "VALUES (@nombre, @orden, @etiqueta, @tipo, @requerido, @marcador, @opciones, @visible, @clase, @estado, @Id_ConfigForm, @fecha_eliminacion);";
                            conexion.Execute(insertFieldSql, new
                            {
                                nombre = fieldInput.nombre,
                                orden = fieldInput.orden,
                                etiqueta = fieldInput.etiqueta,
                                tipo = fieldInput.tipo,
                                requerido = fieldInput.requerido,
                                marcador = fieldInput.marcador,
                                opciones = fieldInput.opciones,
                                visible = fieldInput.visible,
                                clase = fieldInput.clase,
                                estado = fieldInput.estado,
                                Id_ConfigForm = id,
                                fecha_eliminacion = fieldInput.fecha_eliminacion
                            }, transaction);
                        }

                        transaction.Commit();

                        // Devuelve el ID del formulario actualizado como respuesta HTTP 200 (éxito)
                        return Ok(new { Id = id, Estado = "Cargado correctamente" });
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }



    }
}





