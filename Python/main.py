import School
import program
import course
import faculty
import student
import exam
import result
import person

gbs = School.School(1, "Goa Business School")
mca = program.Program(101, "MCA")
oot = course.Course(201, "Object Oriented Technology", 4)
faculty_obj = faculty.Faculty(301, "HHR", "hhr@gbs.com")
student_obj = student.Student(409, "Aditya Kamat", "Goa", "77989897779")
exam_obj = exam.Exam(1, "Semester End")
result_obj = result.Result(901, "A")
gbs.offer_program(mca)
mca.add_course(oot)
faculty_obj.teach(oot)

print("Student Name:", student_obj.get_name())
print("Student Address:", student_obj.get_address())

student_obj.set_name("Shashank")
print("Updated Name:", student_obj.get_name())

student_obj.show_role()
faculty_obj.show_role()
student_obj.enroll(mca)

p1 = student.Student(501, "Atharv Rasaikar", "Goa", "9876543210")
p2 = faculty.Faculty(601, "Mr. Hanumant Redkar", "hhr@unigoa.ac.in")

p1.show_role()
p2.show_role()

exam_obj.conduct_exam()
result_obj.display_result()