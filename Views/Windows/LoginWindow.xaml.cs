﻿using System.Windows;
using System.Windows.Controls;
using Microsoft.Toolkit.Mvvm.Messaging;
using SecurePass.Repositories.UnitOfWork;
using SecurePass.Services;
using SecurePass.ViewModels;

namespace SecurePass.Views.Windows
{
    /// <summary>
    ///     Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow
    {
        public LoginWindow()
        {
            InitializeComponent();
            WeakReferenceMessenger.Default.Register<LoginWindow, LoginWindowMessage>(this, Login);
            ViewModelManager.LoginVm = new LoginVm();
            DataContext = ViewModelManager.LoginVm;
        }

        private MainWindow Window { get; set; }

        private void Login(LoginWindow recipient, LoginWindowMessage message)
        {
            if (message.IsSuccessful)
            {
                ViewModelManager.MainVm = new MainVm(new UnitOfWork());
                Window = new MainWindow();
                Window.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Wrong password entered", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext != null) (DataContext as dynamic).MasterPassword = ((PasswordBox) sender).SecurePassword;
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MinimizedWindow(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void MoveWindow(object sender, RoutedEventArgs e)
        {
            DragMove();
        }
    }
}