using WebNegocio.Interfaces;

public class FileService : IFileService
{
    public Task<List<string>> GetImageFilesFromCarruselFolderAsync()
    {
        // Asumiendo que las imágenes están en wwwroot/img/carrusel
        var imagesList = new List<string>
        {
            "/img/carrusel/imagen1.jpg",
            "/img/carrusel/imagen2.jpg",
            "/img/carrusel/imagen3.jpg",
            "/img/carrusel/imagen4.jpg",
            "/img/carrusel/imagen5.jpg",
            "/img/carrusel/imagen6.jpg",
            "/img/carrusel/imagen7.jpg",
            "/img/carrusel/imagen8.jpg",
        };

        // Log de las imágenes
        Console.WriteLine("Imágenes cargadas: " + string.Join(", ", imagesList));

        return Task.FromResult(imagesList);
    }
}
