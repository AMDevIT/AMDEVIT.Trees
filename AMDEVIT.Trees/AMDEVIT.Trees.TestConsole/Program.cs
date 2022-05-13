// See https://aka.ms/new-console-template for more information
using AMDEVIT.Trees.TestConsole.Tests;

TestProvider testProvider = new TestProvider();
Console.WriteLine("NTree test.");
Console.WriteLine("Initializing tree with default values.");

testProvider.Initialize();

Console.WriteLine("Printing navigations.");

testProvider.Print();

Console.WriteLine("Press enter key to exit");

while(Console.ReadKey().Key != ConsoleKey.Enter)
{

}

