using Domain.Interfaces.Services.Production;

namespace Infrastructure.Services.Production;

/// <summary>
/// Servicio para convertir cantidades entre diferentes unidades de medida
/// Soporta conversión de peso (g/kg) y volumen (ml/l)
/// </summary>
public class UnitConversionService : IUnitConversionService
{
    // Factores de conversión
    private const double KG_TO_G = 1000;
    private const double L_TO_ML = 1000;

    public double? ConvertQuantity(double quantity, string fromUnit, string toUnit)
    {
        // Normalizar unidades a minúsculas
        fromUnit = fromUnit?.ToLower() ?? "unidades";
        toUnit = toUnit?.ToLower() ?? "unidades";

        // Si las unidades son iguales, no hay conversión
        if (fromUnit == toUnit)
            return quantity;

        // Verificar si son compatibles
        if (!AreUnitsCompatible(fromUnit, toUnit))
            return null;

        // Conversiones de peso
        if (fromUnit == "kg" && toUnit == "g")
            return quantity * KG_TO_G;
        
        if (fromUnit == "g" && toUnit == "kg")
            return quantity / KG_TO_G;

        // Conversiones de volumen
        if (fromUnit == "l" && toUnit == "ml")
            return quantity * L_TO_ML;
        
        if (fromUnit == "ml" && toUnit == "l")
            return quantity / L_TO_ML;

        // Si llegamos aquí, algo salió mal
        return null;
    }

    public bool AreUnitsCompatible(string unit1, string unit2)
    {
        unit1 = unit1?.ToLower() ?? "unidades";
        unit2 = unit2?.ToLower() ?? "unidades";

        // Misma unidad siempre es compatible
        if (unit1 == unit2)
            return true;

        // Unidades de peso son compatibles entre sí
        var weightUnits = new[] { "g", "kg" };
        if (weightUnits.Contains(unit1) && weightUnits.Contains(unit2))
            return true;

        // Unidades de volumen son compatibles entre sí
        var volumeUnits = new[] { "ml", "l" };
        if (volumeUnits.Contains(unit1) && volumeUnits.Contains(unit2))
            return true;

        // Unidades no son compatibles
        return false;
    }
}
