#mySQL for TODO
##installation MAC

$brew info mysql


## run MAC

To have launchd start mysql now and restart at login:

  $brew services start mysql

Or, if you don't want/need a background service you can just run:

  $mysql.server start
  
## Connect & create

$mysql -uroot

mysql> CREATE USER 'newuser'@'localhost' IDENTIFIED BY 'password';


mysql> GRANT ALL PRIVILEGES ON * . * TO 'newuser'@'localhost';

mysql> CREATE DATABASE todos;

## Backup

$mysqldump -uroot --quick todos | gzip >mySQL_DB_todos.backup.gz;

## C#  

###NuGet

- Newtonsoft.Json
- MySql.Data

### Misc

- DB admin : Sequel-pro-1.1.2
- https://www.codeproject.com/Articles/43438/Connect-C-to-MySQL
- https://nodogmablog.bryanhogan.net/2016/01/asp-net-5-web-api-controller-with-multiple-get-methods/