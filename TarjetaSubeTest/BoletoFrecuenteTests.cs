using NUnit.Framework;
using System;
using TarjetaSube;

[TestFixture]
public class BoletoFrecuenteTests_v2  // Cambia el nombre aquí
{
    [Test]
    public void TarjetaComun_CalcularDescuento_Primeros29Viajes_SinDescuento()
    {
        var tarjeta = new TarjetaComun();

        for (int i = 0; i < 29; i++)
        {
            tarjeta.RegistrarViajeParaTest();
        }

        decimal descuento = tarjeta.CalcularDescuentoTarjetaComun(1580m);
        Assert.AreEqual(0m, descuento);
    }

    [Test]
    public void TarjetaComun_CalcularDescuento_Viaje30a59_20PorcientoDescuento()
    {
        var tarjeta = new TarjetaComun();

        for (int i = 0; i < 35; i++)
        {
            tarjeta.RegistrarViajeParaTest();
        }

        decimal descuento = tarjeta.CalcularDescuentoTarjetaComun(1580m);
        Assert.AreEqual(316m, descuento);
    }

    [Test]
    public void TarjetaComun_CalcularMontoPasaje_ConDescuento()
    {
        var tarjeta = new TarjetaComun();
        tarjeta.Cargar(10000m);

        for (int i = 0; i < 40; i++)
        {
            tarjeta.RegistrarViajeParaTest();
        }

        decimal monto = tarjeta.CalcularMontoPasaje(1580m);
        Assert.AreEqual(1264m, monto);
    }
}