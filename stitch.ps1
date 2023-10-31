# Install ImageMagick via scoop
# scoop install imagemagick

# Check if ImageMagick is installed
if (-not (Test-Path -Path (Get-Command magick.exe -ErrorAction SilentlyContinue))) {
    Write-Host "ImageMagick is not installed. Please install it first."
    exit 1
}

# Get a list of PNG files in the current working directory
$inputFiles = Get-ChildItem -Filter *.png

# Check if there are any PNG files
if ($inputFiles.Count -eq 0) {
    Write-Host "No PNG files found in the current directory."
    exit 1
}

# Output file name
$outputFile = "tile_set.png"

# Use ImageMagick's convert to stitch the PNG files together horizontally
$cmd = "magick.exe convert " + ($inputFiles | ForEach-Object { $_.Name }) + " +append $outputFile"
Invoke-Expression $cmd

# Check if the operation was successful
if ($LASTEXITCODE -eq 0) {
    Write-Host "Images stitched together successfully. Result saved as $outputFile."
} else {
    Write-Host "Image stitching failed."
    exit 1
}

