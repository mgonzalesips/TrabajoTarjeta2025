using Xunit;
using Tarjeta.Clases;
using TarjetaClass = Tarjeta.Clases.Tarjeta;

namespace TarjetaTests
{
    public class ColectivoTests
    {
        [Fact]
        public void Constructor_DeberiaInicializarColectivoConLinea()
        {
            string linea = "144";
            
            var colectivo = new Colectivo(linea);
            
            Assert.Equal(linea, colectivo.Linea);
        }

        [Fact]
        public void PagarCon_ConSaldoSuficiente_DeberiaRetornarBoleto()
        {
            var colectivo = new Colectivo("144");
            var tarjeta = new TarjetaClass("12345", 3000);
            decimal tarifaBasica = 1580;
            
            var boleto = colectivo.PagarCon(tarjeta);
            
            Assert.NotNull(boleto);
            Assert.Equal("144", boleto.Linea);
            Assert.Equal(tarifaBasica, boleto.Monto);
            Assert.Equal(1420, tarjeta.Saldo);
        }

        [Fact]
        public void PagarCon_ConSaldoInsuficiente_DeberiaRetornarNull()
        {
            var colectivo = new Colectivo("144");
            var tarjeta = new TarjetaClass("12345", 1000);
            decimal saldoAnterior = tarjeta.Saldo;
            
            var boleto = colectivo.PagarCon(tarjeta);
            
            Assert.Null(boleto);
            Assert.Equal(saldoAnterior, tarjeta.Saldo);
        }

        [Fact]
        public void PagarCon_ConSaldoExacto_DeberiaRetornarBoleto()
        {
            var colectivo = new Colectivo("144");
            var tarjeta = new TarjetaClass("12345", 1580);
            
            var boleto = colectivo.PagarCon(tarjeta);
            
            Assert.NotNull(boleto);
            Assert.Equal("144", boleto.Linea);
            Assert.Equal(1580, boleto.Monto);
            Assert.Equal(0, tarjeta.Saldo);
        }

        [Fact]
        public void PagarCon_ConMontoPersonalizado_DeberiaUsarEseMonto()
        {
            var colectivo = new Colectivo("144");
            var tarjeta = new TarjetaClass("12345", 5000);
            decimal montoPersonalizado = 2000;
            
            var boleto = colectivo.PagarCon(tarjeta, montoPersonalizado);
            
            Assert.NotNull(boleto);
            Assert.Equal("144", boleto.Linea);
            Assert.Equal(montoPersonalizado, boleto.Monto);
            Assert.Equal(3000, tarjeta.Saldo);
        }

        [Fact]
        public void PagarCon_ConMontoPersonalizadoYSaldoInsuficiente_DeberiaRetornarNull()
        {
            var colectivo = new Colectivo("144");
            var tarjeta = new TarjetaClass("12345", 1500);
            decimal montoPersonalizado = 2000;
            decimal saldoAnterior = tarjeta.Saldo;
            
            var boleto = colectivo.PagarCon(tarjeta, montoPersonalizado);
            
            Assert.Null(boleto);
            Assert.Equal(saldoAnterior, tarjeta.Saldo);
        }

        [Theory]
        [InlineData("144")]
        [InlineData("K")]
        [InlineData("102")]
        [InlineData("Inter")]
        public void PagarCon_ConDiferentesLineas_DeberiaCrearBoletoConLineaCorrecta(string linea)
        {
            var colectivo = new Colectivo(linea);
            var tarjeta = new TarjetaClass("12345", 5000);
            
            var boleto = colectivo.PagarCon(tarjeta);
            
            Assert.NotNull(boleto);
            Assert.Equal(linea, boleto.Linea);
        }

        [Fact]
        public void ToString_DeberiaRetornarFormatoCorrect()
        {
            var colectivo = new Colectivo("144");
            
            string resultado = colectivo.ToString();
            
            Assert.Equal("Línea: 144", resultado);
        }
    }
}