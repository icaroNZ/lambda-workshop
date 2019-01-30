using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HelloLambda
{
    public interface IKmsClient
    {
        Task<string> DecryptValueAsync(string encryptedValue);
    }
}
