namespace Domain.Interfaces.Services.Production;

/// <summary>
/// Servicio para convertir cantidades entre diferentes unidades de medida
/// Soporta conversión de peso (g/kg) y volumen (ml/l)
/// </summary>
public interface IUnitConversionService
{
    /// <summary>
    /// Convierte una cantidad de una unidad a otra
    /// </summary>
    /// <param name="quantity">Cantidad a convertir</param>
    /// <param name="fromUnit">Unidad de origen (g, kg, ml, l, unidades)</param>
    /// <param name="toUnit">Unidad de destino (g, kg, ml, l, unidades)</param>
    /// <returns>Cantidad convertida o null si las unidades son incompatibles</returns>
    double? ConvertQuantity(double quantity, string fromUnit, string toUnit);
    
    /// <summary>
    /// Verifica si dos unidades son compatibles para conversión
    /// </summary>
    /// <param name="unit1">Primera unidad</param>
    /// <param name="unit2">Segunda unidad</param>
    /// <returns>True si son compatibles, False en caso contrario</returns>
    bool AreUnitsCompatible(string unit1, string unit2);
}
