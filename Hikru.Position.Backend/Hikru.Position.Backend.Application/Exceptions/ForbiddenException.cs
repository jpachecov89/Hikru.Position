﻿namespace Hikru.Position.Backend.Application.Exceptions
{
	public class ForbiddenException : Exception
	{
		public ForbiddenException(string message)
			: base(message) { }
	}
}
