# Table of contents
* [General info](#general-info)
* [Build](#build)
* [Running the code](#running-the-code)

# General info

This is a console application written to allow a single human player to play a one-sided game of Battleships against ships placed by the computer. Program creates a 10x10 grid, and place several ships on the grid at random with the following sizes:

1x Battleship (5 squares)

2x Destroyers (4 squares)

The player enters or selects coordinates of the form “A5”, where “A” is the column and “5” is the row, to specify a square to target. Shots result in hits, misses or sinks. The game ends when all ships are sunk.

# Build
The build of this tool requires .NET 7.0
To build the project please run the command from the directory `Battleships`

    dotnet build

To execute tests run the command from the directory `Battleships`

    dotnet test

# Running the code
To run the application you have to go the directory `Battleships\Battleships.UI` and run standard dotnet command:

    dotnet run

If you provide additional argument `-showGrids` program will print generated grids, full command for that case

    dotnet run -showGrids