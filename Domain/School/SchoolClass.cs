using System.Collections.Generic;

namespace Domain.School
{
    public class SchoolClass : Entity<SchoolClass>
    {
       private readonly List<Student> _students = new List<Student>();

       public string Number { get; set; }

       public IReadOnlyList<Student> Students => _students;

       public SchoolClass AddStudents(params Student[] students)
       {
          _students.AddRange(students);

          return this;
       }
    }
}
