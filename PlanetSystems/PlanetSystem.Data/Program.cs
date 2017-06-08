using PlanetSystem.Models.Bodies;
using PlanetSystem.Models.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetSystem.Data
{
    class Program
    {
        static void Main(string[] args)
        {
            //TESTPopulateDb();
            //Database.LoadPlanetarySystem("Solar system");
            Console.WriteLine("Done");
            Console.ReadLine();
        }

        static void TESTPopulateDb()
        {
            using (SqlServerContext context = new SqlServerContext())
            {
                PlanetarySystem solarSystem = new PlanetarySystem("Solar system");
                Star sun = new Star(
                    center: new Point(0, 0, 0),
                    mass: 2 * Math.Pow(10, 30),
                    radius: 5,
                    velocity: new Vector(0, 0, 0),
                    name: "Sun");

                Planet earth = new Planet(
                    center: new Point(150 * Math.Pow(10, 9), 0, 0),
                    mass: 5.972 * Math.Pow(10, 24),
                    radius: 5,
                    velocity: new Vector(new Point(0, 29780, 0)),
                    name: "Earth");
                Moon moon = new Moon(
                    center: new Point(200 * Math.Pow(10, 9), 0, 0),
                    mass: 1.972 * Math.Pow(10, 24),
                    radius: 5,
                    velocity: new Vector(new Point(0, 29780, 0)),
                    name: "Moon");
                

                solarSystem.SetStar(sun);
                solarSystem.AddPlanet(earth);
                solarSystem.AttachMoonToPlanet(moon, earth);

                context.PlanetarySystems.Add(solarSystem);
                context.Stars.Add(sun);
                context.Planets.Add(earth);
                context.Moons.Add(moon);

                Console.WriteLine("presave");
                context.SaveChanges();
            }
        }

        static void TESTReadFromDb()
        {
            using (SqlServerContext context = new SqlServerContext())
            {
                var psquery = from ps in context.PlanetarySystems
                               where ps.Name == "Solar system"
                               select ps;
                
                var planetarySystem = psquery.FirstOrDefault<PlanetarySystem>();
                Console.WriteLine("SYSTEM--------");
                Console.WriteLine($"Name {planetarySystem.Name} ");

                var starquery = from st in context.Stars
                        where st.StarId == planetarySystem.PlanetarySystemId
                        select st;

                var star = starquery.FirstOrDefault<Star>();
                Console.WriteLine("STAR------");
                Console.WriteLine($"name {star.Name}");
                Console.WriteLine($"mass {star.Mass}");

                planetarySystem.SetStar(star);
                Console.WriteLine("STAR FROM PS ----");
                Console.WriteLine($"name {planetarySystem.Star.Name}");
                Console.WriteLine($"mass {planetarySystem.Star.Mass}");

                Console.WriteLine("PSFROMSTAR---");
                Console.WriteLine($"name {star.PlanetarySystem.Name}");

                Console.WriteLine();
                Console.WriteLine(planetarySystem.Name);

                Console.WriteLine();
            }
        }
    }
}
