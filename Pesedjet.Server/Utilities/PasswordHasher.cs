using System.Security.Cryptography;
using System.Text;

namespace Pesedjet.Server.Utilities;

public static class PasswordHasher
{
    public static string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(bytes);
    }
    
    public static bool VerifyPassword(string enteredPassword, string storedHash)
    {
        string hashOfEntered = HashPassword(enteredPassword);
        return hashOfEntered == storedHash;
    }
    
    public static void Main()
    {
        Console.WriteLine("=== Iniciando pruebas de PasswordHasher ===\n");

        string originalPassword = "abdiesito";
        
        string hashedPassword = HashPassword(originalPassword);
        Console.WriteLine($"Contraseña original: {originalPassword}");
        Console.WriteLine($"Hash generado (Base64): {hashedPassword}");
        Console.WriteLine();
        
        bool isCorrectValid = VerifyPassword(originalPassword, hashedPassword);
        Console.WriteLine($"¿Verificación con contraseña correcta?: {isCorrectValid} (Esperado: True)");
        
        string wrongPassword = "ContraseñaIncorrecta";
        bool isIncorrectValid = VerifyPassword(wrongPassword, hashedPassword);
        Console.WriteLine($"¿Verificación con contraseña incorrecta?: {isIncorrectValid} (Esperado: False)");

        Console.WriteLine("\n=== Pruebas finalizadas ===");
    }
}