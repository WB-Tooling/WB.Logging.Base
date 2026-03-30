param(
    [string]$FilePath
)

if (-not (Test-Path -Path $FilePath)) {
    Write-Output "File not found: $FilePath"
    exit 1
}

[xml]$xml = Get-Content -Path $FilePath
$node = $xml.SelectSingleNode("//Project/PropertyGroup/Version")

if ($null -ne $node) {
    $version = $node.InnerText
    return $version
} else {
    Write-Output "Version not found in $FilePath"
    exit 1
}