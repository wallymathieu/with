using System.Linq;

namespace With.Xml
{
    public static class Extensions
    {
        public static string Text(this Node[] self)
        {
            return string.Join("", self
                .Where(node=>node.IsText)
                .Select(node => node.Text));
        }
    }
}