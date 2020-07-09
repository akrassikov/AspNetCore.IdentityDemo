using IdentityDemo.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityDemo.Extensions
{
	public class AdditionalUserClaimsPrincipalFactory
			: UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>
	{
		public AdditionalUserClaimsPrincipalFactory(
			UserManager<ApplicationUser> userManager,
			RoleManager<IdentityRole> roleManager,
			IOptions<IdentityOptions> optionsAccessor)
			: base(userManager, roleManager, optionsAccessor)
		{ }

		public async override Task<ClaimsPrincipal> CreateAsync(ApplicationUser user)
		{
			var principal = await base.CreateAsync(user);
			var identity = (ClaimsIdentity)principal.Identity;

			var claims = new List<Claim>();
			//if (user.IsAdmin)
			//{
			//	claims.Add(new Claim(JwtClaimTypes.Role, "admin"));
			//}
			//else
			//{
			//	claims.Add(new Claim(JwtClaimTypes.Role, "user"));
			//}
			if (!string.IsNullOrEmpty(user.FirstName))
            {
				claims.Add(new Claim("FirstName", user.FirstName));
            }


			identity.AddClaims(claims);
			return principal;
		}
	}
}
