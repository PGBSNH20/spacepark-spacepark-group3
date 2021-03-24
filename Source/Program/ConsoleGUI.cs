using System;
using System.Collections.Generic;
using Spectre.Console;

namespace Program
{
    class ConsoleGUI
    {
        private void WelcomeMessage(string applicationName)
        {
            AnsiConsole.MarkupLine($"[red on black]{applicationName} - Developed by Adam, Leo, Aswan & Kadar[/]");
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
            var slots = tree.AddNode("[blue]Parking[/]");
            slots.AddNode(new Table().RoundedBorder()
            .AddColumn("First floor")
            .AddColumn("Second floor")
            .AddRow("Taken", "Available")
            .AddRow("Available", "Taken")
            .AddRow("Available", "Taken"));

            AnsiConsole.Render(tree);
        }

        private void DisplayMenu()
        {
            List<string> availableChoices = new()
            {
                "Start parking",
                "Check available parking spots",
                "Quit"
            };

            var selectedChoice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Welcome! [purple]What would you like to do[/]?")
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
                Environment.Exit(0);
            }
        }

        public void LoadGUI(string applicationName)
        {
            WelcomeMessage(applicationName);
            DisplayMenu();
        }
    }
}