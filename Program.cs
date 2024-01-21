using System;
using System.Linq;
using System.Text;


class program
{
    public static String[] Playerinfo = { };// this array just for user informations
    public static String[] QuestionsList = { };// this array to save questions
    public static String[] AnswersList = { };// this array to save user answers
    public static String[] CorrectAnswersList = { };// this to save Correct answers
    public static String[] ResultsList = { };// this array to save the results of each question

    // main function to start program
    static void Main()
    {   
        // we need variable to ensure app will continue
        bool runApp = true;
        // show device informations
        Console.WriteLine("Windows version: {0}", Environment.OSVersion);
        Console.WriteLine("64 Bit operating system? : {0}", Environment.Is64BitOperatingSystem ? "Yes" : "No");
        Console.WriteLine("PC Name : {0}", Environment.MachineName);
        Console.WriteLine("Number of CPUS : {0}", Environment.ProcessorCount);
        Console.WriteLine("Windows folder : {0}", Environment.SystemDirectory);
        Console.WriteLine("Logical Drives Available : {0}", String.Join(", ", Environment.GetLogicalDrives()).TrimEnd(',', ' ').Replace("\\", String.Empty));
        // start loop 
        while (runApp)
        {
            Console.WriteLine("\n\n\t****** Start our Game ******");
            // call function to get number of questions
            int numOfQuestions = getNumOfQuestions();
            // call function to add player informations
             Playerinfo = AddPlayerInfo();
            // ask questions to user
            AskQuestions(numOfQuestions);
            bool showMenu = true;
            // show user Menu loop
            while (showMenu)
            {
                // show the menu
                Console.WriteLine("To show the Questions with Answers as Table press 1");
                Console.WriteLine("To show the Count of Correct Answers press 2");
                Console.WriteLine("To show the Count of Wrong Answers press 3");
                Console.WriteLine("To Start the Game Again press 4");
                Console.WriteLine("To Exit the Game press exit");
                // get user input
                string input = Console.ReadLine().Trim();
                // do what needed as user choice
                switch (input.ToLower())
                {
                    case "1":// this choice to show all questions as table 
                        {
                            ShowResultTable(numOfQuestions);
                            Console.WriteLine(" **********************************************");

                            break;
                        }
                    case "2":// this choice to show correct answers
                        {
                          int cnt=  ShowCorrectAnswersCount();
                            Console.WriteLine(" The Number of Correct Answeres is: {0}",cnt);
                            Console.WriteLine(" **********************************************");

                            break;
                        }
                    case "3"://this choice to show worng answers
                        {
                           int cnt= ShowWrongAnswersCount();
                            Console.WriteLine(" The Number of Wrong Answeres is: {0}", cnt);
                            Console.WriteLine(" **********************************************");

                            break;
                        }
                    case "4":// this choice to exit menu and start game again
                        {
                            showMenu = false;
                            break;
                        }
                    case "exit": // this choice to exit the menu and the program loop
                        {
                            showMenu = false;
                            runApp = false;
                            break;
                        }
                }
            }
            



        }
    }



   static int getNumOfQuestions()// this is the function to get number of questions from the Player
    {
        int numOfQ = 0;
        // this loop to ask the user how many questions does he need 
        // and it will be repeated until enter number more than zero
        Console.WriteLine("Please enter the maximum number of  questions");
        while (numOfQ <= 0)
        {
            // get the number from the user
            numOfQ = int.Parse(Console.ReadLine());
            // check if the number is valid
            if (numOfQ == 0)
            {
                // the number is not valid so we must repeate again
                Console.WriteLine("The number of questions should be an integer > 0, please enter it again:");
            }
        }
        return numOfQ;
    }
    // this function to enter user information
   static String[] AddPlayerInfo()
    {
        String[] info = { };
        bool goodinfo = false;
        while (goodinfo == false)
        {

            Console.WriteLine("Please enter your name (first name and last name) and your SVU id number and " +
                "with a space between each part (Accepted Chars: A-Z a-z  0-9)" +
                "The entered text should contain at least 2 of Accepted chars.");
            string playerName = Console.ReadLine();
            // now define array to put the info words splited by  space
            info = new string[playerName.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length];
            // check if the user enter more than one informations
            if (info.Length <= 1)
                // if just one or less try again
                continue;
            else
            {
                // go to stor the information and show it
                goodinfo = true;
                for (int i = 0; i < info.Length - 1; i++)
                {
                    info[i] = playerName.Split(' ', StringSplitOptions.RemoveEmptyEntries)[i];
                }
                Console.WriteLine("Your full name and id and ...:{0}", playerName);
                playerName = playerName.Replace(" ", "");
                var distinctChars = playerName.Distinct();
                Console.WriteLine("Distinct Chars are : {0}", string.Join("", distinctChars));

            }

        }
        return info;
    }
    // this is the function which ask many questions ans save data
    static void AskQuestions(int numberofQuestions)
    {
        // redecleare the arrays 
        QuestionsList = new string[numberofQuestions];
        AnswersList = new string[numberofQuestions];
        CorrectAnswersList = new string[numberofQuestions];
        ResultsList = new string[numberofQuestions];
        // make this loop as user question number
        for (int i = 0; i < numberofQuestions; i++)
        {
            int ch = 0;
            // this loop to ensure user input is more than 9
            while (ch < 9 || ch > 99)
            {
                Console.WriteLine("Question {0}", i + 1);
                Console.WriteLine("Please enter an integer value between 9 and 99 ( the" +
                    " number of characters from which to enumerate the most or lesast repeated " +
                    "characters == Degree of Difficulty)");
                ch = int.Parse(Console.ReadLine());
            }
            // now generate random string
            string question = GenerateRandomString(ch);
            // generate random char to ask about it
            char oneCh = GenerateRandomChar();
            // print the question
            Console.WriteLine(" What are the number of times the character {0} is " +
                "repeated in the following text \n {1}", oneCh, question);
            Console.WriteLine("To ignore the question type Ignore");
            // get user answer
            string userAnswer =Console.ReadLine();
            // now add data to arrays
            // save question
            QuestionsList[i] = question;
            // save answer
            AnswersList[i] = userAnswer;
            // calc the correct answer
            int correctAnswer = CalcCorrectAnswer(question, oneCh);
            CorrectAnswersList[i] = correctAnswer.ToString();
            // check if the answer is ignore  case insensitive
            if (userAnswer.ToLower() == "ignore")
            {
                ResultsList[i] = "Wrong";
                continue;
            }
            else // if not ignore we must save the answer and check if correct
            {
                int answer = int.Parse(userAnswer);
                // check if correct answer
                if (correctAnswer == answer)
                    ResultsList[i] = "Correct";
                else
                    ResultsList[i] = "Wrong";
            }
           // end the question
            Console.WriteLine(" **********************************************");
        }

    }
    static char GenerateRandomChar()
    {
        string inputString = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        Random random = new Random();

        // Generate a random index within the range of the string length
        int randomIndex = random.Next(inputString.Length);

        // Return the character at the randomly generated index
        return inputString[randomIndex];
    }
    static string GenerateRandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        // Use StringBuilder collect the chars
        StringBuilder randomString = new StringBuilder(length);
        Random random = new Random();

        // Generate random characters and append to the string
        for (int i = 0; i < length; i++)
        {
            randomString.Append(chars[random.Next(chars.Length)]);
        }

        return randomString.ToString();
    }

    static int CalcCorrectAnswer(string question, char choice)
    {
        int count = 0;
        // search about the char inside the question
        foreach (char c in question)
        {
            if (c == choice)
            {
                count++;
            }
        }
       return count;

    }
    // this function for show the whole data
    static void ShowResultTable(int numberofQuestions)
    {
        // show table header
        Console.WriteLine("{0,-15}\t{1,-18}\t {2,-15}   {3,-15}", "User Answer", "Correct Answer", "Type", "Question");
        for (int i = 0; i < numberofQuestions; i++)
        {
            // show table content from arrays
        Console.WriteLine("{0,-15}\t{1,-18}\t{2,-15}   {3,-15}", AnswersList[i], CorrectAnswersList[i],ResultsList[i],QuestionsList[i]);

        }
    }
    // this function to get  correct result count
    static int ShowCorrectAnswersCount()
    {
        int count = 0;
        for (int i = 0;i < CorrectAnswersList.Length;i++)
        {
            if (ResultsList[i] == "Correct"){
                count++;
            }
        }
        return count;
    }
    // this function to get  Wrong result count
    static int ShowWrongAnswersCount()
    {
        int count = 0;
        for (int i = 0; i < CorrectAnswersList.Length; i++)
        {
            if (ResultsList[i] == "Wrong")
            {
                count++;
            }
        }
        return count;
    }
}


