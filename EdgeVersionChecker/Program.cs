using System;
using System.Diagnostics;
using System.IO;

namespace EdgeVersionChecker
{
    class Program
    {
        static void Main(string[] args)
        {
            FileVersionInfo fileVersion;

            Console.WriteLine("<<< Your Versions >>>");

            try {
                fileVersion = FileVersionInfo.GetVersionInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), @"Microsoft\Edge\Application\msedge.exe"));
                Console.WriteLine("Edge Stable: " + fileVersion.FileVersion);
            } catch {
                Console.WriteLine("Edge Stable: Not installed");
            }

            try {
                fileVersion = FileVersionInfo.GetVersionInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), @"Microsoft\Edge Beta\Application\msedge.exe"));
                Console.WriteLine("Edge Beta: " + fileVersion.FileVersion);
            } catch {
                Console.WriteLine("Edge Beta: Not installed");
            }

            try {
                fileVersion = FileVersionInfo.GetVersionInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), @"Microsoft\Edge Dev\Application\msedge.exe"));
                Console.WriteLine("Edge Dev: " + fileVersion.FileVersion);
            } catch {
                Console.WriteLine("Edge Dev: Not installed");
            }

            try {
                fileVersion = FileVersionInfo.GetVersionInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Microsoft\Edge SxS\Application\msedge.exe"));
                Console.WriteLine("Edge Canary: " + fileVersion.FileVersion);
            } catch {
                Console.WriteLine("Edge Canary: Not installed");
            }

            Console.WriteLine();
            Console.WriteLine("<<< Latest Versions >>>");

            EdgeUpdateService service = new EdgeUpdateService();
            EdgeUpdateResponse results;

            results = service.GetLatestVersions("stable");

            if (results != null) {
                Console.WriteLine("Edge Stable: " + results.ContentId.Version);
            }

            results = service.GetLatestVersions("beta");

            if (results != null) {
                Console.WriteLine("Edge Beta: " + results.ContentId.Version);
            }

            results = service.GetLatestVersions("dev");

            if (results != null) {
                Console.WriteLine("Edge Dev: " + results.ContentId.Version);
            }

            results = service.GetLatestVersions("canary");

            if (results != null) {
                Console.WriteLine("Edge Canary: " + results.ContentId.Version);
            }

            Console.ReadLine();
        }
    }
}
