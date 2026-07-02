# 设置 VS 方案：Project Reference
$src = "D:\WEB\CAR_MOD\src"

$exeLibDeps = @("CarPark.Core","DAT.Entity","N910POSDll","CarPark.DataStore",
    "CarPark.Device.SerialComm","CarPark.DB","Master.SystemCommunication.Lib",
    "Master.SystemCommunication","CarPark.Lib","CarPark.Device","CarPark.Device.CashierBusiness")

$libDeps = @{}
$libDeps["CarPark.DB"] = @("CarPark.Core")
$libDeps["Master.SystemCommunication.Lib"] = @("CarPark.Core")
$libDeps["Master.SystemCommunication"] = @("CarPark.DB","CarPark.Core","Master.SystemCommunication.Lib")
$libDeps["CarPark.Lib"] = @("CarPark.DB")
$libDeps["CarPark.Device"] = @("CarPark.Core","CarPark.DB","DAT.Entity","N910POSDll")
$libDeps["CarPark.Device.CashierBusiness"] = @("CarPark.Device.SerialComm","CarPark.Device","N910POSDll","CarPark.Core","Master.SystemCommunication.Lib","CarPark.DB","Master.SystemCommunication")

$libDirNames = $exeLibDeps

# Step 1: 更新 Library csproj
Write-Host "=== Step 1: 更新 Library csproj ==="
foreach ($name in $libDirNames) {
    $cp = "$src\$name\$name.csproj"
    if (-not (Test-Path $cp)) { Write-Host "  FAIL $name.csproj 不存在"; continue }
    
    $content = Get-Content $cp -Encoding UTF8 -Raw
    $deps = $libDeps[$name]
    if ($deps) {
        foreach ($dep in $deps) {
            $escName = [regex]::Escape($dep)
            $pattern = '<Reference Include="' + $escName + '">\s*<HintPath>[^<]+</HintPath>\s*</Reference>'
            $repl = '<ProjectReference Include="..\' + $dep + '\' + $dep + '.csproj" />'
            $content = $content -replace $pattern, $repl
        }
    }
    $content = $content -replace '\.\.\\\\\.\.\\\\\.\.\\\\(CAR\\)', '..\..\..\$1'
    $content = $content -replace '\\\\CAR\\\\', '\lib\'
    $content = $content -replace '\\\.\\.\\\\\.\.\\\\\.\.\\\\(lib\\)', '..\..\..\$1'
    $content = [regex]::Replace($content, '\.\.\\\.\.\\\.\.\\CAR\\', '..\..\..\lib\')
    Set-Content -Path $cp -Value $content -Encoding UTF8
    Write-Host "  OK $name.csproj"
}

# Step 2: 更新主程式 csproj
Write-Host "=== Step 2: 更新 CarPark2018.csproj ==="
$mainCp = "$src\CarPark2018\CarPark2018.csproj"
$content = Get-Content $mainCp -Encoding UTF8 -Raw

foreach ($dep in $exeLibDeps) {
    $escName = [regex]::Escape($dep)
    $pattern = '<Reference Include="' + $escName + '">\s*<HintPath>[^<]+</HintPath>\s*</Reference>'
    $repl = '<ProjectReference Include="..\' + $dep + '\' + $dep + '.csproj" />'
    $content = $content -replace $pattern, $repl
}

# 更新路徑 ..\..\..\CAR\ 改為 ..\..\..\lib\
# 但 BOC_SmartPay 也要改到 lib\
$content = [regex]::Replace($content, '\.\.\\\.\.\\\.\.\\CAR\\', '..\..\..\lib\')

Set-Content -Path $mainCp -Value $content -Encoding UTF8
Write-Host "  OK CarPark2018.csproj"

# Step 3: 建立 .sln
Write-Host "=== Step 3: 建立 CarPark2018.sln ==="
$projects = @()
$projects += [PSCustomObject]@{Name="CarPark.Core";Guid="A1B2C3D4-E5F6-7890-ABCD-EF1234567801"}
$projects += [PSCustomObject]@{Name="DAT.Entity";Guid="A1B2C3D4-E5F6-7890-ABCD-EF1234567802"}
$projects += [PSCustomObject]@{Name="N910POSDll";Guid="A1B2C3D4-E5F6-7890-ABCD-EF1234567803"}
$projects += [PSCustomObject]@{Name="CarPark.DataStore";Guid="A1B2C3D4-E5F6-7890-ABCD-EF1234567804"}
$projects += [PSCustomObject]@{Name="CarPark.Device.SerialComm";Guid="A1B2C3D4-E5F6-7890-ABCD-EF1234567805"}
$projects += [PSCustomObject]@{Name="CarPark.DB";Guid="A1B2C3D4-E5F6-7890-ABCD-EF1234567806"}
$projects += [PSCustomObject]@{Name="Master.SystemCommunication.Lib";Guid="A1B2C3D4-E5F6-7890-ABCD-EF1234567807"}
$projects += [PSCustomObject]@{Name="Master.SystemCommunication";Guid="A1B2C3D4-E5F6-7890-ABCD-EF1234567808"}
$projects += [PSCustomObject]@{Name="CarPark.Lib";Guid="A1B2C3D4-E5F6-7890-ABCD-EF1234567809"}
$projects += [PSCustomObject]@{Name="CarPark.Device";Guid="A1B2C3D4-E5F6-7890-ABCD-EF1234567810"}
$projects += [PSCustomObject]@{Name="CarPark.Device.CashierBusiness";Guid="A1B2C3D4-E5F6-7890-ABCD-EF1234567811"}
$projects += [PSCustomObject]@{Name="CarPark2018";Guid="A1B2C3D4-E5F6-7890-ABCD-EF1234567812"}

$sln = "Microsoft Visual Studio Solution File, Format Version 12.00`r`n"
$sln += "# Visual Studio Version 17`r`n"
$sln += "VisualStudioVersion = 17.0.31903.59`r`n"
$sln += "MinimumVisualStudioVersion = 10.0.40219.1`r`n"
foreach ($p in $projects) {
    $path = "src\$($p.Name)\$($p.Name).csproj"
    $sln += "Project(`"{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}`") = `"$($p.Name)`", `"$path`", `"{$($p.Guid)}`"`r`n"
    $sln += "EndProject`r`n"
}
$sln += "Global`r`n"
$sln += "	GlobalSection(SolutionConfigurationPlatforms) = preSolution`r`n"
$sln += "		Debug|x86 = Debug|x86`r`n"
$sln += "		Release|x86 = Release|x86`r`n"
$sln += "	EndGlobalSection`r`n"
$sln += "	GlobalSection(ProjectConfigurationPlatforms) = postSolution`r`n"
foreach ($p in $projects) {
    $g = $p.Guid
    $sln += "		{$g}.Debug|x86.ActiveCfg = Debug|x86`r`n"
    $sln += "		{$g}.Debug|x86.Build.0 = Debug|x86`r`n"
    $sln += "		{$g}.Release|x86.ActiveCfg = Release|x86`r`n"
    $sln += "		{$g}.Release|x86.Build.0 = Release|x86`r`n"
}
$sln += "	EndGlobalSection`r`n"
$sln += "EndGlobal`r`n"

Set-Content -Path "D:\WEB\CAR_MOD\CarPark2018.sln" -Value $sln -Encoding UTF8
Write-Host "  OK CarPark2018.sln"

# Step 4: 檢查結果
Write-Host "=== Step 4: 檢查 ==="
$hasIssue = $false
$allDlls = $exeLibDeps + @("BOC_SmartPay")
foreach ($name in $libDirNames) {
    $cp = "$src\$name\$name.csproj"
    $c = Get-Content $cp -Encoding UTF8 -Raw
    foreach ($d in $allDlls) {
        $p = '<Reference Include="' + $d + '">'
        if ($c -match [regex]::Escape($p)) {
            Write-Host "  WARN $name.csproj 仍有: $d"
            $hasIssue = $true
        }
    }
}
$mainC = Get-Content $mainCp -Encoding UTF8 -Raw
$bocOk = $mainC -match '<Reference Include="BOC_SmartPay">'
if ($hasIssue) { Write-Host "  請檢查問題" }
elseif ($bocOk) { Write-Host "  OK 全部 Library 已改 ProjectReference, BOC_SmartPay 保留檔案參考" }
else { Write-Host "  WARN BOC_SmartPay 參考遺失" }

Write-Host "`n=== 完成 ==="
Write-Host "用 VS2022 開啟 D:\WEB\CAR_MOD\CarPark2018.sln"
Write-Host "在方案總管右鍵方案 -> 設定啟始專案 -> CarPark2018"
Write-Host "工具列選 Release|x86 再按 F5 編譯"
