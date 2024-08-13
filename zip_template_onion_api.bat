@echo off
set "source_folder=C:\apps\TemplateOnionAPI"
set "zip_file=C:\apps\VSIXTemplateOnionAPI\VSIXTemplateOnionAPI\ProjectTemplates\TemplateOnionAPI.zip"

echo Zipping folder: %source_folder%
echo Saving zip file as: %zip_file%

powershell Compress-Archive -Path "%source_folder%\*" -DestinationPath "%zip_file%" -Force

echo Zip operation completed.
