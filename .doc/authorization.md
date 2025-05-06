# Authorization Policies

This project defines multiple role-based authorization policies using ASP.NET Core. These policies help restrict access to endpoints based on the user's role or claims.

## 🛡️ Defined Policies

| Policy Name     | Description                                                                 |
|-----------------|-----------------------------------------------------------------------------|
| `AdminOnly`     | Grants access **only to users with the `Admin` role**.                     |
| `ManagerOnly`   | Grants access **only to users with the `Manager` role**.                   |
| `CustomerOnly`  | Grants access **only to users with the `Customer` role**.                  |
| `AdminOrManager`| Grants access to users with either `Admin` **or** `Manager` roles.         |
| `AnyRole`       | Grants access to **any authenticated user with a `Role` claim**.           |

## 🔐 Usage in Controllers

You can apply policies to your controllers or actions using the `[Authorize]` attribute:

```csharp
[Authorize(Policy = "AdminOnly")]
[ApiController]
[Route("api/[controller]")]
public class AdminController : ControllerBase
{
    // Accessible only to Admins
}
```

## 🔧 Configuration Location

Authorization policies are configured in the WebApiModuleInitializer class located at:

Ambev.DeveloperEvaluation.IoC.ModuleInitializers.WebApiModuleInitializer

The configuration uses AddAuthorization with AddPolicy methods.

## 📌 Tip

Ensure that your authentication mechanism sets the appropriate ClaimTypes.Role claim, matching the values defined in UserRole enum:

public enum UserRole
{
    Admin,
    Manager,
    Customer
}