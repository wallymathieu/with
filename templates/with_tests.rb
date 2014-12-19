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
    expected = to_lines("namespace With
{
using System;
using System.Collections.Generic;
using With.SwitchPlumbing;

    public static partial class Switch
    {

    public static ISwitch<In, Out> Match
        <In, Out>(In value, 
               IEnumerable<In> i0, Func<In, Out> f0,
               IEnumerable<In> i1, Func<In, Out> f1)
    {
        return new Switch<In, Out>().Tap(c => c.Instance = value)
               .Case(i0, f0)
               .Case(i1, f1);
    }

    public static ISwitch<In, Out> Match
        <In, Out>(
               IEnumerable<In> i0, Func<In, Out> f0,
               IEnumerable<In> i1, Func<In, Out> f1)
    {
        return new Switch<In, Out>()
               .Case(i0, f0)
               .Case(i1, f1);
    }

    }
}")
    #puts @w.to_s()
    assert_equal expected, to_lines(@w.to_s())
  end

end


class TestTupleExtensions < Minitest::Test
  def setup
    @w=TupleExtensions.new(1)
  end
  def to_lines(lines)
    lines.strip.split("\n").map do |l| l.strip end.to_a
  end

  def test_can_generate_match_switch_1
    expected = to_lines("
namespace With.Destructure
{
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using With.Destructure;
using With.SwitchPlumbing;
    public static partial class Switch_Tuples
    {
        public static Tuple<T0> Let<T0>(
this Tuple<T0> that, Action<T0> action)
        {
            action(that.Item1);
            return that;
        }
        public static Tuple<T0, T1> Let<T0, T1>(
this Tuple<T0, T1> that, Action<T0, T1> action)
        {
            action(that.Item1, that.Item2);
            return that;
        }
        public static TRet Let<T0, TRet>(
this Tuple<T0> that, Func<T0, TRet> func)
        {
            return func(that.Item1);
        }
        public static TRet Let<T0, T1, TRet>(
this Tuple<T0, T1> that, Func<T0, T1, TRet> func)
        {
            return func(that.Item1, that.Item2);
        }
    }
}")
    #puts @w.to_s()
    assert_equal expected, to_lines(@w.to_s())
  end

end

