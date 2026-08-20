from person import Person

class Student(Person):

    def __init__(self, id, name, address, phone):
        super().__init__(id, name)
        self.__address = address
        self.__phone = phone

    def get_address(self):
        return self.__address

    def get_phone(self):
        return self.__phone

    def set_address(self, address):
        self.__address = address

    def set_phone(self, phone):
        self.__phone = phone

    # Polymorphism
    def show_role(self):
        print(self.get_name() + " is a Student")

    def enroll(self, program):
        print(self.get_name() + " enrolled in " + program.name)