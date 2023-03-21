using System.Security.Claims;

namespace Web
{
    public static class ClaimsPrincipalExtensions

    {


        public static long GetUserId(this ClaimsPrincipal principal)

        {

            if (principal == null)

                throw new ArgumentNullException(nameof(principal));




            var selfUserId = principal.FindAll(ClaimTypes.NameIdentifier).Last().Value;
            
            return long.Parse(selfUserId);

        }




    }
}
