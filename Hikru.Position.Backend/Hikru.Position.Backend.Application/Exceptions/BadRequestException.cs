﻿namespace Hikru.Position.Backend.Application.Exceptions
{
	public class BadRequestException : Exception
	{
		public BadRequestException(string message)
			: base(message) { }
	}
}
