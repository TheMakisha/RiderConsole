using JetbrainsTerminal;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace AvaloniaUI;

public static class TerminalExec
{
    public static async Task<TerminalExecResult> ExecuteAsync(string command)
    {
        ProcessStartInfo processStartInfo = new ProcessStartInfo()
        {
            FileName = GetOSTerminalName(),
            Arguments = GetOSTerminalExitCommand() + command,
            RedirectStandardError = true,
            RedirectStandardOutput = true,
            CreateNoWindow = true
        };

        using Process process = new Process()
        {
            StartInfo = processStartInfo
        };

        process.Start();

        Task<string> standardOutputTask = process.StandardOutput.ReadToEndAsync();
        Task<string> standardErrorTask = process.StandardError.ReadToEndAsync();

        await Task.WhenAll(standardOutputTask, standardErrorTask, process.WaitForExitAsync());

        var result = new TerminalExecResult(standardOutputTask.Result, standardErrorTask.Result, process.ExitCode);

        return result;
        
    }

    private static string GetOSTerminalName()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) return "cmd";
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
            || RuntimeInformation.IsOSPlatform(OSPlatform.OSX)) return "/bin/sh";
        else throw new UnsupportedOSException("Running terminal on this platform isn't supported.");
    }

    //We wanna make sure that after a terminal is finished executing the command it terminates itself
    //the command for this depends on whether we are using CMD on Windows or Bash on Linux/OSX
    private static string GetOSTerminalExitCommand()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) return "/C";
        else return "-c";
    }
}
