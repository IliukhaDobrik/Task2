using System;
using System.Collections.Generic;
using System.Windows;
using DataLayer;
using DataLayer.Entities;
using Microsoft.IdentityModel.Tokens;

namespace Task2;

public partial class ViewFilesList : Window
{
    private readonly MyDbContext _context;
    public ViewFilesList(MyDbContext context, List<File> files)
    {
        _context = context;
        InitializeComponent();
        filesList.ItemsSource = files;
    }

    private void FilesList_OnSelected(object sender, RoutedEventArgs e)
    {
        var file = filesList.SelectedItems[0] as File;
        var fileName = file.FileName;
        
        var viewExcelTableWindow = new ViewExcelTable(_context, fileName);
        viewExcelTableWindow.Show();
    }
}