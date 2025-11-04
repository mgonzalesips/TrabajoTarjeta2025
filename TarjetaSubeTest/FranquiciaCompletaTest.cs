using NUnit.Framework;
using TarjetaSube;

namespace TarjetaSubeTest
{
    [TestFixture]
    public class FranquiciaCompletaTest
    {
        [Test]
        public void TestFranquiciaCompletaPrimerViajeGratuito()
        {
            TiempoFalso tiempoFalso = new TiempoFalso();
            tiempoFalso.AgregarMinutos(360); 
            Colectivo colectivo = new Colectivo("202", tiempoFalso);
            FranquiciaCompleta tarjeta = new FranquiciaCompleta();

            Boleto boleto = colectivo.PagarCon(tarjeta);

            Assert.IsNotNull(boleto);
            Assert.AreEqual(0, boleto.TotalAbonado);
            Assert.AreEqual(0, tarjeta.ObtenerSaldo());
            Assert.AreEqual("FranquiciaCompleta", boleto.TipoTarjeta);
        }

        [Test]
        public void TestFranquiciaCompletaSegundoViajeGratuito()
        {
            TiempoFalso tiempoFalso = new TiempoFalso();
            tiempoFalso.AgregarMinutos(360); 
            Colectivo colectivo = new Colectivo("202", tiempoFalso);
            FranquiciaCompleta tarjeta = new FranquiciaCompleta();

            Boleto boleto1 = colectivo.PagarCon(tarjeta);
            Assert.AreEqual(0, boleto1.TotalAbonado);

            Boleto boleto2 = colectivo.PagarCon(tarjeta);
            Assert.IsNotNull(boleto2);
            Assert.AreEqual(0, boleto2.TotalAbonado);
            Assert.AreEqual(0, tarjeta.ObtenerSaldo());
        }

        [Test]
        public void TestFranquiciaCompletaNoPermiteMasDeDosViajesGratuitos()
        {
            TiempoFalso tiempoFalso = new TiempoFalso();
            tiempoFalso.AgregarMinutos(360);
            Colectivo colectivo = new Colectivo("202", tiempoFalso);
            FranquiciaCompleta tarjeta = new FranquiciaCompleta();
            tarjeta.Cargar(5000);

            Boleto boleto1 = colectivo.PagarCon(tarjeta);
            Assert.IsNotNull(boleto1);
            Assert.AreEqual(0, boleto1.TotalAbonado);
            Assert.AreEqual(5000, tarjeta.ObtenerSaldo());

            Boleto boleto2 = colectivo.PagarCon(tarjeta);
            Assert.IsNotNull(boleto2);
            Assert.AreEqual(0, boleto2.TotalAbonado);
            Assert.AreEqual(5000, tarjeta.ObtenerSaldo());

            Boleto boleto3 = colectivo.PagarCon(tarjeta);
            Assert.IsNotNull(boleto3);
            Assert.AreEqual(1580, boleto3.TotalAbonado);
            Assert.AreEqual(3420, tarjeta.ObtenerSaldo());
        }

        [Test]
        public void TestFranquiciaCompletaViajesPosterioresAlSegundoSeCobran()
        {
            TiempoFalso tiempoFalso = new TiempoFalso();
            tiempoFalso.AgregarMinutos(360); 
            Colectivo colectivo = new Colectivo("202", tiempoFalso);
            FranquiciaCompleta tarjeta = new FranquiciaCompleta();
            tarjeta.Cargar(10000);

            int saldoInicial = tarjeta.ObtenerSaldo();

            Boleto boleto1 = colectivo.PagarCon(tarjeta);
            Assert.AreEqual(0, boleto1.TotalAbonado);
            Assert.AreEqual(saldoInicial, tarjeta.ObtenerSaldo());

            Boleto boleto2 = colectivo.PagarCon(tarjeta);
            Assert.AreEqual(0, boleto2.TotalAbonado);
            Assert.AreEqual(saldoInicial, tarjeta.ObtenerSaldo());

            Boleto boleto3 = colectivo.PagarCon(tarjeta);
            Assert.AreEqual(1580, boleto3.TotalAbonado);
            Assert.AreEqual(saldoInicial - 1580, tarjeta.ObtenerSaldo());

            Boleto boleto4 = colectivo.PagarCon(tarjeta);
            Assert.AreEqual(1580, boleto4.TotalAbonado);
            Assert.AreEqual(saldoInicial - 1580 - 1580, tarjeta.ObtenerSaldo());
        }

        [Test]
        public void TestFranquiciaCompletaReiniciaContadorAlDiaSiguiente()
        {
            TiempoFalso tiempoFalso = new TiempoFalso();
            tiempoFalso.AgregarMinutos(360); 
            Colectivo colectivo = new Colectivo("202", tiempoFalso);
            FranquiciaCompleta tarjeta = new FranquiciaCompleta();
            tarjeta.Cargar(10000);

            Boleto boleto1 = colectivo.PagarCon(tarjeta);
            Assert.AreEqual(0, boleto1.TotalAbonado);

            Boleto boleto2 = colectivo.PagarCon(tarjeta);
            Assert.AreEqual(0, boleto2.TotalAbonado);

            Boleto boleto3 = colectivo.PagarCon(tarjeta);
            Assert.AreEqual(1580, boleto3.TotalAbonado);
            int saldoDespuesViaje3 = tarjeta.ObtenerSaldo();

            tiempoFalso.AgregarDias(1);

            Boleto boleto4 = colectivo.PagarCon(tarjeta);
            Assert.IsNotNull(boleto4);
            Assert.AreEqual(0, boleto4.TotalAbonado);
            Assert.AreEqual(saldoDespuesViaje3, tarjeta.ObtenerSaldo());
        }

        [Test]
        public void TestFranquiciaCompletaSinSaldoNoPermiteTercerViaje()
        {
            TiempoFalso tiempoFalso = new TiempoFalso();
            tiempoFalso.AgregarMinutos(360);
            Colectivo colectivo = new Colectivo("202", tiempoFalso);
            FranquiciaCompleta tarjeta = new FranquiciaCompleta();

            Boleto boleto1 = colectivo.PagarCon(tarjeta);
            Assert.IsNotNull(boleto1);
            Assert.AreEqual(0, boleto1.TotalAbonado);

            Boleto boleto2 = colectivo.PagarCon(tarjeta);
            Assert.IsNotNull(boleto2);
            Assert.AreEqual(0, boleto2.TotalAbonado);

            Boleto boleto3 = colectivo.PagarCon(tarjeta);
            Assert.IsNull(boleto3);
        }

        [Test]
        public void TestFranquiciaCompletaTercerViajePagaTarifaCompleta()
        {
            TiempoFalso tiempoFalso = new TiempoFalso();
            tiempoFalso.AgregarMinutos(360); 
            Colectivo colectivo = new Colectivo("202", tiempoFalso);
            FranquiciaCompleta tarjeta = new FranquiciaCompleta();
            tarjeta.Cargar(5000);

            colectivo.PagarCon(tarjeta);
            colectivo.PagarCon(tarjeta);

            Boleto boleto3 = colectivo.PagarCon(tarjeta);

            Assert.IsNotNull(boleto3);
            Assert.AreEqual("FranquiciaCompleta", boleto3.TipoTarjeta);
            Assert.AreEqual(1580, boleto3.TotalAbonado);
            Assert.AreEqual(3420, boleto3.SaldoRestante);
        }

        [Test]
        public void TestFranquiciaCompletaTieneIdUnico()
        {
            FranquiciaCompleta tarjeta1 = new FranquiciaCompleta();
            FranquiciaCompleta tarjeta2 = new FranquiciaCompleta();

            Assert.AreNotEqual(tarjeta1.Id, tarjeta2.Id);
        }

        [Test]
        public void TestFranquiciaCompletaCuartoViajeDelMismoDiaCobrado()
        {
            TiempoFalso tiempoFalso = new TiempoFalso();
            tiempoFalso.AgregarMinutos(360); 
            Colectivo colectivo = new Colectivo("202", tiempoFalso);
            FranquiciaCompleta tarjeta = new FranquiciaCompleta();
            tarjeta.Cargar(10000);

            Boleto boleto1 = colectivo.PagarCon(tarjeta);
            Assert.AreEqual(0, boleto1.TotalAbonado);

            Boleto boleto2 = colectivo.PagarCon(tarjeta);
            Assert.AreEqual(0, boleto2.TotalAbonado);

            Boleto boleto3 = colectivo.PagarCon(tarjeta);
            Assert.AreEqual(1580, boleto3.TotalAbonado);

            Boleto boleto4 = colectivo.PagarCon(tarjeta);
            Assert.AreEqual(1580, boleto4.TotalAbonado);
            Assert.AreEqual(10000 - 1580 - 1580, tarjeta.ObtenerSaldo());
        }

        [Test]
        public void TestFranquiciaCompletaDespuesDeDosDiasSigueTeniendoDosViajesGratis()
        {
            TiempoFalso tiempoFalso = new TiempoFalso();
            tiempoFalso.AgregarMinutos(360); 
            Colectivo colectivo = new Colectivo("202", tiempoFalso);
            FranquiciaCompleta tarjeta = new FranquiciaCompleta();
            tarjeta.Cargar(10000);

            colectivo.PagarCon(tarjeta);
            colectivo.PagarCon(tarjeta);
            colectivo.PagarCon(tarjeta);

            tiempoFalso.AgregarDias(1);
            Boleto boletoD2_1 = colectivo.PagarCon(tarjeta);
            Assert.AreEqual(0, boletoD2_1.TotalAbonado);

            Boleto boletoD2_2 = colectivo.PagarCon(tarjeta);
            Assert.AreEqual(0, boletoD2_2.TotalAbonado);

            Boleto boletoD2_3 = colectivo.PagarCon(tarjeta);
            Assert.AreEqual(1580, boletoD2_3.TotalAbonado);
        }
    }
}