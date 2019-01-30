using System;
using Serilog;

namespace HelloLambda
{
    public static class SettingsReader
    {
        public static Settings LoadSettings(IKmsClient kmsClient)
        {
            string oracleConnectionString = Environment.GetEnvironmentVariable("ORACLE_CONNECTIONSTRING");
            
            //Log the connection string with the markup (no password will be revealed)
            Log.Logger.Information($"Decrypting connection string {oracleConnectionString}");

            string oracleUsername = Environment.GetEnvironmentVariable("ORACLE_USER");
            var oraclePassword = kmsClient.DecryptValueAsync(Environment.GetEnvironmentVariable("ORACLE_PASSWORD"));
            var oracleLicenseKey = kmsClient.DecryptValueAsync(Environment.GetEnvironmentVariable("LICENSE_KEY"));

            string connection = oracleConnectionString
                .Replace("{OracleUsername}", oracleUsername)
                .Replace("{OraclePassword}", oraclePassword.Result)
                .Replace("{LicenseKey}", oracleLicenseKey.Result);


            return new Settings()
            {
                OracleConnectionString = "User Id=WORKSHOPUSR;Password=development;Server=workshop-oracle;Direct=True;Sid=XE;Port=1521;License Key=\"Y5KYxkcsZ3bPHhVwfZ1cGCBol8AltkfgDJPJuhE/1li07sxt7FDnk0yHKN6OHYxjcR4ikMH1d3N6B2SoLtGrqY11/iMh/AtwlOfCJmMxaJIY+hz5G68knDTb1/9BkaOqNeFHukY+xxRKfhaX3+Vt/AT8/GRPKjG3tXSp8SszlvSXKZRfYF2KuXCeP/ackVM0VSaOJ6NvYy51j/WixsKMn8je1Uoa50pAaLPIwyURX27MYSw4fkMywuRGv3EpPLavhmgieg04ql1iI208qHbeEg==\""
            };
            
        }
    }
}
