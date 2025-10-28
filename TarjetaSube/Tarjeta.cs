using System;

namespace TarjetaSube
{
    public class Tarjeta
    {
        private int saldo;
        private const int SALDO_MAXIMO = 40000;
        private const int SALDO_NEGATIVO_MAXIMO = -1200;
        
        private int[] montosPermitidos = { 2000, 3000, 4000, 5000, 8000, 10000, 15000, 20000, 25000, 30000 };

        public Tarjeta(int saldo = 0)
        {
            this.saldo = saldo;
        }

        public int Saldo
        {
            get { return saldo; }
            set { saldo = value; }
        }
        
        public void Cargar(int importe)
        {
            saldo += importe;
        }

        // i2:nueva sobrecarga con validación de montos
        public bool CargarConMontoValido(int importe)
        {
            bool montoValido = false;
            foreach (int montoPermitido in montosPermitidos)
            {
                if (importe == montoPermitido)
                {
                    montoValido = true;
                    break;
                }
            }

            if (!montoValido)
                return false;

            //si hay saldo negativo, primero se descuenta
            if (saldo < 0)
            {
                int saldoARecuperar = Math.Min(importe, Math.Abs(saldo));
                saldo += saldoARecuperar;
                importe -= saldoARecuperar;
            }

            if (saldo + importe > SALDO_MAXIMO)
                return false;

            saldo += importe;
            return true;
        }

        public void Pagar()
        {
            saldo -= 50;
        }

        //i2: nuevo metodo para pagar con tarifa especifica
        public bool PagarConTarifa(int tarifa)
        {
            if (saldo - tarifa < SALDO_NEGATIVO_MAXIMO)
                return false;

            saldo -= tarifa;
            return true;
        }

        //i2:verificar si tiene saldo suficiente
        public bool TieneSaldoSuficiente(int tarifa)
        {
            return saldo - tarifa >= SALDO_NEGATIVO_MAXIMO;
        }
    }
}