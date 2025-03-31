namespace JetbrainsTerminal;
public record TerminalExecResult(string StandardOutput, string StandardError, int ExitCode);