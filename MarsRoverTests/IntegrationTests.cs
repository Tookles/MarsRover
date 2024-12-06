﻿using FluentAssertions;
using MarsRover.Enums;
using MarsRover.Input.ParserModels;
using MarsRover.LogicLater.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions; 

namespace MarsRoverTests
{
    internal class IntegrationTests
    {

        [Test]
        public void CreateAndMoveRover()
        {
            // Arrange

            Plateau testPlateau = new Plateau(5, 5);
            MissionControl testMissionControl = new MissionControl(testPlateau);
            Position testPosOne = new Position(1, 2, Facing.NORTH);
            Rover testRoverOne = new Rover(testPosOne);
            testMissionControl.AddRover(testRoverOne);
            Position testPosTwo = new Position(3, 3, Facing.EAST);
            Rover testRoverTwo = new Rover(testPosTwo);
            testMissionControl.AddRover(testRoverTwo);


            InstructionParser instructionsForRoverOne = new InstructionParser("LMLMLMLMM");
            List<Instructions> listOfInstructionsRoverOne = instructionsForRoverOne.Result;
            InstructionParser instructions2 = new InstructionParser("MMRMMRMRRM");
            List<Instructions> myList2 = instructions2.Result;

            // Act 

            testMissionControl.RunInstructions(testRoverOne, listOfInstructionsRoverOne);
            testMissionControl.RunInstructions(testRoverTwo, myList2);

            // Assert

            testRoverOne.Position.ToString().Should().Be($"1, 3 facing {Facing.NORTH}");
            testRoverTwo.Position.ToString().Should().Be($"5, 1 facing {Facing.EAST}");

        }

        //[Test]
        //public void CreateAndMoveRover()
        //{
        //    // Arrange

        //    Plateau testPlateau = new Plateau(5, 5);
        //    MissionControl testMissionControl = new MissionControl(testPlateau);
        //    Position testPosOne = new Position(1, 2, Facing.NORTH);
        //    Rover testRoverOne = new Rover(testPosOne);
        //    testMissionControl.AddRover(testRoverOne);
        //    Position testPosTwo = new Position(3, 3, Facing.EAST);
        //    Rover testRoverTwo = new Rover(testPosTwo);
        //    testMissionControl.AddRover(testRoverTwo);


        //    InstructionParser instructionsForRoverOne = new InstructionParser("LMLMLMLMM");
        //    List<Instructions> listOfInstructionsRoverOne = instructionsForRoverOne.Result;
        //    InstructionParser instructions2 = new InstructionParser("MMRMMRMRRM");
        //    List<Instructions> myList2 = instructions2.Result;

        //    // Act 

        //    testMissionControl.RunInstructions(testRoverOne, listOfInstructionsRoverOne);
        //    testMissionControl.RunInstructions(testRoverTwo, myList2);

        //    // Assert

        //    testRoverOne.Position.ToString().Should().Be($"1, 3 facing {Facing.NORTH}");
        //    testRoverTwo.Position.ToString().Should().Be($"5, 1 facing {Facing.EAST}");

        //}




    }
}
