class With
    def initialize(num)
        @num = num
        @m = MatchSwitch.new
    end
    def header
          "
using System;
using System.Collections.Generic;
using With.SwitchPlumbing;

namespace With
{
    public static partial class Switch
    {"
    end
    def footer
"
    }
}"
    end
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
    def to_s
        matches = (1..@num).map do |n|
            @m.get_match_switch_tap(n)
        end
        matches += (1..@num).map do |n|
            @m.get_match_switch(n)
        end
        n = "\n"
        "#{header}\n#{matches.join(n)}\n#{footer}"
    end
end
if __FILE__ == $0
    require "test/unit"

    class TestWith < Test::Unit::TestCase
        def setup
            @w=With.new(1)
        end
        def test_can_generate_match_switch_1
            puts @w.to_s()
        end

    end
end