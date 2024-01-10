
# Técnologias

* EntityFramework Core
* Net 8
* Minimal API
* Mediator
* Clean
* Jwt

# Manual

Para ejecutar el proyecto, es necesario actualizar la cadena de conexión. La sección "Security" es para la Organización y Usuarios. "One" y "Two" representan a los múltiples inquilinos (tenants).

Para ejecutar el siguiente comando, ubíquese en la carpeta Tenant.Api:

```
dotnet ef database update --verbose --project ../Infra --context SecurityDbContext 
```  

Luego de este ejecutar los siguiente comandos:

```
dotnet ef database update --verbose --project ../Infra --context TenantDbContext --connection "Server=.;Database=One; Trusted_Connection=True;TrustServerCertificate=True"

dotnet ef database update --verbose --project ../Infra --context TenantDbContext --connection "Server=.;Database=Two; Trusted_Connection=True;TrustServerCertificate=True"
```
Esto generara dos organizaciones con los siguientes Id:

```
ba3bef5e-5250-4f49-8719-ebdb0bf2b58f One
7e6f1a79-c36c-410e-a478-306bf0adb053 Two
```

Descargue la colección de Postman ubicada en la raíz de este repositorio.

