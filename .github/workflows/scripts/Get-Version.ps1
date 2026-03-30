param(
    [string]$StartDir = (Get-Location).Path
)

function Get-VersionFromProps {
    param([string]$File)

    if (-not (Test-Path $File)) { return $null }

    try {
        $xml = [xml](Get-Content $File -Raw)
        return $xml.Project.PropertyGroup.Version
    }
    catch {
        return $null
    }
}

function Get-VersionFromCsproj {
    param([string]$File)

    if (-not (Test-Path $File)) { return $null }

    try {
        $xml = [xml](Get-Content $File -Raw)
        $pg = $xml.Project.PropertyGroup

        return @(
            $pg.Version,
            $pg.VersionPrefix,
            $pg.VersionSuffix
        ) | Where-Object { $_ } | Select-Object -First 1
    }
    catch {
        return $null
    }
}

# --- 1) Suche aufwärts nach Directory.Build.props ---
$current = Resolve-Path $StartDir

$propsFile = $null
while ($current) {
    $candidate = Join-Path $current "Directory.Build.props"
    if (Test-Path $candidate) {
        $propsFile = $candidate
        break
    }

    $parent = Split-Path $current -Parent
    if ($parent -eq $current) { break }
    $current = $parent
}

# --- 2) Falls gefunden: Version lesen ---
$version = $null
if ($propsFile) {
    $version = Get-VersionFromProps $propsFile
}

# --- 3) Falls keine Version: nächstes csproj suchen ---
if (-not $version) {
    $csproj = Get-ChildItem -Path $StartDir -Recurse -Filter *.csproj -ErrorAction SilentlyContinue |
    Select-Object -First 1

    if ($csproj) {
        $version = Get-VersionFromCsproj $csproj.FullName
    }
}

# --- Ergebnis ---
if ($version) {
    Write-Output $version
}
else {
    Write-Warning "Keine Version in Directory.Build.props oder csproj gefunden."
}
