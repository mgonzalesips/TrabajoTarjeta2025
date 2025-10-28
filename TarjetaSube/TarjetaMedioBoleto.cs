namespace TarjetaSube
{
    public class TarjetaMedioBoleto : Tarjeta
    {
        public TarjetaMedioBoleto(int saldo = 0) : base(saldo)
        {
        }

        public new bool PagarConTarifa(int tarifa)
        {
            int tarifaConDescuento = tarifa / 2;
            return base.PagarConTarifa(tarifaConDescuento);
        }
    }
}