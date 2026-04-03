using System;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.VersionControl.Client;

namespace RemoveTFVCPolicy
{
    class Program
    {
        static int Main(string[] args)
        {
            if (args.Length == 0 || IsHelpRequested(args))
            {
                PrintHelp();
                return 0;
            }

            string collectionUri = null;
            string projectName = null;

            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i].ToLowerInvariant())
                {
                    case "-collection":
                    case "--collection":
                        if (i + 1 < args.Length)
                        {
                            collectionUri = args[++i];
                        }
                        break;

                    case "-project":
                    case "--project":
                        if (i + 1 < args.Length)
                        {
                            projectName = args[++i];
                        }
                        break;
                }
            }

            if (string.IsNullOrWhiteSpace(collectionUri) || string.IsNullOrWhiteSpace(projectName))
            {
                Console.Error.WriteLine("ERROR: Both -collection and -project are required.\n");
                PrintHelp();
                return 1;
            }

            try
            {
                RemoveCheckinPolicies(collectionUri, projectName);
                Console.WriteLine("✅ Check-in policies removed successfully.");
                return 0;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("❌ Failed to remove check-in policies.");
                Console.Error.WriteLine(ex.Message);
                return 2;
            }
        }

        private static void RemoveCheckinPolicies(string collectionUri, string projectName)
        {
            var uri = new Uri(collectionUri);

            using (var tpc = new TfsTeamProjectCollection(uri))
            {
                tpc.EnsureAuthenticated();

                var versionControlServer = tpc.GetService<VersionControlServer>();
                var teamProject = versionControlServer.GetTeamProject(projectName);

                teamProject.SetCheckinPolicies(null);
            }
        }

        private static bool IsHelpRequested(string[] args)
        {
            foreach (var arg in args)
            {
                switch (arg.ToLowerInvariant())
                {
                    case "-h":
                    case "--help":
                    case "-help":
                    case "/?":
                        return true;
                }
            }
            return false;
        }

        private static void PrintHelp()
        {
            Console.WriteLine(
@"RemoveTFVCPolicy

Removes all TFVC check-in policies from a specified Azure DevOps project.

USAGE:
  RemoveTFVCPolicy.exe -collection <collectionUri> -project <projectName>

PARAMETERS:
  -collection   Azure DevOps organization or collection URL
  -project      Team project name

OPTIONS:
  -help, -h, --help, /?   Show this help text

EXAMPLE:
  RemoveTFVCPolicy.exe -collection https://contoso.visualstudio.com/ -project fabrikam
");
        }
    }
}
