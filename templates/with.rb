require_relative 'match_switch'
require_relative 'tuple_extensions'
class With
  def initialize(num)
    @num = num
    @m = MatchSwitch.new(num)
    @t = TupleExtensions.new(num)
  end
  def header
    ""
  end
  def footer
    ""
  end

  def to_s
    "#{header}\n#{@m}\n#{@t}\n#{footer}"
  end
end
