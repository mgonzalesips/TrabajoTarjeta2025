namespace TrabajoTarjeta
{
    public class BoletoGratuito : Tarjeta
    {
        public override decimal CalcularTarifa(decimal tarifaBase)
        {
            return 0;
        }

        public override bool PuedeDescontar(decimal monto)
        {
            // Siempre puede descontar porque la tarifa es 0
            return true;
        }
    }
}