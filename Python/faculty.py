from person import Person

class Faculty(Person):

    def __init__(self, id, name, department):
        super().__init__(id, name)
        self.__department = department

    def get_department(self):
        return self.__department

    def set_department(self, department):
        self.__department = department
        
    def show_role(self):
        print(self.get_name() + " is a Faculty Member")

    def teach(self, course):
        print(self.get_name() + " is teaching " + course.name)      