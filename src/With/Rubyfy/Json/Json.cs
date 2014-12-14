using System.Web.Script.Serialization;
namespace With.Rubyfy.Json
{
    public static class Json
    {
        private static JavaScriptSerializer _serializer;
        static Json()
        {
            _serializer = new JavaScriptSerializer();
        }
        public static string ToJson(this object self)
        {
            return _serializer.Serialize(self);
        }
    }
}
