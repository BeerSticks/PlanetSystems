using PlanetSystem.Models.Bodies;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public static PlanetarySystem LoadPlanetarySystem(string name)
        {
            PlanetarySystem planetarySystem;
            using (var context = new SqlServerContext())
            {
                planetarySystem = context.PlanetarySystems
                    .Where(ps => ps.Name == name)
                    .Include(ps => ps.Planets.Select(pl => pl.Moons))
                    .Include(ps => ps.Asteroids)
                    .Include(ps => ps.ArtificialObjects)
                    .FirstOrDefault();

                //planetarySystem = context.PlanetarySystems
                //    .Where(ps => ps.Name == name)
                //    .FirstOrDefault();

                //var stars = from st
                //            in context.Stars
                //            select st;
                //foreach (var star in stars)
                //{

                //}

                //var planets = from p in context.Planets
                //              select p;
                //foreach (var p in planets)
                //{

                //}

                //Console.WriteLine(planetarySystem.Moons.FirstOrDefault().Planet.Name);
                //var psQuery = from ps
                //              in context.PlanetarySystems
                //              where ps.Name == name
                //              select ps;
                //planetarySystem = psQuery.First<PlanetarySystem>();

                //var starQuery = from st
                //                in context.Stars
                //                where st.StarId == planetarySystem.PlanetarySystemId
                //                select st;
                //var star = starQuery.FirstOrDefault<Star>();
                //planetarySystem.SetStar(star);

                //var planetsQuery = from pl
                //                   in context.Planets
                //                   where pl.PlanetarySystemId == planetarySystem.PlanetarySystemId
                //                   select pl;
                
                //foreach (var planet in planetsQuery)
                //{
                //    //Console.WriteLine(planet.Name); //-----------------
                //    //testlist.Add(planet);
                //    //Console.WriteLine(planetarySystem.Planets.Count);
                //    //planetarySystem.Planets.Add(planet);
                //    //Console.WriteLine(planetarySystem.Planets.Count);
                //}

                //var moonsQuery = from m
                //                 in context.Moons
                //                 where m.PlanetarySystemId == planetarySystem.PlanetarySystemId
                //                 select m;

                //foreach (var moon in moonsQuery)
                //{

                //}
                ////planetarySystem.Planets.ToList().ForEach(planet =>
                ////{
                ////    var localMoons = from lm
                ////                     in moonsQuery
                ////                     where lm.PlanetId == planet.PlanetId
                ////                     select lm;
                ////    planet.AttackMoons(localMoons.ToList());
                    
                ////});


                //var asteroidsQuery = from a
                //                     in context.Asteroids
                //                     where a.PlanetarySystemId == planetarySystem.PlanetarySystemId
                //                     select a;
                //foreach (var asteroid in asteroidsQuery)
                //{
                //    planetarySystem.AddAsteroid(asteroid);
                //}

                //var artificialObjectsQuery = from o
                //                             in context.ArtificialObjects
                //                             where o.PlanetarySystemId == planetarySystem.PlanetarySystemId
                //                             select o;
                //foreach (var aObj in artificialObjectsQuery)
                //{
                //    planetarySystem.AddArtificialObject(aObj);
                //}
            }
            return planetarySystem;
        }
    }
}
