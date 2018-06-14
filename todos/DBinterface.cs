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
        // get correct SQL
    private String MyQueryString(String key)
    {
        var map = new Dictionary<string, string>();

        map.Add("listtasks", @" 
            select
              a.description as 'todo task desc',
              d.status as 'task status',
              b.description as 'task descr',
              c.name as 'todo doer'
            from
              tasks as a,
              tasktype as b,
              users as c,
              taskStatus as d
            where
              a.typeID = b.id
              and a.userID = c.id
              and a.taskStatusID = d.id
            order by c.name asc
        ");
        map.Add("listusers", @"
            Select * from users order by name asc          
        ");
        map.Add("liststatus", @"
            Select * from taskStatus order by status asc          
        ");
        map.Add("listtasktype", @"
            Select * from taskType order by value asc          
        ");
        string mapValue;
        if (map.TryGetValue(key, out mapValue))
        {
            return mapValue;
        }
        return "";
    }    



        // public DB fn ::

    /*public Stream ConvertDataSetToJson(DataSet ds) { 
        try { 
            byte[] resultBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(ds, Formatting.Indented)); 
            //WebOperationContext.Current.OutgoingResponse.ContentType = "text/plain"; 
            return new MemoryStream(resultBytes); 
        } catch (Exception ex) { throw ex; } 
    }*/

    //Get statement

    public String Get(String dataRequest)
    {
        try
        {   string query = MyQueryString(dataRequest);
                if (query.Length == 0)
                {
                    var error = "{ \"error\" : \"not valid query\"}";
                    return error;

                }
                
            MySqlDataAdapter ma = new MySqlDataAdapter(query, conn);
            DataSet DS = new DataSet();
            ma.Fill(DS);
            return JsonConvert.SerializeObject(DS.Tables[0], Formatting.Indented);
            //return DS.Tables[0];
        }
        catch (MySqlException e)
        {
            throw new Exception(e.Message);
        }
    }

    /* public JsonResult test()
     {
         var result = new JsonResult();
         result.Data = new
         {
             id = 1
         };
         result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
         return result;
     }
     */

    //Get statement
    public String __Get(String query)
    {



        if (OpenConnection() == true)
        {
            List<string[]> list = new List<string[]>();

            String retVal = "";

            MySqlCommand cmd = new MySqlCommand(query, conn);
            //Create a data reader and Execute the command
            MySqlDataReader dataReader = cmd.ExecuteReader();

            //Read the data and store them in the list
            while (dataReader.Read())
            {
                String dataRecord = "";
                //make string[] the size of the row being returned
                string[] row = new string[dataReader.FieldCount];
                //fill sting[] with row data
                for (int i = 0; i < row.Length; i++)
                {

                    row[i] = dataReader[i].ToString();
                    //System.Diagnostics.Trace.WriteLine("data " + row[i]);
                    dataRecord += " " + row[i].ToString();
                    //add string[] to return list
                    list.Add(row);

                }
                log("record :" + dataRecord);
                list.Add(row);
                retVal += "\n" + dataRecord;
            }
            CloseConnection();
            return JsonConvert.SerializeObject(list, Formatting.Indented);

            //return retVal;
        }
        return "null";

    }

    //Insert statement
    public void Insert()
    {
    }

    //Update statement
    public void Update()
    {
    }

    //Delete statement
    public void Delete()
    {
    }


    //Count statement
    public int Count()
    {
        return 0;
    }
}

}