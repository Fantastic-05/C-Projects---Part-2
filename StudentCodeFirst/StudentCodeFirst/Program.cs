using System;
using System.Linq;

namespace StudentCodeFirst
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new StudentContext())
            {
                var student = new Student()
                {
                    StudentName = "Hiba"
                };

                context.Students.Add(student);
                context.SaveChanges();

                var query = from s in context.Students
                            select s;

                foreach (var item in query)
                {
                    Console.WriteLine(item.StudentName);
                }
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}