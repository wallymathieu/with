require_relative 'match_switch'
require_relative 'tuple_extensions'
class With
  def initialize(num)
    @num = num
    @m = MatchSwitch.new(num)
    @t = TupleExtensions.new(num)
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
    matches = @m.to_s
    "#{header}\n#{matches}\n#{footer}"
  end
end
