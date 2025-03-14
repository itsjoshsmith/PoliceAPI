# PoliceAPI
A wrapper for the UK government police data API, to fetch crime data for a certain location and surrounding areas.

The project depends on [RestSharp](https://github.com/restsharp/RestSharp) to do the leg work of handling the API requests and responses.

# Usage

### Create an instance of the client

```cs
PoliceAPIClient apiClient = new PoliceAPIClient();
```
There are a couple of helper functions attached to the client, such as

**GetAPILastUpdate()**
```cs
Console.WriteLine($"Crimes API was last updated on the {apiClient.GetAPILastUpdate().Date.ToString("MM/yyyy")}");
```
This will return a DateTime object containing the information about the last time the api was updated.

**GetForces()**
```cs
List<Force> availableForces = apiClient.GetForces();
```

This will return a list of available forces, this will be important when creating the SearchLocation class later on.

### Create a location

```cs
SearchLocation location = new SearchLocation("Hackney", -0.06086237552047594, 51.54527031041626, "metropolitan");
```

The search location object will hold majority of the information that the API will supply, such as a list of crimes and stop and searches.
The first parameter is a name / alias and has no relevance to the api calls themselves, the three parameters following are the important ones. The second
parameter is the longitude of the location, the third parameter is the latitude and the forth parameter is the force name that governs the area, you can use the
**GetForces()** call to retrieve a list of the available forces.

### Update the location

```cs
location.Update(apiClient);
```

To actually gather the information for the given location all that needs to be done is to call **Update(apiClient)** on the object. The parameter is the api client created above.

### Check how much crime is going on

```cs
foreach (Crime c in location.Crimes)
{
  Console.WriteLine(c.Category);
  Console.WriteLine(c.Location.Street.Name);
  Console.WriteLine(c.Month);
}

Console.WriteLine($"Stop and searches for {location.Name}");
foreach (StopAndSearch s in location.StopAndSearches)
{
  Console.WriteLine(s.DateTime.ToString());
  Console.WriteLine(s.Legislation);
  Console.WriteLine(s.Outcome);
}
```

Once the location has been updated, an internal list of crimes and stop and searches will be updated and populated.

### Sample Console Output


