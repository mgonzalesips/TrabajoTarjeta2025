using NUnit.Framework;
using System;
using System.Threading;
using TarjetaSube;


[TestFixture]
public class MedioBoletoLimitesTests
{
    [Test]
    public void MedioBoleto_NoPermiteDosViajesEnMenosDe5Segundos()
    {
        var medioBoleto = new MedioBoletoEstudiantil();
        medioBoleto.Cargar(5000m);
        var colectivo = new Colectivo("132");

        Console.WriteLine("=== PRIMER VIAJE ===");
        var boleto1 = colectivo.PagarCon(medioBoleto);
        medioBoleto.DebugInfo();

        Console.WriteLine("=== SEGUNDO VIAJE INMEDIATO ===");
        var boleto2 = colectivo.PagarCon(medioBoleto);
        medioBoleto.DebugInfo();

        Console.WriteLine($"Boleto1 válido: {boleto1.EsValido}");
        Console.WriteLine($"Boleto2 válido: {boleto2.EsValido}");

        Assert.IsTrue(boleto1.EsValido);
        Assert.IsFalse(boleto2.EsValido, "No debe permitir segundo viaje en menos de 5 segundos");
    }

    [Test]
    public void MedioBoleto_NoPermiteMasDeDosViajesPorDia()
    {
        var medioBoleto = new MedioBoletoEstudiantil();
        medioBoleto.Cargar(10000m);
        var colectivo = new Colectivo("132");

        // Primer viaje (medio boleto)
        var boleto1 = colectivo.PagarCon(medioBoleto);
        Assert.IsTrue(boleto1.EsValido);
        Assert.AreEqual(790m, boleto1.Monto);

        // Esperar 6 segundos
        Thread.Sleep(6000);

        // Segundo viaje (medio boleto)
        var boleto2 = colectivo.PagarCon(medioBoleto);
        Assert.IsTrue(boleto2.EsValido);
        Assert.AreEqual(790m, boleto2.Monto);

        // Esperar otros 6 segundos
        Thread.Sleep(6000);

        // Tercer viaje (debe ser BLOQUEADO - no permitido)
        var boleto3 = colectivo.PagarCon(medioBoleto);
        Assert.IsFalse(boleto3.EsValido, "Tercer viaje debe ser bloqueado - límite de 2 viajes por día");
    }

    [Test]
    public void MedioBoleto_ComportamientoCorrecto()
    {
        var medioBoleto = new MedioBoletoEstudiantil();
        medioBoleto.Cargar(5000m);
        var colectivo = new Colectivo("132");

        // Primer viaje con medio boleto
        var boleto1 = colectivo.PagarCon(medioBoleto);
        Assert.IsTrue(boleto1.EsValido);
        Assert.AreEqual(790m, boleto1.Monto);
        Assert.AreEqual(4210m, medioBoleto.Saldo);

        // Intentar segundo viaje inmediatamente - DEBE FALLAR (menos de 5 segundos)
        var boleto2Inmediato = colectivo.PagarCon(medioBoleto);
        Assert.IsFalse(boleto2Inmediato.EsValido, "Segundo viaje inmediato debe fallar");
        Assert.AreEqual(4210m, medioBoleto.Saldo, "Saldo no debe cambiar");

        // Esperar 6 SEGUNDOS (más de 5 segundos)
        Thread.Sleep(6000);

        // Segundo viaje después de espera - DEBE SER MEDIO BOLETO
        var boleto2Espera = colectivo.PagarCon(medioBoleto);
        Assert.IsTrue(boleto2Espera.EsValido, "Segundo viaje después de espera debe ser válido");
        Assert.AreEqual(790m, boleto2Espera.Monto, "Segundo viaje debe ser medio boleto");
        Assert.AreEqual(3420m, medioBoleto.Saldo);

        // Esperar otros 6 SEGUNDOS
        Thread.Sleep(6000);

        // Tercer viaje - DEBE SER BLOQUEADO (límite de 2 viajes por día)
        var boleto3 = colectivo.PagarCon(medioBoleto);
        Assert.IsFalse(boleto3.EsValido, "Tercer viaje debe ser bloqueado - límite de 2 viajes por día");
    }

    [Test]
    public void MedioBoleto_VerificacionRapida()
    {
        var medioBoleto = new MedioBoletoEstudiantil();
        medioBoleto.Cargar(5000m);

        // Verificar que el primer viaje es medio boleto
        Assert.AreEqual(790m, medioBoleto.CalcularMontoPasaje(1580m));

        // Verificar que puede pagar el primer viaje
        Assert.IsTrue(medioBoleto.PuedePagar(1580m));

        
    }
}

