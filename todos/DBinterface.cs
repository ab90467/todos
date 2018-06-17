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



namespace todos{
class DBinterface
{
    private MySqlConnection conn;

        public object Select { get; private set; }


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

        var mainListQuery = @" 
            select
              a.id,
              b.value as 'tasktype',
              d.status as 'status',
              a.description as 'descr',
              c.name as 'user',
              c.id as 'userId'
            from
              tasks as a,
              tasktype as b,
              users as c,
              taskStatus as d
            where
              a.typeID = b.id
              and a.userID = c.id
              and a.taskStatusID = d.id";
        
        map.Add("_mainQuery_", mainListQuery);
        map.Add("tasks", mainListQuery + " order by d.status desc");
        map.Add("users", "Select id, name from users order by name asc");
        map.Add("status", "Select id, status as 'name' from taskStatus order by status desc");
        map.Add("tasktypes", "Select id, value as 'name' from taskType order by value asc");

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



    //Get generic
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

    // TASK

    //Get 
    public String GetTaskWithSpesificId(String Id)
    {
        string query = "select * from tasks where id=" + Id;
        return getDBdata(query);
    }

    public String GetTaskForSpesificUser(String Id)
    {
        string query = MyQueryString("_mainQuery_") + " and c.id =" + Id;
            log(query);
        return getDBdata(query);
    }

    //Insert 
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



    //Update 
    public String UpdateTask(Task updatedTask)
    {
        if (OpenConnection() == true)
        {
            
            string query = "update tasks set typeID='"+updatedTask.typeID+"',userID='" + updatedTask.userID + "',taskStatusID='" + updatedTask.taskStatusID + "', description='"+updatedTask.description+"' where id='" + updatedTask.id +"'";
            log(query);
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            return query;
        }
        return "error";
            
    }

    //USER

    //GET spesific user
    public String GetUser(String userid)
    {
        var query = "select * from users where id = " + userid;
        return getDBdata(query);
    }

    // save new user
    public String saveUser(User newUser)
    {

        if (OpenConnection() == true)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "INSERT INTO users(name,email,skills) VALUES(?name,?email,?skills)";
            cmd.Parameters.Add("?name", MySqlDbType.VarChar).Value = newUser.name;
            cmd.Parameters.Add("?email", MySqlDbType.VarChar).Value = newUser.email;
            cmd.Parameters.Add("?skills", MySqlDbType.VarChar).Value = newUser.skills;
            cmd.ExecuteNonQuery();
            return cmd.CommandText.ToString();
        }
        return "error";
    }
    //update
    public String UpdateUser(User updatedUser)
    {
        if (OpenConnection() == true)
        {
            string query = "update users set name='"+updatedUser.name+"',email='" + updatedUser.email + "',skills='" + updatedUser.skills + "' where id='" + updatedUser.id +"'";
            //log(query);
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            return query;
        }
        return "error";

    } 
 }
}