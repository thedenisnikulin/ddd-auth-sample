using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Chattitude.Api.Dto;

namespace Chattitude.Api.Attributes
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
	public class Authorize : Attribute, IAuthorizationFilter
	{
		public void OnAuthorization(AuthorizationFilterContext ctx)
		{
			UserDto userDto = (UserDto) ctx.HttpContext.Items["user"];
			if (userDto == null)
			{
				ctx.Result = new UnauthorizedResult();
				return;
			}
		}
	}
}