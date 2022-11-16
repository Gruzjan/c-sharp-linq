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
            zadanie1();
        }

        static void zadanie1()
        {
            var db = ApplicationDbContext.Instance;

            var klasy = db.Students
                .GroupBy(k => k.Class)
                .Select(s => new
                {
                    Dziewczyny = s.Count(c => c.FirstName.EndsWith("a")),
                    Wszyscy = s.Count(),
                    Klasa = s.Key
                }).Where(x => x.Dziewczyny > x.Wszyscy / 2)
                .Select(s => s.Klasa);

            Console.WriteLine($"Klasy: { String.Join(", ", klasy)}");

        }
    }
}