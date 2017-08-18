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


  }
}
