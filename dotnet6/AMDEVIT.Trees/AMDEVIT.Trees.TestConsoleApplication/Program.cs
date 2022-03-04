// See https://aka.ms/new-console-template for more information

using AMDEVIT.Trees.TestConsoleApplication;

TestObject testObject = new TestObject();
Console.WriteLine("Node test.");

testObject.Initialize();
testObject.Print();

Console.WriteLine("Press ENTER to exit");
while(Console.ReadKey().Key != ConsoleKey.Enter)
{

}
Console.WriteLine(("Ending."));
