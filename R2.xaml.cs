using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Kursova_OOP_3sem
{
  public partial class R2 : Window, WindowWithList
  {
    public R2()
    {
      InitializeComponent();
      SizeChanged += R2_SizeChanged;
    }
    public void ShowList(List<PhoneDirectory> data)
    {
      List.ItemsSource = data;
      ShowDialog();
      Close();
    }
    private void R2_SizeChanged(object sender, SizeChangedEventArgs e)
    {
      double totalWidth = List.ActualWidth;

      foreach (var column in ((GridView) List.View).Columns)
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
    private void Exit(object sender, RoutedEventArgs e)
    {
      Close();
    }
  }
}
