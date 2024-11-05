using CasusExotischNederland.Model;
using System.Numerics;

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
            Console.WriteLine($"This is your ID: {user.Id}, Name: {user.Name}, Age: {user.Age}, Email: {user.Email}, Phonenumber: {user.PhoneNumber}");
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
                Console.WriteLine("Welkom bij het updaten van jouw profiel.");
                Console.WriteLine("geef de id op");
                int selectedID = Convert.ToInt32(Console.ReadLine());
                user.GetUserbyId(user.Id);

                Console.WriteLine("Enter User Name: ");
                string newUserName = Console.ReadLine();

                Console.WriteLine("Enter User Age: ");
                int newUserAge = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Enter User Email: ");
                string newUserEmail = Console.ReadLine();

                Console.WriteLine("Enter Phone Number");
                int newUserPhoneNumber = Convert.ToInt32(Console.ReadLine());
                
                User updatedUser = new User(selectedID, newUserName, newUserAge, newUserEmail, newUserPhoneNumber);
                updatedUser.UpdateUser();
                Console.WriteLine("User has been updated.");
                Program.ShowProfile();
            }
            else if (ProfileAction == "3")
            {
                Console.WriteLine("Do you really want to delete your profile?");
                Console.WriteLine("Press 1 if you want to delete your profile. \nPress 2 to cancel this action.");
                var deleteUser = Console.ReadLine();

                if (deleteUser == "1")
                {
                    user.DeleteUser(user.Id);
                    Console.WriteLine("Your profile has been deleted.");
                    Program.StartApp();
                }
                else if (deleteUser == "2")
                {
                    Console.WriteLine("This action was canceled.");
                    Program.ShowProfile();
                }
            }
        }
        static async Task StartApp()
        {


            Console.WriteLine("Welcome to the app!");
            Console.WriteLine("Press 1 to if you are a admin.\nPress 2 if u are a user.");
            var isAdmin = Console.ReadLine();

            if(isAdmin == "1")
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
