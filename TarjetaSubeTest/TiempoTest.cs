using NUnit.Framework;
using System;
using TarjetaSube;

namespace TarjetaSubeTest
{
    [TestFixture]
    public class TiempoTest
    {
        [Test]
        public void TestTiempoRetornaFechaActual()
        {
            Tiempo tiempo = new Tiempo();
            DateTime ahora = tiempo.Now();

            Assert.IsTrue(ahora <= DateTime.Now);
            Assert.IsTrue(ahora >= DateTime.Now.AddSeconds(-1));
        }

        [Test]
        public void TestTiempoFalsoIniciaEn14Octubre2024()
        {
            TiempoFalso tiempoFalso = new TiempoFalso();
            DateTime fecha = tiempoFalso.Now();

            Assert.AreEqual(2024, fecha.Year);
            Assert.AreEqual(10, fecha.Month);
            Assert.AreEqual(14, fecha.Day);
            Assert.AreEqual(0, fecha.Hour);
            Assert.AreEqual(0, fecha.Minute);
            Assert.AreEqual(0, fecha.Second);
        }

        [Test]
        public void TestTiempoFalsoAgregarDias()
        {
            TiempoFalso tiempoFalso = new TiempoFalso();

            tiempoFalso.AgregarDias(1);
            DateTime fecha = tiempoFalso.Now();

            Assert.AreEqual(15, fecha.Day);
            Assert.AreEqual(10, fecha.Month);
            Assert.AreEqual(2024, fecha.Year);
        }

        [Test]
        public void TestTiempoFalsoAgregarMinutos()
        {
            TiempoFalso tiempoFalso = new TiempoFalso();

            tiempoFalso.AgregarMinutos(30);
            DateTime fecha = tiempoFalso.Now();

            Assert.AreEqual(0, fecha.Hour);
            Assert.AreEqual(30, fecha.Minute);
        }

        [Test]
        public void TestTiempoFalsoAgregarMinutosCambiaDia()
        {
            TiempoFalso tiempoFalso = new TiempoFalso();

            tiempoFalso.AgregarMinutos(1440);
            DateTime fecha = tiempoFalso.Now();

            Assert.AreEqual(15, fecha.Day);
        }

        [Test]
        public void TestTiempoFalsoAgregarDiasYMinutos()
        {
            TiempoFalso tiempoFalso = new TiempoFalso();

            tiempoFalso.AgregarDias(2);
            tiempoFalso.AgregarMinutos(125);

            DateTime fecha = tiempoFalso.Now();

            Assert.AreEqual(16, fecha.Day);
            Assert.AreEqual(10, fecha.Month);
            Assert.AreEqual(2, fecha.Hour);
            Assert.AreEqual(5, fecha.Minute);
        }
    }
}