using Newtonsoft.Json;

namespace PhoneBookManagment.BLL.RepositoryService.GenericImplementation
{
    public static class Serialize_Write<T> where T : class
    {
        public static void SerializeWriteOnFile(List<T> obj, string text)
        {
            var writeOnFile = JsonConvert.SerializeObject(obj, Formatting.Indented);
            File.WriteAllText(text, writeOnFile);
        }
    }
}
