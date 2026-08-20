class Result:
    def __init__(self, result_id, grade):
        self.result_id = result_id
        self.grade = grade

    def display_result(self):
        print("Grade: " + self.grade)