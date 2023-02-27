using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using PasswordGenerator.Console.BaseElements;

namespace PasswordGenerator.Console.Generator
{
    public class CreatePasswordBase
    {
        public void CreatePassword()
        {
            characterArrays characterArrays = new characterArrays();

            // numerik, büyük, küçük ve özel karakter dizilerimizi alıyoruz:

            string[] Numbers = characterArrays.Numbers;
            string[] LowercaseCharacters = characterArrays.LowercaseCharacters;
            string[] UppercaseCharacters = characterArrays.UppercaseCharacters;
            string[] SpecialCharacters = characterArrays.SpecialCharacters;

            // kullanıcı seçimleri için sorularımızı alıyoruz: 

            string questionNumbers = Questions.questionNumbers.ToString();
            string questionLowercase = Questions.questionLowercase.ToString();
            string questionUppercase = Questions.questionUppercase.ToString();
            string questionSpecial = Questions.questionSpecial.ToString();
            string questionLength = Questions.questionPasswordLength.ToString();

            // sorulara karşılık gelen cevapları rahat bir şekilde toplamak için sözlük oluşturuyoruz:

            var questionsAndArrays = new Dictionary<string, string[]>
            {
                {questionNumbers, Numbers },
                {questionLowercase, LowercaseCharacters },
                {questionUppercase, UppercaseCharacters },
                {questionSpecial, SpecialCharacters },

            };

            string[] chosenarr = { };

            foreach (KeyValuePair<string, string[]> entry in questionsAndArrays)
            {
                string message = entry.Key;
                string[] array = entry.Value;

                System.Console.Write(message);
                char answer;
                answer = System.Convert.ToChar(System.Console.ReadLine());

                switch (answer)
                {
                    case 'y':

                        chosenarr = chosenarr.Concat(array).ToArray();
                        break;
                    case 'Y':

                        chosenarr = chosenarr.Concat(array).ToArray();
                        break;
                    case 'n':
                        break;
                    case 'N':
                        break;
                    default:
                        System.Console.WriteLine("** You made an incomplete or incorrect entry ");
                        break;
                }
            }
            System.Console.Write(questionLength);
            
            Random random= new Random();

            
            // kullanıcıdan aldığımız bilgilere göre ekrana random şifre veriyoruz:

            void final()
            {
                GeneratorScreen generatorScreen = new GeneratorScreen();
                
                try
                {
                    int wantedLength = System.Convert.ToInt32(System.Console.ReadLine());
                    generatorScreen.OutputPanel();
                    System.Console.WriteLine();
                    for (int i = 0; i < wantedLength; i++)
                    {
                        int chosencharacter;
                        chosencharacter = random.Next(0, chosenarr.Length);
                        System.Console.Write(chosenarr[chosencharacter]);
                    }
                    
                }
                catch 
                {
                    generatorScreen.ErrorPanel();
                }
                
            }
            final();
        }
    }
}
