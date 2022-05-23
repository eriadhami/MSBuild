using System;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Diagnostics;

namespace CSharpProject1
{
    public class BuildDateTimeTask : Microsoft.Build.Utilities.Task
    {
        public override bool Execute()
        {
            BuildData buildData = new BuildData();
            buildData.BuildDate = DateTimeOffset.Now;

            buildData.GitBranch = GitProcess("rev-parse --abbrev-ref HEAD");
            buildData.GitHash = GitProcess("rev-parse HEAD");

            string json = JsonSerializer.Serialize(buildData);

            string dir = Directory.GetCurrentDirectory();
            string file = Path.Combine(dir, "BuildInfo.json");
            File.WriteAllText(file, json);
            return true;
        }

        private static string GitProcess(string command)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo("git");

            startInfo.UseShellExecute = false;
            startInfo.WorkingDirectory = Directory.GetCurrentDirectory();
            startInfo.RedirectStandardInput = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.Arguments = command;

            Process process = new Process();
            process.StartInfo = startInfo;
            process.Start();
            return process.StandardOutput.ReadLine();
        }
    }
}
