using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using SpacePark.Config;
using SpacePark.DB.Models;
using SpacePark.Logic;
using SpacePark.Networking;
using Spectre.Console;

namespace Program.GUI
{
    public class GUI
    {
        private readonly string _applicationName;
        private readonly int spotsPerFloor = 3;
        private List<Spot> spots = new();
        private List<int> availableSpotIds = new();
        private List<ParkingStatus> parkingStatuses = new();
        private readonly Helpers _helpers;
        private readonly Logic _logic;

        public GUI(string applicationName)
        {
            _applicationName = applicationName;
            _helpers = new Helpers(_applicationName);
            _logic = new Logic();

            // Fetches from the database
            GetData();
        }

        public void LoadGUI()
        {
            _helpers.WelcomeMessage();

            while (true)
            {
                DisplayMenu();
                AnsiConsole.Console.Clear(true);
            }
        }
        private void DisplayMenu()
        {
            FetchAvailableParking();

            AnsiConsole.MarkupLine("");
            AnsiConsole.MarkupLine("Parking slots");
            AnsiConsole.Render(new BreakdownChart()
            .FullSize()
            .Width(60)
            .AddItem("Available", availableSpotIds.Count, Color.Green)
            .AddItem("Taken", spots.Count - availableSpotIds.Count, Color.Red));

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
                _helpers.ExitProgram();
            }
        }
        private void StartParking(bool randomSlot, string name, Ship ship)
        {
            int spotID;

            FetchAvailableParking();

            if (randomSlot)
                spotID = RandomizeParkinSlot();
            else
                spotID = PromptUserSelectionSlot();

            Spot spot = Spot.GetByID(spotID);
            if (spot.Size < ship.Length)
            {
                AnsiConsole.MarkupLine("Your ship is too big to park at this spot!");
                Thread.Sleep(2000);
                return;
            }

            _logic.CreateParkingStatus(name, spotID);

            AnsiConsole.MarkupLine($"You have started parking at spot: {spotID}");
            Thread.Sleep(3000);
        }

        private int RandomizeParkinSlot()
        {
            Random rand = new();
            return availableSpotIds[rand.Next(0, availableSpotIds.Count - 1)];
        }

        private int PromptUserSelectionSlot()
        {
            var tree = new Tree("Available parking slots");
            var parking = tree.AddNode("[purple]Available parking slots[/]");

            for (int floor = 0; floor < Math.Min(availableSpotIds.Count, spotsPerFloor); floor++)
            {
                AddSpotsToTree(floor, parking);
            }

            List<string> availableSpots = new();
            foreach (int spotID in availableSpotIds)
            {
                var spot = Spot.GetByID(spotID);
                availableSpots.Add($"Spot: {spotID} ({spot.Size}M) - Price/H {spot.Price}");
            }

            var selectedChoice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[purple]Welcome![/] What would you like to do?")
                .PageSize(availableSpots.Count)
                .AddChoices(availableSpots));

            // Fetch the spotID from the selected choice
            return int.Parse(selectedChoice.Split(" ")[1]);
        }

        private Ship SelectShipMenu()
        {
            StarWarsAPI api = new();
            List<Ship> availableShips = api.GetStarWarsShips();
            List<string> shipNames = new();
            api.GetStarWarsShips().ForEach(s => shipNames.Add($"{s.Name} - {s.Length}M"));

            var selectedChoice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[purple]Please select your ship[/]")
                .PageSize(availableShips.Count)
                .AddChoices(shipNames));

            return availableShips.SingleOrDefault(ship => ship.Name == selectedChoice.Substring(0, selectedChoice.LastIndexOf("-") - 1));
        }

        private void FetchAvailableParking()
        {
            parkingStatuses = ParkingStatus.GetAll().ToList();
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

        private void InitiateParking()
        {
            // Check if the name comes from the Star Wars universe (eligble to park)
            var inputName = AnsiConsole.Ask<string>("What is your name?");
            if (availableSpotIds.Count == 0)
            {
                AnsiConsole.MarkupLine("Sorry, come back later, no avaible spots!");
                Thread.Sleep(2000);
                return;
            }

            if (!_logic.CanUserPark(inputName))
            {
                AnsiConsole.MarkupLine("Sorry, you cannot park here!");
                Thread.Sleep(2000);
                return;
            }

            Ship selectedShip = SelectShipMenu();

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
                StartParking(true, inputName, selectedShip);
            }
            else if (selectedChoice == availableChoices[1])
            {
                StartParking(false, inputName, selectedShip);
            }
            else
            {
                DisplayMenu();
            }
        }

        private void GetData()
        {
            spots = Spot.GetAll().ToList();
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
                    var spot = Spot.GetByID(spotID + 1);
                    parking.Nodes[floor].AddNode($"[green]Spot {spot.ID}: Available ({spot.Size}M) - Price/H {spot.Price}[/]");
                }
                else
                {
                    var status = _logic.GetParkingStatusBySpotID(spotID + 1);
                    parking.Nodes[floor].AddNode($"[red]Spot {status.SpotID}: Taken by {status.Customer.Name}[/]");
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
                _helpers.ExitProgram();
            }
        }

        private void EndParking()
        {
            var inputName = AnsiConsole.Ask<string>("What is your name?");
            var invoice = _logic.EndParkingByName(inputName);

            if (invoice == null)
            {
                AnsiConsole.MarkupLine($"There is no active parking registered on {inputName}!");
                Thread.Sleep(2000);
                return;
            }

            AnsiConsole.MarkupLine($"[purple]Thank you for parking with {AppConfig.GetConfig().Name}.[/]");

            invoice.CalculateCost();
            String invoiceMessage = string.Format("[green]{0}[/] - [red]{1}[/]\n[purple]Total cost:[/] [yellow]${2}[/] [blue](${3}/60min)[/]", invoice.StartedTime, invoice.EndTime, (int)invoice.TotalCost, (int)invoice.HourlyPrice);

            AnsiConsole.MarkupLine(invoiceMessage);
            Thread.Sleep(5000);
        }
    }
}