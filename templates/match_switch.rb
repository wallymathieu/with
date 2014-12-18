class MatchSwitch
    def get_array_to_func_pair(n)
"               IEnumerable<In> i#{n}, Func<In, Out> f#{n}"
    end
    def get_case(n)
"               .Case(i#{n}, f#{n})"
    end

    def get_match_switch_tap(num)
        input = (0..num).map do |n|
            get_array_to_func_pair(n)
        end.join(",\n")
        cases = (0..num).map do |n|
            get_case(n)
        end.join("\n")
    "
    public static IMatchSwitch<In, Out> Match
        <In, Out>(In value, 
#{input})
    {
        return new MatchSwitch<In, Out>().Tap(c => c.Instance = value)
#{cases};
    }"
    end
    def get_match_switch(num)
        input = (0..num).map do |n|
            get_array_to_func_pair(n)
        end.join(",\n")
        cases = (0..num).map do |n|
            get_case(n)
        end.join("\n")
    "
    public static IMatchSwitch<In, Out> Match
        <In, Out>(
#{input})
    {
        return new MatchSwitch<In, Out>()
#{cases};
    }"  
    end
end