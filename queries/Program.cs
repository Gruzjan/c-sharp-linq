using queries.Data;
using queries.Data.Tables;
using queries.Properties;

namespace queries
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var db = ApplicationDbContext.Instance;

            if(db.Subjects.Count() == 0)
            {
                Console.WriteLine("Adding subjects");
                var lines = Resources.przedmioty
                    .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                    .Skip(1)
                    .Select(x => x.Split('\t'));

                var subjects = new List<Subject>();
                foreach (var l in lines)
                {
                    subjects.Add(new Subject()
                    {
                        SubjectId = Int32.Parse(l[0]),
                        Name = l[1],

                    });
                }

                db.Subjects.AddRange(subjects);
                db.SaveChanges();
                Console.WriteLine("Subjects added!");
            }

            if (db.Students.Count() == 0)
            {
                Console.WriteLine("Adding students");
                var lines = Resources.uczniowie
                    .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                    .Skip(1)
                    .Select(x => x.Split('\t'));

                var students = new List<Student>();
                foreach (var l in lines)
                {
                    students.Add(new Student()
                    {
                        StudentId = l[0],
                        FirstName = l[1],
                        LastName = l[2],
                        Class = l[3],
                    });
                }

                db.Students.AddRange(students);
                db.SaveChanges();
                Console.WriteLine("Students added!");
            }

            if (db.Grades.Count() == 0)
            {
                Console.WriteLine("Adding grades");
                var lines = Resources.oceny
                    .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                    .Skip(1)
                    .Select(x => x.Split('\t'));

                var grades = new List<Grade>();
                foreach (var l in lines)
                {
                    grades.Add(new Grade()
                    {
                        GradeId = Int32.Parse(l[0]),
                        Date = DateTime.Parse(l[1]),
                        StudentId = l[2],
                        SubjectId = Int32.Parse(l[3]),
                        Value = Int32.Parse(l[4]),
                    });
                }

                db.Grades.AddRange(grades);
                db.SaveChanges();
                Console.WriteLine("Grades added!");
            }

            Console.WriteLine("Migration ended");
            //zadanie1();
            //zadanie2();
            //zadanie3();
            zadanie4();
            //zadanie3();
        }

        static void zadanie1()
        {
            var db = ApplicationDbContext.Instance;

            var classes = db.Students
                .GroupBy(s => s.Class)
                .Select(s => new
                {
                    girls = s.Count(s => s.FirstName.EndsWith("a")),
                    everyone = s.Count(),
                    Class = s.Key
                }).Where(s => s.girls > s.everyone / 2)
                .Select(s => s.Class);

            Console.WriteLine($"Classes: {String.Join(", ", classes)}");
        }

        static void zadanie2()
        {
            var db = ApplicationDbContext.Instance;

            var dates = db.Grades
                .GroupBy(s => s.Date)
                .Select(s => new
                {
                    date = s.Key,
                    ones = s.Count(s => s.Value == 1)
                }).Where(s => s.ones > 10)
                .Select(s => s.date);

            Console.WriteLine($"Dates: {String.Join(", ", dates)}");
        }
        static void zadanie3()
        {
            var db = ApplicationDbContext.Instance;

            var averages = db.Grades
                .Where(s => s.Subject.Name == "j.polski")
                .Where(s => s.Student.Class.Contains("IV"))
                .Select(s => s.Value)
                .Average();
            Console.WriteLine($"Avg: {Math.Round(averages, 2)}");
        }
        //something is wrong here man
        static void zadanie4()
        {
            var db = ApplicationDbContext.Instance;

            var fives = db.Grades
                .GroupBy(x => x.SubjectId)
                .Select(x => new
                {
                    count = x.Count(x => x.Value == 5),
                    Subject = x.Key,
                    Month = x.Select(x => x.Date.Month),
                })
                .GroupBy(x => x.Month);

            Console.WriteLine(String.Join(", ", fives));
        }
        static void zadanie5()
        {
            var db = ApplicationDbContext.Instance;

            var students = db.Grades.Where(x => x.Student.Class.Equals("II A"))
                .Where(x => x.Subject.Name == "sieci komputerowe")
                .Select(x => x.StudentId).ToList();
            var studentsWithNoGrades = db.Students.Where(x => x.Class.Equals("II A"))
                .Where(x => !students.Contains(x.StudentId))
                .Select(x => $"{x.FirstName} {x.LastName}");

            Console.WriteLine(String.Join(", ", studentsWithNoGrades));
        }


    }
}