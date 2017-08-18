using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace HairSalon.Models
{
  public class Client
  {
    private int _id;
    private string _firstName;
    private string _lastName;
    private string _phone;
    private string _email;
    private int _stylistId;

    public Client(string firstName, string lastName, string phone, string email, int stylistId, int id=0)
    {
      _id = id;
      _firstName = firstName;
      _lastName = lastName;
      _phone = phone;
      _email = email;
      _stylistId = stylistId;
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

    public string GetPhone()
    {
      return _phone;
    }

    public string GetEmail()
    {
      return _email;
    }

    public int GetStylistId()
    {
      return _stylistId;
    }

    public string GetDetails()
   {
     return "ID: " +_id + ", First Name: " + _firstName + ", Last Name: " + _lastName + ", phone: " + _phone + ",  Email: " + _email + ", StylistID: " + _stylistId;
   }

   public override bool Equals(System.Object otherClient)
  {
    if(!(otherClient is Client))
    {
      return false;
    }
    else
    {
      Client newClient = (Client) otherClient;
      bool idEquality = (this.GetId() == newClient.GetId());
      bool firstNameEquality = (this.GetFirstName() == newClient.GetFirstName());
      bool lastNameEquality = (this.GetLastName() == newClient.GetLastName());
      bool phoneEquality = (this.GetPhone() == newClient.GetPhone());
      bool emailEquality = (this.GetEmail() == newClient.GetEmail());
      bool stylistIdEquality = (this.GetStylistId() == newClient.GetStylistId());
      return (idEquality && firstNameEquality && lastNameEquality && phoneEquality && emailEquality && stylistIdEquality);
    }
  }

  public override int GetHashCode()
  {
    return this.GetFirstName().GetHashCode();
  }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM clients;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static List<Client> GetAll()
    {
      List<Client> allClients = new List<Client>();
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText= @"SELECT * FROM clients ORDER BY last_name ASC;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int ClientId = rdr.GetInt32(0);
        string ClientFirstName = rdr.GetString(1);
        string ClientLastName = rdr.GetString(2);
        string ClientPhone = rdr.GetString(3);
        string ClientEmail = rdr.GetString(4);
        int StylistId = rdr.GetInt32(5);
        Client newClient = new Client(ClientFirstName, ClientLastName, ClientPhone, ClientEmail, StylistId, ClientId);
        allClients.Add(newClient);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allClients;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO clients(first_name, last_name, phone, email, stylist_id) VALUES(@firstName, @lastName, @phone, @email, @stylistId);";

      MySqlParameter firstName = new MySqlParameter();
      firstName.ParameterName = "@firstName";
      firstName.Value = this._firstName;
      cmd.Parameters.Add(firstName);

      MySqlParameter lastName = new MySqlParameter();
      lastName.ParameterName = "@lastName";
      lastName.Value = this._lastName;
      cmd.Parameters.Add(lastName);

      MySqlParameter phone = new MySqlParameter();
      phone.ParameterName = "@phone";
      phone.Value = this._phone;
      cmd.Parameters.Add(phone);

      MySqlParameter email = new MySqlParameter();
      email.ParameterName = "@email";
      email.Value = this._email;
      cmd.Parameters.Add(email);

      MySqlParameter stylistId = new MySqlParameter();
      stylistId.ParameterName = "@stylistId";
      stylistId.Value = this._stylistId;
      cmd.Parameters.Add(stylistId);

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
