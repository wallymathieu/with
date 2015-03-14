require 'albacore'
require 'nuget_helper'

dir = File.dirname(__FILE__)

desc "build using msbuild"
build :build => [:install_packages] do |msb|
  msb.prop :configuration, :Debug
  msb.target = [:Rebuild]
  msb.logging = 'minimal'
  msb.sln =File.join(dir, "src", "With.sln")
end

task :core_copy_to_nuspec => [:build] do
  output_directory_lib = File.join(dir,"nuget/lib/40/")
  mkdir_p output_directory_lib
  ['With'].each{ |project|
    cp Dir.glob("./src/#{project}/bin/Debug/*.dll"), output_directory_lib
  }
  
end

desc "create the nuget package"
task :nugetpack => [:core_nugetpack]

task :core_nugetpack => [:core_copy_to_nuspec] do |nuget|
  cd File.join(dir,"nuget") do
    NugetHelper.exec "pack With.nuspec"
  end
end

task :nugetpush => [:nugetpack] do |nuget|
  cd File.join(dir,"nuget") do
    last = Dir.glob("With.*.nupkg").last
    NugetHelper.exec "push #{last}"
  end
end


desc "Install missing NuGet packages."
task :install_packages do
    NugetHelper.exec("restore ./src/with.sln -source http://www.nuget.org/api/v2/")
end

desc "test using console"
test_runner :test => [:build] do |runner|
  runner.exe = NugetHelper.xunit_path
  files = [File.join(File.dirname(__FILE__),"src","Tests","bin","Debug","Tests.dll")]
  runner.files = files 
end

require_relative 'templates/with'
desc "Generate switch extensions"
task :generate do |t|
  file = File.join(File.dirname(__FILE__),"src","With","Switch_generated.cs")
  File.open(file,"w") do |f|
    f<<With.new().to_s
  end
end
