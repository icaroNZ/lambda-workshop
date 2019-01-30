using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Amazon.KeyManagementService;
using Amazon.KeyManagementService.Model;

namespace HelloLambda
{
    public class KmsClient: AmazonKeyManagementServiceClient, IKmsClient
    {
        public async Task<string> DecryptValueAsync(string encryptedValue)
        {
            try
            { 
                var ciphertestStream = new MemoryStream(Convert.FromBase64String(encryptedValue)) { Position = 0 };

                var decryptRequest = new DecryptRequest
                {
                    CiphertextBlob = ciphertestStream
                };

                var response = await DecryptAsync(decryptRequest);

                var buffer = new byte[response.Plaintext.Length];

                var bytesRead = response.Plaintext.Read(buffer, 0, (int)response.Plaintext.Length);

                return Encoding.UTF8.GetString(buffer, 0, bytesRead);
            }
            catch
            {
                return encryptedValue;
            }
        }
    }
    
}
