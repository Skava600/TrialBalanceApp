# Data Manipulator
## Task
1. Analyze the structure of the excel file "ОСВ для тренинга". Develop a scheme (several
tables) in a DBMS in which it will be most convenient to store data from files of such
format
2. Using the web interface, implement
a. Loading data from an excel file of this format into the DBMS
b. Viewing a list of downloaded files
c. Displaying data from the DBMS by visual analogy with an excel file for each
from uploaded files

3. Implement saving data from a web server to a file in a convenient form
 ractional numbers

## Configuring

 - In appsettings.json you can configure the apllication  - specify the connectionstring and loggingsetting
 
After specifying connectionstring to configure database use commands:
```sh
> add-migration init
> update-database
```
## Developing of database
 The following objects was created:
 1) Bank
 2) AccountClass
 3) Account
 4) Balance
 5) Revenue
 6) Additional table for logging - Serilog
 
## Used additional nugget packages
 - Npgsql.EntityFrameworkCore.PostgreSQL
 - Npgsql.EntityFrameworkCore.PostgreSQL.Design
 - Serilog.Enrichers.Environment
 - Serilog.Extensions.Logging
 - Serilog.Sinks.Console
 - Serilog.Sinks.Postgresql.Alternative
 - Microsoft.EntityFrameworkCore.Tools
 
For reading of excel file i used IronXL.Excel  package.
 
## Demonstration of work

![alt text](https://github.com/Skava600/TrialBalanceApp/blob/master/Screens/BankTable.png?raw=true)
**Bank table** 

![alt text](https://github.com/Skava600/TrialBalanceApp/blob/master/Screens/AccountClassTable.png?raw=true)
**Account class table**

![alt text](https://github.com/Skava600/TrialBalanceApp/blob/master/Screens/AccountTable.png?raw=true)
**Account table**

![alt text](https://github.com/Skava600/TrialBalanceApp/blob/master/Screens/BalanceTable.png?raw=true)
**Balance table**

![alt text](https://github.com/Skava600/TrialBalanceApp/blob/master/Screens/RevenueTable.png?raw=true)
**Revenue table**

![alt text](https://github.com/Skava600/TrialBalanceApp/blob/master/Screens/LoggingTable.png?raw=true)
**Logging table**

![alt text](https://github.com/Skava600/TrialBalanceApp/blob/master/Screens/LoggingFile.png?raw=true)
**Logging file**

![alt text](https://github.com/Skava600/TrialBalanceApp/blob/master/Screens/HomePage.png?raw=true)
**Home page**

![alt text](https://github.com/Skava600/TrialBalanceApp/blob/master/Screens/BankPage.png?raw=true)
**Banks page**

![alt text](https://github.com/Skava600/TrialBalanceApp/blob/master/Screens/ScreenTable1.png?raw=true)
**Trial balance table**

![alt text](https://github.com/Skava600/TrialBalanceApp/blob/master/Screens/JsonSavedFile.png?raw=true)
**Trial balance export to json file**




