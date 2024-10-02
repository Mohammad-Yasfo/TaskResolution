var app = angular.module('studentApp', []);

app.controller('StudentController', ['$scope', '$http', function ($scope, $http) {
    $scope.students = [];
    $scope.subjects = [];
    $scope.selectedStudent = {};
    $scope.newStudent = { Subjects: [] };
    $scope.errorMessage = '';

    // Base URL of the REST API
    var apiBaseUrl = 'https://localhost:44371/api/Students';

    // Fetch all students
    $scope.getAllStudents = function () {
        $http.get(apiBaseUrl)
            .then(function (response) {
                $scope.students = response.data;
            }, function (error) {
                console.log(error);
                $scope.errorMessage = 'Error fetching students.';
            });
    };

    // Fetch all subjects
    $scope.getAllSubjects = function () {
        $http.get('https://localhost:44371/api/Students/Subjects')
            .then(function (response) {
                $scope.allSubjects = response.data;
            }, function (error) {
                console.log(error);
                $scope.errorMessage = 'Error fetching subjects.';
            });
    };

    // Fetch a single student by Number
    $scope.getStudentByNumber = function (number) {
        $http.get(apiBaseUrl + '/' + number)
            .then(function (response) {
                $scope.selectedStudent = response.data;
                $scope.getStudentSubjects(number);
            }, function (error) {
                console.log(error);
                $scope.errorMessage = 'Error fetching student.';
            });
    };

    // Create a new student
    $scope.createStudent = function () {
        $http.post(apiBaseUrl, $scope.newStudent)
            .then(function (response) {
                $scope.getAllStudents();
                $scope.newStudent = { Subjects: [] };
            }, function (error) {
                console.log(error);
                $scope.errorMessage = 'Error creating student.';
            });
    };

    // Update an existing student
    $scope.updateStudent = function (student) {
        $http.put(apiBaseUrl + '/' + student.Number, student)
            .then(function (response) {
                $scope.getAllStudents();
            }, function (error) {
                console.log(error);
                $scope.errorMessage = 'Error updating student.';
            });
    };

    // Delete a student
    $scope.deleteStudent = function (number) {
        $http.delete(apiBaseUrl + '/' + number)
            .then(function (response) {
                $scope.getAllStudents();
            }, function (error) {
                console.log(error);
                $scope.errorMessage = 'Error deleting student.';
            });
    };

    // Fetch all subjects for a student
    $scope.getStudentSubjects = function (number) {
        $http.get(apiBaseUrl + '/' + number + '/subjects')
            .then(function (response) {
                $scope.subjects = response.data;
            }, function (error) {
                console.log(error);
                $scope.errorMessage = 'Error fetching subjects.';
            });
    };

    // Initial load
    $scope.getAllStudents();
    $scope.getAllSubjects();
}]);
