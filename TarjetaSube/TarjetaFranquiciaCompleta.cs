namespace TarjetaSube
{
    public class TarjetaFranquiciaCompleta : Tarjeta
    {
        public TarjetaFranquiciaCompleta(int saldo = 0) : base(saldo)
        {
        }

        public new bool TieneSaldoSuficiente(int tarifa)
        {
            return true;
        }

        public new bool PagarConTarifa(int tarifa)
        {
            return true;
        }
    }
}