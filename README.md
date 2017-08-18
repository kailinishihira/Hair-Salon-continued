# _Hair Salon_

#### _An app to organize stylists' and clients' information, 08.18.17_

#### By _**Kaili Nishihira**_

## Description

_An app which will enable the user to enter and retrieve a stylist's name and service pricing. The user will also be able to assign a stylist to a client and enter and retrieve a client's name and contact information._

|| Behavior  | Input  | Output  |
|---|---|---|---|
|| User may view a list of all stylists   | Click `View all stylists`  | All Stylists: <li>Ken Paves</li> <li>Frederic Fekkai</li>  |
|| User may view a stylist's details  | Click 'Ken Paves'  | Ken Paves <br> Women's Cut $125 <br> Men's Cut $90  |
|| User may enter a new stylist <li>Click `Add Stylist` on Index view</li> <li>View returns a form to enter the stylist's information| Enter a new stylist: <br> First Name: Chris <br> Last Name: McMillan <br> Women's Cut: 100 <br> Men's Cut: 80 <br> Click `Add Stylist`| All Stylists: <br> ... <br> Chris McMillan <br> ... |
|| User can view stylist's details. <li>Click stylist's name on All Stylists view</li>  | Click 'Chris McMillan'  | Chris McMillan <br> Women's Cut $100 <br> Men's Cut $80 <br>  |
|| User may add a new client to a specific stylist <li>In stylist's details view, click `Add Client`</li> <li>View returns a form to enter the client's information</li>  | Enter a new client for Chris McMillan: <br> First Name: Lisa <br> Last Name: Smith <br> Phone: 808-555-1234 <br> Email: lisa.smith@gmail.com | Chris McMillan Client List: <br> ... <br> Smith, Lisa <br> ... |
|| User may view a client's details  | Click on 'Smith, Lisa'  | Lisa Smith <br> Stylist: Chris McMillan <br> Phone: 808-555-1234 <br> Email: lisa.smith@gmail.com   |
|| User may update a client's name <li>Click on client's name</li> <li>View returns the client's details</li> <li>Click `Update client name`</li><li>View returns a form to update the client's name</li>  | Update details for Lisa Smith: <br> First Name: Lisa <br> Last Name: Ford </li> | Chris McMillan Client List: <br> ... <br> Ford, Lisa <br> ... |
|| User may delete a client <li>Click on client's name</li> <li>View returns the client's details</li>  | Click `Delete client`  | Confirmation page: 'Client has been deleted'  |



## Setup/Installation Requirements

* _Download and install [.NET Core 1.1 SDK](https://www.microsoft.com/net/download/core)_
* _Download and install [Mono](http://www.mono-project.com/download/)_
* _Download and install [MAMP](https://www.mamp.info/en/)_
* _Set MySQL Port to 3306_
* _Clone repository_

#### There are two options to create the database:
##### 1. In MySQL
`> CREATE DATABASE hair_salon;`<br>
`> USE hair_salon;`<br>
`> CREATE TABLE stylists (id serial PRIMARY KEY, first_name VARCHAR(255), last_name VARCHAR(255), womens_cut INT, mens_cut INT);`<br>
`> CREATE TABLE clients (id serial PRIMARY KEY, first_name VARCHAR(255), last_name VARCHAR(255), phone VARCHAR(255), email VARCHAR(255), stylist_id INT );`
##### 2. Import from the Cloned Repository
* _Click 'Open start page' in MAMP_
* _Under 'Tools', select 'phpMyAdmin'_
* _Click 'Import' tab_
* _Choose database file (from cloned repository folder)_
* _Click 'Go'_

## Technologies Used
* _C#_
* _.NET_
* _[Bootstrap](http://getbootstrap.com/getting-started/)_
* _[MySQL](https://www.mysql.com/)_

### License

Copyright (c) 2017 **_Kaili Nishihira**

*Licensed under the [MIT License](https://opensource.org/licenses/MIT)*
