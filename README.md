## Specs

| Behavior - Plain English              | Sample Input                                                                             | Sample Output                                                             | Description of Spec                                                                     |
|---------------------------------------|------------------------------------------------------------------------------------------|---------------------------------------------------------------------------|-----------------------------------------------------------------------------------------|
| Saves a stylist                       | new Stylist Lisa()                                                                       | Stylists: Lisa                                                            | Checks to see if the program can save and return a single stylist                       |
| Deletes a stylist                     | Stylist.Remove(Lisa)                                                                     | Stylists: { }                                                             | Checks to see if we can remove the stylist that we added                                |
| Lists all stylists                    | new Stylist Lisa(); new Stylist Gregory()                                                | Stylists: Lisa, Gregory                                                   | Checks to see if we can list at least two stylists                                      |
| Saves a client                        | new Client Susie()                                                                       | Clients: Susie                                                            | Checks to see if the program can save and return a single client                        |
| Deletes a client                      | Client.Remove(Susie)                                                                     | Clients: { }                                                              | Checks to see if we can remove the stylist that we added                                |
| See a stylist's details               |  new Stylist Lisa(MTuWTh, Color and Style) Lisa.Details()                                | Stylist Lisa Availability: MTuWTh Services: Color and Style               | Checks to see if the added stylist's details are correctly associated with that stylist |
| See a client's details                | new Client Susie(Monday, 11:30) Susie.Details()                                          | Client Susie Appointment Day: Monday Appointment Time: 11:30              | Checks to see if the added client's details are correctly associated with that client   |
| Update a stylists details             | new Stylist Lisa(MTuWTh, Color and Style) Lisa.Update(MTuWF, Color and Style)            | Stylist Lisa Availability: MTuWF Services:Color and Style                 | Checks to see if the added stylist's details are updated correctly                      |
| Update a client's details             | new Client Susie(Monday, 11:30) Susie.Update(Tuesday, 11:30)                             | Client Susie Appointment Day: Tuesday Appointment Time: 11:30             | Checks to see if the added client's details are updated correctly                       |
| See a list of all a stylist's clients | new Stylist Lisa(MTuWF, Color and Style) new Client Susie(Tuesday, 11:30) Lisa.Details() | Stylist Lisa Availability: MTuWF Services: Color and Style Clients: Susie | Checks to see if a client is correctly associated with a stylist                        |


## SQL Database Set-Up Instructions

In SQLCMD:<br>
\>CREATE DATABASE hair_salon <br>
\>GO <br>
\>USE hair_salon <br>
\>GO <br>
\>CREATE TABLE stylists (id INT IDENTITY(1,1), name VARCHAR(255), availability VARCHAR(255), services VARCHAR(255)); <br>
\>CREATE TABLE clients (id INT IDENTITY(1,1), name VARCHAR(255), stylist_id INT, appointment_day VARCHAR(255), appointment_time VARCHAR(255)); <br>
\>GO
