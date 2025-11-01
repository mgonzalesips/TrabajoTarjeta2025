public class TarjetaMedioBoleto : Tarjeta
{
    private const decimal DESCUENTO = 0.5m;

    public override bool PagarPasaje()
    {
        return Descontar(ObtenerTarifa());
    }

    public override decimal ObtenerTarifa()
    {
        return base.ObtenerTarifa() * DESCUENTO;
    }
}
