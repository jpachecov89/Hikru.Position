﻿namespace Hikru.Position.Backend.Application.Exceptions
{
	public class UnauthorizedException : Exception
	{
		public UnauthorizedException(string message)
			: base(message) { }
	}
}
