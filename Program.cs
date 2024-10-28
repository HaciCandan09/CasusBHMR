static void StartApp()
{

    Console.WriteLine("Welcome to the app!");
    Console.WriteLine("Press 1 to go to Profile.\n Press 2 to go to Games. \n Press 3 to go to Routes. \n Press 4 to Add a observation.)");
    var input = Console.ReadLine();
    int value;
    if (Int32.TryParse(input.ToString(), out value))
    {
        if (value == 1) { }
        else if (value == 2) { }
        else if (value == 3) { }
        else if (value == 4) { }
        else { Console.WriteLine("Invalid input, please try again."); StartApp(); }
    }
    else { Console.WriteLine("Invalid input, please try again."); StartApp(); }
}
static void Main(string[] args)
{
    StartApp();
}