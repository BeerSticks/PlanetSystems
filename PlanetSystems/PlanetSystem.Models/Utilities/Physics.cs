using PlanetSystem.Models.Utilities.Contracts;
using System;
using System.Collections.Generic;

namespace PlanetSystem.Models.Utilities
{
    public static class Physics
    {
        public const double GravitationalConstant = 6.67408; // * 10^-11
        public const double GravitationalConstantDecimalFix = 0.00000000001;
        public static double GetDistanceBetweenPoints(Point point1, Point point2)
        {
            double result = Math.Sqrt(
                (point1.X - point2.X) * (point1.X - point2.X) +
                (point1.Y - point2.Y) * (point1.Y - point2.Y) +
                (point1.Z - point2.Z) * (point1.Z - point2.Z));
            return result;
        }

        public static double GetDistanceBetweenSphericalSurfaces(Sphere sphere1, Sphere sphere2)
        {
            double distanceBetweenCenters = GetDistanceBetweenPoints(sphere1.Center, sphere2.Center);
            double distanceBetweenSurfaces = distanceBetweenCenters - (sphere1.Radius + sphere2.Radius);
            return distanceBetweenSurfaces;
        }

        public static double GetGravitationalForceMagnitude(AstronomicalBody body1, AstronomicalBody body2)
        {
            double distance = GetDistanceBetweenPoints(body1.Center, body2.Center);
            double resultNotFixed = GravitationalConstant *
                                    (body1.Mass * body2.Mass) /
                                    (distance * distance);
            double result = resultNotFixed * GravitationalConstantDecimalFix;
            return result;
        }

        public static Vector GetGravitationalForceVector(AstronomicalBody body1, AstronomicalBody body2)
        {
            double forceMagnitude = GetGravitationalForceMagnitude(body1, body2);
            Vector prototypeVector = new Vector(new Point(
                body2.Center.X - body1.Center.X,
                body2.Center.Y - body1.Center.Y,
                body2.Center.Z - body1.Center.Z));
            prototypeVector.Length = forceMagnitude;
            return prototypeVector;
        }

        public static Vector GetAccelerationVector(AstronomicalBody body, Vector forceVector)
        {
            Vector resultingVector = forceVector / body.Mass;
            return resultingVector;
        }

        public static Vector GetFinalVelocityVectorFromAcceleration(
            AstronomicalBody body,
            Vector accelerationVector,
            double secondsUnderAcceleration)
        {
            Vector startingVelocity = body.Velocity;
            Vector deltaVelocity = accelerationVector * secondsUnderAcceleration;
            Vector finalVelocity = startingVelocity + deltaVelocity;
            return finalVelocity;
        }

        public static Vector GetFinalVelocityVectorFromForce(
            AstronomicalBody body,
            Vector forceVector,
            double secondsUnderForce)
        {
            Vector accelerationVector = GetAccelerationVector(body, forceVector);
            Vector velocityVector = GetFinalVelocityVectorFromAcceleration(
                body, accelerationVector, secondsUnderForce);
            return velocityVector;
        }

        public static AstronomicalBody AddVelocityToBody(ref AstronomicalBody body, Vector velocity)
        {
            body.Velocity += velocity;
            return new AstronomicalBody(body);
        }

        public static AstronomicalBody AdvanceMovementOfBody(ref AstronomicalBody body, double timeInSeconds)
        {
            double resultingX = body.Center.X + (body.Velocity.X * timeInSeconds);
            double resultingY = body.Center.Y + (body.Velocity.Y * timeInSeconds);
            double resultingZ = body.Center.Z + (body.Velocity.Z * timeInSeconds);
            Point newCenter = new Point(resultingX, resultingY, resultingZ);
            body.Center = newCenter;
            return new AstronomicalBody(body);
        }

        public static AstronomicalBody ApplyForceToBody(ref AstronomicalBody body, Vector force, double secondsUnderForce)
        {
            Vector startingVelocity = new Vector(body.Velocity);
            Vector finalVelocityFromForce = GetFinalVelocityVectorFromForce(body, force, secondsUnderForce);
            Vector averageVelocity = Vector.GetAverage(new List<Vector> { startingVelocity, finalVelocityFromForce });
            body.Velocity = averageVelocity;
            AdvanceMovementOfBody(ref body, secondsUnderForce);
            body.Velocity = finalVelocityFromForce;
            return body;
        }

        public static Vector GetGravitationalForceOnBody(IList<AstronomicalBody> bodies, int index)
        {
            Vector gravityOnBody = new Vector();
            for (int i = 0; i < bodies.Count; i++)
            {
                if (i != index)
                {
                    gravityOnBody += GetGravitationalForceVector(bodies[index], bodies[i]);
                }
            }

            return gravityOnBody;
        }

        public static void SimulateGravitationalInteraction(ref List<AstronomicalBody> bodies, double secondsOfSimulation)
        {
            for (int i = 0; i < bodies.Count; i++)
            {
                Vector gravityOnBody = GetGravitationalForceOnBody(bodies, i);
                bodies[i].ApplyForce(gravityOnBody, secondsOfSimulation);
            }
        }
        
        public static double GetRelativeTangentialSpeedForOrbit(AstronomicalBody satellite, AstronomicalBody primary)
        {
            double distance = GetDistanceBetweenPoints(satellite.Center, primary.Center);
            double requiredTangentialSpeedFixed = Math.Sqrt((GravitationalConstant * primary.Mass / distance) * GravitationalConstantDecimalFix);
            return requiredTangentialSpeedFixed;
        }

        public static double GetRadiusOfOrbit(AstronomicalBody satellite, AstronomicalBody primary, double relativeTangentialSpeed)
        {
            double requiredRadiusNotFixed = GravitationalConstant * primary.Mass / (relativeTangentialSpeed * relativeTangentialSpeed);
            double requiredRadiusFixed = requiredRadiusNotFixed * GravitationalConstantDecimalFix;
            return requiredRadiusFixed;
        }

        public static void EnterOrbitByGivenRadius<T>(ref T satellite, AstronomicalBody primary, double radius
            /*Vector orbitAxis, double startingPointOffset*/) where T : AstronomicalBody, new()
        {
            // startingPointOffset == 0 => point with the highest x
            // orbit axis - right hand rule
            // TODO: Implement orbital axis and startingPointOffset
            
            satellite.Center = new Point(
                primary.Center.X + radius,
                primary.Center.Y,
                primary.Center.Z);
            double relativeTangentialSpeed = GetRelativeTangentialSpeedForOrbit(satellite, primary);
            Vector satelliteRelativeVelocity = new Vector(relativeTangentialSpeed, Math.PI / 2, Math.PI / 2);
            Vector satelliteAbsoluteVelocity = satelliteRelativeVelocity + primary.Velocity;
            satellite.Velocity = satelliteAbsoluteVelocity;
        }

        public static void EnterOrbitByGivenSpeed<T>(ref T satellite, AstronomicalBody primary, double relativeTangentialSpeed
            /*Vector orbitAxis, double StartingPointOffset*/) where T : AstronomicalBody, new()
        {
            // startingPointOffset == 0 => point with the highest x
            // orbit axis - right hand rule
            // TODO: Implement orbital axis and startingPointOffset
            double radius = GetRadiusOfOrbit(satellite, primary, relativeTangentialSpeed);
            satellite.Center = new Point(
                primary.Center.X + radius,
                primary.Center.Y,
                primary.Center.Z);
            Vector satelliteRelativeVelocity = new Vector(relativeTangentialSpeed, Math.PI / 2, Math.PI / 2);
            Vector satelliteAbsoluteVelocity = satelliteRelativeVelocity + primary.Velocity;
            satellite.Velocity = satelliteAbsoluteVelocity;
        }
    }
}
