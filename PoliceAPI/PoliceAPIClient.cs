using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliceAPI
{
  public class PoliceAPIClient
  {
    private readonly RestClient _client;
    private readonly PoliceAPICacheHandler _cache;
    private const string BaseUrl = "https://data.police.uk/api";

    public PoliceAPIClient()
    {
      _client = new RestClient(BaseUrl);
      _cache = new PoliceAPICacheHandler();
    }

    public List<Force> GetForces()
    {
      var request = new RestRequest("forces", Method.Get);
      var response = _client.Execute<List<Force>>(request);
      if (response.IsSuccessful && response.Data != null)
      {
        return response.Data;
      }
      throw new ApplicationException($"Error retrieving police forces: {response.ErrorMessage}");
    }

    public ForceDetail GetForceDetails(string forceId)
    {
      var request = new RestRequest($"forces/{forceId}", Method.Get);
      var response = _client.Execute<ForceDetail>(request);
      if (response.IsSuccessful && response.Data != null)
      {
        return response.Data;
      }
      throw new ApplicationException($"Error retrieving force details: {response.ErrorMessage}");
    }

    public List<SeniorOfficer> GetForceOfficers(string forceid)
    {
      var request = new RestRequest($"/forces/{forceid}/people", Method.Get);

      var response = _client.Execute<List<SeniorOfficer>>(request);
      if (response.IsSuccessful && response.Data != null)
      {
        return response.Data;
      }
      throw new ApplicationException($"Error retrieving crimes: {response.ErrorMessage}");
    }

    public List<Crime> GetLatestCrimes(double latitude, double longitude)
    {
      var request = new RestRequest("/crimes-street/all-crime?", Method.Get);
      request.AddQueryParameter("date", GetAPILastUpdate().Date.ToString("yyyy-MM"));
      request.AddQueryParameter("lat", latitude.ToString());
      request.AddQueryParameter("lng", longitude.ToString());

      var response = _client.Execute<List<Crime>>(request);
      if (response.IsSuccessful && response.Data != null)
      {
        return response.Data;
      }
      throw new ApplicationException($"Error retrieving crimes: {response.ErrorMessage}");
    }

    public List<Crime> GetLatestCrimes(SearchLocation location)
    {
      var request = new RestRequest("/crimes-street/all-crime?", Method.Get);
      request.AddQueryParameter("date", GetAPILastUpdate().Date.ToString("yyyy-MM"));
      request.AddQueryParameter("lat", location.Latitude.ToString());
      request.AddQueryParameter("lng", location.Longitude.ToString());

      var response = _client.Execute<List<Crime>>(request);
      if (response.IsSuccessful && response.Data != null)
      {
        return response.Data;
      }
      throw new ApplicationException($"Error retrieving crimes: {response.ErrorMessage}");
    }

    public List<Crime> GetCrimes(string date, double latitude, double longitude)
    {
      var request = new RestRequest("/crimes-street/all-crime?", Method.Get);
      request.AddQueryParameter("date", date);
      request.AddQueryParameter("lat", latitude.ToString());
      request.AddQueryParameter("lng", longitude.ToString());
      
      var response = _client.Execute<List<Crime>>(request);
      if (response.IsSuccessful && response.Data != null)
      {
        return response.Data;
      }
      throw new ApplicationException($"Error retrieving crimes: {response.ErrorMessage}");
    }

    public List<Crime> GetCrimes(string date, SearchLocation location)
    {
      var request = new RestRequest("/crimes-street/all-crime?", Method.Get);
      request.AddQueryParameter("date", date);
      request.AddQueryParameter("lat", location.Latitude.ToString());
      request.AddQueryParameter("lng", location.Longitude.ToString());

      var response = _client.Execute<List<Crime>>(request);
      if (response.IsSuccessful && response.Data != null)
      {
        return response.Data;
      }
      throw new ApplicationException($"Error retrieving crimes: {response.ErrorMessage}");
    }

    public List<StopAndSearch> GetLatestStopAndSearches(double latitude, double longitude)
    {
      var request = new RestRequest("/stops-street?", Method.Get);
      request.AddQueryParameter("date", GetAPILastUpdate().Date.ToString("yyyy-MM"));
      request.AddQueryParameter("lat", latitude.ToString());
      request.AddQueryParameter("lng", longitude.ToString());

      var response = _client.Execute<List<StopAndSearch>>(request);
      if (response.IsSuccessful && response.Data != null)
      {
        return response.Data;
      }
      throw new ApplicationException($"Error retrieving crimes: {response.ErrorMessage}");
    }

    public List<StopAndSearch> GetStopAndSearches(string date, double latitude, double longitude)
    {
      var request = new RestRequest("/stops-street?", Method.Get);
      request.AddQueryParameter("date", date);
      request.AddQueryParameter("lat", latitude.ToString());
      request.AddQueryParameter("lng", longitude.ToString());

      var response = _client.Execute<List<StopAndSearch>>(request);
      if (response.IsSuccessful && response.Data != null)
      {
        return response.Data;
      }
      throw new ApplicationException($"Error retrieving crimes: {response.ErrorMessage}");
    }

    public List<StopAndSearch> GetLatestStopAndSearches(SearchLocation location)
    {
      var request = new RestRequest("/stops-street?", Method.Get);
      request.AddQueryParameter("date", GetAPILastUpdate().Date.ToString("yyyy-MM"));
      request.AddQueryParameter("lat", location.Latitude.ToString());
      request.AddQueryParameter("lng", location.Longitude.ToString());

      var response = _client.Execute<List<StopAndSearch>>(request);
      if (response.IsSuccessful && response.Data != null)
      {
        return response.Data;
      }
      throw new ApplicationException($"Error retrieving crimes: {response.ErrorMessage}");
    }

    public List<StopAndSearch> GetStopAndSearches(string date, SearchLocation location)
    {
      var request = new RestRequest("/stops-street?", Method.Get);
      request.AddQueryParameter("date", date);
      request.AddQueryParameter("lat", location.Latitude.ToString());
      request.AddQueryParameter("lng", location.Longitude.ToString());

      var response = _client.Execute<List<StopAndSearch>>(request);
      if (response.IsSuccessful && response.Data != null)
      {
        return response.Data;
      }
      throw new ApplicationException($"Error retrieving crimes: {response.ErrorMessage}");
    }

    public LastUpdateDate GetAPILastUpdate()
    {
      var request = new RestRequest("crime-last-updated", Method.Get);
      
      var response = _client.Execute<LastUpdateDate>(request);
      if (response.IsSuccessful && response.Data != null)
      {
        return response.Data;
      }
      throw new ApplicationException($"Error retrieving last updated time: {response.ErrorMessage}");
    }
  }

  public class Force
  {
    public string Id { get; set; }
    public string Name { get; set; }
  }

  public class ForceDetail
  {
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Url { get; set; }
    public List<ContactDetail> EngagementMethods { get; set; }
  }

  public class ContactDetail
  {
    public string Title { get; set; }
    public string Url { get; set; }
    public string Description { get; set; }
  }

  public class Crime
  {
    public string Category { get; set; }
    public string LocationType { get; set; }
    public Location Location { get; set; }
    public string Context { get; set; }
    public OutcomeStatus OutcomeStatus { get; set; }
    public string PersistentId { get; set; }
    public int Id { get; set; }
    public string LocationSubtype { get; set; }
    public string Month { get; set; }
  }

  public class Location
  {
    public string Latitude { get; set; }
    public Street Street { get; set; }
    public string Longitude { get; set; }
  }

  public class Street
  {
    public int Id { get; set; }
    public string Name { get; set; }
  }

  public class OutcomeStatus
  {
    public string Category { get; set; }
    public string Date { get; set; }
  }

  public class StopAndSearch
  {
    public string AgeRange { get; set; }
    public string OfficerDefinedEthnicity { get; set; }
    public bool InvolvedPerson { get; set; }
    public string SelfDefinedEthnicity { get; set; }
    public string Gender { get; set; }
    public string Legislation { get; set; }
    public string OutcomeLinkedToObjectOfSearch { get; set; }
    public DateTime DateTime { get; set; }
    public OutcomeObject OutcomeObject { get; set; }
    public Location Location { get; set; }
    public string ObjectOfSearch { get; set; }
    public bool? Operation { get; set; }
    public string Outcome { get; set; }
    public string Type { get; set; }
    public string OperationName { get; set; }
    public bool RemovalOfMoreThanOuterClothing { get; set; }
  }

  public class OutcomeObject
  {
    public string Id { get; set; }
    public string Name { get; set; }
  }

  public class LastUpdateDate
  {
    public DateTime Date { get; set; }
  }

  public class SearchLocation
  {
    public string Name;
    public double Longitude;
    public double Latitude;
    public string ForceID;
    public List<Crime> Crimes = new List<Crime>();
    public List<StopAndSearch> StopAndSearches = new List<StopAndSearch>();
    public Force ActingForce;
    public ForceDetail ActingForceDetails;
    public List<SeniorOfficer> ActingForceSeniorOfficers = new List<SeniorOfficer>();

    public SearchLocation(string Name, double Long, double Lat, string ForceID)
    {
      this.Name = Name;
      this.Longitude = Long;
      this.Latitude = Lat;
      this.ForceID = ForceID;
    }

    public void Update(PoliceAPIClient Client) 
    {
      Crimes = Client.GetLatestCrimes(this);
      StopAndSearches = Client.GetLatestStopAndSearches(this);
      if(ForceID != null && ForceID != "")
      {
        ActingForce = new Force() { Id = ForceID, Name = "" };
        ActingForceDetails = Client.GetForceDetails(ForceID);
        ActingForce.Name = ActingForceDetails.Name;
        ActingForceSeniorOfficers = Client.GetForceOfficers(ForceID);
      }
    }

    public void Update(PoliceAPIClient Client, string Date)
    {
      Crimes = Client.GetCrimes(Date, this);
      StopAndSearches = Client.GetStopAndSearches(Date, this);
      if (ForceID != null && ForceID != "")
      {
        ActingForce = new Force() { Id = ForceID, Name = "" };
        ActingForceDetails = Client.GetForceDetails(ForceID);
        ActingForce.Name = ActingForceDetails.Name;
        ActingForceSeniorOfficers = Client.GetForceOfficers(ForceID);
      }
    }

  }

  public class SeniorOfficer
  {
    public string Bio { get; set; }
    public ContactDetails ContactDetails { get; set; }
    public string Email { get; set; }
    public string Telephone { get; set; }
    public string Mobile { get; set; }
    public string Fax { get; set; }
    public string Web { get; set; }
    public string Address { get; set; }
    public string Facebook { get; set; }
    public string Twitter { get; set; }
    public string YouTube { get; set; }
    public string Myspace { get; set; }
    public string Bebo { get; set; }
    public string Flickr { get; set; }
    public string GooglePlus { get; set; }
    public string Forum { get; set; }
    public string EMessaging { get; set; }
    public string Blog { get; set; }
    public string Rss { get; set; }
    public string Name { get; set; }
    public string Rank { get; set; }
  }

  public class ContactDetails
  {
    public string Email { get; set; }
    public string Telephone { get; set; }
    public string Mobile { get; set; }
    public string Fax { get; set; }
    public string Web { get; set; }
    public string Address { get; set; }
  }
}
