using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursova_OOP_3sem
{
  public class CAddress
  {
    public string City
    {
      get; private set;
    }
    public string Street
    {
      get; private set;
    }
    public string HouseNumber
    {
      get; private set;
    }
    public CAddress(string city, string street, string houseNumber)
    {
      City = city;
      Street = street;
      HouseNumber = houseNumber;
    }
    public bool Compare(CAddress obj)
    {
      if (obj == null) { return false; }

      bool one = obj.City.ToLower() == this.City.ToLower();
      bool two = obj.Street.ToLower() == this.Street.ToLower();
      bool three = obj.HouseNumber.ToLower() == this.HouseNumber.ToLower();
      return one && two && three;
    }
  }
}
