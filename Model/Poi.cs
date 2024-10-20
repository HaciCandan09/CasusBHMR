﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasusExotischNederland.Model
{
    public class Poi
    {
        public int Id { get; set; }
        public RoutePoint RoutePoint { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float CoordinateX { get; set; }
        public float CoordinateY { get; set; }
        public string Type { get; set; }

        public Poi(int id, string name, string description, float coordinateX, float coordinateY, string type)
        {
            Id = id;
            Name = name;
            Description = description;
            CoordinateX = coordinateX;
            CoordinateY = coordinateY;
            Type = type;
        }
    }
}

