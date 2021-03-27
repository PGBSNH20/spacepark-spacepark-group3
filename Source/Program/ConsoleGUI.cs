using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using SpacePark.DB.Models;
using SpacePark.DB.Queries;
using SpacePark.Networking;
using Spectre.Console;

namespace Program
{
    public class ConsoleGUI
    {
        private string applicationName;
        private readonly int spotsPerFloor = 3;
        private List<Spot> spots = new();
        private List<int> availableSpotIds = new();
        private List<ParkingStatus> parkingStatuses = new();

        public ConsoleGUI()
        {
            // Fetches from the database
            GetData();
        }

        private void WelcomeMessage()
        {
            AnsiConsole.MarkupLine($"[yellow]{applicationName} - Developed by Adam, Leo, Aswan & Kadar[/]");
        }

        private void StartParking(bool randomSlot, Customer customer)
        {
            Query query = new();
            ParkingStatus parkingStatus = new();
            parkingStatus.Customer = customer;

            FetchAvailableParking();

            if (randomSlot)
            {
                Random rand = new();
                parkingStatus.SpotID = availableSpotIds[rand.Next(0, availableSpotIds.Count - 1)];
            }
            else
            {
                var tree = new Tree("Available parking slots");
                var parking = tree.AddNode("[purple]Available parking slots[/]");

                for (int floor = 0; floor < Math.Min(availableSpotIds.Count, spotsPerFloor); floor++)
                {
                    AddSpotsToTree(floor, parking);
                }

                AnsiConsole.Render(tree);
                AnsiConsole.MarkupLine("");

                List<string> availableSpots = new();
                foreach (int spotID in availableSpotIds)
                {
                    availableSpots.Add($"Spot: {spotID}");
                }

                var selectedChoice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[purple]Welcome![/] What would you like to do?")
                    .PageSize(availableSpots.Count)
                    .AddChoices(availableSpots));

                // Fetch the spotID from the selected choice
                parkingStatus.SpotID = int.Parse(selectedChoice[selectedChoice.IndexOf(" ")..]);
            }

            parkingStatus.ArrivalTime = DateTime.Now;
            query.CreateParkingStatus(parkingStatus);

            AnsiConsole.MarkupLine($"You have started parking at spot: {parkingStatus.SpotID}");
            Thread.Sleep(3000);
        }

        private void FetchAvailableParking()
        {
            Query query = new();
            parkingStatuses = query.GetAllParkingStatus().ToList();
            List<int> availableSpotIds = new();

            // Add all spots to available
            spots.ForEach(spot => availableSpotIds.Add(spot.ID));

            // Remove the occupied slots from available
            foreach (ParkingStatus status in parkingStatuses)
            {
                foreach (Spot spot in spots)
                {
                    if (spot.ID == status.SpotID)
                    {
                        // Remove slot if already occupied
                        availableSpotIds.Remove(availableSpotIds.First(s => s == spot.ID));
                    }
                }
            }

            this.availableSpotIds = availableSpotIds;
        }

        private bool CanPark(string name)
        {
            FetchAvailableParking();

            // Verifies that there are available spots
            if (availableSpotIds.Count > 0)
            {
                StarWarsAPI starwars = new();
                if (!starwars.UserFromStarWars(name))
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
            // Check if the name comes from the Star Wars universe (eligble to park)
            var inputName = AnsiConsole.Ask<string>("What is your name?");

            if (CanPark(inputName))
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

                Customer customer = new();
                customer.Name = inputName;
                customer.ID = 0;

                if (selectedChoice == availableChoices[0])
                {
                    StartParking(true, customer);
                }
                else if (selectedChoice == availableChoices[1])
                {
                    StartParking(false, customer);
                }
                else
                {
                    DisplayMenu();
                }
            }
        }

        private void GetData()
        {
            Query query = new();
            spots = query.GetSpots();
            FetchAvailableParking();
        }

        private void AddSpotsToTree(int floor, TreeNode parking)
        {
            parking.AddNode($"[yellow]Floor {floor}:[/]");

            int nodeIndex = 0;
            parking.Nodes.ForEach(node => nodeIndex += node.Nodes.Count);
            for (int spotID = nodeIndex; spotID < Math.Min(nodeIndex + 3, spots.Count); spotID++)
            {
                if (availableSpotIds.Contains(spotID + 1))
                {
                    parking.Nodes[floor].AddNode($"Spot {spotID + 1}: Available");
                }
                else
                {
                    Query query = new();
                    ParkingStatus taken = parkingStatuses.SingleOrDefault(x => x.Spot.ID == spotID + 1);
                    parking.Nodes[floor].AddNode($"Spot {taken.SpotID}: {taken.Customer.Name}");
                }
            }
        }

        private void ShowAvailableParking()
        {
            GetData();
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

        private void EndParking()
        {
            Query query = new();
            var inputName = AnsiConsole.Ask<string>("What is your name?");

            var result = query.GetParkingStatusByName(inputName);
            if (result == null)
            {
                AnsiConsole.MarkupLine($"There is no active parking registered on {inputName}!");
                Thread.Sleep(2000);
            }

            query.DeleteParkingStatusByName(result);
            AnsiConsole.MarkupLine("Parking ended.");
            // print invoice
            Thread.Sleep(3000);
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
                "End parking",
                "Quit"
            };

            var selectedChoice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[purple]Welcome![/] What would you like to do?")
                .PageSize(4)
                .AddChoices(availableChoices));

            if (selectedChoice == availableChoices[0])
            {
                InitiateParking();
            }
            else if (selectedChoice == availableChoices[1])
            {
                ShowAvailableParking();
            }
            else if (selectedChoice == availableChoices[2])
            {
                EndParking();
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