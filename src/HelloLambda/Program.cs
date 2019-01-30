using System;
using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using Newtonsoft.Json;
using HelloLambda.Repository;
using Serilog;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace HelloLambda
{
    public class Function
    {

        private readonly MyInsertUseCase _myInsertUseCase;
        private readonly MyUpdateUseCase _myUpdateUseCase;
        public Function()
        {
            var kmsClient = new KmsClient();

            /* Create and keep the repository globally allocated, improve .net connection pooling */
            var repository = new OracleRepository(SettingsReader.LoadSettings(kmsClient));
            _myInsertUseCase = new MyInsertUseCase(repository);
            _myUpdateUseCase = new MyUpdateUseCase(repository);
        }

        public APIGatewayProxyResponse FunctionHandler(APIGatewayProxyRequest request, ILambdaContext context)
        {
            ConfigureLogger(context.AwsRequestId);

            var bodyString = request?.Body;

            if (string.IsNullOrEmpty(bodyString))
            {
                return ApiResponse(statusCode: 400, message: "Missing request body");
            }

            var myRequest = JsonConvert.DeserializeObject<MessageRequest>(bodyString);

            if ((myRequest == null) ||
                (string.IsNullOrEmpty(myRequest.Message)))
            {
                return ApiResponse(statusCode: 400, message: "Invalid request");
            }

            var success = false;

            try
            {
                if (myRequest.Id.HasValue)
                {
                    _myUpdateUseCase.UpdateWorkshopMessage(myRequest.Id.Value, myRequest.Message);
                    success = true;
                    return ApiResponse(statusCode: 200, message: "Update complete");
                }
                else
                {
                    _myInsertUseCase.InsertWorkshopMessage(myRequest.Message);
                    success = true;
                    return ApiResponse(statusCode: 200, message: "Insert complete");
                }
            }
            catch (Exception exception)
            {
                Log.Logger.Error(exception.Message);
                return ApiResponse(statusCode: 500, message: exception.Message);
            }
            finally
            {
                MonitoringClient.Increment("MyRequest", $"success:{success}");
            }
        }

        private APIGatewayProxyResponse ApiResponse(int statusCode, string message)
        {
            return new APIGatewayProxyResponse
            {
                StatusCode = statusCode,
                Body = ResponseBody(message)
            };
        }

        private static string ResponseBody(string message)
        {
            return JsonConvert.SerializeObject(new {notificationResponse = message});
        }

        private static void ConfigureLogger(
            string requestId)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .Enrich.WithProperty("RequestId", requestId)
                .CreateLogger();
        }
    }
}
