using ChordConnect.Api.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChordConnect.Api;

public class ChordAPI : BaseAPI
{
    private readonly string baseUrl;

    internal override string CurrentPath => Flurl.Url.Combine(this.baseUrl, "api");

    public UserClient User { get; init; }

    public ChordAPI(string baseUrl, string accessToken) : base(null, accessToken)
    {
        this.baseUrl = baseUrl;

        this.User = new UserClient(this, accessToken);
    }
}
