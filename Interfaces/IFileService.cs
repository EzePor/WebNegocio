namespace WebNegocio.Interfaces
{
    public interface IFileService
    {
        Task<List<string>> GetImageFilesFromCarruselFolderAsync();
    }

}
