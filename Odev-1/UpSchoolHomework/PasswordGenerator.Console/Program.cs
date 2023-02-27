
using PasswordGenerator.Console.BaseElements; // sınıflarımı kullanabilmek için klasörümü çağırıyorum
using PasswordGenerator.Console.Generator;

GeneratorScreen generatorScreen= new GeneratorScreen(); // welcomepanel ve outputpanel kullanımı için bu işlemi yapıyorum
generatorScreen.WelcomePanel();

CreatePasswordBase createPassword= new CreatePasswordBase();
createPassword.CreatePassword();


Console.Read();