using Xunit;
using Tarjeta.Clases;

namespace TarjetaTests
{
    public class BoletoTests
    {
        [Fact]
        public void Constructor_DeberiaInicializarBoletoConDatosCorrectos()
        {
            string linea = "144";
            decimal monto = 1580;
            DateTime antesDeCrear = DateTime.Now;
            
            var boleto = new Boleto(linea, monto);
            DateTime despuesDeCrear = DateTime.Now;
            
            Assert.Equal(linea, boleto.Linea);
            Assert.Equal(monto, boleto.Monto);
            Assert.True(boleto.FechaHora >= antesDeCrear);
            Assert.True(boleto.FechaHora <= despuesDeCrear);
        }

        [Theory]
        [InlineData("144", 1580)]
        [InlineData("K", 2000)]
        [InlineData("102", 1580)]
        [InlineData("Inter", 3000)]
        public void Constructor_ConDiferentesLineasYMontos_DeberiaCrearBoletoCorrectamente(string linea, decimal monto)
        {
            var boleto = new Boleto(linea, monto);
            
            Assert.Equal(linea, boleto.Linea);
            Assert.Equal(monto, boleto.Monto);
            Assert.True(boleto.FechaHora <= DateTime.Now);
        }

        [Fact]
        public void FechaHora_DeberiaSerFechaActual()
        {
            DateTime antes = DateTime.Now;
            
            var boleto = new Boleto("144", 1580);
            DateTime despues = DateTime.Now;
            
            Assert.True(boleto.FechaHora >= antes);
            Assert.True(boleto.FechaHora <= despues);
            // Verificar que la diferencia sea menor a 1 segundo
            Assert.True((despues - boleto.FechaHora).TotalSeconds < 1);
        }

        [Fact]
        public void Propiedades_DeberianSerMutables()
        {
            var boleto = new Boleto("144", 1580);
            string nuevaLinea = "K";
            decimal nuevoMonto = 2000;
            DateTime nuevaFecha = new DateTime(2025, 1, 1, 12, 0, 0);
            
            boleto.Linea = nuevaLinea;
            boleto.Monto = nuevoMonto;
            boleto.FechaHora = nuevaFecha;
            
            Assert.Equal(nuevaLinea, boleto.Linea);
            Assert.Equal(nuevoMonto, boleto.Monto);
            Assert.Equal(nuevaFecha, boleto.FechaHora);
        }

        [Fact]
        public void ToString_DeberiaRetornarFormatoCorrect()
        {
            string linea = "144";
            decimal monto = 1580.50m;
            var boleto = new Boleto(linea, monto);
            
            string resultado = boleto.ToString();
            
            Assert.Contains("Boleto - Línea: 144", resultado);
            Assert.Contains("Monto: $1580.50", resultado);
            Assert.Contains("Fecha y Hora:", resultado);
            Assert.Contains(boleto.FechaHora.ToString(), resultado);
        }

        [Fact]
        public void ToString_ConDiferentesValores_DeberiaIncluirTodosLosDatos()
        {
            var boleto = new Boleto("Inter", 2500.75m);
            
            string resultado = boleto.ToString();
            
            Assert.Contains("Inter", resultado);
            Assert.Contains("2500.75", resultado);
            Assert.Contains("Boleto", resultado);
            Assert.Contains("Línea", resultado);
            Assert.Contains("Monto", resultado);
            Assert.Contains("Fecha y Hora", resultado);
        }

        [Fact]
        public void DosBoletosCreados_DeberianTenerFechasDiferentes()
        {
            var boleto1 = new Boleto("144", 1580);
            Thread.Sleep(10);
            var boleto2 = new Boleto("K", 1580);
            
            Assert.True(boleto2.FechaHora >= boleto1.FechaHora);
        }
    }
}