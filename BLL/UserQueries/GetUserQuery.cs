using BLL.Interfaces;
using BLL.ViewModels;

namespace BLL.UserQueries
{
	public class GetUserQuery : IQuery<UserModel?>
	{
		public GetUserQuery(int userId)
		{
			UserId = userId;
		}

		public int UserId { get; set; }
	}
}