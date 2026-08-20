class Exam:
    def __init__(self, id, name):
        self.id = id
        self.name =  name
        
    def conduct_exam(self):
        print("Conducted " + self.name + " Examination")
        