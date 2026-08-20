class Program:
    def __init__(self, id, name):
        self.id = id
        self.name= name
        
    def add_course(self, course):
        print(course.name + "added to " + self.name)