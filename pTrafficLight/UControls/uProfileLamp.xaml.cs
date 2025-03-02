using System;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace pSvetofor.UControls
{
    /// <summary>
    /// Логика взаимодействия для uProfileLamp.xaml
    /// </summary>
    public partial class uProfileLamp : UserControl, INotifyPropertyChanged
    {
        public string S7Operand
        {
            get
            {
                return s7Operand;
            }
            set
            {
                s7Operand = value;
                ParseAddress(s7Operand);
                OnPropertyChanged("S7Operand");
            }
        }
        string s7Operand;

        int Byte_Num;
        int Bit_Num;

        public Brush LampActiveColor
        {
            get;
            set;
        }

        public Brush LampColor
        {
            get
            {
                return lampColor;
            }
            set
            {
                lampColor = value;
                OnPropertyChanged("LampColor");
            }
        }
        Brush lampColor = Brushes.Black;

        public uProfileLamp()
        {
            InitializeComponent();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            timer.Start();
            timer.Tick += (e, s) =>
            {
                UpdateVisu();
            };
        }

        void ParseAddress(string s7operand)
        {
            string adr = s7operand.Replace("M", "").Replace("I", "").Replace("Q", "").Replace(" ", "");
            var adr_arr = adr.Split('.');
            Byte_Num = int.Parse(adr_arr[0]);
            Bit_Num = int.Parse(adr_arr[1]);
        }

        public void UpdateVisu()
        {
            var output_state = Models.PlcSim.ReadOutputBit(Byte_Num, Bit_Num);
            LampColor = output_state == true ? LampActiveColor : Brushes.Black;
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
