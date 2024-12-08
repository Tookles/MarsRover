﻿using MarsRover.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.LogicLayer.Models
{
    public abstract class Vehicle 
    {
        ulong Id;

        XYPosition Position;

        Facing Direction; 

        Boolean IsIntact { get; set; }

        
    }
}