using System;
using System.Collections.Generic;
using System.Threading;
using SpacePark.DB.Interfaces;
using SpacePark.DB.Models;
using SpacePark.DB.Queries;
using SpacePark.Networking;
using Spectre.Console;

namespace Program
{
    public class ConsoleGUI
    {
        private String applicationName;
        private readonly int spotsPerFloor = 3;
        private List<Spot> spots = new();

        public ConsoleGUI()
        {
            // Fetches the layout/spots of the parking
            GetSpots();
        }

        private void WelcomeMessage()
        {
            AnsiConsole.MarkupLine($"[yellow]{applicationName} - Developed by Adam, Leo, Aswan & Kadar[/]");
        }

        private void StartParking(bool randomSlot)
        {
            if (randomSlot)
            {

            }
            else
            {

            }
        }

        private bool IsParkingAvailable()
        {
            return true;
        }

        private bool CanPark()
        {
            // Verifies that there are available spots
            if (IsParkingAvailable())
            {
                // Check if the name comes from the Star Wars universe (eligble to park)
                var inputName = AnsiConsole.Ask<string>("What is your name?");

                StarWarsAPI starwars = new();
                if (!starwars.UserFromStarWars(inputName))
                {
                    AnsiConsole.MarkupLine("Sorry, you cannot park here!");
                    Thread.Sleep(2000);
                    return false;
                }

                return true;
            }

            return false;
        }

        private void InitiateParking()
        {
            if (CanPark())
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
        }

        private void GetSpots()
        {
            ISpotQuery spotQuery = new SpotQuery();
            spotQuery.GetSpots().ForEach(spot => spots.Add(spot));
            spots = spotQuery.GetSpots();
        }

        private void AddSpotsToTree(int floor, TreeNode parking)
        {
            parking.AddNode($"[yellow]Floor {floor + 1}:[/]");

            int spotsAdded = 0;
            for (int spot = 0; spot < spotsPerFloor; spot++)
            {
                parking.Nodes[floor].AddNode($"Spot {spot + 1}: Available");
                spotsAdded++;

                if (spotsAdded >= spots.Count)
                {
                    return;
                }
            }
        }

        private void ShowAvailableParking()
        {
            GetSpots();
            var tree = new Tree("Available parking slots");
            var parking = tree.AddNode("[purple]Parking[/]");

            for (int floor = 0; floor < (spots.Count / spotsPerFloor); floor++)
            {
                AddSpotsToTree(floor, parking);
            }


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