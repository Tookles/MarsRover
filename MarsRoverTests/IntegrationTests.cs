﻿using FluentAssertions;
using MarsRover.Enums;
using MarsRover.Input.ParserModels;
using MarsRover.LogicLayer.Models;
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

            Plateau testPlateau = new Plateau(10, 10);
            MissionControl testMissionControl = new MissionControl(testPlateau);
            Rover testRoverOne = new Rover((3, 4), Facing.NORTH);
            testMissionControl.AddRover(testRoverOne);
            Rover testRoverTwo = new Rover((3,3), Facing.EAST);
            testMissionControl.AddRover(testRoverTwo);


            InstructionParser instructionsForRoverOne = new InstructionParser("LMLMLMLMM");
            List<Instructions> listOfInstructionsRoverOne = instructionsForRoverOne.Result;
            InstructionParser instructions2 = new InstructionParser("MMRMMRMRRM");
            List<Instructions> myList2 = instructions2.Result;

            // Act 

            testMissionControl.RunInstructions(testRoverOne, listOfInstructionsRoverOne);
            testMissionControl.RunInstructions(testRoverTwo, myList2);

            // Assert

            testRoverOne.ToString().Should().Be($"Rover 0 is at (3, 3) facing {Facing.NORTH}");
            testRoverTwo.ToString().Should().Be($"Rover 1 is at (5, 5) facing {Facing.EAST}");

        }

        [Test]
        public void CreateAndMoveRoverOntoRocks()
        {
            // Arrange

            Plateau testPlateau = new Plateau(5, 5);
            MissionControl testMissionControl = new MissionControl(testPlateau);
            Rover testRoverOne = new Rover((3, 3), Facing.NORTH);
            testMissionControl.AddRover(testRoverOne);


            InstructionParser instructionsForRoverOne = new InstructionParser("M");
            List<Instructions> listOfInstructionsRoverOne = instructionsForRoverOne.Result;

            // Act
            testMissionControl.RunInstructions(testRoverOne, listOfInstructionsRoverOne);


            // Assert
            testRoverOne.IsIntact.Should().BeTrue();

            // Arrange 

            InstructionParser instructionsForRoverAgain = new InstructionParser("MMM");
            List<Instructions> listOfInstructionsRoverAgain = instructionsForRoverAgain.Result;


            // Act 

            testMissionControl.RunInstructions(testRoverOne, listOfInstructionsRoverAgain);

            // Assert

            testRoverOne.IsIntact.Should().BeFalse();

        }




    }
}
