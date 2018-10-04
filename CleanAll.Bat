
rmdir /S /Q Debug
rmdir /S /Q Release
rmdir /S /Q dist
rmdir /S /Q TestResults
del /F /S *.sdf
del /F /S /A:H *.suo
del /Q InnerWorkings\JobFilesOutput\*
del /Q InnerWorkingsTests\JobFilesOutput\*
rmdir /S /Q InnerWorkings\bin
rmdir /S /Q InnerWorkings\obj
rmdir /S /Q InnerWorkingsTests\bin
rmdir /S /Q InnerWorkingsTests\obj