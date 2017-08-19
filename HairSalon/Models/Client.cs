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

    public static Client Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM clients WHERE id = (@searchId);";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int ClientId = 0;
      string ClientFirstName = "";
      string ClientLastName = "";
      string ClientPhone = "";
      string ClientEmail = "";
      int ClientStylistId = 0;

      while(rdr.Read())
      {
        ClientId = rdr.GetInt32(0);
        ClientFirstName = rdr.GetString(1);
        ClientLastName = rdr.GetString(2);
        ClientPhone = rdr.GetString(3);
        ClientEmail = rdr.GetString(4);
        ClientStylistId = rdr.GetInt32(5);
      }
      Client newClient = new Client(ClientFirstName, ClientLastName, ClientPhone, ClientEmail, ClientStylistId, ClientId);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return newClient;
    }

    public void UpdateClientLastName(string newLastName)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE clients SET last_name = @newLastName WHERE id = @searchId;";

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

    public void UpdateClientFirstName(string newFirstName)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE clients SET first_name = @newFirstName WHERE id = @searchId;";

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

    public void UpdateClientPhone(string newPhone)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE clients SET phone = @newPhone WHERE id = @searchId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = _id;
      cmd.Parameters.Add(searchId);

      MySqlParameter phone = new MySqlParameter();
      phone.ParameterName = "@newPhone";
      phone.Value = newPhone;
      cmd.Parameters.Add(phone);

      cmd.ExecuteNonQuery();
      _phone = newPhone;
      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
    }

    public void UpdateClientEmail(string newEmail)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE clients SET email = @newEmail WHERE id = @searchId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = _id;
      cmd.Parameters.Add(searchId);

      MySqlParameter email = new MySqlParameter();
      email.ParameterName = "@newEmail";
      email.Value = newEmail;
      cmd.Parameters.Add(email);

      cmd.ExecuteNonQuery();
      _email = newEmail;
      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
    }

    public void UpdateClientStylistId(int newStylistId)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE clients SET stylist_id = @newStylistId WHERE id = @searchId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = _id;
      cmd.Parameters.Add(searchId);

      MySqlParameter stylistId = new MySqlParameter();
      stylistId.ParameterName = "@newStylistId";
      stylistId.Value = newStylistId;
      cmd.Parameters.Add(stylistId);

      cmd.ExecuteNonQuery();
      _stylistId = newStylistId;
      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
    }

    public void UpdateClient(string newFirstName, string newLastName, string newPhone, string newEmail)
    {
      this.UpdateClientFirstName(newFirstName);
      this.UpdateClientLastName(newLastName);
      this.UpdateClientPhone(newPhone);
      this.UpdateClientEmail(newEmail);
    }

    public static void DeleteClient(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM clients WHERE id = @thisId;";

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

    public string GetStylist()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM stylists WHERE id = @thisId;";

      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = this._stylistId;
      cmd.Parameters.Add(thisId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      string stylistFirstName = "";

      while (rdr.Read())
      {
        stylistFirstName = rdr.GetString(1);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return stylistFirstName;
    }

  }
}
