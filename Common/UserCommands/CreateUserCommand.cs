using Common.Interfaces;

namespace Common.UserCommands
{
    public class CreateUserCommand : ICommand<int>
    {
        public string Name { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
	}
}
