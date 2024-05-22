using System;
using System.Data;

using MySql.Data;
using MySql.Data.MySqlClient;
using ZstdSharp.Unsafe;


public class DataClient {
    
    //enum('Asia','Europe','North America','Africa','Oceania','Antarctica','South America')
      
    public struct Country
    {
        public string Code;
        public string Name;
        public string Continent;
        public string Region;
    }
     private struct Pair
    {
        public string Key;
        public string Value;
    }


    private static string connStr = "server=localhost;user=root;database=world;port=3306;password=secretpass";
    private static MySqlConnection conn = new MySqlConnection(connStr);

    public void Insert(Country country)
    {
        string query = $"INSERT INTO country (code, name, continent, region) VALUES ('{country.Code}', '{country.Name}', '{country.Continent}', '{country.Region}');";
        try
        {
            conn.Open();

            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read()) { 
                Console.WriteLine(rdr.GetString(0));
            }

        } catch(Exception e)
        {
            Console.WriteLine(e.Message);
        }



    }

    public void Update(Country country) {
        string query = $"UPDATE country set Code = '{country.Code}', Name = '{country.Name}', Continent = '{country.Continent}', Region = '{country.Region}' WHERE Code = '{country.Code}' and Name = '{country.Name}'";

        try
        {
            conn.Open();

            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                Console.WriteLine(rdr.GetString(0));
            }

        } catch(Exception e) { 
            Console.WriteLine(e.Message);
        }

    }

    public void Delete( string field, string value)
    {
        try
        {
            conn.Open();
            string query = $"DELETE FROM country WHERE {field} = '{value}';";
            Console.WriteLine(query);
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                Console.WriteLine(rdr.GetString(0));
            }
            
        } catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        conn.Close();
    }

    private List<Country> Work(string query) {

        List<Country> result = new List<Country>();
        try
        {
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataReader rdr =  cmd.ExecuteReader();
            
            int i = 0; 
            while(rdr.Read())
            {
                result.Add(new Country
                {

                    Code = rdr.GetString("Code"),
                    Name = rdr.GetString("Name"),
                    Continent = rdr.GetString("Continent"),
                    Region = rdr.GetString("Region")

                });
            }
            rdr.Close();
        } catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
        conn.Close ();
        return result;
    }

    public List<Country> searchByName(string name)
    {
        string query = $"SELECT code, name, Continent, Region FROM country WHERE Name = '{name}';";
        return Work(query);
    }

    public List<Country> SearchByContinent(string continent)
    {
        string query = $"SELECT code, name, Continent, Region FROM country WHERE Continent='{continent}';";
        return Work(query);
    }
    public List<Country> SearchByCode(string code)
    {
        string query = $"SELECT code, name, Continent, Region FROM country WHERE Code = '{code}';";
        return Work(query);
    }

}
