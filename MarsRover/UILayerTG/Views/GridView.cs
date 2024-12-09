﻿using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;

namespace MarsRover.UILayerTG.Utils
{
    public class GridView : View
    {
        public GridView(string[,] myGrid)
        {
            X = 0;
            Y = 0;
            Width = 4 + myGrid.GetLength(1);
            Height = 4 + myGrid.GetLength(0);

            int startX = 2;
            int startY = 2;
            for (int i = myGrid.GetLength(0) - 1; i >= 0; i--)
            {
                for (int j = 0; j < myGrid.GetLength(1); j++)
                {
                    var colorSet = new ColorScheme
                    {
                        Normal = new Terminal.Gui.Attribute(Terminal.Gui.Color.BrightCyan, Terminal.Gui.Color.Black)
                    };

                    if (myGrid[i, j] == "V")
                    {
                        colorSet = new ColorScheme
                        {
                            Normal = new Terminal.Gui.Attribute(Terminal.Gui.Color.Magenta, Terminal.Gui.Color.Black)
                        };

                    }
                    else if (myGrid[i, j] == "_")
                    {
                        colorSet = new ColorScheme
                        {
                            Normal = new Terminal.Gui.Attribute(Terminal.Gui.Color.BrightRed, Terminal.Gui.Color.Black)
                        };

                    }
                    else if (myGrid[i, j] == "@")
                    {
                        colorSet = new ColorScheme
                        {
                            Normal = new Terminal.Gui.Attribute(Terminal.Gui.Color.BrightYellow, Terminal.Gui.Color.BrightYellow)
                        };
                    }

                    var label = new Label(myGrid[i, j])
                    {
                        X = startX + j,
                        Y = startY + i,
                        Height = 1,
                        ColorScheme = colorSet
                    };

                    Add(label);

                }
            }
        }
    }



}

