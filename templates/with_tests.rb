require_relative 'with'
require "minitest/autorun"

class TestWith < Minitest::Test
    def setup
        @w=With.new(1)
    end
    def test_can_generate_match_switch_1
        puts @w.to_s()
    end

end