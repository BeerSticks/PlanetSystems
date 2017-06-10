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
            Database.Clear();

            var solarSystem = GetSolarSystem();
            Database.SavePlanetarySystem(solarSystem);
            //var otherSystem = GetOtherSystem();
            //Database.SavePlanetarySystem(otherSystem);
            //Database.DeletePlanetarySystem("Solar system");
            //Database.DeletePlanetarySystem("Other system");

            //var solarSystem = Database.LoadPlanetarySystem("Solar system");
            //var otherSystem = Database.LoadPlanetarySystem("Other system");

            Console.WriteLine("Done");
            Console.ReadLine();
        }

        static PlanetarySystem GetSolarSystem()
        {
            PlanetarySystem solarSystem = new PlanetarySystem("Solar system");
            Models.Bodies.Star sun = new Models.Bodies.Star(
                center: new Point(0, 0, 0),
                mass: 2 * Math.Pow(10, 30),
                radius: 5,
                velocity: new Vector(0, 0, 0),
                name: "Sun");
            solarSystem.SetStar(sun);

            Planet earth = new Planet(
                center: new Point(0, 0, 0),
                mass: 5.972 * Math.Pow(10, 24),
                radius: 5,
                velocity: new Vector(new Point(0, 0, 0)),
                name: "Earth");
            solarSystem.AddPlanetByOrbitalRadius(earth, 150 * Math.Pow(10, 9));

            Moon moon = new Moon(
                center: new Point(0, 0, 0),
                mass: 7.342 * Math.Pow(10, 22),
                radius: 5,
                velocity: new Vector(new Point(0, 0, 0)),
                name: "Moon");
            solarSystem.AddMoonByOrbitalSpeed(moon, earth, 1022);

            Asteroid someAsteroid = new Asteroid(
                center: new Point(380 * Math.Pow(10, 13), 4560, 7867860),
                mass: 7987,
                radius: 5,
                velocity: new Vector(new Point(-456, 645, 6)),
                name: "someAsteroid");
            solarSystem.AddAsteroid(someAsteroid);

            ArtificialObject something = new ArtificialObject(
                center: new Point( -456786, 1568790, 12370),
                mass: 2000,
                radius: 5,
                velocity: new Vector(new Point(620, -82, 894)),
                name: "something");
            solarSystem.AddArtificialObject(something);

            return solarSystem;
        }

        static PlanetarySystem GetOtherSystem()
        {
            PlanetarySystem otherSystem = new PlanetarySystem("Other system");
            Models.Bodies.Star sun = new Models.Bodies.Star(
                center: new Point(0, 0, 0),
                mass: 2 * Math.Pow(10, 34),
                radius: 5,
                velocity: new Vector(0, 0, 0),
                name: "not the sun");
            otherSystem.SetStar(sun);

            Planet aiur = new Planet(
                center: new Point(0, 0, 0),
                mass: 6.772 * Math.Pow(10, 16),
                radius: 5,
                velocity: new Vector(new Point(0, 0, 0)),
                name: "Aiur");
            otherSystem.AddPlanetByOrbitalRadius(aiur, 75 * Math.Pow(10, 9));

            Moon someAuirMoon = new Moon(
                center: new Point(0, 0, 0),
                mass: 8.123 * Math.Pow(10, 16),
                radius: 5,
                velocity: new Vector(new Point(0, 0, 0)),
                name: "Some Aiur moon");
            otherSystem.AddMoonByOrbitalSpeed(someAuirMoon, aiur, 220);

            Planet notAiur = new Planet(
                center: new Point(0, 0, 0),
                mass: 4.772 * Math.Pow(10, 20),
                radius: 5,
                velocity: new Vector(new Point(0, 0, 0)),
                name: "NotAiur");
            otherSystem.AddPlanetByOrbitalRadius(notAiur, 15 * Math.Pow(10, 9));

            //Moon floatingBrick = new Moon(
            //    center: new Point(0, 0, 0),
            //    mass: 2121.123 * Math.Pow(10, 16),
            //    radius: 5,
            //    velocity: new Vector(new Point(0, 0, 0)),
            //    name: "FoatingBrick");
            //otherSystem.AddMoonByOrbitalSpeed(floatingBrick, notAiur, 1520);

            Moon aDamnSubmarine = new Moon(
                center: new Point(0, 0, 0),
                mass: 1.123 * Math.Pow(10, 13),
                radius: 5,
                velocity: new Vector(new Point(0, 0, 0)),
                name: "a damn submarine");
            otherSystem.AddMoonByOrbitalSpeed(aDamnSubmarine, notAiur, 999);

            Asteroid someOtherAsteroid = new Asteroid(
                center: new Point(-250 * Math.Pow(10, 17), -874560, 565784),
                mass: 7987,
                radius: 5,
                velocity: new Vector(new Point(754, 245, -3)),
                name: "someOtherAsteroid");
            otherSystem.AddAsteroid(someOtherAsteroid);

            ArtificialObject somethingElse = new ArtificialObject(
                center: new Point(1334124, -54656, 9654187654),
                mass: 2000,
                radius: 5,
                velocity: new Vector(new Point(-450, 15, -9987)),
                name: "somethingElse");
            otherSystem.AddArtificialObject(somethingElse);

            return otherSystem;
        }
        static void TESTPopulateDb()
        {
            using (SqlServerContext context = new SqlServerContext())
            {
                PlanetarySystem solarSystem = new PlanetarySystem("Solar system");
                Models.Bodies.Star sun = new Models.Bodies.Star(
                    center: new Point(0, 0, 0),
                    mass: 2 * Math.Pow(10, 30),
                    radius: 5,
                    velocity: new Vector(0, 0, 0),
                    name: "Sun");

                Planet earth = new Planet(
                    center: new Point(0, 0, 0),
                    mass: 5.972 * Math.Pow(10, 24),
                    radius: 5,
                    velocity: new Vector(new Point(0, 0, 0)),
                    name: "Earth");
                Moon moon = new Moon(
                    center: new Point(0, 0, 0),
                    mass: 7.342 * Math.Pow(10, 22),
                    radius: 5,
                    velocity: new Vector(new Point(0, 0, 0)),
                    name: "Moon");


                solarSystem.SetStar(sun);
                solarSystem.AddPlanetByOrbitalRadius(earth, 150 * Math.Pow(10, 9));
                //solarSystem.AddPlanetByOrbitalSpeed(earth, 29780);
                solarSystem.AddMoonByOrbitalRadius(moon, earth, 384000000);
                //solarSystem.AttachMoonToPlanetByOrbitalSpeed(moon, earth, 1022);

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

                var star = starquery.FirstOrDefault();
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

//PlanetarySystem solarSystem = new PlanetarySystem("Solar system");
//Star sun = new Star(
//    center: new Point(0, 0, 0),
//    mass: 2 * Math.Pow(10, 30),
//    radius: 5,
//    velocity: new Vector(0, 0, 0),
//    name: "Sun");

//Planet earth = new Planet(
//    center: new Point(150 * Math.Pow(10, 9), 0, 0),
//    mass: 5.972 * Math.Pow(10, 24),
//    radius: 5,
//    velocity: new Vector(new Point(0, 29780, 0)),
//    name: "Earth");
//Moon moon = new Moon(
//    center: new Point(384000000, 0, 0),
//    mass: 7.342 * Math.Pow(10, 22),
//    radius: 5,
//    velocity: new Vector(new Point(0, 1022, 0)),
//    name: "Moon");
