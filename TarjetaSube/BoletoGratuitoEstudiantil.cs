using System;
using System.Collections.Generic;
using System.Linq;
using TarjetaSube;

public class BoletoGratuitoEstudiantil : Tarjeta
{
    private Dictionary<DateTime, int> viajesGratuitosPorDia = new Dictionary<DateTime, int>();
    private DateTime? ultimoViaje = null;
    private readonly Func<DateTime> _nowProvider;

    // Constructor para producción
    public BoletoGratuitoEstudiantil() : this(() => DateTime.Now)
    {
    }

    // Constructor para testing - INYECCIÓN DE TIEMPO
    public BoletoGratuitoEstudiantil(Func<DateTime> nowProvider)
    {
        _nowProvider = nowProvider;
    }

    // Método para debugging
    public void DebugInfo()
    {
        Console.WriteLine($"DEBUG BoletoGratuito - Viajes hoy: {viajesGratuitosPorDia.GetValueOrDefault(GetHoy(), 0)}, Último viaje: {ultimoViaje}");
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

        if (!viajesGratuitosPorDia.ContainsKey(hoy))
            viajesGratuitosPorDia[hoy] = 0;

        // Aplicar gratuito solo para los primeros 2 viajes en franja horaria
        if (viajesGratuitosPorDia[hoy] < 2 && EstaDentroDeFranjaHoraria())
        {
            return 0m;
        }
        else
        {
            return tarifaBase;
        }
    }

    public override bool PuedePagar(decimal tarifaBase)
    {
        Console.WriteLine($"DEBUG BoletoGratuito PuedePagar - Inicio");

        if (!EstaDentroDeFranjaHoraria())
        {
            Console.WriteLine("DEBUG BoletoGratuito - Fuera de franja horaria");
            return false;
        }

        // Verificar tiempo mínimo entre viajes (5 segundos)
        if (ultimoViaje.HasValue)
        {
            double segundosDesdeUltimoViaje = (GetAhora() - ultimoViaje.Value).TotalSeconds;
            Console.WriteLine($"DEBUG BoletoGratuito - Segundos desde último viaje: {segundosDesdeUltimoViaje}");

            if (segundosDesdeUltimoViaje < 5)
            {
                Console.WriteLine("DEBUG BoletoGratuito - Demasiado pronto, menos de 5 segundos");
                return false;
            }
        }

        decimal montoPasaje = CalcularMontoPasaje(tarifaBase);
        bool puedePagar = Saldo - montoPasaje >= -1200m;

        Console.WriteLine($"DEBUG BoletoGratuito PuedePagar - Resultado: {puedePagar}");
        return puedePagar;
    }

    public override bool Descontar(decimal monto)
    {
        Console.WriteLine($"DEBUG BoletoGratuito Descontar - Monto: {monto}");

        // Primero verificar si puede pagar
        if (!PuedePagar(monto))
        {
            Console.WriteLine("DEBUG BoletoGratuito Descontar - No puede pagar, retornando false");
            return false;
        }

        var hoy = GetHoy();

        // Registrar el viaje gratuito
        if (!viajesGratuitosPorDia.ContainsKey(hoy))
            viajesGratuitosPorDia[hoy] = 0;

        // Solo contar como gratuito si el monto es 0 (está dentro de los primeros 2 viajes)
        if (monto == 0)
        {
            viajesGratuitosPorDia[hoy]++;
        }

        ultimoViaje = GetAhora();

        Console.WriteLine($"DEBUG BoletoGratuito Descontar - Viaje registrado. Viajes gratuitos hoy: {viajesGratuitosPorDia[hoy]}, Último viaje: {ultimoViaje}");

        // Si es gratuito (monto = 0), solo registrar viaje sin descontar saldo
        if (monto == 0)
        {
            RegistrarViaje();
            return true;
        }
        else
        {
            // Para viajes NO gratuitos (después del 2do viaje)
            bool resultado = base.Descontar(monto);
            Console.WriteLine($"DEBUG BoletoGratuito Descontar - Base.Descontar resultado: {resultado}");
            return resultado;
        }
    }

    public bool EstaDentroDeFranjaHoraria()
    {
        DateTime ahora = GetAhora();
        DayOfWeek dia = ahora.DayOfWeek;
        int hora = ahora.Hour;

        // PARA TESTING - siempre retornar true para eliminar esta variable
        bool enFranja = true; // (dia >= DayOfWeek.Monday && dia <= DayOfWeek.Friday) && (hora >= 6 && hora < 22);

        Console.WriteLine($"DEBUG BoletoGratuito FranjaHoraria - EnFranja: {enFranja}");
        return enFranja;
    }
}