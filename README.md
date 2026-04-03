# RemoveTFVCPolicy

A command-line utility that removes all TFVC (Team Foundation Version Control) check-in policies from a specified Azure DevOps project.

## Background

Microsoft has announced changes to how TFVC check-in policies are stored and validated in Azure DevOps. Existing check-in policies that were previously stored in the server-side registry are now considered obsolete and should be removed. Failing to remove these outdated policies can cause issues for developers using TFVC in affected projects.

This tool automates the removal of check-in policies across an entire TFVC project, replacing what would otherwise require manual steps through the Azure DevOps UI.

For more information on why you need to remove these policies, see the following Microsoft blog posts:

- [TFVC Policies Storage Updates](https://devblogs.microsoft.com/devops/tfvc-policies-storage-updates/)
- [TFVC – Remove Existing Obsolete Policies ASAP](https://devblogs.microsoft.com/devops/tfvc-remove-existing-obsolete-policies-asap/)

## Prerequisites

- **Windows** with [.NET Framework 4.8](https://dotnet.microsoft.com/en-us/download/dotnet-framework/net48) or later installed
- **Visual Studio** (2019 or later) to build the solution
- An account with permission to modify project check-in policies on the target Azure DevOps project

## Building

1. Clone or download this repository.
2. Open `RemoveTFVCPolicy.sln` in Visual Studio.
3. Restore NuGet packages (Visual Studio typically does this automatically).
4. Build the solution (**Build** > **Build Solution**, or `Ctrl+Shift+B`).

The compiled executable (`RemoveTFVCPolicy.exe`) will be in the `RemoveTFVCPolicy\bin\Debug` or `RemoveTFVCPolicy\bin\Release` folder depending on your build configuration.

## Usage

```
RemoveTFVCPolicy.exe -collection <collectionUri> -project <projectName>
```

### Parameters

| Parameter | Required | Description |
|-----------|----------|-------------|
| `-collection` | Yes | The URL of your Azure DevOps organization or project collection (e.g., `https://contoso.visualstudio.com/` or `https://dev.azure.com/contoso/`). |
| `-project` | Yes | The name of the team project whose TFVC check-in policies should be removed. |

### Options

| Option | Description |
|--------|-------------|
| `-help`, `-h`, `--help`, `/?` | Display help text and exit. |

### Exit Codes

| Code | Meaning |
|------|---------|
| `0` | Success – check-in policies were removed. |
| `1` | Validation error – one or more required parameters are missing. |
| `2` | Runtime error – an exception occurred (e.g., authentication failure or connection error). |

## Examples

Remove all TFVC check-in policies from the `fabrikam` project in the `contoso` organization:

```
RemoveTFVCPolicy.exe -collection https://contoso.visualstudio.com/ -project fabrikam
```

Using the `dev.azure.com` URL format:

```
RemoveTFVCPolicy.exe -collection https://dev.azure.com/contoso/ -project fabrikam
```

Display help:

```
RemoveTFVCPolicy.exe -help
```

## Authentication

The tool uses the Azure DevOps client libraries to authenticate. It will prompt for credentials if your current Windows credentials do not have access to the specified collection. You can also pre-authenticate using a Personal Access Token (PAT) or Microsoft Entra ID credentials.

## References

- [TFVC Policies Storage Updates – Azure DevOps Blog](https://devblogs.microsoft.com/devops/tfvc-policies-storage-updates/)
- [TFVC – Remove Existing Obsolete Policies ASAP – Azure DevOps Blog](https://devblogs.microsoft.com/devops/tfvc-remove-existing-obsolete-policies-asap/)
