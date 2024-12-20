﻿using CasusExotischNederland.Model;
namespace CasusExotischNederland
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Welcome to the app!");
            Console.WriteLine("Press 1 if you want to create a user. \nPress 2 if you want to login.");
            var LoginAction = Console.ReadLine();

            if (LoginAction == "1")
            {
                CreateUser();
                Console.WriteLine($"This is your ID: {GlobalVariables.CurrentUserId}");
            }
            else if (LoginAction == "2")
            {
                Console.WriteLine("Please enter your user Id: ");
                GlobalVariables.CurrentUserId = Int32.Parse(Console.ReadLine());
                Menu();
            }
        }

        static void CreateUser()
        {
            Console.WriteLine("Welcome to creating a user.");
            Console.WriteLine("Enter User Name: ");
            string userName = Console.ReadLine();
            Console.WriteLine("Enter User Age: ");
            int UserAge = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter User Email: ");
            string UserEmail = Console.ReadLine();
            Console.WriteLine("Enter User PhoneNumber: ");
            int UserPhoneNumber = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Press y if u agree to the saving of your personal information.");
            var agree = Console.ReadLine();

            if (agree != "y")
            {
                Console.WriteLine("You have to agree to the saving of your personal information.");
                CreateUser();
            }
            else { }
            User user = new User(0, userName, UserAge, UserEmail, UserPhoneNumber);
            GlobalVariables.CurrentUserId = user.Create();
            Console.WriteLine("User has been Added");
            Console.WriteLine("Press 'Enter' to go to the main menu.");
            Console.ReadLine();
            Console.Clear();
            Menu();
        }

        static void GameLogic()
        {
            Area area = new Area();
            Route route = new Route();
            Game game = new Game();
            Question question = new Question();
            User user = new User();
            
            User currentUser = user.GetbyId(GlobalVariables.CurrentUserId);

            Console.WriteLine("Select an area:");
            foreach (Area myArea in area.GetAll())
            {
                Console.WriteLine(myArea.Id + "  " + myArea.Name);
            }

            Console.WriteLine("Enter the area ID:");
            int areaId = int.Parse(Console.ReadLine());
            area = area.GetById(areaId);
            Console.WriteLine($"Available routes in {area.Name}. Please select a route:");
            
            foreach (Route myRoute in route.GetRoutesByArea(area.Id))
            {
                Console.WriteLine(myRoute.Id + "  " + myRoute.Name);
            }

            Console.WriteLine("Enter the route ID:");
            int routeId = int.Parse(Console.ReadLine());
            route = route.GetById(routeId);
            Console.WriteLine($"Available games on {route.Name}. Please select a game:");
            List<Game> games = game.GetGamesByRoute(route.Id);
            
            foreach (Game myGame in games)
            {
                Console.WriteLine(myGame.Id + "  " + myGame.Name);
            }

            Console.WriteLine("Enter the game ID:");
            int gameId = int.Parse(Console.ReadLine());
            game= games.FirstOrDefault(g => g.Id == gameId);
            Console.WriteLine("Game questions");
            int questionsCounter = 0;
            List<Question> questions = game.Questions;
            int points = 0;
            
            while (questionsCounter < questions.Count)
            {
                Question myQuestion = questions[questionsCounter];
                Console.WriteLine("Provide your answer:");
                Console.WriteLine($"{myQuestion.QuestionText}");
                int answersCounter = 1;
                foreach(Answer answer in myQuestion.Answers)
                {
                    Console.WriteLine($"\t{answersCounter} {answer.AnswerText}");
                    answersCounter++;
                }
                int choosenAnswerId = Int32.Parse(Console.ReadLine());
                Answer choosenAnswer = myQuestion.Answers[choosenAnswerId-1];
                if (choosenAnswer.IsCorrect == true)
                {
                    Console.WriteLine("Correct answer!");
                    points += myQuestion.AmountOfPoints;
                }
                else
                {
                    Answer answer = myQuestion.Answers.FirstOrDefault(a => a.IsCorrect);
                    Console.WriteLine($"Incorrect answer.\nThe correct answer is {answer.AnswerText}");
                }
                questionsCounter++;
                game.SaveGivenAnswer(currentUser, myQuestion, choosenAnswer);
            }

            Console.WriteLine($"Game completed!\nYour score is: {points}");
            Console.WriteLine("Press 'Enter' to go to the main menu.");
            Console.ReadLine();
            Console.Clear();
            Menu();
        }

        static async Task AddObservation()
        {
            Area area = new Area();
            Species species = new Species();
            User user = new User();

            Console.WriteLine("Enter the observation name:");
            string observationName = Console.ReadLine();

            Console.WriteLine("Enter the observation photo URL:");
            string observationPhoto = Console.ReadLine();

            Console.WriteLine("Choose an area: ");
            foreach (Area myArea in area.GetAll())
            {
                Console.WriteLine(myArea.Id + "  " + myArea.Name);
            }

            Console.WriteLine("Enter the area ID: ");
            int areaId = int.Parse(Console.ReadLine());
            Area selectedArea = area.GetById(areaId);
            Console.WriteLine("Choose a species: ");
          
            foreach (Species mySpecies in species.GetAll())
            {
                Console.WriteLine(mySpecies.Id + "  " + mySpecies.Name);
            }

            Console.WriteLine("Enter the species ID: ");
            int speciesId = int.Parse(Console.ReadLine());
            Species selectedSpecies = species.GetById(speciesId);

            LocationService locationService = new LocationService();
            LocationInfo locationInfo = await locationService.GetLocationInfoAsync();

            string location = locationInfo.Region + " " + locationInfo.City;
            float coordinateX = locationInfo.CoordinateX;
            float coordinateY = locationInfo.CoordinateY;
            Console.WriteLine("Fetching location...");
            Console.WriteLine("Observation location: " + location);
            Console.WriteLine("Press 'Enter' to save the observation.");
            Console.ReadLine();

            Observation observation = new Observation(1, selectedArea, selectedSpecies, user.GetbyId(GlobalVariables.CurrentUserId), DateTime.Now.Date, observationName, coordinateX, coordinateY, observationPhoto, location);
            observation.Create();

            Console.WriteLine("Observation has been successfully added.");
            Console.WriteLine("Press 'Enter' to go to the main menu.");
            Console.ReadLine();
            Console.Clear();
            Menu();
        }

        static void ShowProfile()
        {
            User user = new User();
            user = user.GetbyId(GlobalVariables.CurrentUserId);
            user.GetbyId(user.Id);
            Console.WriteLine($"This is your ID: {user.Id}, Name: {user.Name}, Age: {user.Age}, Email: {user.Email}, Phonenumber: {user.PhoneNumber}");
            Console.WriteLine("Press 1 if you want to return to main menu. \nPress 2 if you want to update your profile. \nPress 3 if you want to delete your profile.");
            var ProfileAction = Console.ReadLine();

            if (ProfileAction == "1")
            {
                Menu();
            }
            else if (ProfileAction == "2")
            {
                Console.WriteLine("Welcome to updating your profile.");
                Console.WriteLine("Enter your ID:");
                int selectedID = Convert.ToInt32(Console.ReadLine());
                user.GetbyId(user.Id);

                Console.WriteLine("Enter User Name: ");
                string newUserName = Console.ReadLine();

                Console.WriteLine("Enter User Age: ");
                int newUserAge = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Enter User Email: ");
                string newUserEmail = Console.ReadLine();

                Console.WriteLine("Enter Phone Number");
                int newUserPhoneNumber = Convert.ToInt32(Console.ReadLine());
                
                User updatedUser = new User(selectedID, newUserName, newUserAge, newUserEmail, newUserPhoneNumber);
                updatedUser.Update();
                Console.WriteLine("User has been updated.");
                ShowProfile();
            }
            else if (ProfileAction == "3")
            {
                Console.WriteLine("Do you really want to delete your profile?");
                Console.WriteLine("Press 1 if you want to delete your profile. \nPress 2 to cancel this action.");
                var deleteUser = Console.ReadLine();

                if (deleteUser == "1")
                {
                    user.Delete(user.Id);
                    Console.WriteLine("Your profile has been deleted.");
                    CreateUser();
                }
                else if (deleteUser == "2")
                {
                    Console.WriteLine("This action was canceled.");
                    ShowProfile();
                }
            }
        }

        static void SelectRoute()
        {
            Area area = new Area();
            List<Area> areas = area.GetAll();

            Console.WriteLine("Select an Area:");
            foreach (var a in areas)
            {
                Console.WriteLine($"{a.Id}: {a.Name} - {a.Description}");
            }

            Console.Write("Enter the Area ID: ");
            int selectedAreaId = int.Parse(Console.ReadLine());
            
            Area selectedArea = area.GetById(selectedAreaId);
            if (selectedArea == null)
            {
                Console.WriteLine("Invalid Area selected.");
                return;
            }
            
            Console.WriteLine($"\nRoutes available in Area: {selectedArea.Name}");
            Route route = new Route();
            List<Route> routes = route.GetRoutesByArea(selectedAreaId);  

            if (routes.Count == 0)
            {
                Console.WriteLine("No routes found for the selected area.");
                return;
            }

            foreach (var r in routes)
            {
                Console.WriteLine($"{r.Id}: {r.Name} - {r.Description}");
            }
            
            Console.Write("Enter the Route ID: ");
            int selectedRouteId = int.Parse(Console.ReadLine());
            
            Route selectedRoute = routes.Find(r => r.Id == selectedRouteId);
            if (selectedRoute == null)
            {
                Console.WriteLine("Invalid Route selected.");
                return;
            }
             
            Console.WriteLine($"\nRoute points for Route: {selectedRoute.Name}");
            List<RoutePoint> routePoints = route.GetRoutePoints(selectedRouteId);  

            if (routePoints != null && routePoints.Count > 0)
            {
                foreach (var point in routePoints)
                {
                    Console.WriteLine($"{point.Id}: {point.Name} - {point.Description}");
                    Console.WriteLine($"Coordinates: ({point.CoordinateX}, {point.CoordinateY})");

                    Poi poi = new();
                    if (point.Id == poi.GetAll()[0].RoutePoint.Id)
                    {
                        point.GetPoiByRoutePointId(point.Id);
                    }
                    
                    if (point.poi != null)
                    {
                        Console.WriteLine($"POI: {point.poi.Name} - {point.poi.Description}");
                        Console.WriteLine($"POI Type: {point.poi.Type}");
                    }
                    else
                    {
                        Console.WriteLine("No POI associated with this route point.");
                    }
                    Console.WriteLine();  
                }
            }
            else
            {
                Console.WriteLine("No route points found for this route.");
            }
           
            Console.WriteLine("\nPress 'Enter' to return to the main menu.");
            Console.ReadLine();
            Console.Clear();
            Menu();
        }

        static void GenerateShortestRoute()
        {
            Console.WriteLine("Enter the start point(a,b,c,d,e,f): ");
            string start = Console.ReadLine();
            Console.WriteLine("Enter the end point(a,b,c,d,e,f): ");
            string end = Console.ReadLine();
            Console.WriteLine("Generating shortest route...");
            Dictionary<string, Dictionary<string, int>> graph = new Dictionary<string, Dictionary<string, int>>()
            {
                { "a", new Dictionary<string, int> { { "f", 12 }, { "b", 1 }, { "e", 2 } } },
                { "b", new Dictionary<string, int> { { "a", 1 }, { "e", 6 }, { "c", 2 }, { "d", 1 } } },
                { "c", new Dictionary<string, int> { { "b", 2 }, { "f", 8 } } },
                { "d", new Dictionary<string, int> { { "b", 1 }, { "e", 6 }, { "f", 6 } } },
                { "e", new Dictionary<string, int> { { "a", 2 }, { "b", 6 }, { "d", 6 } } },
                { "f", new Dictionary<string, int> { { "a", 12 }, { "c", 8 }, { "d", 6 } } }
            };
            Dictionary<string, int> distances = new Dictionary<string, int>();
            Dictionary<string, string> previousNodes = new Dictionary<string, string>();
            List<string> unvisitedNodes = graph.Keys.ToList();

            foreach (var node in graph.Keys)
            {
                distances[node] = int.MaxValue;
                previousNodes[node] = null;
            }
            distances[start] = 0;

            while (unvisitedNodes.Count > 0)
            {
                string currentNode = unvisitedNodes.OrderBy(node => distances[node]).First();
                unvisitedNodes.Remove(currentNode);

                if (currentNode == end)
                {
                    break;
                }

                foreach (var neighbor in graph[currentNode])
                {
                    if (!unvisitedNodes.Contains(neighbor.Key))
                    {
                        continue;
                    }
                    int newDist = distances[currentNode] + neighbor.Value;
                    if (newDist < distances[neighbor.Key])
                    {
                        distances[neighbor.Key] = newDist;
                        previousNodes[neighbor.Key] = currentNode;
                    }
                }
            }

            List<string> path = new List<string>();
            string step = end;
            while (step != null)
            {
                path.Insert(0, step);
                step = previousNodes[step];
            }

            if (path[0] == start)
            {
                Console.WriteLine(string.Join(", ", path));
            }
            else
            {
                Console.WriteLine( new List<string> { "No path found" });
            }
            Console.WriteLine("Press 'Enter' to go to the main menu.");
            Console.ReadLine();
            Console.Clear();
            Menu();
        }

        static void Menu()
        {
            User user = new User();
            List<int> userRols= new List<int>();
            userRols = user.GetRolesByUserId(Convert.ToInt32(GlobalVariables.CurrentUserId));

            if (userRols.Contains(1))
            {
                Console.WriteLine("Press 1 to go to Profile.\nPress 2 to go to Games. \nPress 3 to go to Routes. \nPress 4 to Add a observation. \nPress 5 to create Route. \nPress 6 to create Area.\nPress 7 to create Game. \nPress 8 to create POI. \nPress 9 to create RoutePoint.");
            } else 
            {
                Console.WriteLine("Press 1 to go to Profile.\nPress 2 to go to Games. \nPress 3 to go to Routes. \nPress 4 to Add a observation. \nPress 5 to create user. \nPress 6 to to see the map.");
            }
            var input = Console.ReadLine();
            
            switch (input)
            {
                case "1":
                    ShowProfile();
                    break;
                case "2":
                    GameLogic();
                    break;
                case "3":
                    SelectRoute();
                    break;
                case "4":
                    AddObservation().Wait();
                    break;
                case "5":
                    CreateUser();
                    break;
                case "6":
                    GenerateShortestRoute();
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    Menu();
                    break;
            }
        }
    }
}
