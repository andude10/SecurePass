using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;

namespace SecurePass.Views
{
    /// <summary>
    ///     Interaction logic for GeneratePasswordWindow.xaml
    /// </summary>
    public partial class GeneratePasswordWindow : Window, INotifyPropertyChanged
    {
        private bool _addNumbers;
        private bool _addSymbols;
        private bool _addUpperCase;
        private string _maxlength;
        private string _password;

        public GeneratePasswordWindow()
        {
            InitializeComponent();
            DataContext = this;
            Owner = Application.Current.MainWindow;

            AddUpperCase = true;
            AddNumbers = true;
            Maxlength = "12";
        }

        public bool AddUpperCase
        {
            get => _addUpperCase;
            set => RaisePropertyChanged(ref _addUpperCase, value);
        }

        public bool AddNumbers
        {
            get => _addNumbers;
            set => RaisePropertyChanged(ref _addNumbers, value);
        }

        public bool AddSymbols
        {
            get => _addSymbols;
            set => RaisePropertyChanged(ref _addSymbols, value);
        }

        public string Maxlength
        {
            get => _maxlength;
            set => RaisePropertyChanged(ref _maxlength, value);
        }

        public string Password
        {
            get => _password;
            set => RaisePropertyChanged(ref _password, value);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private string generatePassword(int length)
        {
            var validChars = new StringBuilder();
            if (AddUpperCase) validChars.Append("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ");
            if (AddNumbers) validChars.Append("abcdefghijklmnopqrstuvwxyz1234567890");
            if (AddSymbols) validChars.Append("abcdefghijklmnopqrstuvwxyz?!@#$%^&*");

            var res = new StringBuilder();
            var rnd = new Random();
            while (0 < length--) res.Append(validChars[rnd.Next(validChars.Length)]);
            return res.ToString();
        }

        #region Generate

        // Generate password once button is clicked
        private void btnGeneratePassword_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var length = Convert.ToInt32(Maxlength);
                if (length < 20)
                {
                    // Generate password
                    var pass = generatePassword(length);

                    // Show password in application
                    Password = pass;
                    DialogResult = true;
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Please select a correct password length.", "Error", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        #endregion

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public void RaisePropertyChanged<T>(ref T property, T newValue, [CallerMemberName] string propertyName = "")
        {
            property = newValue;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}