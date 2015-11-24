@echo off
echo "Create symlink between incorrect directory and correct directory"
mklink "C:\Program Files (x86)\Microsoft Visual Studio 12.0\Common7\Redistributables" "C:\Program Files (x86)\Add-in Express\Add-in Express for .NET\Redistributables"
echo "Copy needed dll"
copy "C:\Program Files (x86)\Add-in Express\Add-in Express for .NET\Bin\AddinExpress.MSO.2005.dll" "AddinExpress.MSO.2005.dll"
copy "C:\Program Files (x86)\Add-in Express\Add-in Express for .NET\Bin\AddinExpress.XL.2005.dll" "AddinExpress.XL.2005.dll"