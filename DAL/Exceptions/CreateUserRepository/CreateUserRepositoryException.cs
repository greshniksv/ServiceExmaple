using System.Runtime.Serialization;

namespace DAL.Exceptions.CreateUserRepository
{
	[Serializable]
	public class CreateUserRepositoryException : RepositoryException
	{
		public CreateUserRepositoryException()
			: base()
		{
		}

		public CreateUserRepositoryException(string? message)
			: base(message)
		{
		}

		public CreateUserRepositoryException(string? message, Exception? innerException)
			: base(message, innerException)
		{
		}

		protected CreateUserRepositoryException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
