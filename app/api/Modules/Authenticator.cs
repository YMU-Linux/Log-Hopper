using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace api.Modules
{
    public class Authenticator
    {
        private (bool, string?) IsMalformedToken()
        {
            // TODO: Write a check if token is malformed
            return (true, "Token is malformed!");
        }

        private (bool, string?) IsTokenAlive()
        {
            // TODO: Write a token alive check
            return (true, null);
        }

        private (bool, string?) IsTokenOwner()
        {
            // TODO: Write check for token owner
            return (true, null);
        }

        public (bool, string?) IsValidToken()
        {
            var (isMalformed, tokenErr) = IsMalformedToken();
            var (isAlive, aliveErr) = IsTokenAlive();
            var (isOwner, ownerErr) = IsTokenOwner();

            if (isMalformed)
            {
                return (false, tokenErr);
            }

            if (!isAlive)
            {
                return (false, aliveErr);
            }

            if (!isOwner)
            {
                return (false, ownerErr);
            }

            return (true, null);
        }
    }
}
