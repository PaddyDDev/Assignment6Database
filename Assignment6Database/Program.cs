using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using NUnit.Framework;

namespace Assignment6Database
{
    public class Program
    {

        static void Main(string[] args)
        {
            //every time the main is called a new OleDbConnection, and OleDbCommand object are created.
            OleDbConnection connection;
            OleDbCommand command;
            connection = new OleDbConnection(connectionString: @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = \\fs2\17103770\Desktop\NETDatabase\Assign6DB.accdb;
            Persist Security Info = False; ");
            String start= "01/03/2018", end="31/03/2018";
            
            /*
            *   Try to connectinitially by calling Open() on the created  
            *   OleDbConnection object: connection
            */
            try
            {

                //Within the command class there is a Connection data member, 
                //so for each object it will store the connection
                connection.Open();
                Console.WriteLine("Connection Open");

                //create command object above
                command = connection.CreateCommand();
                command.Connection = connection;
               // command.CommandText = "SELECT * FROM Students";
                command.CommandType = CommandType.Text;

                Console.WriteLine("\n");
                //Calling the execute reader method on this Execute reader object
                //and returns an oledb data reader
                 Displayo(command);
                 Console.WriteLine("\n");
                 InsertInto(command, connection);
                 Console.WriteLine("\n");
                 Displayo(command);
                 Console.WriteLine("\n");
                 Update(command, connection);
                 Console.WriteLine("\n");
                 Displayo(command);
                 Console.WriteLine("\n");
                 Delete(command, connection);
                 Console.WriteLine("\n");
                 Displayo(command);
                CountPermits(command, connection);
                Console.WriteLine("\n");
                Month(command, start, end);
                Console.WriteLine("\n");
                FeesCalculation(command, start, end);
            }

            /*
             * use a catch should the connection fail and return exception object
            */
            catch (Exception ex)
            {
                Console.WriteLine("Connection Failed");
                Console.WriteLine(ex);
            }
            /*
             * if the connection is not null, close the connection
            */
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                    Console.WriteLine("Connection Closed");
                }
            }
            Console.ReadKey();
        }

        /*
         * Pass in the OleDbCommand object and the command
         */
        public static void Displayo(OleDbCommand command)
        {
            Console.WriteLine("CURRENT RECORDS");
            //sql command - note, no @ symbol needed
            command.CommandText = "SELECT * FROM Students";
            command.CommandType = CommandType.Text;
            //create OleDbDataReader object and assign it to command.ExecuteReader
            //and while there is data to be read, keep the reader open by calling Read on the reader instance
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine("Student ID: " + reader["Student_ID"] + " \n"+ "Owner: " + reader["Owner"]+" \n" + "Vehicle Model: " + reader["Vehicle_Model"]);
                Console.WriteLine("");
            }
            //close the reader when no longer needed
            reader.Close();
        }
        /*
         * Pass in the OleDbCommand and OleDbConnection objects
         * Create an SQL statement of type string to insert hard coded data
         */
        public static void InsertInto(OleDbCommand command, OleDbConnection connection)
        {
            Console.WriteLine("UPDATE RECORDS VIA INSERT");
            String Insert = @"INSERT INTO Students(Student_ID, Vehicle_Model, Registration, Owner, Apartment) VALUES ('00000006', 'Batmobile', 'BAT1', 'Bruce Wayne', 11);";
            command.CommandText = Insert;
            command.CommandType = CommandType.Text;

            command.ExecuteNonQuery();
        }
        /*
         * Pass in the OleDbCommand and OleDbConnection objects
         * Create an SQL statement of type string to update previously inserted data
         */
        public static void Update(OleDbCommand command, OleDbConnection connection)
        {
            Console.WriteLine("UPDATE RECORD");
            String Update = @"UPDATE Students SET Owner ='Jason Todd' WHERE Student_ID= '00000006'";
            command.CommandText = Update;
            command.CommandType = CommandType.Text;

            command.ExecuteNonQuery();
        }
        /*
         * Pass in the OleDbCommand and OleDbConnection objects
         * Create an SQL statement of type string to delete previously updated data
         */
        public static void Delete(OleDbCommand command, OleDbConnection connection)
        {
            Console.WriteLine("DELETE RECORDS VIA DELETE");
            String Delete = @"DELETE FROM  Students WHERE Student_ID= '00000006'";

            command.CommandText = Delete;
            command.CommandType = CommandType.Text;
            

            command.ExecuteNonQuery();
            
        }
        public static void CountPermits(OleDbCommand command, OleDbConnection connection)
        {
            int counter =0;
            command.CommandText = "SELECT * FROM Students";
            command.CommandType = CommandType.Text;
            //create OleDbDataReader object and assign it to command.ExecuteReader
            //and while there is data to be read, keep the reader open by calling Read on the reader instance
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                counter++;

            }
            Console.WriteLine("Nuber of permits: "+counter);
            //close the reader when no longer needed
            reader.Close();
        }
        public static void Month(OleDbCommand command, String start, String end)
        {
            //create two DateTime objects for the start and end of month
            DateTime startOfMonth;
            DateTime endOfMonth;
            String validUntil;
            DateTime valUntil;
            int count = 0;
            //parse the start and end taken in, and assign to start/endOfMonth
            startOfMonth =DateTime.Parse(start);
            endOfMonth = DateTime.Parse(end);

            command.CommandText = "SELECT * FROM Students";
            command.CommandType = CommandType.Text;
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {   
                //store reader valid until into validUntil variable
                validUntil=reader["Valid_Until"].ToString();
                valUntil = DateTime.Parse(validUntil);
                if(valUntil<=endOfMonth && valUntil>=startOfMonth)
                {
                    count++;
                    
                }
            }
            Console.WriteLine("Number of valid permits: "+count);
            //close the reader when no longer needed
            reader.Close();

        }
        public static int FeesCalculation(OleDbCommand command, String start, String end)
        {
            //create two DateTime objects for the start and end of month
            DateTime startOfMonth;
            DateTime endOfMonth;
            String validUntil;
            DateTime valUntil;
            //int count = 0;
            int fees = 100;
            int totalFees = 0;
            //parse the start and end taken in, and assign to start/endOfMonth
            startOfMonth = DateTime.Parse(start);
            endOfMonth = DateTime.Parse(end);

            command.CommandText = "SELECT * FROM Students";
            command.CommandType = CommandType.Text;
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                //store reader valid until into validUntil variable
                validUntil = reader["Valid_Until"].ToString();
                valUntil = DateTime.Parse(validUntil);
                if (valUntil>DateTime.Now)
                {
                    totalFees=totalFees+fees;
                }
            }
            Console.WriteLine("Total fees: " + totalFees);
            //close the reader when no longer needed
            reader.Close();
            return totalFees;
            
        }
        public static void Unique(OleDbCommand command, String start, String end)
        {

        }
        //public static int 
        //this is a further test
    }
}
