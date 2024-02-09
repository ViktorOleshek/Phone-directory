using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Kursova_OOP_3sem
{
  /// <summary>
  /// Логика взаимодействия для R4.xaml
  /// </summary>
  public partial class R4 : Window, WindowWithList
  {
    public R4()
    {
      InitializeComponent();
      SizeChanged += R4_SizeChanged;
    }
    public void ShowList(List<PhoneDirectory> data)
    {
      List.ItemsSource = data;
      ShowDialog();
      Close();
    }
    private void R4_SizeChanged(object sender, SizeChangedEventArgs e)
    {
      double totalWidth = List.ActualWidth;
      foreach (var column in (((GridView) List.View).Columns))
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
    private void Exit(object sender, EventArgs e)
    {
      Close();
    }
  }
}
