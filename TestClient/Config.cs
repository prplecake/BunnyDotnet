using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace TestClient;

public class Config
{
    public static Configuration SetupConfiguration()
    {
        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(PascalCaseNamingConvention.Instance)
            .Build();
        var config = deserializer.Deserialize<Configuration>(File.ReadAllText("settings.yml"));
        return config;
    }
}
public class Configuration
{
    public string ApiToken { get; set; }
}
