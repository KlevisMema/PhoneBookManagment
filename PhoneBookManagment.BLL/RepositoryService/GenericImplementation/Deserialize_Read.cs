using Newtonsoft.Json;

namespace PhoneBookManagment.BLL.RepositoryService.GenericImplementation
{
    public static class Deserialize_Read<T> where T : class
    {
        public static List<T> DesirializeRead(string text)
        {
            var readFromFile = File.ReadAllText(text);
            var jsonConvert = JsonConvert.DeserializeObject<List<T>>(readFromFile)?.ToList();

            return jsonConvert;
        }
    }
}
