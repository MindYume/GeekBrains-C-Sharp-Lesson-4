using System;

namespace Homework.Utils
{
    /// <summary>
    /// Данный класс содержит методы, которые часто используются при написании программ
    /// </summary>
    public class CommonMethods
    {
        /// <summary>
        /// Выводит в консоли номер урока, номер домашней работы, описание задачи и имя студента
        /// </summary>
        /// <param name="lessonNumber">Номер урока</param>
        /// <param name="homeworkNumber">Номер домашней работы</param>
        /// <param name="taskDescription">Описание задачи в домашней работе</param>
        /// <param name="studentName">Имя студента</param>
        public static void PrintTaskInfo(int lessonNumber, int homeworkNumber, string taskDescription, string studentName)
        {
            Console.WriteLine("=====================================================");
            Console.WriteLine($"Номер урока: {lessonNumber}");
            Console.WriteLine($"Номер домашней работы: {homeworkNumber}");
            Console.WriteLine($"Описание задачи: \n\t{taskDescription}");
            Console.WriteLine();
            Console.WriteLine($"Студент: {studentName}");
            Console.WriteLine("=====================================================");
            Console.WriteLine();
        }

        /// <summary>
        /// Запрашивает у пользователся ввод данных в консоли
        /// </summary>
        /// <param name="message">Сообщение, которое будет выведено перед тем, как пользователь сможет ввести данные</param>
        public static string AskInfo(string message)
        {
            Console.Write(message);
            return Console.ReadLine();
        }

        /// <summary>
        /// Запрашивает у пользователся ввод данных в консоли в виде целого числа
        /// </summary>
        /// <param name="message">Сообщение, которое будет выведено перед тем, как пользователь сможет ввести данные</param>
        public static int AskInfoInt(string message)
        {
            int retNum = 0;

            bool runInput = true;

            while (runInput)
            {
                Console.Write(message);

                switch (int.TryParse(Console.ReadLine(), out retNum))
                {
                    case true:
                        runInput = false;
                        break;

                    case false:
                        Console.WriteLine("Некорректый ввод числа. Повторие ввод.\n");
                        break;
                }
            }

            return retNum;
        }

        /// <summary>
        /// Запрашивает у пользователся ввод данных в консоли в виде дробного числа
        /// </summary>
        /// <param name="message">Сообщение, которое будет выведено перед тем, как пользователь сможет ввести данные</param>
        public static double AskInfoDouble(string message)
        {
            double retNum = 0;

            bool runInput = true;

            while (runInput)
            {
                Console.Write(message);

                switch (double.TryParse(Console.ReadLine(), out retNum))
                {
                    case true:
                        runInput = false;
                        break;

                    case false:
                        Console.WriteLine("Некорректый ввод числа. Повторие ввод.\n");
                        break;
                }
            }

            return retNum;
        }

        /// <summary>
        /// Приостанавливает выполнение программы
        /// </summary>
        /// <param name="message">Сообщение, которое будет выведено в консоли во время паузы</param>
        public static void Pause(string message)
        {
            Console.WriteLine(message);
            Console.ReadKey();
        }

        /// <summary>
        /// Приостанавливает выполнение программы
        /// </summary>
        public static void Pause()
        {
            Console.ReadKey();
        }
    }
}
