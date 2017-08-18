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
      cmd.CommandText = @"SELECT * FROM stylists ORDER BY last_name ASC;";
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


  }
}
