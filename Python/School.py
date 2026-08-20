class School:
    def __init__(self, id, name):
        self.id = id
        self.name = name

    def offer_program(self, program):
        print(self.name + " offers " + program.name)