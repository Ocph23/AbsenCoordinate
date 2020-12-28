using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace AbsenCoordinateMobile.Helpers
{
    public interface IHTTPClientHandlerCreationService
    {
        HttpClientHandler GetInsecureHandler();
    }
}
