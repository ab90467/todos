using System;
namespace todos
{

    public class Task
    {
        public int id { get; set; }
        public int typeID { get; set; }
        public string description { get; set; }
        public int userID { get; set; }
        public int taskStatusID { get; set; }
    }

}
