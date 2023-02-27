using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace PasswordGenerator.Console.BaseElements
{
    public class GeneratorScreen
    {
        readonly string stars = new string('*', 100);
        readonly string blank = new string(' ', 96 );

        // generator üst alan yazısı:
        public void WelcomePanel() {

            System.Console.WriteLine(stars);
            System.Console.WriteLine("**                    Welcome to the B E S T   P A S S W O R D   M A N A G E R                    **");
            System.Console.WriteLine(stars);
            System.Console.WriteLine();
        }

        // generator parola çıktısı yazısı
        public void OutputPanel()
        {
            System.Console.WriteLine(stars);
            System.Console.WriteLine("**                              G E N E R A T E D   P A S S W O R D:                              **");
            System.Console.WriteLine(stars);
        }

        // generator parola çıktısı yazısı
        public void ErrorPanel()
        {
            System.Console.WriteLine(stars);
            System.Console.Write("**");
            System.Console.Write(blank);
            System.Console.WriteLine("**");
            System.Console.WriteLine("**                                         W A R N I N G !                                        **");
            System.Console.WriteLine("**                            You made an incomplete or incorrect entry                           **");
            System.Console.Write("**");
            System.Console.Write(blank);
            System.Console.WriteLine("**");
            System.Console.WriteLine(stars);
        }



    }
}
