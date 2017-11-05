using Newtonsoft.Json;
namespace Tests.Json
{
    public static class Json
    {
        public static string ToJson(this object self)=>
            JsonConvert.SerializeObject(self);
    }
}
