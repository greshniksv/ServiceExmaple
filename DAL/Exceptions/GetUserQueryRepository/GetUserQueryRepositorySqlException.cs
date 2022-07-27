using System.Runtime.Serialization;

namespace DAL.Exceptions.GetUserQueryRepository
{
	[Serializable]
	public class GetUserQueryRepositorySqlException : RepositorySqlException
	{
		public GetUserQueryRepositorySqlException()
			: base()
		{
		}

		public GetUserQueryRepositorySqlException(string? message)
			: base(message)
		{
		}

		public GetUserQueryRepositorySqlException(string? message, Exception? innerException)
			: base(message, innerException)
		{
		}

		protected GetUserQueryRepositorySqlException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
