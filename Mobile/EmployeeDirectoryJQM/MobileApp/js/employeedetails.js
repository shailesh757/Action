$('#detailsPage').live('pageshow', function(event) {
	var id = getUrlVars()["id"];
	$.getJSON('http://pnf1l-cts-c-107:8081/api/Tower/1', displayEmployee);
});

function displayEmployee(data) {
		$.getJSON('http://pnf1l-cts-c-107:8081/api/Tower/1', function(data) {
		$('#actionList li').remove();
		Towers = data;
		$.each(Towers, function(index, Tower) {
			$('#actionList').append('<li><a href="ApartmentList.html?id=' + Tower.Id + '">' +
					'<h4>' + Tower.TowerName + ' ' + Tower.TowerDirection + '</h4>' +
					'<p>' + Tower.Description + '</p>' +
					'<span class="ui-li-count">' + Tower.Id + '</span></a></li>');
		});
		$('#actionList').listview('refresh');
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
