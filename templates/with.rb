require_relative 'match_switch'
require_relative 'tuple_extensions'
class With
  def initialize()
    @m = MatchSwitch.new(6)
    @t = TupleExtensions.new(6)
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
