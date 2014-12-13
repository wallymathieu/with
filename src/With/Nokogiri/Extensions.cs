using System.Linq;

namespace With.Nokogiri
{
    internal static class NokogiriExtensions
    {
        public static string Text(this Nokogiri.Node[] self)
        {
            return string.Join("", self
                .Where(node=>node.IsText)
                .Select(node => node.Text));
        }
    }
}