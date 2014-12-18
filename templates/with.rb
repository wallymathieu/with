require_relative 'match_switch'

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
