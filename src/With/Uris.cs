using System;
namespace With
{
    /// <summary>
    /// Uri extensions
    /// </summary>
    public static class Uris
    {
        /// <summary>
        /// Create a new Uri based on base url and relative path. Throws an 
        /// argument exception if the values can't composed to an Uri.
        /// </summary>
        public static Uri Create (string baseUrl, string relativePath)
        {
            Uri res;
            if (Uri.TryCreate (new Uri (baseUrl), relativePath, out res)) return res;
            throw new ArgumentException ($"Could not create uri! baseUrl: {baseUrl} relativePath: {relativePath}");
        }
    }
}
