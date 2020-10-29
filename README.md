# Bază de date pentru Fundraising

Această aplicație oferă un sistem de organizare a informațiilor necesare procesului de fundraising în cadrul ASMI.

## Configurare

### Autentificare cu conturi de Google

Pentru autentificarea cu Google, [se pot folosi comenzile următoare](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/social/google-logins?view=aspnetcore-3.1#store-the-google-client-id-and-secret) pentru a seta ID-ul și codul secret al aplicației:

```sh
dotnet user-secrets set "Authentication:Google:ClientId" "<client-id>"
dotnet user-secrets set "Authentication:Google:ClientSecret" "<client-secret>"
```
