using AutoMapper;
using Common.UserCommands;
using Database.DbModels;

namespace Common.AutoMapperProfiles
{
	public class CommandToDbModelProfile : Profile
	{
		public CommandToDbModelProfile()
		{
			CreateMap<CreateUserCommand, User>();
		}
	}
}
