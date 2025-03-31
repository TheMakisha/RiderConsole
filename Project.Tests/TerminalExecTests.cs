using AvaloniaUI;

namespace JetbrainsTerminal.Tests;

public class TerminalExecTests
{
    [Test]
    public async Task ShouldCaptureStandardOutput()
    {
        var result = await TerminalExec.ExecuteAsync("echo Hello");

        Assert.That(result.ExitCode, Is.EqualTo(0));
        Assert.That(result.StandardOutput, Is.EqualTo("Hello\r\n"));
        Assert.That(result.StandardError, Is.EqualTo(string.Empty));
    }

    [Test]
    public async Task ShouldCaptureNoOutputOrError()
    {
        var result = await TerminalExec.ExecuteAsync("cd .");

        Assert.That(result.ExitCode, Is.EqualTo(0));
        Assert.That(result.StandardOutput, Is.EqualTo(string.Empty));
        Assert.That(result.StandardError, Is.EqualTo(string.Empty));
    }

    [Test]
    public async Task ShouldCaptureErrorOnInvalidCommand()
    {
        var result = await TerminalExec.ExecuteAsync("ups");

        Assert.That(result.ExitCode, Is.Not.EqualTo(0));
        Assert.That(result.StandardOutput, Is.EqualTo(string.Empty));
        Assert.That(result.StandardError, Is.Not.EqualTo(string.Empty));
    }

    [Test]
    public async Task OutputBufferShouldSuccessfullyHandleLargeOutput()
    {
        var result = await TerminalExec.ExecuteAsync("for /l %i in (1, 1, 1000) do @echo TEST");

        Assert.That(result.ExitCode, Is.EqualTo(0));
        Assert.That(result.StandardOutput, Is.Not.EqualTo(string.Empty));
        Assert.That(result.StandardError, Is.EqualTo(string.Empty));
    }

    [Test]
    public async Task ShouldExitWithCustomExitCode()
    {
        var result = await TerminalExec.ExecuteAsync("exit 10");

        Assert.That(result.ExitCode, Is.EqualTo(10));
        Assert.That(result.StandardOutput, Is.EqualTo(string.Empty));
        Assert.That(result.StandardError, Is.EqualTo(string.Empty));
    }
}