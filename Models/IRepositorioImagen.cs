namespace AnaYAntonio_ProyectoInmobiliaria.Models
{
    public interface IRepositorioImagen
    {
        int Alta(Imagen imagen);
        int Baja(int id);
        int Modificacion(Imagen imagen);
        IList<Imagen> ObtenerLista();
        int ObtenerCantidad();
        Imagen ObtenerPorId(int id);
        IList<Imagen> BuscarPorInmueble(int inmuebleId);
    }
}