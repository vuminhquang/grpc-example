using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace grpc_client
{
    //this validator will trust incoming certificate with matching thumbprint
    //use when we need to call using SSL to other "local" service in docker
    public class SingleCertificateValidator
    {
        private readonly X509Certificate2 _trustedCertificate;

        public SingleCertificateValidator(X509Certificate2 trustedCertificate)
        {
            _trustedCertificate = trustedCertificate;
        }

        public bool Validate(HttpRequestMessage httpRequestMessage, X509Certificate2 x509Certificate2, X509Chain x509Chain, SslPolicyErrors sslPolicyErrors)
        {
            if (sslPolicyErrors == SslPolicyErrors.None)
                return true;

            string thumbprint = x509Certificate2.GetCertHashString();
            //TODO: add more validation logic if needed
            return thumbprint == _trustedCertificate.Thumbprint;
        }
    }
}
