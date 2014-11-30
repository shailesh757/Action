var serviceURL = "http://localhost:8081/api/";

var employees;

$('#employeeListPage').bind('pageinit', function(event) {
	getEmployeeList();
});

function getEmployeeList() {
	$.getJSON('http://localhost:8081/api/Property', function(data) {
		$('#employeeList li').remove();
		employees = data;
		$.each(employees, function(index, employee) {
			$('#employeeList').append('<li><a href="employeedetails.html?id=' + employee.Id + '">' +
					'<h4>' + employee.Feature + ' ' + employee.Description + '</h4>' +
					'<p>' + employee.Location + '</p>' +
					'<span class="ui-li-count">' + employee.Id + '</span></a></li>');
		});
		$('#employeeList').listview('refresh');
	});
}