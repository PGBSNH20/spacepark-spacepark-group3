using System;
using System.Collections.Generic;
using Spectre.Console;

namespace Program
{
    class ConsoleGUI
    {
        private void WelcomeMessage()
        {
            AnsiConsole.MarkupLine("[red on black]SpacePark - Developed by Adam, Leo, Aswan & Kadar[/]");
        }

        private void DisplayMenu()
        {
            List<string> availableChoices = new List<string>(){"Start parking",
            "Check available parking spots",
            "Quit"};

            var selectedChoice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Welcome! [purple]What would you like to do[/]?")
                .PageSize(3)
                .AddChoices(availableChoices));

            if (selectedChoice == availableChoices[0])
            {

            }
            else if (selectedChoice == availableChoices[1])
            {

            }
            else
            {
                Environment.Exit(0);
            }
        }

        public void LoadGUI(string applicationName)
        {
            WelcomeMessage();
            DisplayMenu();
        }
    }
}