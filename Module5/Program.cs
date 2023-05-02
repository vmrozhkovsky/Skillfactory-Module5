using System.Buffers;
using System.ComponentModel.Design;

internal class Program
{
    private static void Main(string[] args)
    {
        //Метод вывода результатов на экран
        static void ShowUserData((string UserName, string UserLastName, int UserAge, bool UserHavePet, bool UserHaveColors) UserData, string[] UserPetNames, string[] UserColors)
        {
            Console.WriteLine("Здравствуйте {0} {1}. Вам {2} лет.", UserData.UserName, UserData.UserLastName, UserData.UserAge);
            if (UserData.UserHavePet)
            {
                Console.WriteLine("У вас {0} домашних животных.", UserPetNames.Length);
                Console.WriteLine("Их клички:");
                foreach (string PetName in UserPetNames)
                    Console.WriteLine(PetName);
            }
            else
                Console.WriteLine("У вас нет домашних животных.");
            if (UserData.UserHaveColors)
            {
                Console.WriteLine("У вас {0} любимых цветов.", UserColors.Length);
                Console.WriteLine("Их названия:");
                foreach (string Color in UserColors)
                    Console.WriteLine(Color);
            }
            else
                Console.WriteLine("У вас нет любимых цветов.");
        }
        //Метод сбора информации о пользователе
        static (string UserName, string UserLastName, int UserAge, bool UserHavePet, bool UserHaveColors) GetUserData(out string[] UserPetNames, out string[] UserColors)
        {
            (string UserName, string UserLastName, int UserAge, bool UserHavePet, bool UserHaveColors) UserData;
            Console.WriteLine("Введите Ваше имя:");
            UserData.UserName = Console.ReadLine();
            Console.WriteLine("Введите Вашу фамилию:");
            UserData.UserLastName = Console.ReadLine();
            Console.WriteLine("Введите Ваш возраст:");
            string UserAgeString = Console.ReadLine();
            int UserAnswerInt = 0;
            while (IsCorrect(UserAgeString, "int", out UserAnswerInt))
            {
                Console.WriteLine("Вы ввели неверный символ!");
                Console.WriteLine("Введите Ваш возраст:");
                UserAgeString = Console.ReadLine();
            }
            UserData.UserAge = UserAnswerInt;
            UserPetNames = GetUserEnv("pet", out bool UserHaveEnv);
            UserData.UserHavePet = UserHaveEnv;
            UserColors = GetUserEnv("color", out UserHaveEnv);
            UserData.UserHaveColors = UserHaveEnv;
            return UserData;
        }
        //Метод сбора информации о предпочтении пользователя с помещением их в массив, в зависимости от переданного в метод параметра
        static string[] GetUserEnv(string UserEnvType, out bool UserHaveEnv)
        {
            string UserAnswer = "";
            string UserAnswer2 = "";
            string UserAnswer3 = "";
            string ErrorString = "";
            switch (UserEnvType)
            {
                case "pet":
                    UserAnswer = "У Вас есть домашние животные? (y/n)";
                    UserAnswer2 = "Сколько у вас домашних животных?";
                    UserAnswer3 = "Очень жаль. Рекомендую завести.";
                    ErrorString = "Вы ввели неверный символ!";
                    break;
                case "color":
                    UserAnswer = "У Вас есть любимые цвета? (y/n)";
                    UserAnswer2 = "Сколько у вас любимых цветов?";
                    UserAnswer3 = "Очень жаль.";
                    ErrorString = "Вы ввели неверный символ!";
                    break;
            }
            Console.WriteLine(UserAnswer);
            string UserHaveEnvАnswer = Console.ReadLine();
            while (IsCorrect(UserHaveEnvАnswer, "string", out int UsesAnswerInt))
            {
                Console.WriteLine("{0}\n{1}", ErrorString, UserAnswer);
                UserHaveEnvАnswer = Console.ReadLine();
            }
            var UserEnvNames = new string[] { };
            switch (UserHaveEnvАnswer)
            {
                case "y":
                    UserHaveEnv = true;
                    int UserAnswerInt;
                    Console.WriteLine(UserAnswer2);
                    string UserEnvColString = Console.ReadLine();
                    while (IsCorrect(UserEnvColString, "int", out UserAnswerInt))
                    {
                        Console.WriteLine("{0}\n{1}", ErrorString, UserAnswer2);
                        UserEnvColString = Console.ReadLine();
                    }
                    UserEnvNames = ReadDataFromConsole(UserAnswerInt, UserEnvType);
                    break;
                case "n":
                    Console.WriteLine(UserAnswer3);
                    UserHaveEnv = false;
                    break;
                default:
                    UserHaveEnv = false;
                    break;
            }
            return UserEnvNames;
        }
        //Метод формирования массива предпочтений пользователя, в зависимости от переданного в метод параметра
        static string[] ReadDataFromConsole(int ArrayCol, string UserEnvType)
        {
            string UserAnswer = "";
            switch (UserEnvType)
            {
                case "pet":
                    UserAnswer = "Введите кличку животного номер";
                    break;

                case "color":
                    UserAnswer = "Введите любимый цвет номер";
                    break;
            }
            string[] ResultArray = new string[ArrayCol];
            for (int i = 0; i < ResultArray.Length; i++)
            {
                Console.WriteLine("{1} {0}:", i + 1, UserAnswer);
                ResultArray[i] = Console.ReadLine();
            }
            return ResultArray;
        }
        //Метод проверки корректности введенного пользователем значения, в зависимости от переданного в метод параметра
        static bool IsCorrect(string UserAnswerString, string UserAnswerType, out int UserAnswerInt)
        {
                switch (UserAnswerType)
                {
                    case "int":
                        if (int.TryParse(UserAnswerString, out int result) && result > 0)
                        {
                            UserAnswerInt = result;
                            return false;
                        }
                        else
                        {
                            UserAnswerInt = 0;
                            return true;
                        }
                    case "string":
                        if (UserAnswerString == "y" | UserAnswerString == "n")
                        {
                            UserAnswerInt = 1;
                            return false;
                        }
                        else
                        {
                            UserAnswerInt = 0;
                            return true;
                        }
                        default:
                    {
                        UserAnswerInt = 0;
                        return true;
                    }
            }
        }
        var UserData = GetUserData(out string[] UserPetNames, out string[] UserColors);
        ShowUserData(UserData, UserPetNames, UserColors);
    }
}