using S7PROSIMLib;
using System;

namespace pSvetofor.Models
{
    public class PlcSim
    {
        //Экземпляр симулятора
        public static S7ProSim PLCSim = new S7ProSim();

        /// <summary>
        /// Событие, генерируемое при изменении состояния 
        /// PlcSim
        /// </summary>
        public static event Action<bool> ChangePlcSimState;

        public static void Connect()
        {
            PLCSim.Connect();
            PLCSim.SetState("RUN");
            PLCSim.SetScanMode(ScanModeConstants.ContinuousScan);

            //PLCSim.ConnectionError += (ce, err) =>
            //{ 
            //    bool t = false;
            //};
        }

        /// <summary>
        /// Запись сигнала в симулятор наподобие I0.0
        /// </summary>
        public static bool IsAvailable
        {
            get
            {
                //Проверяем, если сумулятора нет, то выходим из функции
                if (PLCSim == null)
                {
                    return false;
                }

                var isconn = PLCSim.GetState().Contains("RUN");

                //Выделяем событие ChangePlcSimState
                if (isconn != isavailable)
                {
                    isavailable = isconn;
                    ChangePlcSimState?.Invoke(isconn);
                }

                return isconn;
            }
        }

        private static bool isavailable;

        /// <summary>
        /// Запись дискретного сигнала в симулятор наподобие
        /// </summary>
        /// <param name="ByteIndex"></param>
        /// <param name="BitIndex"></param>
        /// <param name="value"></param>
        public static void WriteInputBit(int ByteIndex, int BitIndex, bool value)
        {
            //Если симулятор не отвечает, тогда выходим из метода
            if (IsAvailable == false)
            {
                return;
            }

            //Создаем объект, в который помещаем элемент для записи
            object Value = value;

            //Записываем позиции в симулятор
            PLCSim.WriteInputPoint(ByteIndex, BitIndex, ref Value);
        }


        /// <summary>
        /// Запись слова PIW
        /// </summary>
        /// <param name="Address"></param>
        /// <param name="value"></param>
        public static void WriteInputWord(int Address, short value)
        {
            //Если симулятор не отвечает, тогда выходим из метода
            if (IsAvailable == false)
            {
                return;
            }

            //Создаем объект, в который помещаем элемент для записи
            object Value = value;

            //Получаем значение
            object PIW_VALUE = new short[1] { value };

            //Запись значения в симулятор
            PLCSim.WriteInputImage(Address, ref PIW_VALUE);
        }


        /// <summary>
        /// Чтение дискретного сигнала в симулятор наподобие
        /// </summary>
        /// <param name="ByteIndex"></param>
        /// <param name="BitIndex"></param>
        /// <returns></returns>
        public static bool ReadOutputBit(int ByteIndex, int BitIndex)
        {
            //Если симулятор не отвечает, тогда выходим из метода
            if (IsAvailable == false)
            {
                return false;
            }

            //Читаем состояние бита и возвращаем результат
            object BitState = false;
            PLCSim.ReadOutputPoint(ByteIndex, BitIndex, S7PROSIMLib.PointDataTypeConstants.S7_Bit, ref BitState);

            return (bool)BitState;
        }


    }
}
