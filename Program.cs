using CasusExotischNederland.Model;

namespace CasusExotischNederland
{
    internal class Program
    {
        static async Task AddObservation()
        {
            Area area = new Area(1, "area1", "des area1", 11, 12);
            
            User user = new User(1, "user1", 44, "email1@gmail.com", 23563634);

            Console.WriteLine("Enter species name: ");
            string speciesName = Console.ReadLine();
            Console.WriteLine("Species photo: ");
            string speciesPhoto = Console.ReadLine();
            Console.WriteLine("Enter species category: ");
            string speciesCategory = Console.ReadLine();

            Species species = new Species(1, speciesName, speciesPhoto, speciesCategory);

            Console.WriteLine("Enter observation name: ");
            string observationName = Console.ReadLine();

            LocationService locationService = new LocationService();
            LocationInfo locationInfo = await locationService.GetLocationInfoAsync();

            string location = locationInfo.Region + " " + locationInfo.City;
            float coordinateX = locationInfo.CoordinateX;
            float coordinateY = locationInfo.CoordinateY;

            Observation observation = new Observation(1, area, species, user, DateTime.Now.Date, observationName, coordinateX, coordinateY, speciesPhoto,location);
            observation.AddObservation();

        }
        static async Task StartApp()
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
                else if (value == 4) { await AddObservation(); }
                else { Console.WriteLine("Invalid input, please try again."); StartApp(); }
            }
            else { Console.WriteLine("Invalid input, please try again."); StartApp(); }
        }
        static async Task Main(string[] args)
        {

            await StartApp();
        }
    }

}
