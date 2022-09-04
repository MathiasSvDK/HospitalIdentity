using IdentityServer.Models;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServer.Services
{
    public class Gangster : IProfileService
    {
        private readonly IUserClaimsPrincipalFactory<ApplicationUser> _claimsFactory;
        private readonly UserManager<ApplicationUser> _userManager;

        public Gangster(UserManager<ApplicationUser> userManager, IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory)
        {
            _userManager = userManager;
            _claimsFactory = claimsFactory;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(sub);
            
            context.AddRequestedClaims(context.Subject.Claims);
            context.IssuedClaims.Add(new Claim("name", user.UserName));
            context.IssuedClaims.Add(new Claim("firstname", user.Firstname));
            context.IssuedClaims.Add(new Claim("lastname", user.Lastname));
            context.IssuedClaims.Add(new Claim("addrses", user.Address));
            context.IssuedClaims.Add(new Claim("mobilnr", user.Mobilnr));
            context.IssuedClaims.Add(new Claim("gruppe", user.Role.ToString()));
            context.IssuedClaims.Add(new Claim("hospitalid", user.HospitalId.ToString()));
            context.IssuedClaims.Add(new Claim("email", user.Email));
            //return Task.FromResult(0);
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            context.IsActive = true;
            return Task.FromResult(0);
        }
    }
}