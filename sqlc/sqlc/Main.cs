using System;

namespace sqlc
{
     class Program
    {
        

        public static void Main(string[] args)
        {
            DataClient client = new DataClient();
            DataClient.Country country = new DataClient.Country
            {
                Code = "ZAR",
                Name = "Zarkan Kingdom",
                Continent = "Europe",
                Region = "North"
            };

            IO io = new IO();
            io.GetCmd();

            //client.Insert(country);
            //client.Update(country);
            //client.Delete("Code", "ZAR");
            //List<DataClient.Country> c = client

            //foreach (DataClient.Country country in c)
            //{
            //    Console.WriteLine(country.Code + country.Name+ country.Continent+country.Region);
            //}

            

        }       
           
    }
}

