require 'albacore'
require_relative './src/.nuget/nuget'

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
    NuGet::exec "pack With.nuspec"
  end
end

desc "Install missing NuGet packages."
task :install_packages do
  package_paths = FileList["src/**/packages.config"]+["src/.nuget/packages.config"]

  package_paths.each.each do |filepath|
      NuGet::exec("i #{filepath} -o ./src/packages -source http://www.nuget.org/api/v2/")
  end
end

desc "test using nunit console"
test_runner :test => [:build] do |nunit|
  nunit.exe = NuGet::xunit_path
  files = [File.join(File.dirname(__FILE__),"src","Tests","bin","Debug","Tests.dll")]
  nunit.files = files 
end

