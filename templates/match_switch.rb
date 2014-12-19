class MatchSwitch
  def initialize(num)
    @num = num
  end
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
    public static ISwitch<In, Out> Match
        <In, Out>(In value, 
#{input})
    {
        return new Switch<In, Out>().Tap(c => c.Instance = value)
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
    public static ISwitch<In, Out> Match
        <In, Out>(
#{input})
    {
        return new Switch<In, Out>()
#{cases};
    }"  
  end
  def header
          "namespace With
{
using System;
using System.Collections.Generic;
using With.SwitchPlumbing;

    public static partial class Switch
    {"
  end
  def footer
"
    }
}"
  end
  def body
    matches = (1..@num).map do |n|
      get_match_switch_tap(n)
    end
    matches += (1..@num).map do |n|
      get_match_switch(n)
    end
    return matches.join("\n")
  end

  def to_s
      "#{header}\n#{body}\n#{footer}"
  end
end
