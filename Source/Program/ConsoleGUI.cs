using System;
using System.Collections.Generic;
using Spectre.Console;

namespace Program
{
    class ConsoleGUI
    {
        private String applicationName;

        private void WelcomeMessage()
        {
            AnsiConsole.MarkupLine($"[yellow]{applicationName} - Developed by Adam, Leo, Aswan & Kadar[/]");
        }

        private void StartParking(bool randomSlot)
        {

        }

        private void InitiateParking()
        {
            // TODO Check if there are any available slots
            List<string> availableChoices = new()
            {
                "Park at any available spot",
                "Choose a specific spot",
                "Go back"
            };

            var selectedChoice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[purple]How would you like to park[/]?")
                .PageSize(3)
                .AddChoices(availableChoices));

            if (selectedChoice == availableChoices[0])
            {
                StartParking(false);
            }
            else if (selectedChoice == availableChoices[1])
            {
                StartParking(true);
            }
            else
            {
                DisplayMenu();
            }
        }

        private void ShowAvailableParking()
        {
            var tree = new Tree("Available parking slots");
            var parking = tree.AddNode("[purple]Parking[/]");

            var firstFloor = parking.AddNode("[yellow]First floor[/]");
            firstFloor.AddNodes(new string[] { "Taken", "Available", "Taken" });
            var secondFloor = parking.AddNode("[blue]Second floor[/]");
            secondFloor.AddNodes(new string[] { "Taken", "Available", "Taken" });

            AnsiConsole.Render(tree);
            var goBack = AnsiConsole.Confirm("Go back");
            if (!goBack)
            {
                ExitProgram();
            }
        }

        private void DisplayMenu()
        {
            AnsiConsole.MarkupLine("");
            AnsiConsole.MarkupLine("Parking slots");
            AnsiConsole.Render(new BreakdownChart()
            .FullSize()
            .Width(60)
            .AddItem("Available", 2, Color.Green)
            .AddItem("Taken", 4, Color.Red));

            AnsiConsole.MarkupLine("");
            List<string> availableChoices = new()
            {
                "Start parking",
                "Check available parking spots",
                "Quit"
            };

            var selectedChoice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[purple]Welcome![/] What would you like to do?")
                .PageSize(3)
                .AddChoices(availableChoices));

            if (selectedChoice == availableChoices[0])
            {
                InitiateParking();
            }
            else if (selectedChoice == availableChoices[1])
            {
                ShowAvailableParking();
            }
            else
            {
                ExitProgram();
            }
        }

        public void LoadGUI(string applicationName)
        {
            this.applicationName = applicationName;
            WelcomeMessage();

            while (true)
            {
                DisplayMenu();
                AnsiConsole.Console.Clear(true);
            }
        }

        private void ExitProgram()
        {
            AnsiConsole.Console.Clear(true);
            AnsiConsole.MarkupLine($"Thank you for using {applicationName}!");
            Environment.Exit(0);
        }
    }
}