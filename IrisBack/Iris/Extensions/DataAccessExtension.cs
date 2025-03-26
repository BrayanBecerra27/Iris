using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2;
using Amazon.Runtime;
using FluentValidation;
using Iris.Validators;
using IrisCore.DTOs;
using IrisCore.InterfacesRepositories;
using IrisCore.Services.Implementations;
using IrisCore.Services.Interfaces;
using IrisInfraestructure.Repositories;

namespace Iris.Extensions
{
    internal static class DataAccessExtension
    {
        internal static void AddDataAccess(this IServiceCollection services)
        {
            var awsAccessKeyId = Environment.GetEnvironmentVariable("AWS_ACCESS_KEY_ID");
            var awsSecretAccessKey = Environment.GetEnvironmentVariable("AWS_SECRET_ACCESS_KEY");
            var credentials = new BasicAWSCredentials(awsAccessKeyId, awsSecretAccessKey);
            var config = new AmazonDynamoDBConfig { RegionEndpoint = Amazon.RegionEndpoint.USEast2 };
            var dynamoDbClient = new AmazonDynamoDBClient(credentials, config);

            services.AddSingleton<IAmazonDynamoDB>(dynamoDbClient);
            services.AddSingleton<DynamoDBContext>(sp =>
            {
                var client = sp.GetRequiredService<IAmazonDynamoDB>();
                return new DynamoDBContext(client);
            });
        }
    }
}
