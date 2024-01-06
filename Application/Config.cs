using IdentityServer4.Models;

namespace Application
{
	public static class Config
	{
		public static IEnumerable<ApiScope> ApiScopes =>
			new List<ApiScope>
			{
				new("openid", "OpenID"), new("sov", "sov client")
			};

		public static IReadOnlyList<Client> GetClients()
		{
			return (new List<Client> {
				new Client {
					ClientId = "lc_client",
					AllowedGrantTypes = { GrantType.ResourceOwnerPassword, "ldap" },
					RequireClientSecret = false,
					AccessTokenType = AccessTokenType.Jwt,
					AllowOfflineAccess = true,
					RefreshTokenUsage = TokenUsage.OneTimeOnly,

					// scopes that client has access to
					AllowedScopes = { "openid", "offline_access", "openid" },
				}
			}).AsReadOnly();
		}
	}
}
