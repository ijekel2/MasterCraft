using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace MasterCraft.Client.Authentication
{
    public class JwtParser
    {
        public static IEnumerable<Claim> ParseClaimsFromJwt(string pJwt)
        {
            var lClaims = new List<Claim>();
            var lPayload = pJwt.Split('.')[1];
            var lJsonBytes = ParseBase64WithoutPadding(lPayload);
            var lKeyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(lJsonBytes);

            ExtractRolesFromJwt(lClaims, lKeyValuePairs);

            lClaims.AddRange(lKeyValuePairs.Select(lKvp => new Claim(lKvp.Key, lKvp.Value.ToString())));

            return lClaims;
        }

        private static void ExtractRolesFromJwt(List<Claim> pClaims, Dictionary<string, object> pKeyValuePairs)
        {
            pKeyValuePairs.TryGetValue(ClaimTypes.Role, out object lRoles);

            if (lRoles is not null)
            {
                var lParsedRoles = lRoles.ToString().Trim().TrimStart('[').TrimEnd(']').Split(',');

                if (lParsedRoles.Length > 1)
                {
                    foreach (var lParsedRole in lParsedRoles)
                    {
                        pClaims.Add(new Claim(ClaimTypes.Role, lParsedRole.Trim('"')));
                    }
                }
                else
                {
                    pClaims.Add(new Claim(ClaimTypes.Role, lParsedRoles[0]));
                }

                pKeyValuePairs.Remove(ClaimTypes.Role);
            }
        }

        private static byte[] ParseBase64WithoutPadding(string pBase64)
        {
            switch (pBase64.Length % 4)
            {
                case 2:
                    pBase64 += "==";
                    break;
                case 3:
                    pBase64 += "=";
                    break;
            }

            return Convert.FromBase64String(pBase64);
        }
    }
}
