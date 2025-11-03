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

            Boleto boleto = colectivo.PagarCon(tarjeta);

            Assert.IsNotNull(boleto);
            Assert.AreEqual(3420, tarjeta.ObtenerSaldo());
            Assert.AreEqual(1580, boleto.TotalAbonado);
            Assert.AreEqual(3420, boleto.SaldoRestante);
            Assert.AreEqual("120", boleto.LineaColectivo);
            Assert.AreEqual("Tarjeta", boleto.TipoTarjeta);
            Assert.AreEqual(tarjeta.Id, boleto.IdTarjeta);
        }

        [Test]
        public void TestPagarConTarjetaSinSaldo()
        {
            Colectivo colectivo = new Colectivo("102");
            Tarjeta tarjeta = new Tarjeta();

            Boleto boleto = colectivo.PagarCon(tarjeta);

            Assert.IsNull(boleto);
            Assert.AreEqual(0, tarjeta.ObtenerSaldo());
        }

        [Test]
        public void TestMultiplesPagos()
        {
            Colectivo colectivo = new Colectivo("115");
            Tarjeta tarjeta = new Tarjeta();
            tarjeta.Cargar(10000);

            Boleto boleto1 = colectivo.PagarCon(tarjeta);
            Assert.IsNotNull(boleto1);
            Assert.AreEqual(8420, tarjeta.ObtenerSaldo());

            Boleto boleto2 = colectivo.PagarCon(tarjeta);
            Assert.IsNotNull(boleto2);
            Assert.AreEqual(6840, tarjeta.ObtenerSaldo());
        }

        [Test]
        public void TestPagarConMedioBoleto()
        {
            TiempoFalso tiempoFalso = new TiempoFalso();
            Colectivo colectivo = new Colectivo("200", tiempoFalso);
            MedioBoleto tarjeta = new MedioBoleto();
            tarjeta.Cargar(2000);

            Boleto boleto = colectivo.PagarCon(tarjeta);

            Assert.IsNotNull(boleto);
            Assert.AreEqual(2000 - 1580 / 2, tarjeta.ObtenerSaldo());
            Assert.AreEqual(790, boleto.TotalAbonado);
            Assert.AreEqual(tarjeta.Id, boleto.IdTarjeta);
        }

        [Test]
        public void TestMedioBoletoNoPuedeViajarAntesDe5Minutos()
        {
            TiempoFalso tiempoFalso = new TiempoFalso();
            Colectivo colectivo = new Colectivo("200", tiempoFalso);
            MedioBoleto tarjeta = new MedioBoleto();
            tarjeta.Cargar(5000);

            // Primer viaje - debe ser exitoso
            Boleto boleto1 = colectivo.PagarCon(tarjeta);
            Assert.IsNotNull(boleto1);
            Assert.AreEqual(790, boleto1.TotalAbonado);

            // Intentar viajar inmediatamente (0 minutos después)
            Boleto boleto2 = colectivo.PagarCon(tarjeta);
            Assert.IsNull(boleto2); // No debe permitir el viaje

            // Avanzar 3 minutos (aún no son 5)
            tiempoFalso.AgregarMinutos(3);
            Boleto boleto3 = colectivo.PagarCon(tarjeta);
            Assert.IsNull(boleto3); // Todavía no debe permitir el viaje

            // Avanzar 2 minutos más (total 5 minutos)
            tiempoFalso.AgregarMinutos(2);
            Boleto boleto4 = colectivo.PagarCon(tarjeta);
            Assert.IsNotNull(boleto4); // Ahora sí debe permitir el viaje
            Assert.AreEqual(790, boleto4.TotalAbonado);
        }

        [Test]
        public void TestMedioBoletoNoPermiteMasDeDosViajesPorDia()
        {
            TiempoFalso tiempoFalso = new TiempoFalso();
            Colectivo colectivo = new Colectivo("200", tiempoFalso);
            MedioBoleto tarjeta = new MedioBoleto();
            tarjeta.Cargar(10000);

            // Primer viaje - medio boleto (790)
            Boleto boleto1 = colectivo.PagarCon(tarjeta);
            Assert.IsNotNull(boleto1);
            Assert.AreEqual(790, boleto1.TotalAbonado);
            int saldoDespuesViaje1 = tarjeta.ObtenerSaldo();

            // Avanzar 5 minutos
            tiempoFalso.AgregarMinutos(5);

            // Segundo viaje - medio boleto (790)
            Boleto boleto2 = colectivo.PagarCon(tarjeta);
            Assert.IsNotNull(boleto2);
            Assert.AreEqual(790, boleto2.TotalAbonado);
            int saldoDespuesViaje2 = tarjeta.ObtenerSaldo();

            // Avanzar 5 minutos más
            tiempoFalso.AgregarMinutos(5);

            // Tercer viaje - tarifa completa (1580)
            Boleto boleto3 = colectivo.PagarCon(tarjeta);
            Assert.IsNotNull(boleto3);
            Assert.AreEqual(1580, boleto3.TotalAbonado); // Ya no es medio boleto
            int saldoDespuesViaje3 = tarjeta.ObtenerSaldo();

            // Verificar que los descuentos fueron correctos
            Assert.AreEqual(saldoDespuesViaje1 - 790, saldoDespuesViaje2);
            Assert.AreEqual(saldoDespuesViaje2 - 1580, saldoDespuesViaje3);
        }

        [Test]
        public void TestMedioBoletoReiniciaContadorAlDiaSiguiente()
        {
            TiempoFalso tiempoFalso = new TiempoFalso();
            Colectivo colectivo = new Colectivo("200", tiempoFalso);
            MedioBoleto tarjeta = new MedioBoleto();
            tarjeta.Cargar(10000);

            // Dos viajes el primer día
            Boleto boleto1 = colectivo.PagarCon(tarjeta);
            Assert.AreEqual(790, boleto1.TotalAbonado);

            tiempoFalso.AgregarMinutos(5);
            Boleto boleto2 = colectivo.PagarCon(tarjeta);
            Assert.AreEqual(790, boleto2.TotalAbonado);

            // Tercer viaje del día - tarifa completa
            tiempoFalso.AgregarMinutos(5);
            Boleto boleto3 = colectivo.PagarCon(tarjeta);
            Assert.AreEqual(1580, boleto3.TotalAbonado);

            // Avanzar al día siguiente
            tiempoFalso.AgregarDias(1);

            // Primer viaje del nuevo día - debe ser medio boleto de nuevo
            Boleto boleto4 = colectivo.PagarCon(tarjeta);
            Assert.IsNotNull(boleto4);
            Assert.AreEqual(790, boleto4.TotalAbonado);
        }

        [Test]
        public void TestPagarConBoletoGratuito()
        {
            Colectivo colectivo = new Colectivo("201");
            BoletoGratuito tarjeta = new BoletoGratuito();

            Boleto boleto = colectivo.PagarCon(tarjeta);

            Assert.IsNotNull(boleto);
            Assert.AreEqual(0, tarjeta.ObtenerSaldo());
            Assert.AreEqual(0, boleto.TotalAbonado);
            Assert.AreEqual(tarjeta.Id, boleto.IdTarjeta);
        }

        [Test]
        public void TestPagarConFranquiciaCompleta()
        {
            Colectivo colectivo = new Colectivo("202");
            FranquiciaCompleta tarjeta = new FranquiciaCompleta();

            Boleto boleto = colectivo.PagarCon(tarjeta);

            Assert.IsNotNull(boleto);
            Assert.AreEqual(0, tarjeta.ObtenerSaldo());
            Assert.AreEqual(0, boleto.TotalAbonado);
            Assert.AreEqual(tarjeta.Id, boleto.IdTarjeta);
        }

        [Test]
        public void TestTipoTarjetaRegistradoEnBoleto()
        {
            TiempoFalso tiempoFalso = new TiempoFalso();
            Colectivo colectivo = new Colectivo("203", tiempoFalso);
            MedioBoleto tarjeta = new MedioBoleto();
            tarjeta.Cargar(2000);

            Boleto boleto = colectivo.PagarCon(tarjeta);

            Assert.IsNotNull(boleto);
            Assert.AreEqual("MedioBoleto", boleto.TipoTarjeta);
        }

        [Test]
        public void TestBoletoRetornadoContieneTodosLosDatos()
        {
            Colectivo colectivo = new Colectivo("150");
            Tarjeta tarjeta = new Tarjeta();
            tarjeta.Cargar(5000);

            Boleto boleto = colectivo.PagarCon(tarjeta);

            Assert.IsNotNull(boleto);
            Assert.AreEqual("Tarjeta", boleto.TipoTarjeta);
            Assert.AreEqual("150", boleto.LineaColectivo);
            Assert.AreEqual(1580, boleto.TotalAbonado);
            Assert.AreEqual(3420, boleto.SaldoRestante);
            Assert.AreEqual(tarjeta.Id, boleto.IdTarjeta);
            Assert.IsNotNull(boleto.Fecha);
        }
    }
}