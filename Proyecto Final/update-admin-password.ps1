# Script para actualizar la contraseña del admin en Render
# Ejecutar desde el directorio del proyecto

$email = "admin@example.com"
$password = "Admin_123"

Write-Host "Generando hash de contraseña..."

# Generar hash usando el proyecto
dotnet run --project "Proyecto Final" -- hash-password $password
