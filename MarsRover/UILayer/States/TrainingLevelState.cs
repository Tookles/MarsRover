﻿using MarsRover.Enums;
using MarsRover.LogicLayer.Models;
using MarsRover.Input.ParserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sharprompt;

namespace MarsRover.UILayer.States
{
    public class TrainingLevelState : IState
    {
        public Application _application;

        public TrainingLevelState(Application application)
        {
            _application = application;
        }

        public string GetUserInput(string request)
        {
            string? userInput = Prompt.Input<string>(request);
            return userInput != null ? userInput : "";
        }

        public void Run()
        {

            XYPosition randomPos = _application.MissionControl.PositionGenerator();
            _application.MissionControl.AddObject(new ChargingStation(randomPos));
            _application.MissionControl.DisplayGrid();

            Console.WriteLine("LEVEL 0: TRAINING");
            Console.WriteLine("Aim: move your Rovers to get to the charging station");

            Boolean isLooping = true; 
            while (isLooping)
            {
                foreach (Rover rover in _application.MissionControl.Rovers.Where(x => x.IsIntact == true))
                {

                    Console.WriteLine($"Rover {rover.Id} is at {rover.Position.ToString()}");
                    InstructionParser userIP = new(GetUserInput("How do you want to move? i.e. LLRM"));

                    while (!userIP.Success)
                    {
                        Console.WriteLine(userIP.Message);
                        userIP = new(GetUserInput("How do you want to move? i.e. LLRM"));
                    }

                    List<Instructions> userInstructions = userIP.Result;
                    _application.MissionControl.RunInstructions(rover, userInstructions);
                    _application.MissionControl.DisplayGrid();

                    if (!rover.IsIntact)
                    {
                        Console.WriteLine($"Rover {rover.Id} is destroyed");
                    }
                    else
                    {
                        Console.WriteLine($"Rover {rover.Id} is now at {rover.Position.ToString()}");
                    }

                }

                Boolean HasRoverGotHealthCheck = _application.MissionControl.IsPositionEmpty(randomPos);
                if (!HasRoverGotHealthCheck)
                {
                    Console.WriteLine("You've got the health check. Time to level up!");
                    isLooping = false;
                }
                else if (!_application.MissionControl.AreRoversIntact())
                {
                    Console.WriteLine("Sorry. Looks like you have no rovers left...");
                    isLooping=false;
                    _application.Stop(); 
                }
            }

            _application.CurrentState = new Level1State(_application);

        }
    }
}
    

