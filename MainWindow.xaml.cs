using Kursova_OOP_3sem;
using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections;
using Microsoft.Win32;
using System.Windows.Markup;

namespace Kursova_OOP_3sem
{
  /// <summary>
  /// Логика взаимодействия для MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public BindingList<PhoneDirectory> Data { get; private set; } = new BindingList<PhoneDirectory>();
    public MainWindow()
    {
      InitializeComponent();
      SizeChanged += MainList_SizeChanged;
    }
    private void MainList_SizeChanged(object sender, SizeChangedEventArgs e)
    {
      double totalWidth = MainList.ActualWidth;

      foreach (var column in ((GridView) MainList.View).Columns)
      {
        if (column.Header.ToString() == "№")
        {
          column.Width = totalWidth * 0.08;
        }
        else if (column.Header.ToString() == "Surname")
        {
          column.Width = totalWidth * 0.20;
        }
        else if (column.Header.ToString() == "Name")
        {
          column.Width = totalWidth * 0.20;
        }
        else if (column.Header.ToString() == "Address")
        {
          column.Width = totalWidth * 0.30;
        }
        else if (column.Header.ToString() == "Phone number")
        {
          column.Width = totalWidth * 0.19;
        }
      }
    }
    private void StartProgram(object sender, RoutedEventArgs e)
    {
      Data.Add(new PhoneDirectory("Олешек", "Вікторія", "Львів", "Широка", "58", 380111111111));
      Data.Add(new PhoneDirectory("Ява", "Віктор", "Львів", "Широка", "17", 380111111111));
      Data.Add(new PhoneDirectory("Парус", "Вікторія", "Чернігів", "Широка", "11", 380123456789));
      Data.Add(new PhoneDirectory("Не пам'ятаю", "Анастасія", "Долина", "Чигиринська", "84", 380777777777));
      Data.Add(new PhoneDirectory("Олешек", "Віктор", "Львів", "Широка", "17", 380333333333));
      Data.Add(new PhoneDirectory("Тропак", "Яніна", "Львів", "Широка", "58", 380222222222));
      Data.Add(new PhoneDirectory("Вергелес", "Каріна", "Одеса", "Степана Бандери", "17", 380888888888));
      Data.Add(new PhoneDirectory("Стефанишин", "Тетяна", "Львів", "Широка", "17", 380777777777));
      Data.Add(new PhoneDirectory("Плис", "Альона", "Київ", "Сяйво", "43", 380555555555));

      MainList.ItemsSource = Data;
    }
    private bool IsEmpty()
    {
      if (Data.Count == 0)
      {
        MessageBox.Show("Список пустий.");
        return true;
      }
      return false;
    }
    private void RenumberID()
    {
      for (int i = 0; i < Data.Count; i++)
      {
        Data [i].ChangeId(i + 1);
      }
    }
    private void ShowMatchingUsers(WindowWithList obj,
      List<PhoneDirectory> matchingUsers, int k, string Error)
    {
      if (k == 1) { MessageBox.Show(Error); }
      else
      {
        obj.ShowList(matchingUsers);
      }
    }
    private void Button1_FindAddressAndPhoneNumberBySurname(object sender, RoutedEventArgs e)
    {
      if (IsEmpty()) { return; }

      string userSurname = TextBox_SurnameForFindAddress.Text;
      if (userSurname == "")
      {
        MessageBox.Show("Неправильно введене прізвище");
        return;
      }


      int k = 1; // індекс елемента в таблиці в новому вікні
      var matchingUsers = Data
          .Where(current => current.Surname.Equals(userSurname, StringComparison.OrdinalIgnoreCase))
          .Select(current => { current.ChangeId(k++); return current; })
          .ToList();

      ShowMatchingUsers(new R1(), matchingUsers, k, "прізвища немає в базі даних.");
    }
    private void Button2_FindSurnameAndAddressByPhonenumber(object sender, RoutedEventArgs e)
    {
      if (IsEmpty()) { return; }

      if (!double.TryParse(TextBox_PhonenumberForFindSurnameAndAddress.Text, out double userPhoneNumber)
        || userPhoneNumber == 0)
      {
        MessageBox.Show("Некоректний формат номеру телефону або неправильно введений номер.");
        return;
      }

      if (userPhoneNumber == 0) { MessageBox.Show("Неправильно введений номер"); return; }

      int k = 1;//індекс елемента в таблиці в новому вікні
      var matchingUsers = Data
          .Where(current => current.PhoneNumber == userPhoneNumber)
          .Select(current => { current.ChangeId(k++); return current; })
          .ToList();

      ShowMatchingUsers(new R2(), matchingUsers, k, "Номеру телефона немає в базі даних.");
    }
    private void Button3_FindSurnameAndNameByAddress(object sender, RoutedEventArgs e)
    {
      if (IsEmpty()) { return; }

      string [] str = TextBox_AddressForFindSurnameAndName.Text
        .Split(new char [] { ',' },
        StringSplitOptions.RemoveEmptyEntries)//видалити пробіли
        .Select(s => s.Trim())
        .ToArray();
      if (str.Length != 3)
      {
        MessageBox.Show("Неправильно введена адреса");
        return;
      }

      CAddress userAddress = new CAddress(str [0], str [1], str [2]);

      int k = 1;
      var matchingUsers = Data
          .Where(current => current.Address.Compare(userAddress))
          .Select(current => { current.ChangeId(k++); return current; })
          .ToList();

      ShowMatchingUsers(new R3(), matchingUsers, k, "Адреси немає в базі даних.");
    }
    private void Button4_FindNameAndSurnameWithSameAddress(object sender, RoutedEventArgs e)
    {
      if (IsEmpty()) { return; }

      int k = 1;
      var matchingUsers = Data
          .GroupBy(user =>
          {
            string str = user.Address.City + user.Address.Street + user.Address.HouseNumber;
            return str.ToLower();
          })
          .Where(group => group.Count() > 1) // Відбираємо групи з більш ніж одним користувачем
          .SelectMany(group => group.Select(user =>
          {
            user.ChangeId(k++);
            return user;
          }))
          .ToList();

      ShowMatchingUsers(new R4(), matchingUsers, k, "Адреси немає в базі даних.");
    }
    private void Button5_FindNameAndAddressByPhoneNumber(object sender, RoutedEventArgs e)
    {
      if (IsEmpty()) return;

      if (!double.TryParse(TextBox_PhonenumberForFindNameAndAddress.Text, out double userPhoneNumber) || userPhoneNumber == 0)
      {
        MessageBox.Show("Некоректний формат номеру телефону або неправильно введений номер.");
        return;
      }

      var usersToFind = Data
          .Where(user => user.PhoneNumber == userPhoneNumber)
          .ToList();

      if (usersToFind == null)
      {
        MessageBox.Show("Немає користувача з введеним номером телефону.");
        return;
      }

      int k = 1;
      var matchingUsers = Data
          .Where(currentUser =>
              usersToFind.Any(u => u.Name == currentUser.Name) &&
              usersToFind.Any(u => u.Address.Street == currentUser.Address.Street))
          .Select(current => { current.ChangeId(k++); return current; })
          .ToList();

      ShowMatchingUsers(new R5(), matchingUsers, k, "Номеру телефона немає в базі даних");
    }
    private void Button6_GroupSameAddress(object sender, RoutedEventArgs e)
    {
      if (IsEmpty()) return;

      for (int i = 0; i < Data.Count - 1; i++)
      {
        for (int j = i + 1; j < Data.Count; j++)
        {
          if (Data [i].Address.Compare(Data [j].Address))
          {
            // Swap elements at indices i and j
            (Data [j], Data [i + 1]) = (Data [i + 1], Data [j]);
          }
        }
      }
      RenumberID();
    }
    private double SumDigitsPhoneNumber(double number)
    {
      return number.ToString().Select(c => int.Parse(c.ToString())).Sum();
    }
    private void Button7_SortShella(object sender, RoutedEventArgs e)
    {
      int step = Convert.ToInt32(Data.LongCount() / 2);
      if (step <= 0)
      {
        MessageBox.Show("List is empty.");
        return;
      }
      while (step > 0)
      {
        for (int i = step; i < Data.LongCount(); i++)
        {
          PhoneDirectory temp = Data [i];
          int j = i;

          while (j >= step &&
            SumDigitsPhoneNumber(Data [j - step].PhoneNumber) > SumDigitsPhoneNumber(temp.PhoneNumber))
          {
            Data [j] = Data [j - step];
            j -= step;
          }
          Data [j] = temp;
        }
        step /= 2;
      }
      RenumberID();
    }
    private void Button_AddElement(object sender, RoutedEventArgs e)
    {
      AddElementWindow addElementWindow = new AddElementWindow();
      bool? isOpen = addElementWindow.ShowDialog();

      if (isOpen == true)
      {
        PhoneDirectory newEntry = addElementWindow.NewElement;
        Data.Add(newEntry);
      }
      else
      {
        MessageBox.Show("Не вдалось відкрити вікно, щоб добавити дані. " +
          "Зверніться до системного адміністратора. ");
      }
    }
    //Do It can delete all mark elements?
    private void Button_RemoveSelectedElement(object sender, RoutedEventArgs e)
    {
      if (MainList.SelectedItem != null)
      {
        PhoneDirectory selectedElement = (PhoneDirectory) MainList.SelectedItem;
        // Зменшення Id для наступних елементів
        for (int i = Data.IndexOf(selectedElement) + 1; i < Data.LongCount(); i++)
        {
          Data [i].DecreaseId();
        }
        PhoneDirectory.DecreaseObjCounter();
        Data.Remove(selectedElement);
        MainList.Items.Refresh();
      }
      else
      {
        MessageBox.Show("Виберіть елемент для видалення.");
      }
    }
    private void DeleteSpaceInString(string [] arrayOfStrings)
    {
      for (int i = 0; i < arrayOfStrings.Length - 3; i++)
      {
        // Видаляємо лише пробіли, якщо це не останній елемент (назва вулиці)
        if (i < arrayOfStrings.Length - 1)
        {
          arrayOfStrings [i] = arrayOfStrings [i].Replace(" ", "");
        }
      }
    }
    private void MenuItem_Open(object sender, RoutedEventArgs e)
    {
      var openFileDialog = new Microsoft.Win32.OpenFileDialog();
      if (openFileDialog.ShowDialog() != true)
      {
        MessageBox.Show("Файл не було вибрано.");
        return;
      }

      string path = openFileDialog.FileName;

      BindingList<PhoneDirectory> tmp = new BindingList<PhoneDirectory>();
      Data.ToList().ForEach(tmp.Add);
      Data.Clear();
      try
      {
        PhoneDirectory.SetObjCounter(0);

        foreach (string line in File.ReadLines(path))
        {
          string [] parts = line.Split(',');
          DeleteSpaceInString(parts);
          if (parts.Length == 6 && double.TryParse(parts [5], out double phoneNumber))
          {
            Data.Add(new PhoneDirectory(parts [0], parts [1], parts [2], parts [3], parts [4], phoneNumber));
          }
          else
          {
            throw new Exception();
          }
        }
      }
      catch (Exception ex)
      {
        tmp.ToList().ForEach(Data.Add);
        PhoneDirectory.SetObjCounter(Data.Count);
        MessageBox.Show($"Помилка при читанні файлу: {ex.Message}\n" +
          $"Приклад заповнення: Іванов, Іван, Київ, Високий Замок, 1, 1234567890\r\n");
      }
    }
    private void MenuItem_Save(object sender, RoutedEventArgs e)
    {
      Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
      if (openFileDialog.ShowDialog() == true)
      {
        string path = openFileDialog.FileName;
        File.WriteAllText(path, string.Empty);
        try
        {
          using (StreamWriter file = new StreamWriter(path))//auto close
          {
            foreach (var element in Data)
            {
              file.WriteLine($"{element.Surname}, {element.Name}, м. {element.Address.City} " +
                $"вул. {element.Address.Street} буд. {element.Address.HouseNumber}, {element.PhoneNumber}");
            }
          }
        }
        catch (Exception ex)
        {
          MessageBox.Show($"Помилка при записі у файл: {ex.Message}");
        }
      }
      else
      {
        MessageBox.Show("Файл не було вибрано.");
      }
    }
    private void MenuItem_Help(object sender, RoutedEventArgs e)
    {
      MessageBox.Show("Email system admin: Viktor.Oleshek.PZ.2022@lpnu.ua");
    }
    private void MenuItem_Exit(object sender, RoutedEventArgs e)
    {
      Environment.Exit(0);
    }
  }
}