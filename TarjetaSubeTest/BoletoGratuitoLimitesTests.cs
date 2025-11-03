using NUnit.Framework;
using System;
using TarjetaSube;

namespace TarjetaSubeTest
{
    [TestFixture]
    public class BoletoGratuitoLimitesTests
    {
        [Test]
        public void BoletoGratuito_PermiteDosViajesGratuitosPorDia()
        {
            // Configurar tiempo controlado
            DateTime now = new DateTime(2025, 1, 1, 10, 0, 0);
            var tarjetaGratuita = new BoletoGratuitoEstudiantil(() => now);
            var colectivo = new Colectivo("132");

            // Primer viaje gratuito
            var boleto1 = colectivo.PagarCon(tarjetaGratuita);
            Assert.IsTrue(boleto1.EsValido);
            Assert.AreEqual(0m, boleto1.Monto);

            // Avanzar 6 segundos (sin Thread.Sleep)
            now = now.AddSeconds(6);

            // Segundo viaje gratuito  
            var boleto2 = colectivo.PagarCon(tarjetaGratuita);
            Assert.IsTrue(boleto2.EsValido);
            Assert.AreEqual(0m, boleto2.Monto);
        }

        [Test]
        public void BoletoGratuito_TercerViajeDelDia_TarifaCompleta()
        {
            // Configurar tiempo controlado
            DateTime now = new DateTime(2025, 1, 1, 10, 0, 0);
            var tarjetaGratuita = new BoletoGratuitoEstudiantil(() => now);
            tarjetaGratuita.Cargar(5000m);
            var colectivo = new Colectivo("132");

            // Primer viaje gratuito
            var boleto1 = colectivo.PagarCon(tarjetaGratuita);
            Assert.IsTrue(boleto1.EsValido);
            Assert.AreEqual(0m, boleto1.Monto);

            // Avanzar 6 segundos
            now = now.AddSeconds(6);

            // Segundo viaje gratuito
            var boleto2 = colectivo.PagarCon(tarjetaGratuita);
            Assert.IsTrue(boleto2.EsValido);
            Assert.AreEqual(0m, boleto2.Monto);

            // Avanzar 6 segundos más
            now = now.AddSeconds(6);

            // Tercer viaje - debe ser tarifa completa
            var boleto3 = colectivo.PagarCon(tarjetaGratuita);
            Assert.IsTrue(boleto3.EsValido);
            Assert.AreEqual(1580m, boleto3.Monto, "Tercer viaje debe ser tarifa completa");
            Assert.AreEqual(5000m - 1580m, tarjetaGratuita.Saldo);
        }
    }
}