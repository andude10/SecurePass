using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Linq;
using System;
using System.Security.Cryptography;
using System.Text;

namespace SecurePass.Views
{
    /// <summary>
    /// Логика взаимодействия для GeneratePasswordWindow.xaml
    /// </summary>
    public partial class GeneratePasswordWindow : Window, INotifyPropertyChanged
    {
        public GeneratePasswordWindow()
        {
            InitializeComponent();
            DataContext = this;
        }
        private bool _addUpperCase;
        public bool AddUpperCase
        {
            get { return _addUpperCase; }
            set { RaisePropertyChanged(ref _addUpperCase, value); }
        }
        private bool _addNumbers;
        public bool AddNumbers
        {
            get { return _addNumbers; }
            set { RaisePropertyChanged(ref _addNumbers, value); }
        }
        private bool _addSymbols;
        public bool AddSymbols
        {
            get { return _addSymbols; }
            set { RaisePropertyChanged(ref _addSymbols, value); }
        }
        private string _maxlength;
        public string Maxlength
        {
            get { return _maxlength; }
            set { RaisePropertyChanged(ref _maxlength, value); }
        }
        private string _password;
        public string Password
        {
            get { return _password; }
            set { RaisePropertyChanged(ref _password, value); }
        }
        private string generatePassword(int length)
        {
            StringBuilder validChars = new StringBuilder();
            if (AddUpperCase)
            {
                validChars.Append("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ");
            }
            if (AddNumbers)
            {
                validChars.Append("abcdefghijklmnopqrstuvwxyz1234567890");
            }
            if (AddSymbols)
            {
                validChars.Append("abcdefghijklmnopqrstuvwxyz?!@#$%^&*");
            }

            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(validChars[rnd.Next(validChars.Length)]);
            }
            return res.ToString();
        }

        #region Generate
        // Generate password once button is clicked
        private void btnGeneratePassword_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int length = Convert.ToInt32(Maxlength);
                if (length < 20)
                {
                    // Generate password
                    string pass = generatePassword(length);

                    // Show password in application
                    Password = pass;
                }
                else throw new Exception();
            }
            catch (Exception)
            {
                MessageBox.Show("Please select a correct password length.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion
        private void CloseWindow(object sender, System.Windows.RoutedEventArgs e) => this.Close();

        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged<T>(ref T property, T newValue, [CallerMemberName] string propertyName = "")
        {
            property = newValue;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
