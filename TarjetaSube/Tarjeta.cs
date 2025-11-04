using System;

namespace TarjetaSube
{
    public class Tarjeta
    {
        private static int contadorId = 0;
        public int Id { get; private set; }
        protected int saldo;
        private const int LIMITE_SALDO = 56000;
        private const int LIMITE_NEGATIVO = -1200;
        private int saldoPendiente;
        private int[] CARGAS_VALIDAS = { 2000, 3000, 4000, 5000, 8000, 10000, 15000, 20000, 25000, 30000 };
        protected const int TARIFA_PASAJE = 1580;

        public int Saldo => saldo;
        public int SaldoPendiente => saldoPendiente;


        public Tarjeta()
        {
            Id = ++contadorId;
            saldo = 0;
            saldoPendiente = 0;
        }

        public int ObtenerSaldo()
        {
            return saldo;
        }

        public bool Cargar(int monto)
        {
            bool valido = false;
            foreach (var carga in CARGAS_VALIDAS)
            {
                if (monto == carga)
                {
                    valido = true;
                    break;
                }
            }

            if (!valido) return false;

            int nuevoSaldo = saldo + monto;

            if (nuevoSaldo > LIMITE_SALDO)
            {
                saldoPendiente += nuevoSaldo - LIMITE_SALDO;
                saldo = LIMITE_SALDO;
            }
            else
            {
                saldo = nuevoSaldo;
            }

            return true;
        }

        public void AcreditarCarga()
        {
            if (saldoPendiente > 0 && saldo < LIMITE_SALDO)
            {
                int espacioDisponible = LIMITE_SALDO - saldo;
                int montoAcreditado = Math.Min(espacioDisponible, saldoPendiente);

                saldo += montoAcreditado;
                saldoPendiente -= montoAcreditado;
            }
        }

        public virtual bool Descontar(int monto)
        {
            if (saldo - monto < LIMITE_NEGATIVO)
            {
                return false;
            }

            saldo -= monto;
            AcreditarCarga();
            return true;
        }

        public virtual bool Pagar()
        {
            return Descontar(TARIFA_PASAJE);
        }

        public virtual bool PuedeViajar(Tiempo tiempo)
        {
            return true;
        }

        public virtual void RegistrarViaje(Tiempo tiempo)
        {
        }
    }

    public class MedioBoleto : Tarjeta
    {
        private DateTime? ultimoViaje;
        private DateTime? inicioDelDia;
        private int viajesHoy;
        private const int MINUTOS_ENTRE_VIAJES = 5;
        private const int MAX_VIAJES_CON_DESCUENTO = 2;

        public override bool Descontar(int monto)
        {
            int mitad = monto / 2;
            if (saldo - mitad < -1200) return false;
            saldo -= mitad;
            AcreditarCarga();
            return true;
        }

        public override bool Pagar()
        {
            return Descontar(TARIFA_PASAJE);
        }

        public override bool PuedeViajar(Tiempo tiempo)
        {
            DateTime ahora = tiempo.Now();

            if (ultimoViaje.HasValue)
            {
                TimeSpan diferencia = ahora - ultimoViaje.Value;
                if (diferencia.TotalMinutes < MINUTOS_ENTRE_VIAJES)
                {
                    return false;
                }
            }

            if (!inicioDelDia.HasValue || ahora.Date != inicioDelDia.Value.Date)
            {
                inicioDelDia = ahora;
                viajesHoy = 0;
            }

            return true;
        }

        public override void RegistrarViaje(Tiempo tiempo)
        {
            DateTime ahora = tiempo.Now();
            ultimoViaje = ahora;

            if (!inicioDelDia.HasValue || ahora.Date != inicioDelDia.Value.Date)
            {
                inicioDelDia = ahora;
                viajesHoy = 0;
            }

            viajesHoy++;
        }

        public int ObtenerTarifaActual()
        {
            if (viajesHoy >= MAX_VIAJES_CON_DESCUENTO)
            {
                return TARIFA_PASAJE;
            }
            return TARIFA_PASAJE / 2;
        }

        public bool DescontarSegunViajes()
        {
            int tarifa = ObtenerTarifaActual();
            if (saldo - tarifa < -1200) return false;
            saldo -= tarifa;
            AcreditarCarga();
            return true;
        }
    }

    public class BoletoGratuito : Tarjeta
    {
        private DateTime? inicioDelDia;
        private int viajesHoy;
        private const int MAX_VIAJES_GRATUITOS = 2;

        public override bool Descontar(int monto) => true;
        public override bool Pagar() => true;

        public override void RegistrarViaje(Tiempo tiempo)
        {
            DateTime ahora = tiempo.Now();

            if (!inicioDelDia.HasValue || ahora.Date != inicioDelDia.Value.Date)
            {
                inicioDelDia = ahora;
                viajesHoy = 0;
            }

            viajesHoy++;
        }

        public int ObtenerTarifaActual()
        {
            if (viajesHoy >= MAX_VIAJES_GRATUITOS)
                return TARIFA_PASAJE;
            return 0;
        }

        public bool DescontarSegunViajes(Tiempo tiempo)
        {
            DateTime ahora = tiempo.Now();

            if (!inicioDelDia.HasValue || ahora.Date != inicioDelDia.Value.Date)
            {
                inicioDelDia = ahora;
                viajesHoy = 0;
            }

            int tarifa = ObtenerTarifaActual();

            if (tarifa == 0)
                return true;

            if (saldo - tarifa < -1200)
                return false;

            saldo -= tarifa;
            AcreditarCarga();
            return true;
        }
    }

    public class FranquiciaCompleta : Tarjeta
    {
        private DateTime? inicioDelDia;
        private int viajesHoy;
        private const int MAX_VIAJES_GRATUITOS = 2;

        public override bool Descontar(int monto) => true;
        public override bool Pagar() => true;

        public override void RegistrarViaje(Tiempo tiempo)
        {
            DateTime ahora = tiempo.Now();

            if (!inicioDelDia.HasValue || ahora.Date != inicioDelDia.Value.Date)
            {
                inicioDelDia = ahora;
                viajesHoy = 0;
            }

            viajesHoy++;
        }

        public int ObtenerTarifaActual()
        {
            if (viajesHoy >= MAX_VIAJES_GRATUITOS)
                return TARIFA_PASAJE;
            return 0;
        }

        public bool DescontarSegunViajes(Tiempo tiempo)
        {
            DateTime ahora = tiempo.Now();

            if (!inicioDelDia.HasValue || ahora.Date != inicioDelDia.Value.Date)
            {
                inicioDelDia = ahora;
                viajesHoy = 0;
            }

            int tarifa = ObtenerTarifaActual();

            if (tarifa == 0)
                return true;

            if (saldo - tarifa < -1200)
                return false;

            saldo -= tarifa;
            AcreditarCarga();
            return true;
        }
    }
}
