// Reference https://dusted.codes/dotenv-in-dotnet

namespace DBAPI.Services
{
    public class DotEnv
    {
        public static void Load(string filePath) {
            if (!File.Exists(filePath)) {
                return;
            }

            foreach (var line in File.ReadAllLines(filePath)) {
                var parts = line.Split('=', 
                    StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length != 2) {
                    continue;
                }

                var key = parts[0].Trim();
                var value = parts[1].Trim();
                Environment.SetEnvironmentVariable(key, value);
            }
        }
    }
}