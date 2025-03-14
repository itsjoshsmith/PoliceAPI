using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliceAPI
{
  public class PoliceAPICacheHandler
  {
    public DirectoryInfo CacheDirectory { get; }

    public PoliceAPICacheHandler()
    {
      try
      {
        CacheDirectory = new DirectoryInfo(Path.GetTempPath() + "PoliceAPICache");
        CacheDirectory.Create();
      }
      catch (Exception) { CacheDirectory = null; }
    }

    public string GetDataForDate(DateTime date)
    {
      return "";
    }

    public string SaveDataForDate(DateTime date)
    {
      return "";
    }
  }
}
