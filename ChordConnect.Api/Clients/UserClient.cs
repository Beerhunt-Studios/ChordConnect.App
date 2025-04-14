using Flurl;
using Flurl.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChordConnect.Api.Clients;

public class UserClient : BaseAPI
{
    internal override string CurrentPath => "user";

    public UserClient(BaseAPI? parent, string accessToken) : base(parent, accessToken)
    {
    }

    public Task<bool> HasProfileAsync(CancellationToken cancellationToken = default)
    {
        return Url.Combine(Path, "HasUser").WithOAuthBearerToken(AccessToken).GetJsonAsync<bool>(cancellationToken: cancellationToken);
    }
}
