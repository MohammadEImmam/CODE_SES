using System;
using System.IO;
using System.Reflection;
using System.Text;
using RoslynCSharp;
using RoslynCSharp.Compiler;
using UnityEngine;

/// <summary>
/// Singleton class that uses Roslyn compiler to compile C# code at runtime.
/// </summary>
public class Compiler : MonoBehaviour
{
    public static Compiler instance;


    ScriptDomain domain;

    private void Awake()
    {
        instance = this;

    }

    private void Start()
    {
        domain = ScriptDomain.CreateDomain("TestingDomain");
        domain.RoslynCompilerService.ReferenceAssemblies.Add(AssemblyReference.FromAssembly(typeof(IDE).Assembly));
    }


    /// <summary>
    /// Compile the source code and provde assembly.
    /// </summary>
    /// <returns>the compiled assembly or null if there errors</returns>
    public ScriptAssembly Compile(string sourceCode, Action<string> onError = null)
    {
        var assem = domain.CompileAndLoadSource(sourceCode);

        var result = domain.CompileResult;
        foreach (var error in result.Errors)
        {
            if (error.IsError)
            {
                onError?.Invoke(error.Message);
                Debug.LogError("Compilation Error: " + error.Message);
            }
        }
        return assem;
    }

    /// <summary>
    /// Navigate through the compiled assembly to look for 'Main' function to execute it.
    /// like usual c# program
    /// </summary>
    public string ExecuteMainFunction(ScriptAssembly assembly, string[] args)
    {
        try
        {
            var mainType = assembly.MainType;
            var mainMethod = mainType.SystemType.GetMethod("Main", BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic);
            var consoleOutput = new StringBuilder();
            RedirectStandardOutput(consoleOutput);

            try
            {
                mainMethod.Invoke(null, new object[] { args });
            }
            catch
            {
                throw new Exception("No main entry found");
            }
            finally
            {
                ResetStandardOutput();
            }

            return consoleOutput.ToString();
        }
        catch (NullReferenceException)
        {
            throw new Exception("No main entry found");
        }
    }

    private void RedirectStandardOutput(StringBuilder sb)
    {
        TextWriter oldOut = Console.Out;
        Console.SetOut(new StringWriter(sb));
    }

    private void ResetStandardOutput()
    {
        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
    }
}