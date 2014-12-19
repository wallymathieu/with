require_relative 'with'
require "minitest/autorun"

class TestWith < Minitest::Test
  def setup
    @w=With.new(1)
  end
  def test_can_generate_match_switch_1
    v = @w.to_s()
  end
end

class TestMatchSwitch < Minitest::Test
  def setup
    @w=MatchSwitch.new(1)
  end
  def to_lines(lines)
    lines.strip.split("\n").map do |l| l.strip end.to_a
  end

  def test_can_generate_match_switch_1
    expected = to_lines("    public static IMatchSwitch<In, Out> Match
      <In, Out>(In value, 
             IEnumerable<In> i0, Func<In, Out> f0,
             IEnumerable<In> i1, Func<In, Out> f1)
  {
      return new MatchSwitch<In, Out>().Tap(c => c.Instance = value)
             .Case(i0, f0)
             .Case(i1, f1);
  }

  public static IMatchSwitch<In, Out> Match
      <In, Out>(
             IEnumerable<In> i0, Func<In, Out> f0,
             IEnumerable<In> i1, Func<In, Out> f1)
  {
      return new MatchSwitch<In, Out>()
             .Case(i0, f0)
             .Case(i1, f1);
  }")
    assert_equal expected, to_lines(@w.to_s())
  end

end

