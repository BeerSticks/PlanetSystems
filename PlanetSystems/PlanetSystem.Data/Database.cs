using PlanetSystem.Models.Bodies;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace PlanetSystem.Data
{
    public static class Database
    {
        public static List<string> GetAllPlanetarySystemNames()
        {
            List<string> planetarySystemNames = new List<string>();
            using (var context = new SqlServerContext())
            {
                var query = from ps
                            in context.PlanetarySystems
                            select ps.Name;

                foreach (var name in query)
                {
                    planetarySystemNames.Add(name);
                }
            }
            return planetarySystemNames;
        }

        public static void Clear()
        {
            using (var context = new SqlServerContext())
            {
                context.Asteroids.RemoveRange(context.Asteroids);
                context.ArtificialObjects.RemoveRange(context.ArtificialObjects);
                context.Moons.RemoveRange(context.Moons);
                context.Planets.RemoveRange(context.Planets);
                context.Stars.RemoveRange(context.Stars);
                context.PlanetarySystems.RemoveRange(context.PlanetarySystems);
                context.SaveChanges();
            }
        }

        public static PlanetarySystem LoadPlanetarySystem(string name)
        {
            PlanetarySystem planetarySystem;
            using (var context = new SqlServerContext())
            {
                planetarySystem = context.PlanetarySystems
                    .Where(ps => ps.Name == name)
                    .Include(ps => ps.Star)
                    .Include(ps => ps.Planets.Select(pl => pl.Moons))
                    .Include(ps => ps.Asteroids)
                    .Include(ps => ps.ArtificialObjects)
                    .FirstOrDefault();
            }
            return planetarySystem;
        }

        public static bool SavePlanetarySystem(PlanetarySystem planetarySystem)
        {
            try
            {
                DeletePlanetarySystem(planetarySystem.Name);
                using (var context = new SqlServerContext())
                {
                    context.PlanetarySystems.Add(planetarySystem);
                    context.Stars.Add(planetarySystem.Star);
                    context.Planets.AddRange(planetarySystem.Planets);
                    context.Moons.AddRange(planetarySystem.Moons);
                    context.Asteroids.AddRange(planetarySystem.Asteroids);
                    context.ArtificialObjects.AddRange(planetarySystem.ArtificialObjects);
                    context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool DeletePlanetarySystem(string name)
        {
            try
            {
                using (var context = new SqlServerContext())
                {
                    var planetarySystem = LoadPlanetarySystem(name);
                    // bat shit crazy loading. the line bellow is just to trigger some loading to prevent errors
                    context.PlanetarySystems.Attach(planetarySystem);
                    context.Asteroids.RemoveRange(planetarySystem.Asteroids);
                    context.ArtificialObjects.RemoveRange(planetarySystem.ArtificialObjects);
                    context.Moons.RemoveRange(planetarySystem.Moons);
                    context.Planets.RemoveRange(planetarySystem.Planets);
                    context.Stars.Remove(planetarySystem.Star);
                    context.PlanetarySystems.Remove( planetarySystem );
                    context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
    }
}