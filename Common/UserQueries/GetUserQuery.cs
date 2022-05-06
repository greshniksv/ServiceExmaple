using Common.Interfaces;
using Common.ViewModels;

namespace Common.UserQueries
{
	public class GetUserQuery : IQuery<UserModel>
	{
		public GetUserQuery(int id)
		{
			Id = id;
		}

		public int Id { get; set; }
	}
}