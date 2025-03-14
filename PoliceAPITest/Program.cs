using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using PoliceAPI;

namespace PoliceAPITest
{
  internal class Program
  {
    static void Main(string[] args)
    {
      PoliceAPIClient apiClient = new PoliceAPIClient();
      Console.WriteLine($"Crimes API was last updated on the {apiClient.GetAPILastUpdate().Date.ToString("MM/yyyy")}");

      // Returns a list of the available force names
      List<Force> availableForces = apiClient.GetForces();

      // Create 'helper classes' the search location can be used to encapsulate alot of the long type into a handy little class.
      // All that it needs is a reference to a PoliceAPIClient when Update is called which will update all the internal class information......
      SearchLocation location = new SearchLocation("Hackney", -0.06086237552047594, 51.54527031041626, "metropolitan");
      
      if (location != null)
      {
        try
        {
          // This is where all the updating work is done
          location.Update(apiClient);
        }
        catch (Exception ex)
        {
          Console.WriteLine(ex.ToString());
          Console.ReadLine();
          return;
        }
        
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
      }
      
      Console.ReadLine();
    }
  }
}
