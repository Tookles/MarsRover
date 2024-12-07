﻿using MarsRover.Enums;
using MarsRover.Input.ParserModels;
using MarsRover.UILayer.States;
using Sharprompt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.UILayer.States
{
    internal class AddRoverState : IState
    {
        public Application _application;

        public AddRoverState(Application application)
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
            Console.Clear();


            Console.WriteLine("Let's add some rovers!");
            int RoversAdded = 0; 

            while (RoversAdded < 6)
            {

                string startingPos = GetUserInput("Please select starting position: x y");
                var direction = Prompt.Select("Please select starting direction", new[] { Facing.NORTH, Facing.SOUTH, Facing.EAST, Facing.WEST });
                RoverParser userRP = new(startingPos, direction, _application.MissionControl.Plateau);

                while (!userRP.Success)
                {
                    Console.WriteLine(userRP.Message);
                    startingPos = GetUserInput("Please select starting position: x y");
                    direction = Prompt.Select("Please select starting direction", new[] { Facing.NORTH, Facing.SOUTH, Facing.EAST, Facing.WEST });
                    userRP = new(startingPos, direction,
                    _application.MissionControl.Plateau);

                }
                _application.MissionControl.AddObject(userRP.Result);

                string userAdding = GetUserInput("Done adding rovers? Press E to exit.");
               
                if (userAdding == "E") break; 
               
                RoversAdded++; 
            }

            _application.CurrentState = new TrainingLevelState(_application);
        }

    }
}
