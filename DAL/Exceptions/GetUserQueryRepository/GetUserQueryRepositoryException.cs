using System.Runtime.Serialization;

namespace DAL.Exceptions.GetUserQueryRepository
{
	[Serializable]
	public class GetUserQueryRepositoryException : RepositoryException
	{
		public GetUserQueryRepositoryException()
			: base()
		{
		}

		public GetUserQueryRepositoryException(string? message)
			: base(message)
		{
		}

		public GetUserQueryRepositoryException(string? message, Exception? innerException)
			: base(message, innerException)
		{
		}

		protected GetUserQueryRepositoryException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
