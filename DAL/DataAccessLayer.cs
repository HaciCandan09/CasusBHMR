using CasusExotischNederland.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasusExotischNederland.DAL
{
    public class DataAccessLayer
    {

        private string connectionString = "Data Source=RENAD; Initial Catalog=ExotischNederland; Integrated Security=True;";

        public DataAccessLayer()
        {

        }

        // User CRUD
        public List<User> GetAllUser()
        {
            List<User> users = new List<User>();
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                connect.Open();
                string sql = "SELECT ID,Name,Age,Email,PhoneNumber  FROM [User]";
                using (SqlCommand cmd = new SqlCommand(sql, connect))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            int age = reader.GetInt32(2);
                            string email = reader.GetString(3);
                            int phonenumber = reader.GetInt32(4);

                            users.Add(new User(id, name,age,email,phonenumber));
                        }
                    }
                }
            }
            return users;
        }

        public int CreateUser(User user)
        {
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                connect.Open();
                string sql = "INSERT INTO [User] (Name,Age,Email,PhoneNumber) VALUES (@Name,@Age,@Email,@PhoneNumber); SELECT SCOPE_IDENTITY();";
                using (SqlCommand cmd = new SqlCommand(sql, connect))
                {
                    cmd.Parameters.AddWithValue("@Name", user.Name);
                    cmd.Parameters.AddWithValue("@Age", user.Age);
                    cmd.Parameters.AddWithValue("@Email", user.Email);
                    cmd.Parameters.AddWithValue("@PhoneNumber", user.PhoneNumber);
                    int userId = Convert.ToInt32(cmd.ExecuteScalar());

                    connect.Close();
                    return userId;
                }

            }
        }

        public void UpdateUser(User user)
        {
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                connect.Open();
                string sql = "UPDATE [User] SET Name = @Name, Age = @Age, Email = @Email, PhoneNumber =@PhoneNumber WHERE ID = @ID";
                using (SqlCommand cmd = new SqlCommand(sql, connect))
                {
                    cmd.Parameters.AddWithValue("@ID", user.Id);
                    cmd.Parameters.AddWithValue("@Name", user.Name);
                    cmd.Parameters.AddWithValue("@Age", user.Age);
                    cmd.Parameters.AddWithValue("@Email", user.Email);
                    cmd.Parameters.AddWithValue("@PhoneNumber", user.PhoneNumber);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteUser(int UserId)
        {
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                connect.Open();
                string sql = "DELETE FROM [User] WHERE ID = @ID";
                using (SqlCommand cmd = new SqlCommand(sql, connect))
                {
                    cmd.Parameters.AddWithValue("@ID", UserId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public User GetUserById(int userId)
        {
            User user = null;
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                connect.Open();
                string sql = "SELECT * FROM [User] WHERE ID = @ID ";
                using (SqlCommand cmd = new SqlCommand(sql, connect))
                {
                    cmd.Parameters.AddWithValue("@ID", userId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = new User(
                                reader.GetInt32(0),
                                reader.GetString(1),
                                reader.GetInt32(2),
                                reader.GetString(3),
                                reader.GetInt32(4)
                            );
                        }
                    }
                }
            }
            return user;
        }

        public Rol GetRoleById(int id)
        {
            Rol role = null;
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                connect.Open();
                string sql = "SELECT * FROM [Role] WHERE ID = @ID ";
                using (SqlCommand cmd = new SqlCommand(sql, connect))
                {
                    cmd.Parameters.AddWithValue("@ID", id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            role = new Rol(
                                reader.GetInt32(0),
                                reader.GetString(1)
                            );
                        }
                    }
                }
            }
            return role;
        }

        public List<Rol> GetRolesByUserId(int userId)
        {
            List<Rol> roles = new List<Rol>();
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                connect.Open();
                string sql = "SELECT * FROM [RolUser] WHERE UserID = @UserID ";
                using (SqlCommand cmd = new SqlCommand(sql, connect))
                {
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            
                            roles.Add(GetRoleById(id));
                        }
                    }
                }
            }
            return roles;
        }


        // Route CRUD

        public List<Route> GetAllRoutes()
        {
            List<Route> routes = new List<Route>();
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                connect.Open();
                string sql = "SELECT * FROM Route";
                using (SqlCommand cmd = new SqlCommand(sql, connect))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            routes.Add(new Route(
                                reader.GetInt32(0),
                                GetAreaById(reader.GetInt32(1)),
                                reader.GetString(2),
                                reader.GetString(3)
                            ));
                        }
                    }
                }
            }
            return routes;


        }

        public Route GetRouteById(int routeId)
        {
            Route route = null;
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                connect.Open();
                string sql = "SELECT * FROM Route WHERE ID = @routeID ";
                using (SqlCommand cmd = new SqlCommand(sql, connect))
                {
                    cmd.Parameters.AddWithValue("@routeID", routeId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            route = new Route(
                                reader.GetInt32(0),
                                GetAreaById(reader.GetInt32(1)),
                                reader.GetString(2),
                                reader.GetString(3)
                            );
                        }
                    }
                }
            }
            return route;
        }

        public List<Route> GetRoutesByAreaID(int areaId)
        {
            List<Route> routes = new List<Route>();
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                connect.Open();
                string sql = "SELECT id, areaid, name, description FROM Route WHERE areaid = @AreaId";
                using (SqlCommand cmd = new SqlCommand(sql, connect))
                {
                    cmd.Parameters.AddWithValue("@AreaId", areaId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            Area area = GetAreaById(reader.GetInt32(1));
                            string name = reader.GetString(2);
                            string description = reader.GetString(3);
                            routes.Add(new Route(id, area, name, description));
                        }
                    }
                }
            }
            return routes;
        }

        public void CreateRoute(Route route)
        {
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                connect.Open();
                string sql = "INSERT INTO Route (areaid, name, description) VALUES (@AreaId, @Name, @Description)";
                using (SqlCommand cmd = new SqlCommand(sql, connect))
                {
                    cmd.Parameters.AddWithValue("@AreaId", route.Area.Id);
                    cmd.Parameters.AddWithValue("@Name", route.Name);
                    cmd.Parameters.AddWithValue("@Description", route.Description);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateRoute(Route route)
        {
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                connect.Open();
                string sql = "UPDATE Route SET areaid = @AreaId, name = @Name, description = @Description WHERE id = @Id";
                using (SqlCommand cmd = new SqlCommand(sql, connect))
                {
                    cmd.Parameters.AddWithValue("@Id", route.Id);
                    cmd.Parameters.AddWithValue("@AreaId", route.Id);
                    cmd.Parameters.AddWithValue("@Name", route.Name);
                    cmd.Parameters.AddWithValue("@Description", route.Description);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteRoute(int routeId)
        {
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                connect.Open();
                string sql = "DELETE FROM Route WHERE id = @Id";
                using (SqlCommand cmd = new SqlCommand(sql, connect))
                {
                    cmd.Parameters.AddWithValue("@Id", routeId);
                    cmd.ExecuteNonQuery();
                }
            }
        }



        /*
        public Route GetRouteById(int routeId)
        {
            Route route = null;
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                connect.Open();
                string sql = "SELECT ID, areaid, name, description FROM Route";
                using (SqlCommand cmd = new SqlCommand(sql, connect))
                {
                    cmd.Parameters.AddWithValue("@ID", routeId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            route = new Route(
                                reader.GetInt32(0),
                                reader.GetInt32(1),
                                reader.GetString(2),
                                reader.GetString(3)
                            );
                        }
                    }
                }                    
            }
            return route;
        }
        */

        // RoutePoint CRUD
        public List<RoutePoint> GetRoutePointsByRouteId(int routeId)
        {
            List<RoutePoint> routePoints = new List<RoutePoint>();

            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                connect.Open();

                // SQL Query to fetch all route points associated with the given RouteId
                string sql = @"
            SELECT rp.ID, rp.Name, rp.Description, rp.CoordinateX, rp.CoordinateY
            FROM RoutePointRoute rpr
            JOIN RoutePoint rp ON rpr.RoutePointID = rp.ID
            WHERE rpr.RouteID = @RouteID";  // Using RouteID to filter

                using (SqlCommand cmd = new SqlCommand(sql, connect))
                {
                    cmd.Parameters.AddWithValue("@RouteID", routeId);  // Pass the RouteID as a parameter

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Iterate through the results and map them to RoutePoint objects
                        while (reader.Read())
                        {
                            int pointId = reader.GetInt32(0);  // RoutePoint ID
                            string pointName = reader.GetString(1);  // RoutePoint Name
                            string pointDescription = reader.GetString(2);  // RoutePoint Description
                            float coordinateX = (float)reader.GetDouble(3);  // CoordinateX
                            float coordinateY = (float)reader.GetDouble(4);  // CoordinateY

                            // Add the RoutePoint to the list
                            routePoints.Add(new RoutePoint(pointId, pointName, pointDescription, coordinateX, coordinateY));
                        }
                    }
                }
            }

            return routePoints;
        }
        public List<RoutePoint> GetRoutePoints()
        {
            List<RoutePoint> routePoints = new List<RoutePoint>();
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                connect.Open();
                string sql = "SELECT id, name, description, coordinateX, coordinateY FROM RoutePoint";
                using (SqlCommand cmd = new SqlCommand(sql, connect))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            string description = reader.GetString(2);
                            float coordinateX = reader.GetFloat(3);
                            float coordinateY = reader.GetFloat(4);
                            routePoints.Add(new RoutePoint(id, name, description, coordinateX, coordinateY));
                        }
                    }
                }
            }
            return routePoints;
        }

        //public List<RoutePoint> GetRoutePoints(int routeId)
        //{
        //    List<RoutePoint> routePoints = new List<RoutePoint>();

        //    using (SqlConnection connect = new SqlConnection(connectionString))
        //    {
        //        connect.Open();
        //        // Adjust the query to select route points where RouteID matches the given routeId
        //        string sql = "SELECT ID, Name, Description, CoordinateX, CoordinateY FROM RoutePoint WHERE ID = @ID";

        //        using (SqlCommand cmd = new SqlCommand(sql, connect))
        //        {
        //            cmd.Parameters.AddWithValue("@ID", routeId);

        //            using (SqlDataReader reader = cmd.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    RoutePoint routePoint = new RoutePoint(
        //                        reader.GetInt32(0),    // ID
        //                        reader.GetString(1),   // Name
        //                        reader.GetString(2),   // Description
        //                        reader.GetFloat(3),    // CoordinateX
        //                        reader.GetFloat(4)     // CoordinateY
        //                    );

        //                    routePoints.Add(routePoint);
        //                }
        //            }
        //        }
        //    }

        //    return routePoints;
        //}

        public List<RoutePoint> GetRoutePoints(int routeId)
        {
            List<RoutePoint> routePoints = new List<RoutePoint>();

            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                connect.Open();

                string sql = @"
            SELECT RP.Id, RP.Name, RP.Description, RP.CoordinateX, RP.CoordinateY, 
                   POI.Id AS PoiId, POI.Name AS PoiName, POI.Description AS PoiDescription, 
                   POI.CoordinateX AS PoiCoordinateX, POI.CoordinateY AS PoiCoordinateY, POI.Type AS PoiType
            FROM RoutePoint RP
            LEFT JOIN POI POI ON RP.Id = POI.RoutePointID
            WHERE RP.RouteId = @RouteId";

                using (SqlCommand cmd = new SqlCommand(sql, connect))
                {
                    cmd.Parameters.AddWithValue("@RouteId", routeId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            RoutePoint routePoint = new RoutePoint
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Description = reader.IsDBNull(2) ? null : reader.GetString(2),
                                CoordinateX = reader.GetFloat(3),
                                CoordinateY = reader.GetFloat(4),
                            };

                            // Check if a POI exists for this route point and set it
                            if (!reader.IsDBNull(5)) // PoiId
                            {
                                routePoint.poi = new Poi
                                {
                                    Id = reader.GetInt32(5),
                                    Name = reader.GetString(6),
                                    Description = reader.IsDBNull(7) ? null : reader.GetString(7),
                                    CoordinateX = reader.GetFloat(8),
                                    CoordinateY = reader.GetFloat(9),
                                    Type = reader.IsDBNull(10) ? null : reader.GetString(10)
                                };
                            }

                            routePoints.Add(routePoint);
                        }
                    }
                }
            }

            return routePoints;
        }





        public void CreateRoutePoint(RoutePoint routePoint)
        {
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                connect.Open();
                string sql = "INSERT INTO RoutePoint (name, description, coordinateX, coordinateY) VALUES (@Name, @Description, @CoordinateX, @CoordinateY)";
                using (SqlCommand cmd = new SqlCommand(sql, connect))
                {
                    cmd.Parameters.AddWithValue("@Name", routePoint.Name);
                    cmd.Parameters.AddWithValue("@Description", routePoint.Description);
                    cmd.Parameters.AddWithValue("@CoordinateX", routePoint.CoordinateX);
                    cmd.Parameters.AddWithValue("@CoordinateY", routePoint.CoordinateY);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateRoutePoint(RoutePoint routePoint)
        {
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                connect.Open();
                string sql = "UPDATE RoutePoint SET name = @Name, description = @Description, coordinateX = @CoordinateX, coordinateY = @CoordinateY WHERE id = @Id";
                using (SqlCommand cmd = new SqlCommand(sql, connect))
                {
                    cmd.Parameters.AddWithValue("@Id", routePoint.Id);
                    cmd.Parameters.AddWithValue("@Name", routePoint.Name);
                    cmd.Parameters.AddWithValue("@Description", routePoint.Description);
                    cmd.Parameters.AddWithValue("@CoordinateX", routePoint.CoordinateX);
                    cmd.Parameters.AddWithValue("@CoordinateY", routePoint.CoordinateY);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteRoutePoint(int routePointId)
        {
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                connect.Open();
                string sql = "DELETE FROM RoutePoint WHERE id = @Id";
                using (SqlCommand cmd = new SqlCommand(sql, connect))
                {
                    cmd.Parameters.AddWithValue("@Id", routePointId);
                    cmd.ExecuteNonQuery();
                }
            }
        }


        public RoutePoint GetRoutePointById(int routePointId)
        {
            RoutePoint routePoint = null;
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                connect.Open();
                string sql = "SELECT ID, Name, Description, CoordinateX, CoordinateY FROM RoutePoint";
                using (SqlCommand cmd = new SqlCommand(sql, connect))
                {
                    cmd.Parameters.AddWithValue("@ID", routePointId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            routePoint = new RoutePoint(
                                reader.GetInt32(0),
                                reader.GetString(1),
                                reader.GetString(2),
                                reader.GetFloat(3),
                                 reader.GetFloat(4)
                            );
                        }
                    }
                }
            }
            return routePoint;
        }

        public List<Poi> GetPointsOfInterest()
        {
            List<Poi> pois = new List<Poi>();
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                connect.Open();
                string sql = "SELECT id, routepointid, name, description, coordinateX, coordinateY, type FROM POI";
                using (SqlCommand cmd = new SqlCommand(sql, connect))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            RoutePoint routePointId = GetRoutePointById(reader.GetInt32(1));
                            string name = reader.GetString(2);
                            string description = reader.GetString(3);
                            float coordinateX = reader.GetFloat(4);
                            float coordinateY = reader.GetFloat(5);
                            string type = reader.GetString(6);
                            pois.Add(new Poi(id, routePointId, name, description, coordinateX, coordinateY, type));
                        }
                    }
                }
            }
            return pois;
        }


        public List<Poi> GetPOIsByRoutePointId(int routePointId)
        {
            List<Poi> pois = new List<Poi>();

            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                connect.Open();

                string sql = @"
            SELECT ID, Name, Description, CoordinateX, CoordinateY, Type
            FROM POI
            WHERE RoutePointID = @RoutePointID";

                using (SqlCommand cmd = new SqlCommand(sql, connect))
                {
                    cmd.Parameters.AddWithValue("@RoutePointID", routePointId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Poi poi = new Poi
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Description = reader.IsDBNull(2) ? null : reader.GetString(2), // Handle possible NULL values for Description
                                CoordinateX = (float)reader.GetDouble(3), // You can also use GetFloat() if it's a float in DB
                                CoordinateY = (float)reader.GetDouble(4), // You can also use GetFloat() if it's a float in DB
                                Type = reader.IsDBNull(5) ? null : reader.GetString(5) // Handle possible NULL values for Type
                            };

                            pois.Add(poi);
                        }
                    }
                }
            }

            return pois;
        }

        public void CreatePointOfInterest(Poi poi)
        {
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                connect.Open();
                string sql = "INSERT INTO POI (routepointid, name, description, coordinateX, coordinateY, type) VALUES (@RoutePointId, @Name, @Description, @CoordinateX, @CoordinateY, @Type)";
                using (SqlCommand cmd = new SqlCommand(sql, connect))
                {
                    cmd.Parameters.AddWithValue("@RoutePointId", poi.RoutePoint.Id);
                    cmd.Parameters.AddWithValue("@Name", poi.Name);
                    cmd.Parameters.AddWithValue("@Description", poi.Description);
                    cmd.Parameters.AddWithValue("@CoordinateX", poi.CoordinateX);
                    cmd.Parameters.AddWithValue("@CoordinateY", poi.CoordinateY);
                    cmd.Parameters.AddWithValue("@Type", poi.Type);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdatePointOfInterest(Poi poi)
        {
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                connect.Open();
                string sql = "UPDATE POI SET routepointid = @RoutePointId, name = @Name, description = @Description, coordinateX = @CoordinateX, coordinateY = @CoordinateY, type = @Type WHERE id = @Id";
                using (SqlCommand cmd = new SqlCommand(sql, connect))
                {
                    cmd.Parameters.AddWithValue("@Id", poi.Id);
                    cmd.Parameters.AddWithValue("@RoutePointId", poi.RoutePoint.Id);
                    cmd.Parameters.AddWithValue("@Name", poi.Name);
                    cmd.Parameters.AddWithValue("@Description", poi.Description);
                    cmd.Parameters.AddWithValue("@CoordinateX", poi.CoordinateX);
                    cmd.Parameters.AddWithValue("@CoordinateY", poi.CoordinateY);
                    cmd.Parameters.AddWithValue("@Type", poi.Type);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeletePointOfInterest(int poiId)
        {
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                connect.Open();
                string sql = "DELETE FROM POI WHERE id = @Id";
                using (SqlCommand cmd = new SqlCommand(sql, connect))
                {
                    cmd.Parameters.AddWithValue("@Id", poiId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Area CRUD

        public Area GetAreaById(int areaId)
        {
            Area area = null;
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                connect.Open();
                string sql = "SELECT * FROM Area WHERE ID = @ID ";
                using (SqlCommand cmd = new SqlCommand(sql, connect))
                {
                    cmd.Parameters.AddWithValue("@ID", areaId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            area = new Area(
                                reader.GetInt32(0),
                                reader.GetString(1),
                                reader.GetString(2),
                                reader.GetString(3) 
                                                              
                            );
                        }
                    }
                }
            }
            return area;
        }
        public List<Area> GetAreas()
        {
            List<Area> areas = new List<Area>();
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                connect.Open();
                string sql = "SELECT ID, Name, Description,Location  FROM Area";
                using (SqlCommand cmd = new SqlCommand(sql, connect))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            string description = reader.GetString(2);
                            string location = reader.GetString(3);
                            
                            areas.Add(new Area(id, name, description, location));
                        }
                    }
                }
            }
            return areas;
        }

        public void CreateArea(Area area)
        {
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                connect.Open();
                string sql = "INSERT INTO Area (Name,Description,Location) VALUES (@Name, @Description, @Location)";
                using (SqlCommand cmd = new SqlCommand(sql, connect))
                {
                    cmd.Parameters.AddWithValue("@Name", area.Name);
                    cmd.Parameters.AddWithValue("@Description", area.Description);
                    cmd.Parameters.AddWithValue("@Location", area.Location);
                    
                    
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateArea(Area area)
        {
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                connect.Open();
                string sql = "UPDATE Area SET Name = @Name, Description = @Description, Location = @Location WHERE id = @ID";
                using (SqlCommand cmd = new SqlCommand(sql, connect))
                {
                    cmd.Parameters.AddWithValue("@ID", area.Id);
                    cmd.Parameters.AddWithValue("@Name", area.Name);
                    cmd.Parameters.AddWithValue("@Description", area.Description);
                    cmd.Parameters.AddWithValue("@Location", area.Location);
               
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteArea(int areaId)
        {
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                connect.Open();
                string sql = "DELETE FROM Area WHERE id = @Id";
                using (SqlCommand cmd = new SqlCommand(sql, connect))
                {
                    cmd.Parameters.AddWithValue("@Id", areaId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Observation CRUD
        public void CreateObservation(Observation observation)
        {
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                connect.Open();
                string sql = "INSERT INTO Observation (AreaID,SpeciesID,UserID,Name,Date,Location,CoordinateX,CoordinateY, FotoUrl) VALUES " +
                    "(@AreaID, @SpeciesID, @UserID,@Name,@Date,@Location,@CoordinateX,@CoordinateY,@FotoUrl)";
                using (SqlCommand cmd = new SqlCommand(sql, connect))
                {
                    cmd.Parameters.AddWithValue("@AreaId", observation.Area.Id);
                    cmd.Parameters.AddWithValue("@SpeciesID", observation.Species.Id);
                    cmd.Parameters.AddWithValue("@UserID", observation.User.Id);
                    cmd.Parameters.AddWithValue("@Name", observation.Name);
                    cmd.Parameters.AddWithValue("@Date", DateTime.Now.Date);
                    cmd.Parameters.AddWithValue("@Location", observation.Location);
                    cmd.Parameters.AddWithValue("@CoordinateX", observation.CoordinateX);
                    cmd.Parameters.AddWithValue("@CoordinateY", observation.CoordinateY);
                    cmd.Parameters.AddWithValue("@FotoUrl", observation.FotoUrl);

                    cmd.ExecuteNonQuery();
                }
            }
        }


        //Species CRUD
        public int CreateSpecies(Species species)
        {
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                connect.Open();
                string sql = "INSERT INTO Specie (Name,Category, FotoURL) VALUES " +
                    "(@Name, @Category, @FotoURL); SELECT SCOPE_IDENTITY()";
                using (SqlCommand cmd = new SqlCommand(sql, connect))
                {
                    cmd.Parameters.AddWithValue("@Name", species.Name);
                    cmd.Parameters.AddWithValue("@Category", species.Category);
                    cmd.Parameters.AddWithValue("@FotoURL", species.FotoUrl);


                    int speciesId = Convert.ToInt32(cmd.ExecuteScalar());

                    connect.Close();
                    return speciesId;
                }
            }
        }

        public List<Species> GetSpecies()
        {
            List<Species> species = new List<Species>();
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                connect.Open();
                string sql = "SELECT ID, Name, Category,FotoURL  FROM Specie";
                using (SqlCommand cmd = new SqlCommand(sql, connect))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            string category = reader.GetString(2);
                            string location = reader.GetString(3);

                            species.Add(new Species(id, name, category, location));
                        }
                    }
                }
            }
            return species;
        }

        public Species GetSpeciesById(int speciesId)
        {
            Species species = null;
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                connect.Open();
                string sql = "SELECT * FROM Specie WHERE ID = @ID ";
                using (SqlCommand cmd = new SqlCommand(sql, connect))
                {
                    cmd.Parameters.AddWithValue("@ID", speciesId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            species = new Species(
                                reader.GetInt32(0),
                                reader.GetString(1),
                                reader.GetString(2),
                                reader.GetString(3)

                            );
                        }
                    }
                }
            }
            return species;
        }

        // Game crud

        public List<Game> GetGamesByRouteId(int routeId)
        {
            List<Game> games = new List<Game>();
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                connect.Open();
                string sql = "SELECT * FROM Game WHERE RouteId = @RouteId";
                using (SqlCommand cmd = new SqlCommand(sql, connect))
                {
                    cmd.Parameters.AddWithValue("@RouteId", routeId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);

                            string name = reader.GetString(2);
                            string description = reader.GetString(3);
                            List<Question> questions = GetQuestionsByGameId(id);

                            Game game = new Game(id, name, description);
                            game.Questions = questions;
                            games.Add(game);
                        }
                    }
                }
            }
            return games;
        }

        // Question CRUD

        public Question GetQuestionById(int questionId)
        {
            Question question = null;
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                connect.Open();
                string sql = "SELECT * FROM Question WHERE ID = @ID ";
                using (SqlCommand cmd = new SqlCommand(sql, connect))
                {
                    cmd.Parameters.AddWithValue("@ID", questionId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            question = new Question(
                                reader.GetInt32(0),
                                reader.GetString(2),
                                reader.GetString(3),
                                reader.GetInt32(4)

                            );
                        }
                    }
                }
            }
            return question;
        }


        public List<Question> GetQuestionsByGameId(int gameId)
        {
            List<Question> questions = new List<Question>();
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                connect.Open();
                string sql = "SELECT * FROM Question WHERE GameId = @GameId";
                using (SqlCommand cmd = new SqlCommand(sql, connect))
                {
                    cmd.Parameters.AddWithValue("@GameId", gameId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);

                            string questionText = reader.GetString(2);
                            string type = reader.GetString(3);
                            int amountPoints = reader.GetInt32(4);

                            Question question = new Question(id, questionText, type, amountPoints);
                            question.Answers = GetAnswersByQuestionId(id);
                            questions.Add(question);
                        }
                    }
                }
            }
            return questions;
        }

        // Answer CRUD

        public List<Answer> GetAnswersByQuestionId(int questionId)
        {
            List<Answer> answers = new List<Answer>();
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                connect.Open();
                string sql = "SELECT * FROM Answer WHERE QuestionId = @QuestionId";
                using (SqlCommand cmd = new SqlCommand(sql, connect))
                {
                    cmd.Parameters.AddWithValue("@QuestionId", questionId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);

                            Question question = GetQuestionById(reader.GetInt32(1));
                            string answerText = reader.GetString(2);
                            bool isCorrect = reader.GetBoolean(3);
                            answers.Add(new Answer(id, question, answerText, isCorrect));
                        }
                    }
                }
            }
            return answers;
        }

        // UserQuestion 

        public void CreateUserQuestion(User user , Question question, Answer answer)
        {
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                connect.Open();
                string sql = "INSERT INTO USRQuest (UserID,QuestionID,GivenAnswer) VALUES (@UserID, @QuestionID, @GivenAnswer)";
                using (SqlCommand cmd = new SqlCommand(sql, connect))
                {
                    cmd.Parameters.AddWithValue("@UserID", user.Id);
                    cmd.Parameters.AddWithValue("@QuestionID", question.Id);
                    cmd.Parameters.AddWithValue("@GivenAnswer", answer.AnswerText);

                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}

