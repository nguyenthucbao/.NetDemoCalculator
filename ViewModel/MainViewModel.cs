
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Demo.NET.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string _inputA;
        private string _inputB;
        private string _result;
        private string _selectedOperation;

        public MainViewModel()
        {
            Operations = new ObservableCollection<string> { "Cong", "Tru", "Nhan", "Chia" };
            SelectedOperation = Operations[0];
            CalculateCommand = new SimpleCommand(Calculate);
        }

        public string InputA
        {
            get => _inputA;
            set
            {
                _inputA = value;
                OnPropertyChanged(nameof(InputA));
            }
        }

        public string InputB
        {
            get => _inputB;
            set
            {
                _inputB = value;
                OnPropertyChanged(nameof(InputB));
            }
        }

        public string Result
        {
            get => _result;
            set
            {
                _result = value;
                OnPropertyChanged(nameof(Result));
            }
        }

        public ObservableCollection<string> Operations { get; }

        public string SelectedOperation
        {
            get => _selectedOperation;
            set
            {
                _selectedOperation = value;
                OnPropertyChanged(nameof(SelectedOperation));
            }
        }

        public ICommand CalculateCommand { get; }

        private void Calculate()
        {
            if (double.TryParse(InputA, out double a) && double.TryParse(InputB, out double b))
            {
                string calculationResult = SelectedOperation switch
                {
                    "Cong" => (a + b).ToString(),
                    "Tru" => (a - b).ToString(),
                    "Nhan" => (a * b).ToString(),
                    "Chia" => b != 0 ? (a / b).ToString() : "Invalid",
                };
                Result = $"Result: {calculationResult}";
            }
            else
            {
                Result = "Invalid input!!!";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class SimpleCommand : ICommand
    {
        private readonly Action _execute;

        public SimpleCommand(Action execute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        }

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter) => _execute();

        public event EventHandler CanExecuteChanged;
    }
}
