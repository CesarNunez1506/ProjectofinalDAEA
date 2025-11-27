using Domain.Interfaces.Services.Production;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.Services.Production;

/// <summary>
/// Servicio para almacenamiento de archivos en el sistema de archivos local
/// Gestiona imágenes de productos
/// </summary>
public class FileStorageService : IFileStorageService
{
    private readonly IHostEnvironment _environment;
    private const string UPLOAD_FOLDER = "uploads";

    public FileStorageService(IHostEnvironment environment)
    {
        _environment = environment;
    }

    public async Task<string> SaveFileAsync(byte[] fileContent, string fileName, string folder)
    {
        // Crear directorio si no existe
        var uploadPath = Path.Combine(_environment.ContentRootPath, "public", UPLOAD_FOLDER, folder);
        if (!Directory.Exists(uploadPath))
        {
            Directory.CreateDirectory(uploadPath);
        }

        // Generar nombre único para el archivo
        var fileExtension = Path.GetExtension(fileName);
        var uniqueFileName = $"{Guid.NewGuid()}{fileExtension}";
        var filePath = Path.Combine(uploadPath, uniqueFileName);

        // Guardar archivo
        await File.WriteAllBytesAsync(filePath, fileContent);

        // Retornar URL relativa
        return $"/{UPLOAD_FOLDER}/{folder}/{uniqueFileName}";
    }

    public Task<bool> DeleteFileAsync(string fileUrl)
    {
        try
        {
            // Convertir URL relativa a ruta física
            var filePath = Path.Combine(_environment.ContentRootPath, "public", fileUrl.TrimStart('/'));

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }
        catch
        {
            return Task.FromResult(false);
        }
    }

    public string GetAbsoluteUrl(string relativeUrl, string baseUrl)
    {
        if (string.IsNullOrEmpty(relativeUrl))
            return string.Empty;

        // Si ya es una URL absoluta, retornarla tal cual
        if (relativeUrl.StartsWith("http://") || relativeUrl.StartsWith("https://"))
            return relativeUrl;

        // Combinar base URL con URL relativa
        baseUrl = baseUrl.TrimEnd('/');
        relativeUrl = relativeUrl.TrimStart('/');
        
        return $"{baseUrl}/{relativeUrl}";
    }

    public Task<bool> FileExistsAsync(string fileUrl)
    {
        try
        {
            var filePath = Path.Combine(_environment.ContentRootPath, "public", fileUrl.TrimStart('/'));
            return Task.FromResult(File.Exists(filePath));
        }
        catch
        {
            return Task.FromResult(false);
        }
    }
}
