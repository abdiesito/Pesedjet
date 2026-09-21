# Pesedjet - La Guerra del Panteón

Pesedjet es un juego de arquitectura Cliente-Servidor que utiliza comunicación TCP pura. Este repositorio (monorepo) contiene toda la solución estructurada en tres proyectos interdependientes para garantizar la sincronización del código entre ambas partes del sistema.

## ✦ Arquitectura de la Solución

*   **`Pesedjet.Server`:** Backend (.NET 8). Contiene las reglas de negocio, validaciones y acceso a la base de datos mediante Entity Framework Core.
*   **`Pesedjet.Client`:** Frontend (Avalonia UI). Interfaz gráfica del usuario. No calcula reglas; solo renderiza, captura clics y maneja la internacionalización.
*   **`Pesedjet.Contracts`:** Librería de clases (.NET 8). Contiene las interfaces WCF compartidas (`[ServiceContract]`) y los DTOs.

## ✦ Stack Tecnológico

*   **Framework:** .NET 8 LTS (Estrictamente C# 12).
*   **UI:** Avalonia UI (v11.x).
*   **Comunicaciones:** CoreWCF (Servidor) y `System.ServiceModel.NetTcp` (Cliente).
*   **Base de Datos:** SQL Server (Vía Entity Framework Core).
*   **IDE Recomendado:** JetBrains Rider.

---

## ✦ Configuración del Entorno de Desarrollo

El proyecto está diseñado para funcionar de manera nativa tanto en macOS (ARM64) como en Windows. **Sigue las instrucciones correspondientes a tu sistema operativo.**

### Prerrequisitos Comunes
1. Instalar el [SDK de .NET 8](https://dotnet.microsoft.com/download/dotnet/8.0).
2. Instalar Docker Desktop (para contenedorizar la base de datos de manera aislada).
3. Instalar JetBrains Rider (o Visual Studio 2022 / VS Code con C# Dev Kit).

### Configuración en macOS (Apple Silicon M4)
Debido a la arquitectura ARM64, **no** se debe usar la imagen estándar de SQL Server para Linux, ya que emular x86_64 con Rosetta 2 causa inestabilidad transaccional.
*   **Base de Datos:** Utiliza la imagen **Azure SQL Edge** nativa para ARM64. Ejecuta el siguiente comando:
    ```bash
    docker run -e 'ACCEPT_EULA=Y' -e 'MSSQL_SA_PASSWORD=TuPasswordFuerte123' -p 1433:1433 -d mcr.microsoft.com/azure-sql-edge
    ```

### Configuración en Windows 11
*   **Base de Datos:** Puedes instalar SQL Server Developer Edition nativamente, o utilizar la imagen oficial de Linux mediante Docker:
    ```bash
    docker run -e 'ACCEPT_EULA=Y' -e 'MSSQL_SA_PASSWORD=TuPasswordFuerte123' -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-latest
    ```

---

## ✘️ Reglas de Oro (No negociables)

Para mantener la compatibilidad entre Windows y Mac y evitar horas de depuración, todo el equipo debe acatar estas normas:

1.  **Seguridad TCP:** El `NetTcpBinding` **debe** tener `SecurityMode.None`. De lo contrario, intentará forzar la autenticación de Windows y fallará la conexión desde macOS.
2.  **Rutas de Archivos:** El sistema de archivos de macOS distingue entre mayúsculas y minúsculas (Case-Sensitive), Windows no. *Asegúrate de que el nombre del archivo y la referencia en el código coincidan exactamente letra por letra.*
3.  **Rutas Relativas:** Para cargar o guardar assets, **nunca** uses rutas absolutas. Usa `Path.Combine(Environment.CurrentDirectory, "Assets")`.
4.  **Migraciones de BD:** Prohibido modificar tablas manualmente en el motor de SQL Server. Cualquier cambio requiere una migración:
    *   Para crear: `dotnet ef migrations add NombreMigracion --project Pesedjet.Server`
    *   Para aplicar: `dotnet ef database update --project Pesedjet.Server`
5.  **Variables UI (MVVM):** Usa siempre campos privados en minúscula con `[ObservableProperty]` (ej. `private string _vida;`). Evita las propiedades parciales de versiones Preview de C#.

---

## ➤️ Ejecución del Proyecto

1. Clona este repositorio y restaura la solución desde Rider.
2. Ejecuta el comando de actualización de base de datos (`dotnet ef database update --project Pesedjet.Server`).
3. En Rider, configura tu ejecución (*Run Configuration*) en modo "Multiple Projects".
4. Inicia primero `Pesedjet.Server` y asegúrate de que el servicio esté escuchando.
5. Inicia `Pesedjet.Client`.