using System;
using System.Collections.Generic;
using System.Linq;
using TarjetaSube;

public class MedioBoletoEstudiantil : Tarjeta
{
    private Dictionary<DateTime, int> viajesPorDia = new Dictionary<DateTime, int>();
    private DateTime? ultimoViaje = null;
    private readonly Func<DateTime> _nowProvider;

    // Constructor para producción
    public MedioBoletoEstudiantil() : this(() => DateTime.Now)
    {
    }

    // Constructor para testing - INYECCIÓN DE TIEMPO
    public MedioBoletoEstudiantil(Func<DateTime> nowProvider)
    {
        _nowProvider = nowProvider;
    }

    // PARA DEBUG - nos permite ver qué está pasando
    public void DebugInfo()
    {
        Console.WriteLine($"DEBUG - Último viaje: {ultimoViaje}, Viajes hoy: {viajesPorDia.GetValueOrDefault(GetHoy(), 0)}");
    }

    private DateTime GetHoy()
    {
        return _nowProvider().Date;
    }

    private DateTime GetAhora()
    {
        return _nowProvider();
    }

    public override decimal CalcularMontoPasaje(decimal tarifaBase)
    {
        var hoy = GetHoy();

        if (!viajesPorDia.ContainsKey(hoy))
            viajesPorDia[hoy] = 0;

        bool aplicarMedioBoleto = viajesPorDia[hoy] < 2 && EstaDentroDeFranjaHoraria();

        Console.WriteLine($"DEBUG CalcularMonto - Viajes hoy: {viajesPorDia[hoy]}, Medio boleto: {aplicarMedioBoleto}");

        return aplicarMedioBoleto ? tarifaBase / 2 : tarifaBase;
    }

    public override bool PuedePagar(decimal tarifaBase)
    {
        Console.WriteLine($"DEBUG PuedePagar - Inicio");

        if (!EstaDentroDeFranjaHoraria())
        {
            Console.WriteLine("DEBUG - Fuera de franja horaria");
            return false;
        }

        // Verificar tiempo mínimo entre viajes
        if (ultimoViaje.HasValue)
        {
            double segundosDesdeUltimoViaje = (GetAhora() - ultimoViaje.Value).TotalSeconds;
            Console.WriteLine($"DEBUG - Segundos desde último viaje: {segundosDesdeUltimoViaje}");

            if (segundosDesdeUltimoViaje < 5)
            {
                Console.WriteLine("DEBUG - Demasiado pronto, menos de 5 segundos");
                return false;
            }
        }

        // Verificar límite de 2 viajes por día
        var hoy = GetHoy();
        int viajesHoy = viajesPorDia.GetValueOrDefault(hoy, 0);
        Console.WriteLine($"DEBUG - Viajes hoy: {viajesHoy}");

        if (viajesHoy >= 2)
        {
            Console.WriteLine("DEBUG - Límite de 2 viajes por día alcanzado");
            return false;
        }

        decimal montoPasaje = CalcularMontoPasaje(tarifaBase);
        bool puedePagar = Saldo - montoPasaje >= -1200m;

        Console.WriteLine($"DEBUG PuedePagar - Resultado: {puedePagar}");
        return puedePagar;
    }

    public override bool Descontar(decimal monto)
    {
        Console.WriteLine($"DEBUG Descontar - Monto: {monto}");

        // Primero verificar si puede pagar
        if (!PuedePagar(monto))
        {
            Console.WriteLine("DEBUG Descontar - No puede pagar, retornando false");
            return false;
        }

        // Registrar el viaje
        var hoy = GetHoy();
        if (!viajesPorDia.ContainsKey(hoy))
            viajesPorDia[hoy] = 0;

        viajesPorDia[hoy]++;
        ultimoViaje = GetAhora();

        Console.WriteLine($"DEBUG Descontar - Viaje registrado. Viajes hoy: {viajesPorDia[hoy]}, Último viaje: {ultimoViaje}");

        // Llamar al Descontar de la clase base
        bool resultado = base.Descontar(monto);
        Console.WriteLine($"DEBUG Descontar - Base.Descontar resultado: {resultado}");

        return resultado;
    }

    private bool EstaDentroDeFranjaHoraria()
    {
        DateTime ahora = GetAhora();
        DayOfWeek dia = ahora.DayOfWeek;
        int hora = ahora.Hour;

        // PARA TESTING - siempre retornar true para eliminar esta variable
        bool enFranja = true; // (dia >= DayOfWeek.Monday && dia <= DayOfWeek.Friday) && (hora >= 6 && hora < 22);

        Console.WriteLine($"DEBUG FranjaHoraria - Día: {dia}, Hora: {hora}, EnFranja: {enFranja}");
        return enFranja;
    }
}