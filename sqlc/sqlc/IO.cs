using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataClient;
using static sqlc.IO;

namespace sqlc
{

    public class IO
    {

        private struct Pair
        {
            public string Key;
            public string Value;
        }


        public struct CMD {
            string Cmd;
            DataClient.Country country;
        }

        
        public void GetCmd() {
            Console.WriteLine("Enter the procedure you want to take: Query, Delete, Insert, Update");

            DataClient client = new DataClient();
            DataClient.Country country = new DataClient.Country();
            
            try
            {

                string cmd = Console.ReadLine();

                if (cmd != null)
                {
                    switch (cmd)
                    {
                        case "Insert":
                            country = GetCountry();
                            client.Insert(country);
                            break;
                        case "Delete":
                            Pair pair = GetPair();
                            client.Delete(pair.Key,pair.Value);
                            break;
                        case "Query":
                            QueryHandler(client);
                            break;
                        case "Update":
                            country = GetCountry();
                            client.Insert(country);
                            break;

                    }
                }

            } catch(Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            

        }


        private Pair GetPair()
        {
            Pair pair = new Pair();
            Console.WriteLine("which field do you want to query upon? Code, Name, Region, Continent?");
            try
            {
                pair.Key = Console.ReadLine();
                Console.WriteLine("enter the value to query");
                pair.Value = Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return pair;
        }
        private void QueryHandler(DataClient client)
        {
            Pair pair = GetPair();
            List<DataClient.Country> countries = new List<DataClient.Country>();
            switch (pair.Key) {
                case "Code":
                    countries = client.SearchByCode(pair.Value);
                    break;
                case "Name":
                    countries = client.searchByName(pair.Value);
                    break;
                case "Continent":
                    countries = client.SearchByContinent(pair.Value);
                    break;
            }
            foreach (DataClient.Country country in countries)
            {
                Console.WriteLine(country.Code + " - " + country.Name + " - " + country.Continent + " - " +country.Region);
            }

        }
        private DataClient.Country GetCountry()
        {
            DataClient.Country country = new DataClient.Country();
            try
            {
                Console.WriteLine("Enter Country info\n Name:");
                country.Name = Console.ReadLine();

                Console.WriteLine("Code:");
                country.Code = Console.ReadLine();

                Console.WriteLine("Continent:");
                country.Continent = Console.ReadLine();

                Console.WriteLine("Region:");
                country.Region = Console.ReadLine();
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return country;
        }
    }
}
