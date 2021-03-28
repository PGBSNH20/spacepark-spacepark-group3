using System;
using Spectre.Console;
namespace Program.GUI
{
    public class Helpers
    {

        private readonly string _appName;
        public Helpers(string appName) 
        {
            this._appName = appName;

        }
        public void ExitProgram()
        {
            AnsiConsole.Console.Clear(true);
            AnsiConsole.MarkupLine($"Thank you for using {this._appName}!");
            Environment.Exit(0);
        }

        public void WelcomeMessage()
        {
            AnsiConsole.MarkupLine($"[yellow]{this._appName} - Developed by Adam, Leo, Aswan & Kadar[/]");
        }
    }
}