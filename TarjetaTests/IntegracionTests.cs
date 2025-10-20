using Xunit;
using Tarjeta.Clases;
using TarjetaClass = Tarjeta.Clases.Tarjeta;

namespace TarjetaTests
{
    public class IntegracionTests
    {
        [Fact]
        public void FlujoCompleto_RecargarTarjetaYPagarViaje_DeberiaFuncionarCorrectamente()
        {
            var tarjeta = new TarjetaClass("12345");
            var colectivo = new Colectivo("144");
            
            // Recargar la tarjeta
            bool recargaExitosa = tarjeta.Recargar(5000);
            Assert.True(recargaExitosa);
            Assert.Equal(5000, tarjeta.Saldo);
            
            // Pagar primer viaje
            var primerBoleto = colectivo.PagarCon(tarjeta);
            Assert.NotNull(primerBoleto);
            Assert.Equal("144", primerBoleto.Linea);
            Assert.Equal(1580, primerBoleto.Monto);
            Assert.Equal(3420, tarjeta.Saldo); // 5000 - 1580
            
            // Pagar segundo viaje
            var segundoBoleto = colectivo.PagarCon(tarjeta);
            Assert.NotNull(segundoBoleto);
            Assert.Equal(1840, tarjeta.Saldo); // 3420 - 1580

            // Intentar recargar con monto no válido
            bool recargaInvalida = tarjeta.Recargar(1000);
            Assert.False(recargaInvalida);
            Assert.Equal(1840, tarjeta.Saldo); // Saldo no debe cambiar
        }

        [Fact]
        public void FlujoCompleto_SaldoInsuficiente_NoDeberiaPermitirViaje()
        {
            var tarjeta = new TarjetaClass("67890", 1000);
            var colectivo = new Colectivo("K");
            
            var boleto = colectivo.PagarCon(tarjeta);
            
            Assert.Null(boleto);
            Assert.Equal(1000, tarjeta.Saldo);
        }

        [Fact]
        public void FlujoCompleto_LimiteDeSaldo_DeberiaRespetarLimite()
        {
            var tarjeta = new TarjetaClass("99999", 35000);
            
            // Intento de recarga que excede límite
            bool recargaExcesiva = tarjeta.Recargar(10000);
            Assert.False(recargaExcesiva);
            Assert.Equal(35000, tarjeta.Saldo);
            
            // Recarga que alcanza justo el límite
            bool recargaLimite = tarjeta.Recargar(5000);
            Assert.True(recargaLimite);
            Assert.Equal(40000, tarjeta.Saldo);
            
            // Intento de recarga adicional cuando ya está en el límite
            bool recargaAdicional = tarjeta.Recargar(2000);
            Assert.False(recargaAdicional);
            Assert.Equal(40000, tarjeta.Saldo);
        }

        [Fact]
        public void FlujoCompleto_MultiplesViajesYRecargas_DeberiaFuncionarCorrectamente()
        {
            var tarjeta = new TarjetaClass("11111");
            var colectivo144 = new Colectivo("144");
            var colectivoK = new Colectivo("K");
            
            Assert.True(tarjeta.Recargar(10000));
            Assert.Equal(10000, tarjeta.Saldo);
            
            var boleto1 = colectivo144.PagarCon(tarjeta);
            Assert.NotNull(boleto1);
            Assert.Equal(8420, tarjeta.Saldo);
            
            var boleto2 = colectivoK.PagarCon(tarjeta);
            Assert.NotNull(boleto2);
            Assert.Equal(6840, tarjeta.Saldo);
            
            Assert.True(tarjeta.Recargar(15000));
            Assert.Equal(21840, tarjeta.Saldo);
            
            var boleto3 = colectivo144.PagarCon(tarjeta, 2000);
            Assert.NotNull(boleto3);
            Assert.Equal(2000, boleto3.Monto);
            Assert.Equal(19840, tarjeta.Saldo);
            
            Assert.Equal("144", boleto1.Linea);
            Assert.Equal("K", boleto2.Linea);
            Assert.Equal("144", boleto3.Linea);
            Assert.True(boleto1.FechaHora <= DateTime.Now);
            Assert.True(boleto2.FechaHora <= DateTime.Now);
            Assert.True(boleto3.FechaHora <= DateTime.Now);
        }

        [Fact]
        public void FlujoCompleto_AgotarSaldoCompletamente_DeberiaFuncionarCorrectamente()
        {
            var tarjeta = new TarjetaClass("22222");
            var colectivo = new Colectivo("Inter");
            
            Assert.True(tarjeta.Recargar(3000));
            
            var boleto1 = colectivo.PagarCon(tarjeta);
            Assert.NotNull(boleto1);
            Assert.Equal(1420, tarjeta.Saldo);
            
            var boleto2 = colectivo.PagarCon(tarjeta);
            Assert.Null(boleto2);
            Assert.Equal(1420, tarjeta.Saldo);
            
            Assert.True(tarjeta.Recargar(2000));
            Assert.Equal(3420, tarjeta.Saldo);
            
            var boleto3 = colectivo.PagarCon(tarjeta);
            Assert.NotNull(boleto3);
            Assert.Equal(1840, tarjeta.Saldo);
        }

        [Theory]
        [InlineData(2000, 3000, 4000)]
        [InlineData(5000, 8000, 10000)]
        [InlineData(15000, 20000, 25000)]
        public void FlujoCompleto_CargasAceptadasSecuenciales_DeberiaAcumularCorrectamente(decimal carga1, decimal carga2, decimal carga3)
        {

            var tarjeta = new TarjetaClass("33333");
            var colectivo = new Colectivo("Test");
            
            Assert.True(tarjeta.Recargar(carga1));
            decimal saldoDespuesPrimera = carga1;
            Assert.Equal(saldoDespuesPrimera, tarjeta.Saldo);
            
            Assert.True(tarjeta.Recargar(carga2));
            decimal saldoDespuesSegunda = saldoDespuesPrimera + carga2;
            Assert.Equal(saldoDespuesSegunda, tarjeta.Saldo);
            
            if (saldoDespuesSegunda + carga3 <= 40000)
            {
                Assert.True(tarjeta.Recargar(carga3));
                Assert.Equal(saldoDespuesSegunda + carga3, tarjeta.Saldo);
                
                var boleto = colectivo.PagarCon(tarjeta);
                Assert.NotNull(boleto);
                Assert.Equal("Test", boleto.Linea);
            }
            else
            {
                Assert.False(tarjeta.Recargar(carga3));
                Assert.Equal(saldoDespuesSegunda, tarjeta.Saldo);
            }
        }
    }
}