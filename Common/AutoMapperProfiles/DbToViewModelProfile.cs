using AutoMapper;
using Common.ViewModels;
using Database.DbModels;

namespace Common.AutoMapperProfiles
{
	public class DbToViewModelProfile : Profile
	{
		public DbToViewModelProfile()
		{
			CreateMap<User, UserModel>();
		}
	}
}
