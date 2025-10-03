namespace CaseStudyApi.Presentation.Interfaces
{
    public interface IFileService
    {
        Task DeleteAsync(string path, string fileName);
        List<string> GetFiles(string path);
        bool HasFile(string path, string fileName);
        Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string path, IFormFileCollection files, int id);
    }
}
