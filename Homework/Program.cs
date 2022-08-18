using Homework.Utils;
using ArrayLibrary;
using System;
using System.IO;

namespace Homework
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Включение Кириллицы в консоли
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            bool runProgram = true;

            while (runProgram)
            {
                Console.WriteLine("=====================================================");
                Console.WriteLine("1 -> Задача 1");
                Console.WriteLine("2 -> Задача 2");
                Console.WriteLine("0 -> Выход");
                Console.WriteLine("=====================================================");

                int taskNumber;

                // Если пользователь неправильно ввёл номер задачи, то taskNumber будет равным -1, и в блоке switch
                // программа сообщит об этом пользователю
                if (!int.TryParse(CommonMethods.AskInfo("Введите номер задачи: "), out taskNumber))
                {
                    taskNumber = -1;
                }

                switch (taskNumber)
                {
                    default:
                        Console.WriteLine("Некорректый номер задачи. Повторие ввод.\n");
                        break;

                    case 0:
                        runProgram = false;
                        break;

                    case 1:
                        Task1();
                        break;

                    case 2:
                        Task2();
                        break;
                }
            }

            CommonMethods.Pause("\nНажмите любую клавишу, чтобы выйти...");
        }

        #region Методы для вызова задачь из домашней работы

        static void Task1()
        {
            CommonMethods.PrintTaskInfo(
                4,
                1,
                "а) Дописать класс для работы с одномерным массивом. Реализовать конструктор," +
                "создающий массив определенного размера и заполняющий массив числами от начального" +
                "значения с заданным шагом. Создать свойство Sum, которое возвращает сумму элементов массива," +
                "метод Inverse, возвращающий новый массив с измененными знаками у всех элементов" +
                "массива (старый массив, остается без изменений), метод Multi, умножающий каждый элемент" +
                "массива на определённое число, свойство MaxCount, возвращающее количество максимальных элементов." +
                "\n\tб) * *Создать библиотеку содержащую класс для работы с массивом.Продемонстрировать работу библиотеки",
                "Грачёв Виктор Алексеевич"
            );

            ArrayClass array = ChooseArray();

            Console.Write($"Массив: ");
            array.PrintArray();
            Console.WriteLine();

            PerformArrayAction(array);

            CommonMethods.Pause("Нажмите любую клавишу, чтобы продолжить...");
        }

        static void Task2()
        {
            CommonMethods.PrintTaskInfo(
                4,
                2,
                "Решить задачу с логинами из урока 2, только логины и пароли считать из файла в массив." +
                "\n\tСоздайте структуру Account, содержащую Login и Password",
                "Грачёв Виктор Алексеевич"
            );

            Account account = new Account();
            account.Login = "root";
            account.Password = "GeekBrains";


            #region Загрузка логинов и паролей из файла в массив

            string fileName = AppDomain.CurrentDomain.BaseDirectory + "Accounts.txt";
            string[] accountDataArraty;

            if (File.Exists(fileName))
            {
                StreamReader streamReader = new StreamReader(fileName);
                string[] buf = new string[1000];
                int count = 0;

                while (!streamReader.EndOfStream)
                {
                    buf[count] = streamReader.ReadLine();
                    buf[count+1] = streamReader.ReadLine();

                    count += 2;
                }
                streamReader.Close();

                accountDataArraty = new string[count];
                Array.Copy(buf, accountDataArraty, count);
            }
            else
                throw new FileNotFoundException();

            #endregion

            #region Реализация решения задачи из второго урока, но для массива из логинов и паролой

            int attempts = 3;
            int accountNumber = 1;

            do
            {
                string login    = accountDataArraty[accountNumber * 2 - 2];
                string password = accountDataArraty[accountNumber * 2 - 1];

                if (account.verifyAccount(login, password))
                {
                    Console.WriteLine($"Логин и пароль {accountNumber} аккаунта подходят\n");
                    break;
                }
                else
                {
                    attempts--;

                    if (attempts > 0)
                    {
                        Console.WriteLine($"Логин и пароль {accountNumber} аккаунта не подходят.");
                        Console.WriteLine($"Количество оставшихся попыток: {attempts}\n");
                        accountNumber++;
                    }
                    else
                    {
                        Console.WriteLine($"Логин и пароль {accountNumber} аккаунта не подходят.");
                        Console.WriteLine("Закончились попытки ввести логин и пароль правильно.\n");
                        break;
                    }
                }
            }
            while (true);

            #endregion

            CommonMethods.Pause("Нажмите любую клавишу, чтобы продолжить...");
        }

        #endregion

        #region Вспомогательные методы

        /// <summary>
        /// Спрашивает у пользователя, какой массив создать, и возвращает созданный массив
        /// </summary>
        static ArrayClass ChooseArray()
        {
            while (true)
            {
                Console.WriteLine("Как создать массив?");
                Console.WriteLine();
                Console.WriteLine("1 -> Создать массив определенного размера и заполнить массив числами от начального значения с заданным шагом");
                Console.WriteLine("2 -> Создать массив определённого размера и заполнить его случайными числами от -99 до 99");
                Console.WriteLine("3 -> Создать массив вручную");
                Console.WriteLine("4 -> Загрузить массив из файла Array.txt");
                Console.WriteLine();


                int chooseNumber;

                // Если пользователь неправильно ввёл номер, то chooseNumber будет равным -1, и в блоке switch
                // программа сообщит об этом пользователю
                if (!int.TryParse(CommonMethods.AskInfo("Введите номер: "), out chooseNumber))
                {
                    chooseNumber = -1;
                }

                switch (chooseNumber)
                {
                    default:
                        Console.WriteLine("Некорректый номер. Повторие ввод.\n");
                        break;

                    case 1:
                        return CreateStepArray();

                    case 2:
                        return CreateRandomArray();

                    case 3:
                        return CreateOwnArray();

                    case 4:
                        return new ArrayClass("Array.txt");
                }
            }

            return new ArrayClass();
        }

        /// <summary>
        /// Запрашивает у пользователя данные для создания массива и возвращает созданный массив
        /// </summary>
        static ArrayClass CreateStepArray()
        {
            while (true)
            {
                Console.WriteLine();

                int length = CommonMethods.AskInfoInt("Введите длину массива: ");
                int firstNumber = CommonMethods.AskInfoInt("Введите начальное значение массива: ");
                int step = CommonMethods.AskInfoInt("Введите размер шага: ");

                try
                {
                    return new ArrayClass(length, firstNumber, step);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ ex.Message}");
                    Console.WriteLine($"Введите данные ещё раз");
                }
            }
        }

        /// <summary>
        /// Запрашивает у пользователя длину массива и возвращает созданный массив, заполненный случайными числами от -99 до 99
        /// </summary>
        static ArrayClass CreateRandomArray()
        {
            while (true)
            {
                Console.WriteLine();

                int length = CommonMethods.AskInfoInt("Введите длину массива: ");

                try
                {
                    return new ArrayClass(length);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message}");
                    Console.WriteLine($"Введите данные ещё раз");
                }
            }
        }

        /// <summary>
        /// Запрашивает у пользователя длину массива и значение каждого элемента, после чего возвращает созданный массив
        /// </summary>
        static ArrayClass CreateOwnArray()
        {
            int length;

            while(true)
            {
                length = CommonMethods.AskInfoInt("Введите длину массива: ");

                if (length < 1)
                    Console.WriteLine("Размер массива должен быть больше 0\n");
                else
                    break;
            }

            int[] inputArray = new int[length];

            for (int i = 0; i < length; i++)
            {
                inputArray[i] = CommonMethods.AskInfoInt($"Введите {i} элемент массива: ");
            }

            return new ArrayClass(inputArray);
        }

        /// <summary>
        /// Спрашивет у пользователя, что нужно сделать с массивом и выполняет это действие
        /// </summary>
        /// <param name="array">массив, над которым нужно произвести действие</param>
        static void PerformArrayAction(ArrayClass array)
        {
            bool stop = false;
            while (!stop)
            {
                Console.WriteLine("Что нужно сделат с массивом?");
                Console.WriteLine();
                Console.WriteLine("1 -> Получить сумму всех элементов массива");
                Console.WriteLine("2 -> Создать новый массив на основе созданного, но изменить знаки у всех элементов");
                Console.WriteLine("3 -> Умножить каждый элемент массива на определённое число");
                Console.WriteLine("4 -> Получить количество максимальных элементов");
                Console.WriteLine();


                int chooseNumber;

                // Если пользователь неправильно ввёл номер, то chooseNumber будет равным -1, и в блоке switch
                // программа сообщит об этом пользователю
                if (!int.TryParse(CommonMethods.AskInfo("Введите номер: "), out chooseNumber))
                {
                    chooseNumber = -1;
                }

                switch (chooseNumber)
                {
                    default:
                        Console.WriteLine("Некорректый номер. Повторие ввод.\n");
                        break;

                    case 1:
                        Console.WriteLine($"Сумма всех элементов массива: {array.Sum}\n");
                        stop = true;
                        break;

                    case 2:
                        Console.Write($"Новый массив с изменёнными знаками: ");
                        ArrayClass.PrintArray(array.Inverse());
                        stop = true;
                        break;

                    case 3:
                        int multiplier = CommonMethods.AskInfoInt("Введите множитель: ");
                        array.Multi(multiplier);

                        Console.Write($"Массив с элементами, умноженными на введённое число: ");
                        array.PrintArray();

                        Console.WriteLine();

                        stop = true;
                        break;

                    case 4:
                        Console.WriteLine($"Количество максимальных элементов массива: {array.MaxCount}\n");
                        stop = true;
                        break;
                }
            }
        }

        #endregion
    }

    public struct Account
    { 
        private string login;
        private string password;

        public string Login
        {
            get { return login; }
            set { login = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public bool verifyAccount(string login, string password)
        {
            if (this.login == login && this.password == password)
                return true;
            else
                return false;
        }
    }
}
