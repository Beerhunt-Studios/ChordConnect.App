using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChordConnect.Api;

public abstract class BaseAPI
{
    internal BaseAPI? Parent { private get; init; }

    internal abstract string CurrentPath { get; }

    internal string Path => Parent is null ? this.CurrentPath : Flurl.Url.Combine(this.Parent?.Path, this.CurrentPath);

    internal string AccessToken { get; init; }

    public BaseAPI(BaseAPI? parent, string accessToken)
    {
        Parent = parent;
        AccessToken = accessToken;
    }
}
