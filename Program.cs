using CasusExotischNederland.Model;

namespace CasusExotischNederland
{
    internal class Program
    {
        static async Task AddObservation()
        {
            Area area = new Area();
            
            
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

            Console.WriteLine("Kies een Area: ");
            foreach(Area myArea in area.GetAllAreas())
            {
                Console.WriteLine(myArea.Id + "  " + myArea.Name);
            }
            Console.WriteLine("Vul de area id in: ");
            int areaId = int.Parse(Console.ReadLine());
            Area selectedArea = area.GetArea(areaId);

            LocationService locationService = new LocationService();
            LocationInfo locationInfo = await locationService.GetLocationInfoAsync();

            string location = locationInfo.Region + " " + locationInfo.City;
            float coordinateX = locationInfo.CoordinateX;
            float coordinateY = locationInfo.CoordinateY;

            Observation observation = new Observation(1, selectedArea, species, user, DateTime.Now.Date, observationName, coordinateX, coordinateY, speciesPhoto,location);
            observation.AddObservation();

        }
        static async Task StartApp()
        {


            Console.WriteLine("Welcome to the app!");
            Console.WriteLine("Press 1 to if you are a admin.\n Press 2 if u are a user.");
            var isAdmin = Console.ReadLine();

            if(isAdmin == "1")
            {
                Console.WriteLine("Press 1 to go to Profile.\n Press 2 to go to Games. \n Press 3 to go to Routes. \n Press 4 to Add a observation. \n Press 5 to create Route. \n Press 5 to create Area.\n Press 5 to create Game. \n Press 5 to create POI. \n Press 5 to create RoutePoint.");
            } else 
            {
                Console.WriteLine("Press 1 to go to Profile.\n Press 2 to go to Games. \n Press 3 to go to Routes. \n Press 4 to Add a observation. \n Press 5 to create user.");
            }

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
