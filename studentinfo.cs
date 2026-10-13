using System;
using System.Collections.Generic;

abstract class Person {
    protected string name;
    public Person(string name) { this.name = name; }
    public string GetName() { return name; }
    public abstract void DisplayRole();
}

class University {
    private string name;
    private string chancellor;
    private string viceChancellor;
    private School school;

    public University(string name, string chancellor, string viceChancellor) {
        this.name = name;
        this.chancellor = chancellor;
        this.viceChancellor = viceChancellor;
        school = null;
    }

    public void SetSchool(School school)
    {
        this.school = school;
    }

    public string GetName()
    {
        return name;
    }

    public void RegisterStudent(Student student)
    {
        Console.WriteLine(
            student.GetName() +
            " registered successfully in " + name);
    }
}

class School
{
    private int schoolId;
    private string name;
    private string dean;
    private string viceDean;
    private University university;
    private Discipline discipline;

    public School(int schoolId, string name, string dean,string viceDean, University university)
    {
        this.schoolId = schoolId;
        this.name = name;
        this.dean = dean;
        this.viceDean = viceDean;
        this.university = university;
        discipline = null;
    }

    public void SetDiscipline(Discipline discipline)
    {
        this.discipline = discipline;
    }

    public string GetName()
    {
        return name;
    }
}

class Discipline
{
    private int disciplineId;
    private string name;
    private School school;
    private Program program;

    public Discipline(int disciplineId, string name, School school)
    {
        this.disciplineId = disciplineId;
        this.name = name;
        this.school = school;
        program = null;
    }

    public void SetProgram(Program program)
    {
        this.program = program;
    }

    public string GetName()
    {
        return name;
    }
}

class Program
{
    private int programId;
    private string name;
    private string degree;
    private int duration;

    private Discipline discipline;
    private Student student;
    private Faculty faculty;
    private Course course;

    public Program(int programId, string name, string degree,int duration, Discipline discipline)
    {
        this.programId = programId;
        this.name = name;
        this.degree = degree;
        this.duration = duration;
        this.discipline = discipline;

        student = null;
        faculty = null;
        course = null;
    }

    public void SetStudent(Student student)
    {
        this.student = student;
    }

    public void SetFaculty(Faculty faculty)
    {
        this.faculty = faculty;
    }

    public void SetCourse(Course course)
    {
        this.course = course;
    }

    public string GetName()
    {
        return name;
    }
}

class Course
{
    private string courseCode;
    private string courseName;
    private int credits;
    private Faculty faculty;
    private Program program;

    public Course(string courseCode, string courseName, int credits, Faculty faculty, Program program)
    {
        this.courseCode = courseCode;
        this.courseName = courseName;
        this.credits = credits;
        this.faculty = faculty;
        this.program = program;
    }

    public string GetCourseName()
    {
        return courseName;
    }
}

class Student : Person
{
    private int studentId;
    private int semester;
    private double cgpa;
    private Program program;

    private List<Course> courses = new List<Course>();

    public Student(int studentId, string name, int semester, double cgpa, Program program) : base(name)
    {
        this.studentId = studentId;
        this.semester = semester;
        this.cgpa = cgpa;
        this.program = program;
    }

    public override void DisplayRole()
    {
        Console.WriteLine(GetName() + " is a Student");
    }

    public void EnrollCourse(Course course)
    {
        courses.Add(course);
        Console.WriteLine(GetName() + " enrolled in " + course.GetCourseName());
    }

    public void DropCourse(Course course)
    {
        if (courses.Remove(course))
        {
            Console.WriteLine(GetName() + " dropped " + course.GetCourseName());
        }
        else
        {
            Console.WriteLine("Course not found.");
        }
    }

    public void ViewCourses()
    {
        Console.WriteLine("\nCourses of " + GetName() + ":");
        foreach (Course course in courses)
        {
            Console.WriteLine("- " + course.GetCourseName());
        }
    }

    public void ViewCGPA()
    {
        Console.WriteLine("CGPA: " + cgpa);
    }
}

class Faculty : Person
{
    private int facultyId;
    private string email;
    private Program program;

    public Faculty(int facultyId, string name, string email, Program program) : base(name)
    {
        this.facultyId = facultyId;
        this.email = email;
        this.program = program;
    }

    public override void DisplayRole()
    {
        Console.WriteLine(GetName() + " is a Faculty Member");
    }

    public void MarkAttendance(Student student)
    {
        Console.WriteLine( "Attendance marked for " + student.GetName());
    }

    public void AssignGrade(Student student, string grade)
    {
        Console.WriteLine( "Grade " + grade + " assigned to " + student.GetName());
    }
}

class UniversityManagement
{
    static void Main(string[] args)
    {
        University gu = new University("Goa University", "Mr. ABC", "Mr. XYZ");
        School gbs = new School(1, "Goa Business School", "Mr. MNO","Mr. PQR", gu);
        gu.SetSchool(gbs);
        Discipline cst = new Discipline(1, "Computer Science and Technology", gbs);
        gbs.SetDiscipline(cst);
        Program mca = new Program(101, "MCA", "Master", 2, cst);
        cst.SetProgram(mca);
        Faculty teacher = new Faculty(1001, "Mr. Hanumant Redkar","hhr@unigoa.ac.in", mca);
        mca.SetFaculty(teacher);
        Course java = new Course("MCA101", "Object Oriented Programming",4, teacher, mca);
        mca.SetCourse(java);
        Course dbms = new Course("MCA102", "Database Management System",4, teacher, mca);
        mca.SetCourse(dbms);
        List<Student> students = new List<Student>();

        Console.Write("Enter number of students: ");
        int n = int.Parse(Console.ReadLine());
        for (int i = 0; i < n; i++)
        {
            Console.WriteLine("\nEnter details of Student " + (i + 1));
            Console.Write("Name: ");
            string name = Console.ReadLine();
            Console.Write("Student ID: ");
            int id = int.Parse(Console.ReadLine());
            Console.Write("Semester: ");
            int semester = int.Parse(Console.ReadLine());
            Console.Write("CGPA: ");
            double cgpa = double.Parse(Console.ReadLine());

            Student student = new Student(id, name, semester, cgpa, mca);
            students.Add(student);
            mca.SetStudent(student);
            gu.RegisterStudent(student);
            student.EnrollCourse(java);
            student.EnrollCourse(dbms);
            teacher.MarkAttendance(student);
            teacher.AssignGrade(student, "A");
            teacher.DisplayRole();
            student.ViewCGPA();
            student.ViewCourses();
            Console.WriteLine("\n--- Dropping a Course ---");
            student.DropCourse(dbms);
            Console.WriteLine("\n--- Courses after Dropping ---");
            student.ViewCourses();
            Console.WriteLine("\n--- Enrolling in a New Course ---");

            Course ai = new Course("MCA103", "Artificial Intelligence", 4, teacher, mca);
            student.EnrollCourse(ai);
            Console.WriteLine("\n--- Courses after Enrolling in a New Course ---");
            student.ViewCourses();
            Console.WriteLine();
        }
    }
}