#!/bin/bash

# configure the script variables

version=$1
package_dir=./package
output_dir=./output
targets_dir=targets
source_dir=../SDK/Source
# create or clear a working and package dirs

rm -rf $working_dir && mkdir $working_dir 
rm -rf $package_dir && mkdir $package_dir
rm -rf $output_dir && mkdir $output_dir

core_proj_name=Virgil.SDK.Std
proj1=( "SecureStorage.Droid"  'android' )
proj2=( "SecureStorage.iOS"    'ios'     )
proj3=( "SecureStorage.OSX"    'osx'     )
proj4=( "SecureStorage.Win"    'win'     )
proj5=( "SecureStorage.Mac"    'osx'     )

# fill package structure to package dir
mkdir $package_dir/lib && mkdir $package_dir/lib/netstandard1.1
mkdir $package_dir/runtimes
mkdir $package_dir/build
cp -rf $targets_dir/* $package_dir/build/
#

# build netstandard project and copy to lib
msbuild $source_dir/$core_proj_name/$core_proj_name.csproj /t:Rebuild  /p:Configuration=Release
cp $source_dir/$core_proj_name/bin/Release/netstandard1.1/Virgil.SDK.dll $package_dir/lib/netstandard1.1/


# run loop through all projects and build storages.

for i in ${!proj@}; do
    eval lib=( \${$i[@]} )

    proj_name=${lib[0]}  
    echo "AAAAAAAAAAAA" + $proj_name
    platfrom_name=${lib[1]}

	mkdir $package_dir/runtimes/$platfrom_name && mkdir $package_dir/runtimes/$platfrom_name/lib

    # build storage project for specific platfrom
    msbuild $source_dir/Storages/$proj_name/$proj_name.csproj /t:Rebuild  /p:Configuration=Release
    cp $source_dir/Storages/$proj_name/bin/Release/$proj_name.dll $package_dir/runtimes/$platfrom_name/lib/
	if [ $proj_name == ${proj3[0]} ]; then
	   	cp $source_dir/Storages/$proj_name/bin/Release/netstandard1.1/$proj_name.dll $package_dir/runtimes/$platfrom_name/lib/
	fi	
done

cp -r ./template.nuspec $package_dir/Virgil.SDK-$version.nuspec
sed -i '' "s/%version%/$version/g" $package_dir/Virgil.SDK-$version.nuspec

nuget pack $package_dir/Virgil.SDK-$version.nuspec  -OutputDirectory $output_dir

rm -rf $working_dir
#rm -rf $package_dir



