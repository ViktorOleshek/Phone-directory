using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace Kursova_OOP_3sem
{
  /// <summary>
  /// Логика взаимодействия для AddElementWindow.xaml
  /// </summary>
  public partial class AddElementWindow : Window
  {
    public PhoneDirectory NewElement
    {
      get; private set;
    }
    public AddElementWindow()
    {
      InitializeComponent();
    }

    private bool ValidateData(string surname, string name, string city,
      string street, string houseNumber, string phoneNumberText)
    {
      // Перевірка прізвища
      if (ContainsDigit(surname))
      {
        MessageBox.Show("Прізвище не повинно містити цифр");
        return false;
      }

      // Перевірка імені
      if (ContainsDigit(name))
      {
        MessageBox.Show("Ім'я не повинно містити цифр");
        return false;
      }

      // Перевірка міста
      if (ContainsDigit(city))
      {
        MessageBox.Show("Місто не повинно містити цифр");
        return false;
      }

      // Перевірка вулиці
      if (ContainsDigit(street))
      {
        MessageBox.Show("Вулиця не повинна містити цифр");
        return false;
      }

      // Перевірка номеру будинку
      if (ContainsNonNumericOrSpace(houseNumber))
      {
        MessageBox.Show("Номер будинку не повинен містити розділових знаків, букв або пробілів");
        return false;
      }

      // Перевірка номеру телефону
      if (ContainsNonNumericOrSpace(phoneNumberText))
      {
        MessageBox.Show("Номер телефону не повинен містити розділових знаків, букв або пробілів");
        return false;
      }

      return true;
    }
    private bool ContainsNonNumericOrSpace(string input)
    {
      return input.Any(c => !char.IsDigit(c) && !char.IsWhiteSpace(c));
    }
    private bool ContainsDigit(string input)
    {
      return input.Any(char.IsDigit);
    }

    private void Button_SaveData(object sender, RoutedEventArgs e)
    {
      string surname = TextBox_Surname.Text;
      string name = TextBox_Name.Text;
      string city = TextBox_City.Text;
      string street = TextBox_Street.Text;
      string houseNumber = TextBox_HouseNumber.Text;
      string phoneNumberText = TextBox_PhoneNumber.Text;

      if (string.IsNullOrEmpty(surname) || string.IsNullOrEmpty(name) ||
        string.IsNullOrEmpty(city) || string.IsNullOrEmpty(street) ||
        string.IsNullOrEmpty(houseNumber))
      {
        MessageBox.Show("Всі поля повинні бути заповнені");
        return;
      }
      if (ValidateData(surname, name, city, street, houseNumber, phoneNumberText))
      {
        if (double.TryParse(phoneNumberText, out double phoneNumber))
        {
          NewElement = new PhoneDirectory(surname, name, city, street, houseNumber, phoneNumber);
          DialogResult = true;
        }
        else
        {
          MessageBox.Show("Некоректний номер телефону");
        }
      }
    }
  }
}
