
enum Weather
{
    Sunny,
    Rainy,
    Windy
}

class Orbit
{
    public string Name { get; }
    public int Distance { get; }
    public int Craters { get; }
    public int TrafficSpeed { get; }

    public Orbit(string name, int distance, int craters, int trafficSpeed)
    {
        Name = name;
        Distance = distance;
        Craters = craters;
        TrafficSpeed = trafficSpeed;
    }
}

class Vehicle
{
    public string Name { get; }
    public int Speed { get; }
    public int TimeToCrossOneCrater { get; }

    public Vehicle(string name, int speed, int timeToCrossOneCrater)
    {
        Name = name;
        Speed = speed;
        TimeToCrossOneCrater = timeToCrossOneCrater;
    }
}

class TravelPlanner
{
    private Weather Weather { get; }
    private Orbit Orbit1 { get; }
    private Orbit Orbit2 { get; }
    private Vehicle[] Vehicles { get; }

    public TravelPlanner(Weather weather, Orbit orbit1, Orbit orbit2, Vehicle[] vehicles)
    {
        Weather = weather;
        Orbit1 = orbit1;
        Orbit2 = orbit2;
        Vehicles = vehicles;
    }

    private double CalculateTravelTime(Orbit orbit, Vehicle vehicle)
    {
        double craters;
        if (Weather == Weather.Sunny)
        {
            craters = orbit.Craters * 0.9;
        }
        else if (Weather == Weather.Rainy)
        {
            craters = orbit.Craters * 1.2;
        }
        else
        {
            craters = orbit.Craters;
        }

        double timeToCrossCraters = vehicle.TimeToCrossOneCrater * craters;
        double travelTime = (double)orbit.Distance / vehicle.Speed + timeToCrossCraters;
        return travelTime;
    }

    public Tuple<string, string> FindFastestRoute()
    {
        double fastestTime = double.MaxValue;
        Tuple<string, string> fastestOption = null;

        foreach (var vehicle in Vehicles)
        {
            foreach (var orbit in new Orbit[] { Orbit1, Orbit2 })
            {
                double travelTime = CalculateTravelTime(orbit, vehicle);
                if (travelTime < fastestTime && vehicle.Speed <= orbit.TrafficSpeed)
                {
                    fastestTime = travelTime;
                    fastestOption = new Tuple<string, string>(orbit.Name, vehicle.Name);
                }
            }
        }

        return fastestOption;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Orbit orbit1 = new Orbit("Orbit 1", 18, 20, 12);
        Orbit orbit2 = new Orbit("Orbit 2", 20, 10, 10);

        Vehicle bike = new Vehicle("Bike", 10, 2);
        Vehicle tuktuk = new Vehicle("Tuktuk", 12, 1);
        Vehicle car = new Vehicle("Car", 20, 3);

        TravelPlanner planner = new TravelPlanner(Weather.Sunny, orbit1, orbit2, new Vehicle[] { bike, tuktuk, car });

        var fastestRoute = planner.FindFastestRoute();

        if (fastestRoute != null)
        {
            Console.WriteLine($"Recommended vehicle: {fastestRoute.Item2} on {fastestRoute.Item1}");
        }
        else
        {
            Console.WriteLine("No feasible route found.");
        }
    }
}
