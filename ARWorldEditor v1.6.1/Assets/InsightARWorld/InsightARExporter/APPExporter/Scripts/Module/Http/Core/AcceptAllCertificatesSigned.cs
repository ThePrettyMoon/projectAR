using System.Security.Cryptography.X509Certificates;
using UnityEngine.Networking;

namespace ARWorldEditor
{
    /// <summary>
    /// Based on https://www.owasp.org/index.php/Certificate_and_Public_Key_Pinning#.Net
    /// </summary>
    public class AcceptAllCertificatesSigned : CertificateHandler
    {
        private const string TAG = "AcceptAllCertificatesSigned";
        protected override bool ValidateCertificate(byte[] certificateData)
        {
            // accept all
            X509Certificate2 certificate = new X509Certificate2(certificateData);
            string publicKey = certificate.GetPublicKeyString();
            // InsightDebug.Log(TAG, "certificate " + publicKey);
            return true;
        }
    }
}