class TupleExtensions
  def initialize(num)
    @num = num
  end
  def let_action num
    types = (0..num).map do |n| "T#{n}" end.join(", ")
    items = (0..num).map do |n| "that.Item#{n+1}" end.join(", ")
    "        public static Tuple<#{types}> Let<#{types}>(
this Tuple<#{types}> that, Action<#{types}> action)
        {
            action(#{items});
            return that;
        }"
  end
  def let_func num
    types = (0..num).map do |n| "T#{n}" end.join(", ")
    items = (0..num).map do |n| "that.Item#{n+1}" end.join(", ")
    "        public static TRet Let<#{types}, TRet>(
this Tuple<#{types}> that, Func<#{types}, TRet> func)
        {
            return func(#{items});
        }"
  end
  def header
    "namespace With.Destructure
{
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using With.Destructure;
using With.SwitchPlumbing;
    public static partial class Switch_Tuples
    {"
  end
  def footer
    "    }
}"
  end
  def body
    actions_and_funcs = (0..@num).map do |n|
        let_action(n)
    end
    actions_and_funcs += (0..@num).map do |n|
        let_func(n)
    end
    return actions_and_funcs.join("\n")
  end

  def to_s
    "#{header}\n#{body}\n#{footer}"
  end
end
