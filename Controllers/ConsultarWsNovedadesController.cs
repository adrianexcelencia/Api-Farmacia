using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Threading.Tasks;

using APIformbuilder.Models;
using APIformbuilder.Service.Interface;
using static System.Net.WebRequestMethods;
using System.Text;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Xml;
using System.Data;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata;
using Dapper;
using static System.Runtime.CompilerServices.RuntimeHelpers;
using System.Runtime.ConstrainedExecution;
using APIformbuilder.Service.Concrete;

namespace APIformbuilder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultarWsNovedadesController : ControllerBase
    {
        private readonly string cadenaSQL;
        public ConsultarWsNovedadesController(IConfiguration config)
        {
            cadenaSQL = config.GetConnectionString("CadenaSQL");
        }

        [HttpGet]
        [Route("VerWsNovedades")]
        public async Task VerWsNovedades()
        {
            //consultar ultimo numero de log generado
            Int64 UltimoLog = 0;
            using (var conexion = new SqlConnection(cadenaSQL))
            {
                conexion.Open();

                // verificar si el usuario ingresado existe
                string userExistsQuery = "select top 1 ultimolog from AlfaBetaDescarga order by descargaid desc ";
                SqlCommand userExistsCommand = new SqlCommand(userExistsQuery, conexion);
                 UltimoLog = (Int64)userExistsCommand.ExecuteScalar();
            }

            // Fin ultimo log generado
            // Crear una instancia de la API
            var MiApiNovedades = new MiApiNovedades();
            string rutaWebService = "https://abws.alfabeta.net/alfabeta-webservice/abWsDescargas/7733/polivalente/<parametros><ultimologMF>2975432</ultimologMF><basecompleta>N</basecompleta><solotablas>N</solotablas></parametros>";
            //*****************************************
            //*****************************************
            //*****************************************
            // Llamar al servicio web
            //IMPORTANTE descomentar el codigo para que se genere el XML
            string resultado = await MiApiNovedades.LlamarWebServiceNAsync(rutaWebService, UltimoLog);
           // string resultado = await MiApiNovedades.PruebaAsync();
            

            //*****************************************
            //*****************************************
            //*****************************************
            string assemblyLocation = Assembly.GetExecutingAssembly().Location;
            string escritorio = Path.GetDirectoryName(assemblyLocation);
            // Ruta completa del archivo XML
            string xmlFilePath = @"C:\AddWeb\Creador\APIformbuilder-master\bin\Debug\net6.0\novedades.xml";

            // Crear un nuevo objeto XmlDocument
            XmlDocument xmlDoc = new XmlDocument();

            // Cargar el archivo XML
            xmlDoc.Load(xmlFilePath);
            // Obtener el valor del tag ultimolog
           // Iterar a través de los elementos del XML
            // Iterar sobre cada nodo <articulo>
            XmlNode codigoNode = xmlDoc.SelectSingleNode("//codigo");
            string codigoValue = codigoNode.InnerText;
            XmlNode descripcionNode = xmlDoc.SelectSingleNode("//descripcion");
             string descripcionValue = descripcionNode.InnerText;
            XmlNodeList articuloNodes = xmlDoc.SelectNodes("//logMF");

            
            


            //***************** //*****************
            //*********Grabar Tabla************
            //***************** //*****************
            string resultado_graba = "01";
            using (var conexion = new SqlConnection(cadenaSQL))
            {
                conexion.Open();
                using (var transaction = conexion.BeginTransaction())
                {
                    try
                    {
                       
                        var insertConfigFormSql = "insert INTO AlfaBetaDescarga_Novedades (codigo,descripcion,ultimolog) values (@codigo,@descripcion,@log); SELECT SCOPE_IDENTITY();";
                        int configFormId = conexion.QuerySingle<int>(insertConfigFormSql, new
                        {
                            codigo = codigoValue,
                            descripcion = descripcionValue,
                            log = UltimoLog
                        }, transaction);

                        foreach (XmlNode articuloNode in articuloNodes)
                        {
                            // Obtener el valor de los tags <nom>, <tro> y <bar> para cada <articulo>
                            string tipoValue = articuloNode.SelectSingleNode("tipo").InnerText;
                            string regValue = string.Empty;
                            string vigValue = string.Empty;
                            string prcValue = string.Empty;
                            string numerologValue = articuloNode.SelectSingleNode("log").InnerText;



                            string nomValue = string.Empty;
                            string presentacionValue = string.Empty;
                            string troquelValue = string.Empty;
                            string barrraValue = string.Empty;
                            string uniValue = string.Empty;
                            string forValue = string.Empty;


                            // Verificar si el tag <nom> existe para el artículo actual
                            XmlNode nomNode = articuloNode.SelectSingleNode("nom");
                            if (nomNode != null)
                            {
                                nomValue = nomNode.InnerText;
                            }

                            // Verificar si el tag <prc> existe para el artículo actual
                            XmlNode regNode = articuloNode.SelectSingleNode("reg");
                            if (regNode != null)
                            {
                                regValue = regNode.InnerText;
                            }

                            // Verificar si el tag <prc> existe para el artículo actual
                            XmlNode prcNode = articuloNode.SelectSingleNode("prc");
                            if (prcNode != null)
                            {
                                prcValue = prcNode.InnerText;
                                
                            }
                            if ((tipoValue == "B") || (tipoValue == "C") || (tipoValue == "T"))
                                prcValue = "0.00";

                            // Verificar si el tag <vig> existe para el artículo actual
                            XmlNode vigNode = articuloNode.SelectSingleNode("vig");
                            if (vigNode != null)
                            {
                                vigValue = vigNode.InnerText;
                            }

                            // Verificar si el tag <tro> existe para el artículo actual
                            XmlNode troNode = articuloNode.SelectSingleNode("tro");
                            if (troNode != null)
                            {
                                troquelValue = troNode.InnerText;
                            }
                            // Obtener el valor del tag <bar> para cada <articulo>
                            XmlNode barNode = articuloNode.SelectSingleNode("bars/bar");
                            if (barNode != null)
                            {
                                barrraValue = barNode.InnerText;
                            }
                            // Obtener el valor del tag <pres> para cada <articulo>
                            XmlNode presNode = articuloNode.SelectSingleNode("pres");
                            if (presNode != null)
                            {
                                presentacionValue = presNode.InnerText;
                            }
                            // Obtener el valor del tag <for> para cada <articulo>
                            XmlNode forNode = articuloNode.SelectSingleNode("for");
                            if (forNode != null)
                            {
                                forValue = forNode.InnerText;
                            }
                            // Obtener el valor del tag <unit> para cada <articulo> UNIDADES
                            XmlNode unitNode = articuloNode.SelectSingleNode("uni");
                            if (unitNode != null)
                            {
                                uniValue = unitNode.InnerText;
                            }



                            var insertFieldSql = "INSERT INTO AlfaBeta_Novedades (registro,tipo,numerolog,fechavig,precio,nombre,Presentacion,troquel,CodBarras,uni,descargaid) VALUES (@NroRegistro,@tipo,@numerolog,@fechavig,@precio,@nombre,@presentacion,@troquel,@CodBarras,@Unidades,@descargaid)";
                            conexion.Execute(insertFieldSql, new
                            {
                             numerolog = numerologValue,
                             nombre = nomValue,
                             fechavig = vigValue,
                             precio = prcValue.Trim(),
                             presentacion = presentacionValue,
                             troquel = troquelValue,
                             CodBarras = barrraValue,
                             NroRegistro = regValue,
                             Unidades = uniValue,
                             forma = forValue,
                             descargaid = configFormId,
                             tipo = tipoValue

                            }, transaction);
                        }

                        transaction.Commit();

                        // Devuelve el ID generado como respuesta HTTP 200 (éxito)
                        //return resultado =  "00";
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }

            //***************** //*****************
            //*****************FIN*************** 
            //***************** //*****************


        }
    }
    public class MiApiNovedades
    {
        private readonly HttpClient clienteHttp;
        private string projectDirectory;

        public MiApiNovedades()
        {
            clienteHttp = new HttpClient();
            clienteHttp.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/todos/1");
            string rutaWebService = "https://abws.alfabeta.net/alfabeta-webservice/abWsDescargas/7733/polivalente/<parametros><ultimologMF>XXXXXX</ultimologMF><basecompleta>S</basecompleta><solotablas>N</solotablas></parametros>";

        }

        private readonly string cadenaSQL;
        public MiApiNovedades(IConfiguration config)
        {
            cadenaSQL = config.GetConnectionString("CadenaSQL");
        }

        public async Task<string> PruebaAsync()
        {
            string url = "https://abws.alfabeta.net/alfabeta-webservice/abWsDescargas";
            string respuestanueva = "";

            using (HttpClient client = new HttpClient())
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);
                HttpResponseMessage response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    respuestanueva = await response.Content.ReadAsStringAsync();
                    return respuestanueva;
                }
                else
                {
                    // En este caso, podrías devolver una cadena vacía o un mensaje de error, según lo que desees.
                    return ""; // O return "Error: la solicitud no fue exitosa";
                }
            }
        }

        public async Task<string> LlamarWebServiceNAsync(string ruta, Int64 pUltimoLog)
        {
           



                string a1 = "";
             //******************
            //string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            //string escritorio = Path.Combine(desktopPath, "xml");
            string assemblyLocation = Assembly.GetExecutingAssembly().Location;
            string escritorio = Path.GetDirectoryName(assemblyLocation);
        

            //********

            string archivo = Path.Combine(escritorio, "novedades.xml");
            string template =
           "<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" " +
           "xmlns:des=\"http://descargas.ws.webservice.alfabeta.net/\">" +
           "<soapenv:Header/>" +
           "<soapenv:Body>" +
           "<des:actualizar>" +
           "<id>:ID</id>" +
           "<clave>:CLAVE</clave>" +
           "<xml><![CDATA[:XML]]></xml>" +
           "</des:actualizar>" +
           "</soapenv:Body>" +
           "</soapenv:Envelope>";
            try
            {
                //************************ 

                string url = "https://abws.alfabeta.net/alfabeta-webservice/abWsDescargas";
                string id = "7733";
                string clave = "polivalente";
                string xml = "<parametros><ultimologMF>"+ pUltimoLog.ToString() + "</ultimologMF><basecompleta>N</basecompleta><solotablas>N</solotablas></parametros>";
                string body = template.Replace(":ID", id).Replace(":CLAVE", clave).Replace(":XML", xml);

                using (HttpClient client = new HttpClient())
                {
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);
                    request.Content = new StringContent(body, Encoding.UTF8, "text/xml");
                    request.Headers.Add("SOAPAction", "");
                    try
                    {
                        HttpResponseMessage response = await client.SendAsync(request);
                        if (response.IsSuccessStatusCode)
                        {
                            String respuestanueva = await response.Content.ReadAsStringAsync();

                            int startIndex = respuestanueva.IndexOf("<return>") + "<return>".Length;
                            int endIndex = respuestanueva.IndexOf("</return>", startIndex);
                            string base64 = respuestanueva.Substring(startIndex, endIndex - startIndex);

                            byte[] responseBytes = Convert.FromBase64String(base64);
                            byte[] datosTxt = new byte[0];
                            //************
                            using (MemoryStream zipStream = new MemoryStream(responseBytes))
                            {
                                using (ZipArchive archive = new ZipArchive(zipStream, ZipArchiveMode.Read))
                                {
                                    ZipArchiveEntry? entry = archive.GetEntry("datos.txt");
                                    if (entry != null)
                                    {
                                        using (MemoryStream extractedFileStream = new MemoryStream())
                                        {
                                            using (Stream entryStream = entry.Open())
                                            {
                                                entryStream.CopyTo(extractedFileStream);
                                            }
                                            datosTxt = extractedFileStream.ToArray();
                                        }
                                    }
                                    else
                                    {
                                        a1 = "Error: datos.txt no se encuentra en el ZIP";
                                    }
                                }
                            }

                            await System.IO.File.WriteAllBytesAsync(archivo, datosTxt);
                            a1 = "Archivo descargado en ";
                            //************************
                            if (datosTxt.Length == 0)
                            {
                                a1 = "Error: datos.txt no se encuentra en el ZIP";
                            }
                            using (var fileStream = new FileStream(archivo, FileMode.Create, FileAccess.Write, FileShare.None, bufferSize: 4096, useAsync: true))
                            {
                                await fileStream.WriteAsync(datosTxt, 0, datosTxt.Length);
                            }
                            a1 = "Archivo descargado en " + respuestanueva;
                        }
                        else
                        {
                            a1 = "Error: " + response.StatusCode;
                        }
                    }
                    catch (Exception ex)
                    { }
                }
                //***************************


                //***Estos sirve
                // Realizar la solicitud GET al servicio web
                HttpResponseMessage respuesta = await clienteHttp.GetAsync(ruta);

                // Verificar si la solicitud fue exitosa (código de estado 200)
                if (respuesta.IsSuccessStatusCode)
                {
                    // Leer y devolver la respuesta en formato JSON
                    return await respuesta.Content.ReadAsStringAsync();
                }
                else
                {
                    // Si la solicitud no fue exitosa, lanzar una excepción o devolver un mensaje de error
                    return $"Error en la solicitud. Código de estado: {respuesta.StatusCode}";
                }
            }
            catch (Exception ex)
            {
                // Manejar excepciones en caso de errores
                return $"Error: {ex.Message}";
            }
        }

        //Novedades

        [HttpGet]
        [Route("VerWsNovedades")]
        public async Task VerWsNovedades()
        {
            // Crear una instancia de la API
            var miApi = new MiApi();
            string rutaWebService = "https://abws.alfabeta.net/alfabeta-webservice/abWsDescargas/7733/polivalente/<parametros><ultimologMF></ultimologMF><ofertas>N</ofertas><basecompleta>S</basecompleta></parametros>";

            // Llamar al servicio web
            string resultado = await miApi.LlamarWebServiceNovedadesAsync(rutaWebService);
        }

        public async Task<string> LlamarWebServiceNovedadesAsync(string ruta)
        {
            string a1 = "";
            //string escritorio = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            //******************
            //string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            //string escritorio = Path.Combine(desktopPath, "xml");
            string assemblyLocation = Assembly.GetExecutingAssembly().Location;
            string escritorio = Path.GetDirectoryName(assemblyLocation);
            //C:\AddWeb\Creador\APIformbuilder-master\bin\Debug\net6.0

            //********

            string archivo = Path.Combine(escritorio, "novedades.xml");
            string template =
           "<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" " +
           "xmlns:des=\"http://descargas.ws.webservice.alfabeta.net/\">" +
           "<soapenv:Header/>" +
           "<soapenv:Body>" +
           "<des:actualizar>" +
           "<id>:ID</id>" +
           "<clave>:CLAVE</clave>" +
           "<xml><![CDATA[:XML]]></xml>" +
           "</des:actualizar>" +
           "</soapenv:Body>" +
           "</soapenv:Envelope>";
            try
            {
                //************************ 

                string url = "https://abws.alfabeta.net/alfabeta-webservice/abWsDescargas";
                string id = "7733";
                string clave = "polivalente";
                string xml = "<parametros><ultimologMF></ultimologMF><ofertas>N</ofertas><basecompleta>S</basecompleta></parametros>";
                string body = template.Replace(":ID", id).Replace(":CLAVE", clave).Replace(":XML", xml);

                using (HttpClient client = new HttpClient())
                {
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);
                    request.Content = new StringContent(body, Encoding.UTF8, "text/xml");
                    request.Headers.Add("SOAPAction", "");
                    try
                    {
                        HttpResponseMessage response = await client.SendAsync(request);
                        if (response.IsSuccessStatusCode)
                        {
                            String respuestanueva = await response.Content.ReadAsStringAsync();

                            int startIndex = respuestanueva.IndexOf("<return>") + "<return>".Length;
                            int endIndex = respuestanueva.IndexOf("</return>", startIndex);
                            string base64 = respuestanueva.Substring(startIndex, endIndex - startIndex);

                            byte[] responseBytes = Convert.FromBase64String(base64);
                            byte[] datosTxt = new byte[0];
                            //************
                            using (MemoryStream zipStream = new MemoryStream(responseBytes))
                            {
                                using (ZipArchive archive = new ZipArchive(zipStream, ZipArchiveMode.Read))
                                {
                                    ZipArchiveEntry? entry = archive.GetEntry("datos.txt");
                                    if (entry != null)
                                    {
                                        using (MemoryStream extractedFileStream = new MemoryStream())
                                        {
                                            using (Stream entryStream = entry.Open())
                                            {
                                                entryStream.CopyTo(extractedFileStream);
                                            }
                                            datosTxt = extractedFileStream.ToArray();
                                        }
                                    }
                                    else
                                    {
                                        a1 = "Error: datos.txt no se encuentra en el ZIP";
                                    }
                                }
                            }

                            await System.IO.File.WriteAllBytesAsync(archivo, datosTxt);
                            a1 = "Archivo descargado en ";
                            //************************




                            if (datosTxt.Length == 0)
                            {
                                a1 = "Error: datos.txt no se encuentra en el ZIP";
                            }
                            using (var fileStream = new FileStream(archivo, FileMode.Create, FileAccess.Write, FileShare.None, bufferSize: 4096, useAsync: true))
                            {
                                await fileStream.WriteAsync(datosTxt, 0, datosTxt.Length);
                            }
                            a1 = "Archivo descargado en " + respuestanueva;
                        }
                        else
                        {
                            a1 = "Error: " + response.StatusCode;
                        }
                    }
                    catch (Exception ex)
                    { }
                }
                //***************************


                //***Estos sirve
                // Realizar la solicitud GET al servicio web
                HttpResponseMessage respuesta = await clienteHttp.GetAsync(ruta);

                // Verificar si la solicitud fue exitosa (código de estado 200)
                if (respuesta.IsSuccessStatusCode)
                {
                    // Leer y devolver la respuesta en formato JSON
                    return await respuesta.Content.ReadAsStringAsync();
                }
                else
                {
                    // Si la solicitud no fue exitosa, lanzar una excepción o devolver un mensaje de error
                    return $"Error en la solicitud. Código de estado: {respuesta.StatusCode}";
                }
            }
            catch (Exception ex)
            {
                // Manejar excepciones en caso de errores
                return $"Error: {ex.Message}";
            }
        }
    }
}
