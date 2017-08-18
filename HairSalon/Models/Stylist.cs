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
  }
}
