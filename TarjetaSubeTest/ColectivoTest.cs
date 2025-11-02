using NUnit.Framework;
using TarjetaSube;

namespace TarjetaSubeTest
{
    [TestFixture]
    public class ColectivoTest
    {
        [Test]
        public void TestCrearColectivo()
        {
            Colectivo colectivo = new Colectivo("120");
            Assert.AreEqual("120", colectivo.Linea);
        }

        [Test]
        public void TestPagarConSaldoSuficiente()
        {
            Colectivo colectivo = new Colectivo("120");
            Tarjeta tarjeta = new Tarjeta();
            tarjeta.Cargar(5000);

            bool pagoExitoso = colectivo.PagarCon(tarjeta);

            Assert.IsTrue(pagoExitoso);
            Assert.AreEqual(3420, tarjeta.ObtenerSaldo());


            Boleto boleto = new Boleto(
                tipoTarjeta: "Normal",
                lineaColectivo: "120",
                totalAbonado: 1580,
                saldoRestante: tarjeta.ObtenerSaldo()
            );

            Assert.AreEqual(1580, boleto.TotalAbonado);
            Assert.AreEqual(3420, boleto.SaldoRestante);
            Assert.AreEqual("120", boleto.LineaColectivo);
            Assert.AreEqual("Normal", boleto.TipoTarjeta);
        }

       

        [Test]
        public void TestPagarConTarjetaSinSaldo()
        {
            Colectivo colectivo = new Colectivo("102");
            Tarjeta tarjeta = new Tarjeta();

            bool pagoExitoso = colectivo.PagarCon(tarjeta);

            Assert.IsFalse(pagoExitoso);
            Assert.AreEqual(0, tarjeta.ObtenerSaldo());
        }

        [Test]
        public void TestMultiplesPagos()
        {
            Colectivo colectivo = new Colectivo("115");
            Tarjeta tarjeta = new Tarjeta();
            tarjeta.Cargar(10000);

            bool pago1 = colectivo.PagarCon(tarjeta);
            Assert.IsTrue(pago1);
            Assert.AreEqual(8420, tarjeta.ObtenerSaldo());

            bool pago2 = colectivo.PagarCon(tarjeta);
            Assert.IsTrue(pago2);
            Assert.AreEqual(6840, tarjeta.ObtenerSaldo());
        }

        [Test]
        public void TestPagarConMedioBoleto()
        {
            Colectivo colectivo = new Colectivo("200");
            MedioBoleto tarjeta = new MedioBoleto();
            tarjeta.Cargar(2000);

            bool pagoExitoso = colectivo.PagarCon(tarjeta);

            Assert.IsTrue(pagoExitoso);
            Assert.AreEqual(2000 - 1580 / 2, tarjeta.ObtenerSaldo());
        }

        [Test]
        public void TestPagarConBoletoGratuito()
        {
            Colectivo colectivo = new Colectivo("201");
            BoletoGratuito tarjeta = new BoletoGratuito();

            bool pagoExitoso = colectivo.PagarCon(tarjeta);

            Assert.IsTrue(pagoExitoso);
            Assert.AreEqual(0, tarjeta.ObtenerSaldo());
        }

        [Test]
        public void TestPagarConFranquiciaCompleta()
        {
            Colectivo colectivo = new Colectivo("202");
            FranquiciaCompleta tarjeta = new FranquiciaCompleta();

            bool pagoExitoso = colectivo.PagarCon(tarjeta);

            Assert.IsTrue(pagoExitoso);
            Assert.AreEqual(0, tarjeta.ObtenerSaldo()); 
        }

        [Test]
        public void TestTipoTarjetaRegistradoEnBoleto()
        {
            Colectivo colectivo = new Colectivo("203");
            MedioBoleto tarjeta = new MedioBoleto();
            tarjeta.Cargar(2000);

            bool pagoExitoso = colectivo.PagarCon(tarjeta);

            Assert.IsTrue(pagoExitoso);

            Boleto boleto = new Boleto(
                tipoTarjeta: tarjeta.GetType().Name,
                lineaColectivo: "203",
                totalAbonado: 1580,
                saldoRestante: tarjeta.ObtenerSaldo()
            );

            Assert.AreEqual("MedioBoleto", boleto.TipoTarjeta);
        }
    }
}