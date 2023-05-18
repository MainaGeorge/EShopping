using Duende.IdentityServer.Models;

namespace Eshopping.Identity;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };

    public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
    {
        new ApiResource("Catalog", "Catalog.API") { Scopes = { "catalogapi" } },
        new ApiResource("Basket", "Basket.API") {Scopes = { "basketapi" } }
    };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope("catalogapi"),
            new ApiScope("basketapi")
        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            new Client
            {
                ClientName = "Catalog API client",
                ClientId = "CatalogApiClient",
                ClientSecrets = { new Secret("1F7805C3-968B-458E-8445-D05BE0B50B70".Sha256())},
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowedScopes = {"catalogapi", "basketapi" }
            },
        };
}
