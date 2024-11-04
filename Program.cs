using CasusExotischNederland.Model;
using System.Buffers;
using System.Numerics;
using System.Runtime.InteropServices;

namespace CasusExotischNederland
{
    internal class Program 
    {
        static async Task CreateUser()
        {
            Console.WriteLine("Welkom bij het creeren van een User");

            Console.WriteLine("Enter User Name: ");
            string userName = Console.ReadLine();
            Console.WriteLine("Enter User Age: ");
            int UserAge = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter User Email: ");
            string UserEmail = Console.ReadLine();
            Console.WriteLine("Enter User PhoneNumber: ");
            int UserPhoneNumber = Convert.ToInt32(Console.ReadLine());


            User user = new User(0,userName,UserAge,UserEmail,UserPhoneNumber);
            user.CreateUser();
            Console.WriteLine("User has been Added");

        }
        static async Task AddObservation()
        {
            Area area = new Area();
            Species species = new Species();
            
            
            User user = new User(1, "user1", 44, "email1@gmail.com", 23563634);

            Console.WriteLine("Enter observation name: ");
            string observationName = Console.ReadLine();

            Console.WriteLine("Observation photo: ");
            string observationPhoto = Console.ReadLine();

            Console.WriteLine("Kies een Area: ");
            foreach(Area myArea in area.GetAll())
            {
                Console.WriteLine(myArea.Id + "  " + myArea.Name);
            }
            Console.WriteLine("Vul de area id in: ");
            int areaId = int.Parse(Console.ReadLine());
            Area selectedArea = area.Get(areaId);


            Console.WriteLine("Kies een Specie: ");
            foreach (Species mySpecies in species.GetAll())
            {
                Console.WriteLine(mySpecies.Id + "  " + mySpecies.Name);
            }
            Console.WriteLine("Vul de specie id in: ");
            int speciesId = int.Parse(Console.ReadLine());
            Species selectedSpecies = species.Get(speciesId);
            

            LocationService locationService = new LocationService();
            LocationInfo locationInfo = await locationService.GetLocationInfoAsync();

            string location = locationInfo.Region + " " + locationInfo.City;
            float coordinateX = locationInfo.CoordinateX;
            float coordinateY = locationInfo.CoordinateY;
            Console.WriteLine("Locatie ophalen...");
            Console.WriteLine("Observation location: " + location);
            Console.WriteLine("Druk 'Enter' om observation op te slaan.");
            Console.ReadLine();

            Observation observation = new Observation(1, selectedArea, selectedSpecies, user, DateTime.Now.Date, observationName, coordinateX, coordinateY, observationPhoto,location);
            observation.Add();

        }

        static async Task ShowProfile()
        {
            User user = new User(1, "Bjarne", 18, "bjarne", 123);
            user.GetUserbyId(user.Id);
            Console.WriteLine("This is your ID: " + user.Id, "Name: " + user.Name, "Age: " + user.Age, "Email: " + user.Email, "Phonenumber: " + user.PhoneNumber);
            Console.WriteLine("Press 1 if you want to return to main menu. \nPress 2 if you want to update your profile. \nPress 3 if you want to delete your profile.");
            var ProfileAction = Console.ReadLine();

            if (ProfileAction == "1")
            {
                Console.WriteLine("Press 1 to go to Profile.\nPress 2 to go to Games. \nPress 3 to go to Routes. \nPress 4 to Add a observation. \nPress 5 to create user.");
                Console.ReadLine();
                Program.ShowProfile();
            }
            else if (ProfileAction == "2")
            {
                user.UpdateUser();
            }
            else if (ProfileAction == "3")
            {
                user.DeleteUser(user.Id);

            }
        }

        static List<string> GenerateShortestRoute(string start, string end)
        {
            Console.WriteLine("Generating shortest route...");
            Dictionary<string, Dictionary<string, int>> graph = new Dictionary<string, Dictionary<string, int>>()
            {
                { "a", new Dictionary<string, int> { { "f", 12 }, { "b", 1 }, { "e", 2 } } },
                { "b", new Dictionary<string, int> { { "a", 1 }, { "e", 6 }, { "c", 2 }, { "d", 4 } } },
                { "c", new Dictionary<string, int> { { "b", 2 }, { "f", 8 } } },
                { "d", new Dictionary<string, int> { { "b", 1 }, { "e", 6 }, { "f", 6 } } },
                { "e", new Dictionary<string, int> { { "a", 2 }, { "b", 6 }, { "d", 6 } } },
                { "f", new Dictionary<string, int> { { "a", 12 }, { "c", 8 }, { "d", 6 } } }
            };

            Dictionary<string, int> possibleDistances = new Dictionary<string, int>();
            int tempDistance = 0;
            int defDistance = 0;
            List<string> CurrentRoute = new List<string>();

            foreach (var item in graph[start])
            {
                possibleDistances.Add(item.Key, item.Value);
            }

            tempDistance = possibleDistances.Values.Min();
            defDistance += possibleDistances.Values.Min();
            start = possibleDistances.FirstOrDefault(x => x.Value == possibleDistances.Values.Min()).Key;






            return CurrentRoute;
        }




























           /* var distances = new Dictionary<string, int>();
            var previousVertices = new Dictionary<string, string>();
            var priorityQueue = new SortedSet<(int distance, string vertex)>();

            foreach (var vertex in graph.Keys)
            {
                distances[vertex] = int.MaxValue;
                previousVertices[vertex] = null;
            }
            distances[start] = 0;
            priorityQueue.Add((0, start));

            while (priorityQueue.Count > 0)
            {
                var (currentDistance, currentVertex) = priorityQueue.Min;
                priorityQueue.Remove(priorityQueue.Min);

                if (currentDistance > distances[currentVertex])
                    continue;

                foreach (var (neighbor, weight) in graph[currentVertex])
                {
                    int distance = currentDistance + weight;

                    if (distance < distances[neighbor])
                    {
                        priorityQueue.Remove((distances[neighbor], neighbor));
                        distances[neighbor] = distance;
                        previousVertices[neighbor] = currentVertex;
                        priorityQueue.Add((distance, neighbor));
                    }
                }
            }

            return (previousVertices);
        



    }
        static List<string> GetShortestPath(Dictionary<string, string> previousVertices, string start, string target)
        {
            var path = new List<string>();
            string currentVertex = target;

            while (currentVertex != null)
            {
                path.Add(currentVertex);
                currentVertex = previousVertices[currentVertex];
            }

            path.Reverse();
            return path;
        }*/



        static async Task StartApp()
        {
            GenerateShortestRoute("e","c");
            //foreach(string item in GetShortestPath(GenerateShortestRoute("a"), "a", "f")) { Console.WriteLine(item); }
            Console.WriteLine("Welcome to the app!");
            Console.WriteLine("Please enter your user Id: ");
            var UserId = Console.ReadLine();
            User user = new User();
            List<int> userRols= new List<int>();
            userRols = user.GetRoles(Convert.ToInt32(UserId));

            if (userRols.Contains(1))
            {
                Console.WriteLine("Press 1 to go to Profile.\nPress 2 to go to Games. \nPress 3 to go to Routes. \nPress 4 to Add a observation. \nPress 5 to create Route. \nPress 5 to create Area.\nPress 5 to create Game. \nPress 5 to create POI. \nPress 5 to create RoutePoint.");
            } else 
            {
                Console.WriteLine("Press 1 to go to Profile.\nPress 2 to go to Games. \nPress 3 to go to Routes. \nPress 4 to Add a observation. \nPress 5 to create user.");
            }

            var input = Console.ReadLine();
            int value;
            if (Int32.TryParse(input.ToString(), out value))
            {
                if (value == 1) { await ShowProfile(); }
                else if (value == 2) { }
                else if (value == 3) { }
                else if (value == 4) { await AddObservation(); }
                else if (value == 5) { await CreateUser(); }

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
