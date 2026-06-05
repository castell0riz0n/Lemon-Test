using Lemon_Test.Core.MatrixTheoryData;

namespace Lemon_Test.Core.Tests.MatrixTheoryData;

public class FeatureFlagsTests
{
    [Theory]
    [MemberData(nameof(FeatureFlagMatrix))]
    public void IsFeatureEnabled_ShouldRespectRoleAndEnvironmentRules(
        string feature, 
        string userRole, 
        string environment)
    {
        // Arrange
        var featureFlags = new FeatureFlags();

        // Act
        var result = featureFlags.IsFeatureEnabled(feature, userRole, environment);

        // Assert - Apply business rules
        if (feature.StartsWith("Beta") && environment != "Development")
        {
            Assert.False(result, $"Beta features should only be enabled in Development, not {environment}");
        }
        else if (feature.StartsWith("Admin") && userRole != "Admin")
        {
            Assert.False(result, $"Admin features should only be enabled for Admin role, not {userRole}");
        }
        else if (feature.StartsWith("Premium") && userRole != "Premium" && userRole != "Admin")
        {
            Assert.False(result, $"Premium features should only be enabled for Premium or Admin roles, not {userRole}");
        }
        else
        {
            Assert.True(result, $"Feature {feature} should be enabled for role {userRole} in {environment}");
        }
    }

    [Theory]
    [MemberData(nameof(SimplePermissionMatrix))]
    public void IsFeatureEnabled_BasicPermissionTest(string userRole, bool expectedForBasic)
    {
        // Arrange
        var featureFlags = new FeatureFlags();

        // Act
        var result = featureFlags.IsFeatureEnabled("BasicFeature", userRole, "Development");

        // Assert
        Assert.Equal(expectedForBasic, result);
    }

    // Comprehensive combinations: 4 features × 3 roles × 3 environments = 36 test cases
    public static IEnumerable<object[]> FeatureFlagMatrix()
    {
        var features = new[] { "BasicFeature", "BetaFeature", "AdminFeature", "PremiumFeature" };
        var roles = new[] { "User", "Premium", "Admin" };
        var environments = new[] { "Development", "Staging", "Production" };

        foreach (var feature in features)
        {
            foreach (var role in roles)
            {
                foreach (var env in environments)
                {
                    yield return new object[] { feature, role, env };
                }
            }
        }
    }

    // Simple permission testing: 3 roles with basic features
    public static IEnumerable<object[]> SimplePermissionMatrix()
    {
        var roles = new[] { "User", "Premium", "Admin" };
        
        foreach (var role in roles)
        {
            yield return new object[] { role, true };
        }
    }
}
