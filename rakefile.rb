require 'albacore'

dir = File.dirname(__FILE__)

desc "build using msbuild"
build :build do |msb|
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
    sh "..\\src\\.nuget\\NuGet.exe pack With.nuspec"
  end
end

