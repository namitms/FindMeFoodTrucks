
using System.Diagnostics.CodeAnalysis;

namespace FindMeFoodTrucks.Ingestor.Helpers

{
    /// <summary>
    /// Constaint strings
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class ConstantStrings
    {
        //SFAPI string
        public static string DATA_API_URL = "Configuration:DataAPIurl";

        //Cosmos Strings
        public static string COSMOS_ENDPOINT_URL = "Configuration:CosmosEndpoint";
        public static string COSMOS_PRIMARY_KEY = "Configuration:CosmosKey";
        public static string COSMOS_DATABASE_NAME = "Configuration:CosmosDBname";
        public static string COSMOS_CONTAINER_NAME = "Configuration:CosmosContainer";
    }
}
