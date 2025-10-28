using TarjetaSube;
using NUnit.Framework;
using System;

namespace TarjetaSubeTest
{
    public class Tests
    {
        private Tarjeta t;
        private const int TARIFA_BASICA = 1580;

        [SetUp]
        public void Setup()
        {
            t = new Tarjeta();
        }

        //i1:
        [Test]
        public void CargaTest()
        {
            t.Cargar(100);
            t.Pagar();
            Assert.AreEqual(t.Saldo, 50);
        }

        //i2:

        [Test]
        public void CargaConMontoPermitido_DeberiaAceptar()
        {
            bool resultado = t.CargarConMontoValido(2000);
            Assert.IsTrue(resultado);
            Assert.AreEqual(2000, t.Saldo);
        }

        [Test]
        public void CargaConMontoNoPermitido_DeberiaRechazar()
        {
            bool resultado = t.CargarConMontoValido(1000);
            Assert.IsFalse(resultado);
            Assert.AreEqual(0, t.Saldo);
        }

        [Test]
        public void PagarConTarifa_ConSaldoSuficiente_DeberiaPermitir()
        {
            t.Cargar(2000);
            bool resultado = t.PagarConTarifa(TARIFA_BASICA);
            Assert.IsTrue(resultado);
            Assert.AreEqual(2000 - TARIFA_BASICA, t.Saldo);
        }

        [Test]
        public void PagarConTarifa_SaldoNegativoHastaLimite_DeberiaPermitir()
        {
            t.Cargar(TARIFA_BASICA);
            bool primerViaje = t.PagarConTarifa(TARIFA_BASICA);
            bool segundoViaje = t.PagarConTarifa(1200);
            bool tercerViaje = t.PagarConTarifa(100);
            
            Assert.IsTrue(primerViaje);
            Assert.IsTrue(segundoViaje);
            Assert.IsFalse(tercerViaje);
            Assert.AreEqual(-1200, t.Saldo);
        }

        [Test]
        public void CargarConSaldoNegativo_DeberiaDescontarSaldoNegativo()
        {
            t.Cargar(TARIFA_BASICA);
            t.PagarConTarifa(TARIFA_BASICA);
            t.PagarConTarifa(1000);
            
            bool cargaExitosa = t.CargarConMontoValido(3000);
            
            Assert.IsTrue(cargaExitosa);
            Assert.AreEqual(2000, t.Saldo);
        }

        [Test]
        public void TarjetaFranquiciaCompleta_SiemprePuedePagarBoleto()
        {
            var tarjetaGratuita = new TarjetaFranquiciaCompleta();
            var colectivo = new Colectivo(101, "Semtur");
            
            var boleto = colectivo.PagarCon(tarjetaGratuita);
            
            Assert.IsNotNull(boleto);
        }

        [Test]
        public void TarjetaMedioBoleto_DescuentaMitadDelPasaje()
        {
            var tarjetaMedio = new TarjetaMedioBoleto();
            tarjetaMedio.Cargar(2000);
            int saldoInicial = tarjetaMedio.Saldo;
            var colectivo = new Colectivo(101, "Semtur");
            
            var boleto = colectivo.PagarCon(tarjetaMedio);
            
            Assert.IsNotNull(boleto);
            int montoEsperado = saldoInicial - (TARIFA_BASICA / 2);
            Assert.AreEqual(montoEsperado, tarjetaMedio.Saldo);
        }
    }
}