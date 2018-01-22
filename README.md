# Hello.World
Hello World Console App with Web API

Write a ‘Hello World’ program. 

	a.	The program has 1 current business requirement – write “Hello World” to the console/screen. 
  
	b.	The program should have an API that is separated from the program logic to eventually support mobile applications, web applications, or console applications, or windows services.
  
	c.	The program should support future enhancements for writing to a database, console application, etc. 
  
		i.	Use common design patterns (inheritance, e.g.) to account for these future concerns. 
    
		ii.	Use configuration files or another industry-standard mechanism for determining where to write the information to. 
    
	d.  Write unit tests to support the API.

The Hello World Application consists of:

	1. Hello.World.ConsoleApp - A Console Application that will write to the Console

	2. Hello.World.Api - A Web API for the Console Application to communicate with

	3. Hello.World.Tests - A Test project containing unit tests for the application

To run this application:

	Set the Hello.World.Api project as the startup project.

	Build, and then run the project.

	In a command window, change the directory to (project root) \Hello.World.Console.App\bin\Debug

	At the command prompt, type Hello.World.ConsoleApp.exe

	Upon success you will see Hello World displayed in the console window.
