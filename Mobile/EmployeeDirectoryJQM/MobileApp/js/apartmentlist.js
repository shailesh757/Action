$('#apartmentpage').live('pageshow', function(event) {
	var id = getUrlVars()["id"];
	$.getJSON('http://pnf1l-cts-c-107:8081/api/Apartment/1', displayapt);
});

function displayapt(data) {
		$.getJSON('http://pnf1l-cts-c-107:8081/api/Apartment/1', function(data) {
		$('#apartmentlist li').remove();
		apartments = data;
		$.each(apartments, function(index, apartment) {
			$('#apartmentlist').append('<li><a href="employeedetails.html?id=' + apartment.Id + '">' +
					'<h4>' + apartment.Description + ' ' + apartment.Direction + '</h4>' +
					'<p>' + 'Bedroom - ' + apartment.BedRoom + ' Bathroom - ' + apartment.Bathroom + '</p>' +
					'<span class="ui-li-count">' + apartment.Id + '</span></a></li>');
		});
		$('#apartmentlist').listview('refresh');
	});	
}

function getUrlVars() {
    var vars = [], hash;
    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for(var i = 0; i < hashes.length; i++)
    {
        hash = hashes[i].split('=');
        vars.push(hash[0]);
        vars[hash[0]] = hash[1];
    }
    return vars;
}
