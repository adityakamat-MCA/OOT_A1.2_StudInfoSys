#include <iostream>
#include <string>
#include <vector>
using namespace std;

class Person {
	protected:
	    string name;
	public:
	    Person(string name) { this->name = name; }
	    string getName() { return name; }
	    virtual void displayRole() = 0;
};

class University;
class School;
class Discipline;
class Program;
class Student;
class Faculty;
class Course;

class University {
	private:
	    string name;
	    string chancellor;
	    string viceChancellor;
	    School* school;
	public:
	    University(string name, string chancellor, string viceChancellor) {
	        this->name = name;
	        this->chancellor = chancellor;
	        this->viceChancellor = viceChancellor;
	        school = nullptr;
	    }
    void setSchool(School* school) { this->school = school; }
    string getName() { return name; }
    void registerStudent(Student* student);
};

class School {
private:
    int schoolId;
    string name;
    string dean;
    string viceDean;
    University* university;
    Discipline* discipline;

public:
    School(int schoolId, string name, string dean, string viceDean, University* university) {
        this->schoolId = schoolId;
        this->name = name;
        this->dean = dean;
        this->viceDean = viceDean;
        this->university = university;
        discipline = nullptr;
    }
    void setDiscipline(Discipline* discipline) { this->discipline = discipline; }
    string getName() { return name; }
};
class Discipline {
	private:
	    int disciplineId;
	    string name;
	    School* school;
	    Program* program;
	
	public:
	    Discipline(int disciplineId, string name, School* school) {
	        this->disciplineId = disciplineId;
	        this->name = name;
	        this->school = school;
		        program = nullptr;
	    }
	    void setProgram(Program* program) { this->program = program; }
	    string getName() { return name;}
};
class Program {
	private:
	    int programId;
	    string name;
	    string degree;
	    int duration;
	
	    Discipline* discipline;
	    Student* student;
	    Faculty* faculty;
	    Course* course;
	
	public:
	    Program(int programId, string name, string degree, int duration, Discipline* discipline) {
	
	        this->programId = programId;
	        this->name = name;
	        this->degree = degree;
	        this->duration = duration;
	        this->discipline = discipline;
	
	        student = nullptr;
	        faculty = nullptr;
	        course = nullptr;
	    }
	    void setStudent(Student* student) { this->student = student; }
	    void setFaculty(Faculty* faculty) { this->faculty = faculty; }
	    void setCourse(Course* course) { this->course = course; }
	    string getName() { return name; }
};

class Course {
	private:
	    string courseCode;
	    string courseName;
	    int credits;
	    Faculty* faculty;
	    Program* program;
	
	public:
	    Course(string courseCode, string courseName, int credits, Faculty* faculty, Program* program) {
	        this->courseCode = courseCode;
	        this->courseName = courseName;
	        this->credits = credits;
	        this->faculty = faculty;
	        this->program = program;
	    }
	
	    string getCourseName() { return courseName; }
};

class Student : public Person {
private:
    int studentId;
    int semester;
    double cgpa;
    Program* program;

    vector<Course*> courses;

public:
    Student(int studentId, string name, int semester, double cgpa, Program* program) : Person(name) {
        this->studentId = studentId;
        this->semester = semester;
        this->cgpa = cgpa;
        this->program = program;
    }
    void displayRole() override {
        cout << getName() << " is a Student" << endl;
    }
    void enrollCourse(Course* course) {
        courses.push_back(course);
        cout << getName() << " enrolled in " << course->getCourseName() << endl;
    }
    void dropCourse(Course* course) {
        for (int i = 0; i < courses.size(); i++) {
            if (courses[i] == course) {
                cout << getName() << " dropped " << course->getCourseName() << endl;
                courses.erase(courses.begin() + i);
                return;
            }
        }
        cout << "Course not found." << endl;
    }
    void viewCourses() {
        cout << "\nCourses of " << getName() << ":" << endl;
        for (Course* course : courses) {
            cout << "- " << course->getCourseName() << endl;
        }
    }
    void viewCGPA() {
        cout << "CGPA: " << cgpa << endl;
    }
};

class Faculty : public Person {
private:
    int facultyId;
    string email;
    Program* program;

public:
    Faculty(int facultyId, string name, string email, Program* program) : Person(name) {
        this->facultyId = facultyId;
        this->email = email;
        this->program = program;
    }
    void displayRole() override { cout << getName() << " is a Faculty Member" << endl; }
    void markAttendance(Student* student) { cout << "Attendance marked for " << student->getName() << endl; }
    void assignGrade(Student* student, string grade) { cout << "Grade " << grade << " assigned to " << student->getName() << endl;
    }
};

void University::registerStudent(Student* student) {cout << student->getName() << " registered successfully in " << name << endl;}
int main() {
    University gu("Goa University", "Mr. ABC",  "Mr. XYZ");
    School gbs(1,"Goa Business School","Mr. MNO","Mr. PQR",&gu); gu.setSchool(&gbs);
    Discipline cst(1,"Computer Science and Technology",&gbs); gbs.setDiscipline(&cst);
    Program mca(101,"MCA","Master",2,&cst); cst.setProgram(&mca);
    Faculty teacher(1001,"Mr. Hanumant Redkar","hhr@unigoa.ac.in",&mca); mca.setFaculty(&teacher);
    Course java("MCA101","Object Oriented Programming",4,&teacher,&mca);mca.setCourse(&java);
    Course dbms("MCA102","Database Management System",4,&teacher,&mca); mca.setCourse(&dbms);

    vector<Student*> students;
    int n;
    cout << "Enter number of students: ";
    cin >> n;
    cin.ignore(1000, '\n');
    for (int i = 0; i < n; i++) {
        int id;
        string name;
        int semester;
        double cgpa;
        cout << "\nEnter details of Student " << i + 1 << endl; 
        cout << "Name: ";
        getline(cin, name);
        cout << "Student ID: ";
        cin >> id;
        cout << "Semester: ";
        cin >> semester;
        cout << "CGPA: ";
        cin >> cgpa;

        Student* student = new Student(id,name,semester,cgpa,&mca);
        students.push_back(student);
        mca.setStudent(student);
        gu.registerStudent(student);

        student->enrollCourse(&java);
        student->enrollCourse(&dbms);

        teacher.markAttendance(student);
        teacher.assignGrade(student, "A");
        teacher.displayRole();

        student->viewCGPA();
        student->viewCourses();

        cout << "\n--- Dropping a Course ---" << endl;
        student->dropCourse(&dbms);
        cout << "\n--- Courses after Dropping ---"<< endl;
        student->viewCourses();
        cout << "\n--- Enrolling in a New Course ---" << endl;
        Course ai("MCA103","Artificial Intelligence",4,&teacher,&mca);
        student->enrollCourse(&ai);
        cout << "\n--- Courses after Enrolling in a New Course ---"<< endl;
        student->viewCourses();
        cout << "\n";
    }
}