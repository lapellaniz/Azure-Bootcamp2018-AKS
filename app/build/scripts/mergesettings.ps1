## get script path
$ScriptPath = $(Split-Path -Parent $MyInvocation.MyCommand.Definition) 
$ScriptPath

## load newtonsoft
[Reflection.Assembly]::LoadFile("$ScriptPath\lib\Newtonsoft.Json.dll")

## get json files
Get-ChildItem -Path appsettings.json -Recurse -Force
Get-ChildItem -Path appsettings.deploy.json -Recurse -Force
Get-ChildItem -Path appsettings.deploy.secrets.json -Recurse -Force
$localJson =  (Get-ChildItem -Path appsettings.json -Recurse -Force) | Get-Content | Out-String
$deployJson =  (Get-ChildItem -Path appsettings.deploy.json -Recurse -Force) | Get-Content | Out-String
#$secretsJson =  (Get-ChildItem -Path appsettings.deploy.secrets.json -Recurse -Force) | Get-Content | Out-String

$localJson
$deployJson

## parse json
$local = [Newtonsoft.Json.Linq.JObject]::Parse($localJson) # parse string
$deploy = [Newtonsoft.Json.Linq.JObject]::Parse($deployJson) # parse string
#$secrets = [Newtonsoft.Json.Linq.JObject]::Parse($secretsJson) # parse string

## configure merge settings
$mergeSettings = New-Object -TypeName Newtonsoft.Json.Linq.JsonMergeSettings
$mergeSettings.MergeArrayHandling = [Newtonsoft.Json.Linq.MergeArrayHandling]::Union

## override local with deploy
$local.Merge($deploy, $mergeSettings)

New-Item -Path deploy\config -ItemType directory -Force
$local.ToString() | Out-File "deploy\kube\config\appsettings.json" -Encoding utf8
(Get-ChildItem -Path "deploy\kube\config\appsettings.json" -Recurse -Force) | Get-Content | Out-String