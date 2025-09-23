namespace TrabajoTarjeta
{
    public class FranquiciaCompleta : Tarjeta
    {
        public override decimal CalcularTarifa(decimal tarifaBase)
        {
            return 0;
        }

        public override bool PuedeDescontar(decimal monto)
        {
            // Franquicia completa siempre puede pagar (tarifa = 0)
            return true;
        }
    }
}