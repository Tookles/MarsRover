﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MarsRover.Input.ParserModels;
using MarsRover.LogicLater.Models;
using Sharprompt;

namespace MarsRover.States
{
    public class StartupState : IState 
    {
        public Application _application;

        public StartupState(Application application)
        {
            _application = application;
        }

        public string GetUserInput(string request)
        {
            string? userInput = Prompt.Input<string>(request);
            Console.Clear(); 
            return userInput != null ? userInput : "";
        }

        public void Run()
        {

            Console.WriteLine("Elon Musk has landed on Mars");
            Console.ReadLine();
            Console.WriteLine("There is only one person who can save Mars from colonisation.");
            Console.ReadLine();
            Console.WriteLine("(We mean you...)");
            Console.ReadLine();
            Boolean result = Prompt.Confirm("Are you ready to save Mars?", defaultValue: true);
            if (!result) Console.WriteLine("Well, you're going to have to anyway...");

            Console.Clear(); 

            PlateauSizeParser userPSP = new (GetUserInput("Set the size of the plateau. Format: 'x y'"));
            
            while (!userPSP.Success)
            {
                Console.WriteLine(userPSP.Message);
                userPSP = new (GetUserInput("Set the size of the plateau. Format: 'x y'"));
            }

            _application.MissionControl = new MissionControl(userPSP.Result);

        
            _application.CurrentState = new AddRoverState(_application); 

        }

    }
}
