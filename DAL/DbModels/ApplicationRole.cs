using Microsoft.AspNetCore.Identity;

namespace DAL.DbModels
{
	public class ApplicationRole : IdentityRole<Guid>
	{
		public ApplicationRole()
		{
		}

		public ApplicationRole(string roleName)
			: base(roleName)
		{
		}
	}
}
