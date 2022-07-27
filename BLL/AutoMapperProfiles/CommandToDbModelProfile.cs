using AutoMapper;
using BLL.UserCommands;
using DAL.DbModels;

namespace BLL.AutoMapperProfiles
{
	public class CommandToDbModelProfile : Profile
	{
		public CommandToDbModelProfile()
		{
			CreateMap<CreateUserCommand, User>();
		}
	}
}
