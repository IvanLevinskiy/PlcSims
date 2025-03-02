using pSvetofor.Utilites;
using System.ComponentModel;
using System.Windows.Input;

namespace pSvetofor.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public bool IsConnect
        {
            get
            {
                return Models.PlcSim.IsAvailable;
            }
        }


        /// <summary>
        /// Конструктор класса
        /// </summary>
        public MainViewModel()
        {
            Models.PlcSim.ChangePlcSimState += (state) =>
            {
                //IsConnect = state;
                OnPropertyChanged("IsConnect");
            };
        }

        /// <summary>
        /// Команда для подключения к симулятору
        /// </summary>
        public ICommand ConnectPlcSim_Command
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    Models.PlcSim.Connect();
                },
                (obj) => (true));
            }
        }


        #region Реализация интерфейса INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
