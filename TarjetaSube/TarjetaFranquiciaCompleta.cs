public class TarjetaFranquiciaCompleta : Tarjeta
{
    public override bool PagarPasaje()
    {
        return true;
    }

    public override decimal ObtenerTarifa()
    {
        return 0m;
    }
}
