require 'albacore'
require 'version_bumper'

task :deploy => [:zip, :nup] do
end

zip :zip => :output do | zip |
	Dir.mkdir("build") unless Dir.exists?("build")
    zip.directories_to_zip "out"
    zip.output_file = "SimpleCrypto.v#{bumper_version.to_s}.zip"
    zip.output_path = "build"
end

output :output => :test do |out|
	out.from '.'
	out.to 'out'
	out.file 'src/bin/release/SimpleCrypto.dll', :as=>'SimpleCrypto.dll'
	out.file 'LICENSE.txt'
	out.file 'README.md'
end

desc "Test"
nunit :test => :build do |nunit|
	nunit.command = "tools/NUnit/nunit-console.exe"
	nunit.assemblies "tests/bin/release/SimpleCrypto.Tests.dll"
end

desc "Build"
msbuild :build => :assemblyinfo do |msb|
  msb.properties :configuration => :Release
  msb.targets :Clean, :Build
  msb.solution = "SimpleCrypto.sln"
end

##This does not work from albacore.
#nugetpack :nup => :nus do |nuget|
#   nuget.command     = "tools/NuGet/NuGet.exe"
#   nuget.nuspec      = "SimpleCrypto.nuspec"
#   nuget.base_folder = "out/"
#   nuget.output      = "build/"
#end

##use this until patched
task :nup => :nus do
	sh "tools/NuGet/NuGet.exe pack -BasePath out/ -Output build/ out/SimpleCrypto.nuspec"
end

nuspec :nus => :output do |nuspec|
   nuspec.id="SimpleCrypto"
   nuspec.version = bumper_version.to_s
   nuspec.authors = "Shawn Mclean"
   nuspec.description = "Simple cryptography library that wraps complex hashing algorithms for quick and simple usage."
   nuspec.title = "SimpleCrypto.Net"
   nuspec.language = "en-US"
   nuspec.projectUrl = "https://github.com/ShawnMclean/SimpleCrypto.net"
   nuspec.working_directory = "out/"
   nuspec.output_file = "SimpleCrypto.nuspec"
   nuspec.file "SimpleCrypto.dll", "lib"
end


assemblyinfo :assemblyinfo do |asm|
  asm.version = bumper_version.to_s
  asm.file_version = bumper_version.to_s

  asm.company_name = "Xormis"
  asm.product_name = "SimpleCrypto.Net"
  asm.copyright = "Shawn Mclean (c) 2013"
  asm.output_file = "AssemblyInfo.cs"
end