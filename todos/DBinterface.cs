using System;
using System.Data; 
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;

// https://www.codeproject.com/Articles/43438/Connect-C-to-MySQL
// https://www.digitalocean.com/community/tutorials/how-to-create-a-new-user-and-grant-permissions-in-mysql

/*
 * CREATE USER 'newuser'@'localhost' IDENTIFIED BY 'password';

To connect run:
    mysql -uroot

To have launchd start mysql now and restart at login:
  brew services start mysql
Or, if you don't want/need a background service you can just run:
  mysql.server start

 */


namespace todos{
class DBinterface
{
    private MySqlConnection conn;


    //Constructor
    public DBinterface()
    {
        log("DBinterface :: Connecting to MySQL DB");

        string connStr = "server=localhost;user=newuser;database=todos;port=3306;password=password";
        conn = new MySqlConnection(connStr);

    }
    // logging 
    private void log(String msg)
    {
        System.Diagnostics.Trace.WriteLine(msg);
    }

    //open connection to database
    private bool OpenConnection()
    {
        if (conn.State == System.Data.ConnectionState.Open)
        {
            return true;
        }

        try
        {
            conn.Open();
            return true;
        }
        catch (MySqlException ex)
        {
            log(ex.ToString());
            return false;
        }
    }

    //Close connection
    private bool CloseConnection()
    {
        try
        {
            conn.Close();
            return true;
        }
        catch (MySqlException ex)
        {
            log(ex.Message);
            return false;
        }

    }
    
    // resolve query based on key
    private String MyQueryString(String key)
    {
        var map = new Dictionary<string, string>();

        map.Add("tasks", @" 
            select
              a.id,
              a.description as 'tasktype',
              d.status as 'status',
              b.description as 'descr',
              c.name as 'user'
            from
              tasks as a,
              tasktype as b,
              users as c,
              taskStatus as d
            where
              a.typeID = b.id
              and a.userID = c.id
              and a.taskStatusID = d.id
            order by d.status desc
        ");
        map.Add("users", @"
            Select * from users order by name asc          
        ");
        map.Add("status", @"
            Select * from taskStatus order by status asc          
        ");
        map.Add("tasktypes", @"
            Select * from taskType order by value asc          
        ");
        string mapValue;
        if (map.TryGetValue(key, out mapValue))
        {
            return mapValue;
        }
        return "";
    }
    
    private String getDBdata(String query){
        try
        {   
            MySqlDataAdapter ma = new MySqlDataAdapter(query, conn);
            DataSet DS = new DataSet();
            ma.Fill(DS);
            return JsonConvert.SerializeObject(DS.Tables[0], Formatting.Indented);
        }
        catch (MySqlException e)
        {
            throw new Exception(e.Message);
        }
    }



    //Get statement
    public String Get(String dataRequest)
    {

            string query = MyQueryString(dataRequest);
            if (query.Length == 0)
            {
                var error = "{ \"error\" : \"not valid query\", \"query\" : \""+query+"\"}";
                return error;

            }
            return getDBdata(query);
    }

    //GET spesific user
    public String GetUser(String userid){
        var query = "select * from users where id = " + userid;
        return getDBdata(query);
    }



    
/*
 axios.post('/api/save/task', {
  typeID: 33,
userID: 44,
taskStatusID: 55,
  description: 'Værdens beste task'
}).then(function (response) {
    console.log(response);
  })
  .catch(function (error) {
    console.log(error);
  });
*/

    //Insert statement
    public String saveTask(Task task)
    {
        if(OpenConnection() == true){
                string query = "insert into tasks (typeID,userID,taskStatusID,description) values ({tID},{uID},{tsID},'{desc}')";
                query = query.Replace("{tID}", task.typeID.ToString()).Replace("{uID}", task.userID.ToString()).Replace("{tsID}", task.taskStatusID.ToString()).Replace("{desc}", task.description);
                log(query);
                MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            return query;
        }
        return "error";
        
    }      


    //Update statement
    public String UpdateTask(Task updatedTask)
    {
        if (OpenConnection() == true)
            {
            string query = "update task set typeID=XX,userID=XX,taskStatusID=XX,description=XX where id=" + updatedTask.id;
            log(query);
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            return query;
        }
        return "error";
            
    }
        /*
         axios.post('/api/save/user', {
            name: "king kong",
            email: "jungelen@longisland.jun",
            position: "king over the monkeys"
        }).then(function (response) {
            console.log(response);
          })
          .catch(function (error) {
            console.log(error);
          });
        */

    // save new user
    public String saveUser(User newUser){
            if (OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO users(name,email,skill) VALUES(?name,?email,?skill)";
                cmd.Parameters.Add("?name", MySqlDbType.VarChar).Value = newUser.name;
                cmd.Parameters.Add("?email", MySqlDbType.VarChar).Value = newUser.email;
                cmd.Parameters.Add("?skill", MySqlDbType.VarChar).Value = newUser.position;
                cmd.ExecuteNonQuery();
                return cmd.CommandText.ToString();
            }
            return "error";
    }
 
}

}