using With.SwitchPlumbing;

namespace With
{
    public static partial class Switch
    {
        public static SwitchWithInstance<In, Nothing> Match<In>(In value)
        {
            return new SwitchWithInstance<In,Nothing>(value, new Switch<In, Nothing>());
        }
        public static SwitchWithInstance<In, Nothing> On<In>(In value)
        {
            return new SwitchWithInstance<In,Nothing>(value, new Switch<In, Nothing>());
        }

        public static SwitchWithInstance<In, Out> Match<In, Out>(In value)
        {
            return new SwitchWithInstance<In,Out>(value, new Switch<In, Out>());
        }
        public static SwitchWithInstance<In, Out> On<In, Out>(In value)
        {
            return new SwitchWithInstance<In,Out>(value, new Switch<In, Out>());
        }

        public static Switch<In, Prepared> Match<In>()
        {
            return new Switch<In, Prepared>();
        }
        public static Switch<In, Prepared> On<In>()
        {
            return new Switch<In, Prepared>();
        }

        public static Switch<In, Out> Match<In, Out>()
        {
            return new Switch<In, Out>();
        }
        public static Switch<In, Out> On<In, Out>()
        {
            return new Switch<In, Out>();
        }
    }
}
