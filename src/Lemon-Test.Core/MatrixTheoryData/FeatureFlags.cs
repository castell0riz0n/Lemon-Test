namespace Lemon_Test.Core.MatrixTheoryData;

public class FeatureFlags
{
    public bool IsFeatureEnabled(string feature, string userRole, string environment)
    {
        // Beta features only in Development
        if (feature.StartsWith("Beta") && environment != "Development")
            return false;

        // Admin features require admin role
        if (feature.StartsWith("Admin") && userRole != "Admin")
            return false;

        // Premium features require Premium or Admin role
        if (feature.StartsWith("Premium") && userRole != "Premium" && userRole != "Admin")
            return false;

        return true;
    }
}
