using System;

namespace StringManipulationChallenge
{
    public class Program
    {
        static void Main(string[] args)
        {
            string userInputString; //this will hold your users message
            int elementNum;         //this will hold the element number within the messsage that your user indicates
            char char1;             //this will hold the char value that your user wants to search for in the message.
            string fName;           //this will hold the users first name
            string lName;           //this will hold the users last name
            string userFullName;    //this will hold the users full name;

            //
            //
            //implement the required code here and within the methods below.
            //
            //

            Console.WriteLine("Please enter your message and press enter");
            userInputString = Console.ReadLine();
            Console.WriteLine($"Please enter a number LESS THAN the length of your string and press enter (Length: {userInputString.Length})");
            elementNum = Int32.Parse(Console.ReadLine());
            // Console.WriteLine(elementNum);

            Console.WriteLine("For which character should I search in your original message?");
            char1 = Console.ReadKey().KeyChar;

            //String methods
            StringToUpper(userInputString);
            StringToLower(userInputString);
            StringTrim(userInputString);
            StringSubstring(userInputString, elementNum);

            //Strings with chars
            int charNum = SearchChar(userInputString, char1);
            Console.WriteLine($"The character {char1} first appears in position {charNum}");

            // Concat
            Console.WriteLine("What is your first name?");
            fName = Console.ReadLine();
            Console.WriteLine("What is your last name?");
            lName = Console.ReadLine();
            userFullName = ConcatNames(fName, lName);
            Console.WriteLine($"Your full name is {userFullName}");


        }

        // This method has one string parameter. 
        // It will:
        // 1) change the string to all upper case, 
        // 2) print the result to the console and 
        // 3) return the new string.
        public static string StringToUpper(string x){
            x = x.ToUpper();
            Console.WriteLine(x);
            return x;
            throw new NotImplementedException("StringToUpper method not implemented.");
        }

        // This method has one string parameter. 
        // It will:
        // 1) change the string to all lower case, 
        // 2) print the result to the console and 
        // 3) return the new string.        
        public static string StringToLower(string x){
            x = x.ToLower();
            Console.WriteLine(x);
            return x;
            throw new NotImplementedException("StringToUpper method not implemented.");

        }
        
        // This method has one string parameter. 
        // It will:
        // 1) trim the whitespace from before and after the string, 
        // 2) print the result to the console and 
        // 3) return the new string.
        public static string StringTrim(string x){
            x = x.Trim();
            Console.WriteLine(x);
            return x;
            throw new NotImplementedException("StringTrim method not implemented.");

        }
        
        // This method has two parameters, one string and one integer. 
        // It will:
        // 1) get the substring based on the integer received, 
        // 2) print the result to the console and 
        // 3) return the new string.
        public static string StringSubstring(string x, int elementNum){
            x = x.Substring(elementNum);
            Console.WriteLine(x);
            return x;
            throw new NotImplementedException("StringSubstring method not implemented.");

        }

        // This method has two parameters, one string and one char.
        // It will:
        // 1) search the string parameter for the char parameter
        // 2) return the index of the char.
        public static int SearchChar(string userInputString, char x){
            return userInputString.IndexOf(x);
            throw new NotImplementedException("SearchChar method not implemented.");
        }

        // This method has two string parameters.
        // It will:
        // 1) concatenate the two strings with a space between them.
        // 2) return the new string.
        public static string ConcatNames(string fName, string lName){
            return String.Concat(fName + ' ' + lName);
            throw new NotImplementedException("ConcatNames method not implemented.");
        }



    }//end of program
}
