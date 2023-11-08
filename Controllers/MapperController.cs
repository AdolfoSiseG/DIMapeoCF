using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Dynamic;

namespace Integrador.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MapperController : ControllerBase
    {
        private readonly ILogger<MapperController> _logger;

        public MapperController(ILogger<MapperController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [Route("procesar")]
        public IActionResult MapRequest([FromBody] dynamic request)
        {
            try
            {
                var detalleXml = request.detalle.ToString();
                var detalle = JObject.Parse(@"{
                    'Item': 1,
                    'codigo_producto': 13219,
                    'Cantidad': 6,
                    'PrecUni': 23.49,
                    'ValorT': 140.94,
                    'ivaitem': 18.32
                }");

                dynamic salida = new ExpandoObject();
                salida.documentoRelacionado = null;
                salida.emisor = new
                {
                    nit = "0614-300392-101-5",
                    nrc = "51518-3",
                    nombre = "MONTREAL, S.A. DE C.V.",
                    codActividad = "46484",
                    descActividad = "VENTA DE PRODUCTOS FARMACEUTICOS Y MEDICINALES",
                    nombreComercial = "DROGUERIA INTEGRAL",
                    tipoEstablecimiento = (string)null,
                    direccion = new
                    {
                        departamento = "06",
                        municipio = "14",
                        complemento = "CALLE LOS ABETOS PJE 2, URBANIZACION SAN FRANCISCO, #27"
                    },
                    telefono = "2224-2424",
                    correo = "DIemisorDTE@drogueriaintegralsv.com",
                    codEstable = (string)null,
                    codPuntoVenta = (string)null,
                    codEstableMH = (string)null,
                    codPuntoVentaMH = (string)null
                };
                salida.receptor = null;
                salida.otrosDocumentos = null;
                salida.ventaTercero = null;
                salida.cuerpoDocumento = new[]
                {
                    new
                    {
                        numItem = detalle["Item"],
                        tipoItem = 1,
                        numeroDocumento = 4,
                        cantidad = detalle["Cantidad"],
                        codigo = detalle["codigo_producto"],
                        codTributo = 2,
                        uniMedida = 99,
                        descripcion = "VENTA DE PRODUCTOS FARMACEUTICOS Y MEDICINALES",
                        precioUni = detalle["PrecUni"],
                        montoDescu = 0.00,
                        ventaNoSuj = 0.00,
                        ventaExenta = 0.00,
                        ventaGravada = detalle["ValorT"],
                        tributos = (string)null,
                        psv = 0.00,
                        noGravado = 0.00,
                        ivaItem = detalle["ivaitem"]
                    }
                };
                salida.resumen = new
                {
                    totalNoSuj = 0.00,
                    totalExcenta = 0.00,
                    totalGravada = request.Tgravado,
                    subTotalVentas = request.Tgravado,
                    descuNoSuj = 0.00,
                    descuExenta = 0.00,
                    descuGravada = 0.00,
                    porcentajeDescuento = 0.00,
                    totalDescu = 0.00,
                    tributos = new[] { new { codigo = (string)null } },
                    subTotal = 0.00,
                    ivaRete1 = 0.00,
                    reteRenta = 0.00,
                    montoTotalOperacion = 0.00,
                    totalNoGravado = 0.00,
                    totalPagar = request.Tgravado,
                    totalLetras = "CIENTO CUARENTA DOLARES USD CON NOVENTA Y CUATRO CENTAVOS",
                    saldoFavor = 0.00,
                    totalIva = request.Tiva,
                    condicionOperacion = request.Condi ?? 3,
                    pagos = (string)null
                };
                salida.extension = null;
                salida.apendice = null;
                salida.identificación = new
                {
                    version = "1",
                    ambiente = "00",
                    tipoDte = "01",
                    numeroControl = request.numControl,
                    codigoGeneracion = request.codGeneracion,
                    tipoModelo = "1",
                    tipoOperacion = "1",
                    tipoContingencia = (string)null,
                    motivoContin = (string)null,
                    fecEmi = ((DateTime)request.Fefac).ToString("yyyy-MM-dd"),
                    horEmi = ((DateTime)request.Fefac).ToString("HH:mm:ss"),
                    tipoMoneda = "USD"
                };

                return Ok(salida);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                return BadRequest();
            }
        }
    }
}
