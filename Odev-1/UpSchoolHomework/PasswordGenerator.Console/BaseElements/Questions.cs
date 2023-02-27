using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordGenerator.Console.BaseElements
{
    public static class Questions
    {
        public static string questionNumbers => "** Do u want to Include Numbers? (Y/N)                                                            ** \n   ";
        public static string questionLowercase => "** OK! How about lowercase characters? (Y/N)                                                      **\n   ";
        public static string questionUppercase => "** Very nice! How about uppercase characters? (Y/N)                                               **\n   ";
        public static string questionSpecial => "** All right! We are almost done. Would you also want to add special characters? (Y/N)            **\n   ";
        public static string questionPasswordLength => "** Great! Lastly. How long do you want to keep your password length?                              **\n   ";

    }

}



