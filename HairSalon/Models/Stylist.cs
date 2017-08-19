using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace HairSalon.Models
{
  public class Stylist
  {
    private int _id;
    private string _firstName;
    private string _lastName;
    private int _womensCut;
    private int _mensCut;

    public Stylist(string firstName, string lastName, int womensCut, int mensCut, int id=0)
    {
      _id = id;
      _firstName = firstName;
      _lastName = lastName;
      _womensCut = womensCut;
      _mensCut = mensCut;
    }

    public int GetId()
    {
      return _id;
    }

    public string GetFirstName()
    {
      return _firstName;
    }

    public string GetLastName()
    {
      return _lastName;
    }

    public int GetWomensCut()
    {
      return _womensCut;
    }

    public int GetMensCut()
    {
      return _mensCut;
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM stylists;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static List<Stylist> GetAll()
    {
      List<Stylist> allStylist = new List<Stylist> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM stylists ORDER BY first_name ASC;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int StylistId = rdr.GetInt32(0);
        string StylistFirstName = rdr.GetString(1);
        string StylistLastName = rdr.GetString(2);
        int StylistWomensCut = rdr.GetInt32(3);
        int StylistMensCut = rdr.GetInt32(4);
        Stylist newStylist = new Stylist(StylistFirstName, StylistLastName, StylistWomensCut, StylistMensCut, StylistId);
        allStylist.Add(newStylist);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allStylist;
    }

    public override bool Equals(System.Object otherStylist)
    {
      if(!(otherStylist is Stylist))
      {
        return false;
      }
      else
      {
        Stylist newStylist = (Stylist) otherStylist;
        bool idEquality = (this.GetId() == newStylist.GetId());
        bool firstNameEquality = (this.GetFirstName() == newStylist.GetFirstName());
        bool lastNameEquality = (this.GetLastName() == newStylist.GetLastName());
        bool womensCutEquality = (this.GetWomensCut() == newStylist.GetWomensCut());
        bool mensCutEquality = (this.GetMensCut() == newStylist.GetMensCut());
        return (idEquality && firstNameEquality && lastNameEquality && womensCutEquality && mensCutEquality);
      }
    }

    public override int GetHashCode()
    {
      return this.GetFirstName().GetHashCode();
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO stylists (first_name, last_name, womens_cut, mens_cut) VALUES (@first_name, @last_name, @womens_cut, @mens_cut);";

      MySqlParameter firstName = new MySqlParameter();
      firstName.ParameterName = "@first_name";
      firstName.Value = this._firstName;
      cmd.Parameters.Add(firstName);

      MySqlParameter lastName = new MySqlParameter();
      lastName.ParameterName = "@last_name";
      lastName.Value = this._lastName;
      cmd.Parameters.Add(lastName);

      MySqlParameter womensCut = new MySqlParameter();
      womensCut.ParameterName = "@womens_cut";
      womensCut.Value = this._womensCut;
      cmd.Parameters.Add(womensCut);

      MySqlParameter mensCut = new MySqlParameter();
      mensCut.ParameterName = "@mens_cut";
      mensCut.Value = this._mensCut;
      cmd.Parameters.Add(mensCut);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static Stylist Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM stylists WHERE id = @thisId;";

      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = id;
      cmd.Parameters.Add(thisId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      int stylistId = 0;
      string firstName = "";
      string lastName = "";
      int womensCut = 0;
      int mensCut = 0;

      while (rdr.Read())
      {
        stylistId = rdr.GetInt32(0);
        firstName = rdr.GetString(1);
        lastName = rdr.GetString(2);
        womensCut = rdr.GetInt32(3);
        mensCut = rdr.GetInt32(4);
      }
      Stylist foundStylist = new Stylist(firstName, lastName, womensCut, mensCut, id);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return foundStylist;
    }

    public List<Client> GetClients()
  {
     List<Client> allClients = new List<Client> ();
     MySqlConnection conn = DB.Connection();
     conn.Open();
     var cmd = conn.CreateCommand() as MySqlCommand;
     cmd.CommandText = @"SELECT * FROM clients WHERE stylist_id = @stylistId ORDER BY last_name ASC";

     MySqlParameter stylistId = new MySqlParameter();
     stylistId.ParameterName = "@stylistId";
     stylistId.Value = this._id;
     cmd.Parameters.Add(stylistId);


     var rdr = cmd.ExecuteReader() as MySqlDataReader;
     while(rdr.Read())
     {
       int clientId = rdr.GetInt32(0);
       string firstName = rdr.GetString(1);
       string lastName = rdr.GetString(2);
       string phone = rdr.GetString(3);
       string email = rdr.GetString(4);
       int fk_stylistId = rdr.GetInt32(5);
       Client newClient = new Client(firstName, lastName, phone, email, fk_stylistId, clientId);
       allClients.Add(newClient);
     }
     conn.Close();
     if (conn != null)
     {
       conn.Dispose();
     }
     return allClients;
  }

    public void UpdateStylistLastName(string newLastName)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE stylists SET last_name = @newLastName WHERE id = @searchId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = _id;
      cmd.Parameters.Add(searchId);

      MySqlParameter lastName = new MySqlParameter();
      lastName.ParameterName = "@newLastName";
      lastName.Value = newLastName;
      cmd.Parameters.Add(lastName);

      cmd.ExecuteNonQuery();
      _lastName = newLastName;
      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
    }

    public void UpdateStylistFirstName(string newFirstName)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE stylists SET first_name = @newFirstName WHERE id = @searchId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = _id;
      cmd.Parameters.Add(searchId);

      MySqlParameter firstName = new MySqlParameter();
      firstName.ParameterName = "@newFirstName";
      firstName.Value = newFirstName;
      cmd.Parameters.Add(firstName);

      cmd.ExecuteNonQuery();
      _firstName = newFirstName;
      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
    }

    public void UpdateStylistWomensCut(int newWomensCut)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE stylists SET womens_cut = @newWomensCut WHERE id = @searchId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = _id;
      cmd.Parameters.Add(searchId);

      MySqlParameter womensCut = new MySqlParameter();
      womensCut.ParameterName = "@newWomensCut";
      womensCut.Value = newWomensCut;
      cmd.Parameters.Add(womensCut);

      cmd.ExecuteNonQuery();
      _womensCut = newWomensCut;
      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
    }

    public void UpdateStylistMensCut(int newMensCut)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE stylists SET mens_cut = @newMensCut WHERE id = @searchId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = _id;
      cmd.Parameters.Add(searchId);

      MySqlParameter mensCut = new MySqlParameter();
      mensCut.ParameterName = "@newMensCut";
      mensCut.Value = newMensCut;
      cmd.Parameters.Add(mensCut);

      cmd.ExecuteNonQuery();
      _mensCut = newMensCut;
      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
    }

    public void UpdateStylist(string newFirstName, string newLastName, int newWomensCut, int newMensCut)
    {
      this.UpdateStylistFirstName(newFirstName);
      this.UpdateStylistLastName(newLastName);
      this.UpdateStylistWomensCut(newWomensCut);
      this.UpdateStylistMensCut(newMensCut);
    }

    public static void DeleteStylist (int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM stylists WHERE id = @thisId;";

      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = id;
      cmd.Parameters.Add(thisId);

      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
    }
  }
}
