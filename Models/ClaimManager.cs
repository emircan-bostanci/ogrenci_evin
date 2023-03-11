using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace ogrencievin.Models;
public sealed class ClaimManager
{
    //1. Eleman claim Name 2. Eleman Sema
    public ClaimsPrincipal GetClaimPrincipal(List<Tuple<string,string>> typesAndValues){
        List<Claim> claims = new List<Claim>();
        foreach(Tuple<string,string> typeAndValue in typesAndValues){
            Claim claim = new Claim(typeAndValue.Item1,typeAndValue.Item2);
            claims.Add(claim);
        }
        
        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
        ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
        return claimsPrincipal;
    }
    public async Task<ClaimsPrincipal> ProvideClaim(ClaimsPrincipal claimsPrincipal){
        ClaimsIdentity? identity = claimsPrincipal.Identity as ClaimsIdentity;
        Claim providedClaim = null;
        if(claimsPrincipal.HasClaim(x => x.Type == ClaimTypes.Name)){
            providedClaim = new Claim(ClaimTypes.Name,identity.Name);
            identity.AddClaim(providedClaim);
        }
        throw new NotImplementedException();
    }

    
}