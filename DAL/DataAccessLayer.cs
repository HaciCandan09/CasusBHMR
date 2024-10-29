﻿using CasusExotischNederland.Model;
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
        private string connectionString = "Data Source=DESKTOP-VTDTUPO; Initial Catalog=TennisverenigingDB; Integrated Security=True;";

        public DataAccessLayer()
        {
            
        }

        // Route CRUD
        public List<Route> GetRoutes()
        {
            List<Route> routes = new List<Route>();
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                connect.Open();
                string sql = "SELECT id, areaid, name, description FROM Route";
                using (SqlCommand cmd = new SqlCommand(sql, connect))
                {
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

        public Area GetAreaById(int areaId)
        {
            Area area = null;
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                connect.Open();
                string sql = "SELECT ID, Name, Description, CoordinateX, CoordinateY FROM Area";
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
                                reader.GetFloat(3),
                                 reader.GetFloat(4)
                            );
                        }
                    }
                }
            }
            return area;
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
        public List<Area> GetAreas()
        {
            List<Area> areas = new List<Area>();
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                connect.Open();
                string sql = "SELECT ID, Name, Description, CoordinateX, CoordinateY  FROM Area";
                using (SqlCommand cmd = new SqlCommand(sql, connect))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            string description = reader.GetString(2);
                            float coordinateX = reader.GetInt32(3);
                            float coordinateY = reader.GetInt32(4);
                            areas.Add(new Area(id, name, description, coordinateX, coordinateY));
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
                string sql = "INSERT INTO Area (Name,Description,coordinateX,coordinateY) VALUES (@Name, @Description, @coordinateX,@coordinateY)";
                using (SqlCommand cmd = new SqlCommand(sql, connect))
                {
                    cmd.Parameters.AddWithValue("@Name", area.Name);
                    cmd.Parameters.AddWithValue("@Description", area.Description);
                    cmd.Parameters.AddWithValue("@CoordinateX", area.CoordinateX);
                    cmd.Parameters.AddWithValue("@CoordinateY", area.CoordinateY);
                    
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateArea(Area area)
        {
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                connect.Open();
                string sql = "UPDATE Area SET Name = @Name, Description = @Description, CoordinateX = @CoordinateX, CoordinateY = @CoordinateY WHERE id = @ID";
                using (SqlCommand cmd = new SqlCommand(sql, connect))
                {
                    cmd.Parameters.AddWithValue("@ID", area.Id);
                    cmd.Parameters.AddWithValue("@Name", area.Name);
                    cmd.Parameters.AddWithValue("@Description", area.Description);
                    cmd.Parameters.AddWithValue("@CoordinateX", area.CoordinateX);
                    cmd.Parameters.AddWithValue("@CoordinateÝ", area.CoordinateY);
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


        
    }
}

