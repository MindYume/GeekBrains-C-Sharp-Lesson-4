using System;
using System.IO;

namespace ArrayLibrary
{
    public class ArrayClass
    {
        private int[] array;

        #region Конструкторы

        /// <summary>
        /// Конструктор, предотвращающий создание пустого массива
        /// </summary>
        public ArrayClass()
        {
             throw new Exception("Мы не можете создать пустой массив. Выберите один из конструкторов");
        }

        /// <summary>
        /// Конструктор, создающий массив определенного размера и заполняющий массив числами от начального значения с заданным шагом
        /// </summary>
        /// <param name="length">размер массива</param>
        /// <param name="firstNumber">начальное значене массива</param>
        /// <param name="step">шаг, на который увеличивается значение каждого следующего элемента массива от начального значения при создании</param>
        public ArrayClass(int length, int firstNumber, int step)
        {
            if (length < 1)
                throw new Exception("Размер массива должен быть больше 0");

            array = new int[length];

            array[0] = firstNumber;
            for (int i = 1; i < length; i++)
            {
                array[i] = array[i-1] + step;
            }
        }


        /// <summary>
        /// Конструктор, создающий массив определенного размера и заполняющий массив случайными числами от -99 до 99
        /// </summary>
        /// <param name="length">размер массива</param>
        public ArrayClass(int length)
        {
            if (length < 1)
                throw new Exception("Размер массива должен быть больше 0");

            array = new int[length];
            Random random = new Random();

            for (int i = 0; i < length; i++)
            {
                array[i] = random.Next(-99, 100);
            }
        }


        /// <summary>
        /// Конструктор, которовый привязывает массив класса к массиву, передаваемому в аргументе
        /// </summary>
        /// <param name="array">Массив, к которому будет привязан массив класса</param>
        public ArrayClass(string fileName)
        {
            array = LoadFromFile(fileName);
        }


        /// <summary>
        /// Конструктор, которовый привязывает массив класса к массиву, передаваемому в аргументе
        /// </summary>
        /// <param name="array">Массив, к которому будет привязан массив класса</param>
        public ArrayClass(int[] array)
        {
            this.array = array;
        }

        #endregion

        #region Свойства

        /// <summary>
        /// Индексное свойство, которе позволяет доставать значения из массива и перезаписывать их
        /// </summary>
        /// <param name="index">индекс элемента массива</param>
        /// <returns>Возвращает значение элемента по индексу</returns>
        public int this[int index]
        {
            get { return array[index]; }
            set { array[index] = value; }
        }


        /// <summary>
        /// Свойство, которое возвращает сумму элементов массива
        /// </summary>
        public int Sum
        {
            get
            {
                int sum = 0;

                for (int i = 0; i < array.Length; i++)
                {
                    sum += array[i];
                }

                return sum;
            }
        }


        /// <summary>
        /// свойство, возвращающее количество максимальных элементов
        /// </summary>
        public int MaxCount
        {
            get
            {
                int maxAmount = 1;
                int maxNumber = array[0];

                for (int i = 1; i < array.Length; i++)
                {
                    if (array[i] == maxNumber)
                    {
                        maxAmount++;
                    }
                    else if (array[i] > maxNumber)
                    {
                        maxNumber = array[i];
                        maxAmount = 1;
                    }
                }

                return maxAmount;
            }
        }

        #endregion

        #region Публичные методы

        /// <summary>
        /// Метод, позволяющий найти индекс элемента числовго массива, если он есть
        /// </summary>
        /// <param name="arr">массив, в котором нужно найти индекс элемента</param>
        /// <param name="e">элемент, по которому ищется индекс</param>
        /// <returns>Возвращает индекс элемента. Если элемент не найден, возвращает -1</returns>
        public static int FindElement(int[] arr, int e)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == e)
                { 
                    return i;
                }
            }

            return -1;
        }


        /// <summary>
        /// Выводит значения введённого числового массива
        /// </summary>
        /// <param name="arr">передаваемый массив</param>
        public static void PrintArray(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write($"{arr[i]}\t");
            }

            Console.WriteLine();
        }


        /// <summary>
        /// Выводит значения массива
        /// </summary>
        public void PrintArray()
        {
            for (int i = 0; i < array.Length; i++)
            {
                Console.Write($"{array[i]}\t");
            }

            Console.WriteLine();
        }


        /// <summary>
        /// Метод, возвращающий новый массив с измененными знаками у всех элементов массива
        /// </summary>
        public int[] Inverse()
        {
            int[] arrayInverse = new int[array.Length];

            for (int i = 0; i < array.Length; i++)
            {
                arrayInverse[i] = -array[i];
            }

            return arrayInverse;
        }


        /// <summary>
        /// Метод, умножающий каждый элемент массива на определённое число
        /// </summary>
        /// <param name="multiplier">множитель</param>
        public void Multi(int multiplier)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] *= multiplier;
            }
        }

        #endregion

        #region Приватные методы

        /// <summary>
        /// Вспомогательный метод, который возвращает массив, загруженный из файла
        /// </summary>
        /// <param name="fileName">имя файла</param>
        private int[] LoadFromFile(string fileName)
        {
            if (File.Exists(fileName))
            {
                StreamReader streamReader = new StreamReader(fileName);
                int[] buf = new int[1000];
                int count = 0;

                while (!streamReader.EndOfStream)
                {
                    buf[count] = int.Parse(streamReader.ReadLine());
                    count++;
                }
                streamReader.Close();

                int[] arr = new int[count];
                Array.Copy(buf, arr, count);

                return arr;
            }
            else
                throw new FileNotFoundException();
        }

        #endregion

    }
}
