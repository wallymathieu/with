require 'albacore'
require 'nuget_helper'

$dir = File.dirname(__FILE__)
$sln = File.join($dir, "src", "With.sln")

desc "build using msbuild"
build :build do |msb|
  msb.prop :configuration, :Debug
  msb.target = [:Rebuild]
  msb.logging = 'minimal'
  msb.sln = $sln
end

build :build_release do |msb|
  msb.prop :configuration, :Release
  msb.target = [:Rebuild]
  msb.logging = 'minimal'
  msb.sln = $sln
end

task :core_copy_to_nuspec => [:build_release] do
  output_directory_lib = File.join($dir,"nuget/lib/40/")
  mkdir_p output_directory_lib
  ['With'].each{ |project|
    cp Dir.glob("./src/#{project}/bin/Release/With.*"), output_directory_lib
  }
end

def paket params
  NugetHelper.run_tool("#{File.join($dir,'.paket/paket.exe')} #{params}")
end

desc "bootstrap"
task :bootstrap do
  NugetHelper.run_tool("#{File.join($dir,'.paket/paket.bootstrapper.exe')}")
end

desc "create the nuget package"
task :pack => [:core_copy_to_nuspec] do |nuget|
  cd File.join($dir, "nuget") do
    NugetHelper.exec "pack With.nuspec"
  end
end

desc "Install missing NuGet packages."
task :restore => :bootstrap do
  paket "restore"
end

desc "Install NuGet packages."
task :install => :bootstrap do
  paket "install"
end

desc "Push to NuGet."
task :push => :pack do
  latest = NugetHelper.last_version(Dir.glob('./nuget/With*.nupkg'))
  NugetHelper.exec("push #{latest}")
end

desc "test using console"
test_runner :test => [:build] do |runner|
  runner.exe = NugetHelper.xunit_clr4_path
  files = Dir.glob(File.join($dir, "**", "bin", "Debug", "Tests.dll"))
  runner.files = files 
end

require_relative 'templates/with'
desc "Generate switch extensions"
task :generate do |t|
  file = File.join($dir,"src","With","Switch_generated.cs")
  File.open(file,"w") do |f|
    f<<With.new().to_s
  end
end
