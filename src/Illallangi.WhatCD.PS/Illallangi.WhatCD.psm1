$DebugPreference="Continue"
$WarningPreference="Inquire"

if ($null -eq (Get-Module PSCompletion))
{
	Import-Module PSCompletion -Global -ErrorAction SilentlyContinue
	if ($null -eq (Get-Module PSCompletion))
	{
		Write-Warning "PSCompletion module not found; tab completion will be unavailable."
	}
}

if ($null -ne (Get-Module PSCompletion))
{

}

function Check-DelugeTorrent
{
	param(
		[Parameter(ValueFromPipelineByPropertyName =$true)]
		[string]$Hash,
		[Parameter(ValueFromPipelineByPropertyName =$true)]
		[string]$Name,
		[Parameter(ValueFromPipelineByPropertyName =$true)]
		[string]$TrackerHost
	)

	Process
	{
		$Title = $Name

		if ($TrackerHost -eq "what.cd")
		{
			$Name = $Name -replace '\.mp3$', ''
			$Title = (Get-WhatCdTorrent -Hash "$($Hash)").Title
		}

		if ($TrackerHost -eq "broadcasthe.net" -or $TrackerHost -eq "landof.tv")
		{
			$Title = $Name.ToLowerInvariant().Replace(".internal","")
		}

		if ($Title -cne $Name)
		{
			$Title | clip
			Write-Warning "$($Hash): Should be named ""$($Title)"" (is named ""$($Name)"")"
			New-Object System.Object | 
				Add-Member -type NoteProperty -name Hash -value $Hash -PassThru |
				Add-Member -type NoteProperty -name Name -value $Name -PassThru |
				Add-Member -type NoteProperty -name Title -value $Title -PassThru
		}
		else
		{
			Write-Debug "$($Hash): Correctly named ""$($Title)"""
		}
	}
}
