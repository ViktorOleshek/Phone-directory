using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace Kursova_OOP_3sem
{ 
  public class PhoneDirectory
  {
    public static int ObjCounter;
    public int Id
    {
      get; private set;
    }
    public string Surname
    {
      get; private set;
    }
    public string Name
    {
      get; private set;
    }
    public CAddress Address
    {
      get; private set;
    }
    public double PhoneNumber
    {
      get; private set;
    }
    public PhoneDirectory()
    {
      Id = ++ObjCounter;
    }
    /// <summary></summary>
    public PhoneDirectory(string surname, string name, string city,
          string street, string houseNumber, double phonenumber)
    {
      Id = ++ObjCounter;
      Surname = surname;
      Name = name;
      Address = new CAddress(city, street, houseNumber);
      PhoneNumber = phonenumber;
    }
    public void ChangeId(int i)
    {
      Id = i;
    }
    public static void SetObjCounter(int i)
    {
      ObjCounter = i;
    }
    public void DecreaseId()
    {
      if (Id > 0)
      {
        Id--;
      }
    }
    public static void DecreaseObjCounter()
    {
      if (ObjCounter > 0) { ObjCounter--; }
    }
  }
}
