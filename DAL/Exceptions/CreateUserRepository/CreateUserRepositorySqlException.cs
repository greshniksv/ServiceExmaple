using System.Runtime.Serialization;

namespace DAL.Exceptions.CreateUserRepository
{
	[Serializable]
	public class CreateUserRepositorySqlException : RepositorySqlException
	{
		public CreateUserRepositorySqlException()
			: base()
		{
		}

		public CreateUserRepositorySqlException(string? message)
			: base(message)
		{
		}

		public CreateUserRepositorySqlException(string? message, Exception? innerException)
			: base(message, innerException)
		{
		}

		protected CreateUserRepositorySqlException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
