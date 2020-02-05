# PANIC - Possible Actions kNown In Chess
Simple application which shows for each given pawn, a chessboard with possible moves of this pawn.

## Table of contents
* [General info](#general-info)
* [Technologies](#technologies)
* [Setup](#setup)
* [Features](#features)
* [Status](#status)
* [Inspiration](#inspiration)

## General info
Final project for the programming subject. It's using Batch (menu), C# (for compute possible moves of pawns) and python (generate map of frequency of move's possibility, and open website).

## Technologies
* .Net Core 3 (C# 7.0)
* Python 3.x
	* matplotlib
	* sklearn
	* numpy

## Setup
Actually works only on Windows (cause of batch script). 
To clone and run this application you need [Git](https://git-scm.com/), [.Net Core 3](https://dotnet.microsoft.com/download) and [Python 3.x](https://www.python.org/downloads/)
From your command line:
```
# Install python libraries
> pip install numpy sklearn matplotlib

# Clone this repository
> git clone https://github.com/JustD4nTe/PANIC.git

# Run the program
> menu.bat
```

## Features
List of features ready and To-do's which should be introduced
* Make backup of entire project
* Parsing data
* Generates a map of frequency for each pawn's type

To-do list:
* Make backup only of In, Out folder
* Embellish website
* run internal program by command not exe file

## Status
Project is: _in progress_ because it needs some changes of structure (I will make it more sensible). Now it meets the assumptions of final project for my subject (It's looks awful).

## Inspiration
Project based on polish programming competition [Algorytmion](http://algorytmion.ms.polsl.pl/), exercise 4 from 2014 yr.