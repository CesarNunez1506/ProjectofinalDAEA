namespace Domain.Interfaces.Services.Production;

/// <summary>
/// Interfaz para servicios de almacenamiento de archivos
/// Gestiona la carga, eliminación y obtención de URLs de imágenes de productos
/// </summary>
public interface IFileStorageService
{
    /// <summary>
    /// Guarda un archivo y retorna la URL relativa
    /// </summary>
    /// <param name="fileContent">Contenido del archivo en bytes</param>
    /// <param name="fileName">Nombre del archivo</param>
    /// <param name="folder">Carpeta de destino (ej: "products")</param>
    /// <returns>URL relativa del archivo guardado</returns>
    Task<string> SaveFileAsync(byte[] fileContent, string fileName, string folder);
    
    /// <summary>
    /// Elimina un archivo del sistema de almacenamiento
    /// </summary>
    /// <param name="fileUrl">URL del archivo a eliminar</param>
    /// <returns>True si se eliminó correctamente, False en caso contrario</returns>
    Task<bool> DeleteFileAsync(string fileUrl);
    
    /// <summary>
    /// Genera la URL absoluta para acceder a un archivo
    /// </summary>
    /// <param name="relativeUrl">URL relativa del archivo</param>
    /// <param name="baseUrl">URL base del servidor</param>
    /// <returns>URL absoluta del archivo</returns>
    string GetAbsoluteUrl(string relativeUrl, string baseUrl);
    
    /// <summary>
    /// Verifica si un archivo existe
    /// </summary>
    /// <param name="fileUrl">URL del archivo</param>
    /// <returns>True si existe, False en caso contrario</returns>
    Task<bool> FileExistsAsync(string fileUrl);
}
