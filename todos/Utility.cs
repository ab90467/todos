using System;
using System.Collections.Generic;


public sealed class Utility
{
    private static readonly Utility instance = new Utility();

    // Explicit static constructor to tell C# compiler
    // not to mark type as beforefieldinit
    static Utility()
    {
    }

    private Utility()
    {
    }

    public static Utility Instance
    {
        get
        {
            return instance;
        }
    }

    public String MyQueryString(String key)
    {
        var map = new Dictionary<string, string>();

        map.Add("fullreport",  @" 
            select
              a.description as 'todo task desc',
              d.status as 'task status',
              b.description as 'task descr',
              c.name as 'todo doer'
            from
              task as a,
              tasktype as b,
              user as c,
              taskStatus as d
            where
              a.typeID = b.id
              and a.userID = c.id
              and a.taskStatusID = d.id
            order by c.name asc
        ");
        map.Add("listusers", @"
            Select * from user order by name asc          
        ");
        string mapValue;
        if (map.TryGetValue(key, out mapValue)){
            return mapValue;
        }
        return "";
    }
 }


public class Product
{
    public static Product Instance
    {
        get; private set;
    }

    static Product()
    {
        Instance = new Product(0);
    }

    private Product(int s)
    {
        state = s;
    }
    private int state;

    public void DoSomething()
    {
        System.Console.WriteLine("I did something for the {0} time", ++state);
    }
}