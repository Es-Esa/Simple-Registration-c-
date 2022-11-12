



namespace Registration
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var test = new Registration();




            //registration comes with a true if registration ok.
            Console.WriteLine(test.Register());
            Console.ReadLine();






        }

        public class Registration
        {


            public bool Register()
            {
                Console.WriteLine("Rekisteröityminen");
                Console.WriteLine();

                var fetch = new RetrieveData();
                fetch.Retriver();
                string dir = "c:\\temp\\";
                string fileName = "UserData.txt";

                //fetch data
                string[,] userData = RetrieveData.FetchData(dir, fileName);


                //Registration loop
                bool RegistrationLoop = false;

                while (RegistrationLoop != true)
                {
                    string Username, Password, Name, Surname, Height;

                    int row = -1;

                    Console.Write("Käyttäjätunnus: ");
                    Username = Console.ReadLine();


                    if (MatchUser(userData, Username) == true)
                    {
                        Console.WriteLine("Käyttäjätunnus on käytössä. \nValitse uusi.");
                        Console.WriteLine();




                    }
                    else if (MatchUser(userData, Username) != true)
                    {


                        Console.Write("Salasana: ");
                        Password = Console.ReadLine();

                        Console.Write("Nimi: ");
                        Name = Console.ReadLine();

                        Console.Write("Sukunimi: ");
                        Surname = Console.ReadLine();

                        Console.Write("Pituus: ");
                        Height = Console.ReadLine();
                        //wrtites new user
                        string register = Username + ";" + Password + ";" + "0" + ";" + Name + " " + Surname + ";" +
                                          Height + "\n";

                        File.AppendAllText(dir + fileName, register);


                        RegistrationLoop = true;
                    }

                }
                return true;





            }


        }


        //MatchUser check, checks if users already on Userdata.txt

        static bool MatchUser(string[,] Userdata, string username)
        {
            for (int i = 0; i < Userdata.GetLength(0); i++)
            {
                for (int j = 0; j < Userdata.GetLength(1); j++)
                {

                    if Userdata[i, j] != null && Userdata[i, j].Equals(username))
                    {
                        return true;
                    }

                }
            }
            return false;



        }
    }


    //fetchdata
    internal class RetrieveData
    {
        public void Retriver()
        {
            // Luodaan kansio ja testitiedosto txt muodossa.
            string dir = "c:\\temp\\";
            string fileName = "UserData.txt";

            Directory.CreateDirectory(dir);

            //Testisisältö luo tiedoston jos tiedostoa ei ole olemassa. Huomaa huutomerkki.
            if (!File.Exists(dir + fileName))
            {
                string input1 = "username;password;locked;full name;height" + "\n";
                string input2 = "Turtles;pizza1337;0;Jone Nikula;197" + "\n";
                string input3 = "BurgerKing;Wh00p;0;Marko Hietala;175";

                // Rivien määrän verran lisäyksiä.
                File.AppendAllText(dir + fileName, input1);
                File.AppendAllText(dir + fileName, input2);
                File.AppendAllText(dir + fileName, input3);
            }

            // Fetchdata metodilla tietojen hakeminen kaksiulotteiseen taulukkoon.
            // Metodi ottaa parametreiksi tiedoston sijainnin, tiedoston nimen, taulukon sarakkeiden ja rivien lukumäärän.
            string[,] userData = FetchData(dir, fileName);

            // Taulukon sisällön tulostus.
            for (int i = 0; i < userData.GetLength(0); i++)
            {
                for (int j = 0; j < userData.GetLength(1); j++)
                {
                    Console.Write(userData[i, j] + " ");
                }
                Console.Write("\n");
            }
        }
        public static string[,] FetchData(string dir, string fileName)
        {
            // Rivit taulukoksi. Saadaan rivien määrä.
            string[] dataStream = File.ReadAllLines(dir + fileName).ToArray();

            // Otetaan talteen sarakkeiden määrä.
            string[] columns = dataStream[0].Split(';');

            // Alustetaan palautettava lista.
            string[,] resultData = new string[dataStream.Length - 1, columns.Length];

            // Laskin lähtee 1.stä. Jätetään ensimmäinen rivi kirjoittamatta.
            // Lisätään tiedoston sisältö eroteltuna kasiulotteiseen listaan.
            for (int i = 1; i < dataStream.Length; i++)
            {
                string[] splitStream = dataStream[i].Split(';');
                for (int j = 0; j < columns.Length; j++)
                {
                    // resultDataan halutaan kirjoittaa 0 riviltä lähtien.
                    resultData[i - 1, j] = splitStream[j];
                }
            }
            return resultData;
        }
    }






}

