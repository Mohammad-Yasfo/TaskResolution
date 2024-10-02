using System.Diagnostics.CodeAnalysis;

namespace TaskResolution.Application.Core.Configuration
{
    [ExcludeFromCodeCoverage]
    public class DbConnectionConfiguration
    {
        public string StudentsConnectionString { get; set; }
        public bool AzureIdentity { get; set; }
    }
}