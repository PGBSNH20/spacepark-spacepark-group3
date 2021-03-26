<<<<<<< HEAD
using SpacePark.Config;
=======
﻿using SpacePark.DB.Models;
>>>>>>> main
using SpacePark.DB.Interfaces;
using SpacePark.DB.Queries;
using System;

namespace Program
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            ConsoleGUI gui = new();
            gui.LoadGUI(AppConfig.GetConfig().Name);
        }
    }
}