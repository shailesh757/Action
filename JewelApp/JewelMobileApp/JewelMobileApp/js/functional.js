var globalData;

function dataLoad() {
    $.getJSON(serviceURL + PropertyURL, function (data) {
        $('#employeeList li').remove();
        employees = data;
        $.each(employees, function (index, employee) {
            $('#employeeList').append('<li id = "' + employee.Id + '"><img height="50px" width="50px" src = "' + imageURL + employee.imageDisplayName + '"/>' +
					'<h3>' + employee.Feature + '</h3><p> ' + employee.Description +
					'<br>' + employee.Location + '</p>' +
					'</li>'); //'<a href ="propertydetail.html?id=' + employee.Id + '">' + '<span class="ui-li-count">' + employee.Id + '</span></a>
        });
        $('#employeeList').refresh;
        hideAll();
        $(".content").show();

    }).done(function () {
    })
		.fail(function () {
		    console.log("error");
		})
		.always(function () {
		    console.log("complete");
		});
}

function displayUserProfile(data) {
    $("label[for='uname']").text(data.UserName);
    $("label[for='email']").text(data.EmailId);
    $("label[for='mobile']").text(data.MobileNo);
    $("label[for='dob']").text(data.DOB);
    
    hideAll();
    $(".UserProfile").show();
}

function displayPropertyDetails(id) {
    $.getJSON(serviceURL + PropDetailsURL + id, function (data) {
        propImages = data;
        $("label[for='feature']").text(propImages.Feature);
        $("label[for='desc']").text(propImages.Description);
        $("label[for='location']").text(propImages.Location);
        $("label[for='type']").text(propImages.CodeReference);
        $("#viewTower").attr('itemid', propImages.Id);
        hideAll();
        $(".bookingCnt").show();
    }).done(function () {
    })
		.fail(function () {
		    console.log("error");
		})
		.always(function () {
		    console.log("complete");
		});
}


function displayPropImages(id) {
    var strImg = '';
    $(".carousel-inner").empty();
    $.getJSON(serviceURL + PropImagesURL.replace("{id}", id), function (data) {
        propImages = data;
        $.each(propImages, function (index, propImage) {
            if (index == 0) {
                strImg = strImg + '<div class="item active"><img src = "' + imageURL + propImage.DisplayName + '"></div>'
            }
            else {
                strImg = strImg + '<div class="item"><img src = "' + imageURL + propImage.DisplayName + '"></div>'
            }
        });
        $(".carousel-inner").append(strImg);
        hideAll();
        $(".bookingCnt").show();
        $('.carousel').carousel({
            interval: 500
        });

    }).done(function () {
    })
		.fail(function () {
		    console.log("error");
		})
		.always(function () {
		    console.log("complete");
		});
}

function displayTowers(id) {
    $.getJSON(serviceURL + TowerURL + id, function (data) {
        $('#actionList li').remove();
        Towers = data;
        $.each(Towers, function (index, Tower) {
            $('#actionList').append('<li id = "' + Tower.Id + '"><img height="50px" width="50px" src = "' + imageURL + Tower.imageDisplayName + '"/>' +
					'<h3>' + Tower.TowerName + '</h3>' +
					'<p>' + ' ' + Tower.TowerDirection + '<br>' + Tower.Description + '</p>' +
					'</li>'); //'<span class="ui-li-count">' + Tower.Id + '</span></a></li>');
        });
        $('#actionList').refresh;
        hideAll();
        $(".towcontent").show();
    }).done(function () {
    })
		.fail(function () {
		    console.log("error");
		})
		.always(function () {
		    console.log("complete");
		});
}

function displayTowerDetails(id) {
    $.getJSON(serviceURL + TowerDetailsURL + id, function (data) {
        towerDetails = data;
        $("label[for='feature']").text(towerDetails.TowerName);
        $("label[for='desc']").text(towerDetails.Description);
        $("label[for='location']").text(towerDetails.TowerDirection);
        $("#viewApts").attr('itemid', towerDetails.Id);
        hideAll();
        $(".TowerDetail").show();
    }).done(function () {
    })
		.fail(function () {
		    console.log("error");
		})
		.always(function () {
		    console.log("complete");
		});
}


function displayTowerImages(id) {
    var strImg = '';
    $(".carousel-inner").empty();
    $.getJSON(serviceURL + TowerImagesURL.replace("{id}", id), function (data) {
        TowerImages = data;
        $.each(TowerImages, function (index, TowerImage) {
            if (index == 0) {
                strImg = strImg + '<div class="item active"><img src = "' + imageURL + TowerImage.DisplayName + '"></div>'
            }
            else {
                strImg = strImg + '<div class="item"><img src = "' + imageURL + TowerImage.DisplayName + '"></div>'
            }
        });
        $(".carousel-inner").append(strImg);
        hideAll();
        $(".TowerDetail").show();
    }).done(function () {
    })
		.fail(function () {
		    console.log("error");
		})
		.always(function () {
		    console.log("complete");
		});
}



function displayApts(id) {
    $.getJSON(serviceURL + ApartmentURL + id, function (data) {
        $('#apartmentlist li').remove();
        apartments = data;
        $.each(apartments, function (index, apartment) {
            $('#apartmentlist').append('<li id = "' + apartment.Id + '"><img height="50px" width="50px" src = "' + imageURL + apartment.imageDisplayName + '"/>' +
					'<h3>' + apartment.Description + '</h3>' +
					'<p>' + 'Direction - ' + apartment.Direction + ' Bedroom - ' + apartment.BedRoom + ' Bathroom - ' + apartment.Bathroom + '</p>' +
					'</li>'); //'<span class="ui-li-count">' + apartment.Id + '</span></a></li>');
        });
        $('#apartmentlist').refresh;
        hideAll();
        $(".aptcontent").show();

    }).done(function () {
    })
		.fail(function () {
		    console.log("error");
		})
		.always(function () {
		    console.log("complete");
		});
}

function displayAptDetails(id) {
    $.getJSON(serviceURL + AptDetailsURL + id, function (data) {
        aptDetails = data;
        $("#LockProp").html('Lock Property');
        $("label[for='bedroom']").text(aptDetails.BedRoom);
        $("label[for='bathroom']").text(aptDetails.Bathroom);
        $("label[for='garage']").text(aptDetails.Garage);
        $("label[for='floorlevel']").text(aptDetails.FloorLevel);
        $("label[for='desc']").text(aptDetails.Description);
        $("label[for='location']").text(aptDetails.Direction);
        $("#LockProp").attr('itemid', aptDetails.Id);
        if (aptDetails.IsBlocked) {
            if (isAdmin) {
                $("#LockProp").html('Unlock Property');
            }
            else { $("#LockProp").html('Property Locked'); }
        }
        hideAll();
        $(".AptDetail").show();
    }).done(function () {
    })
		.fail(function () {
		    console.log("error");
		})
		.always(function () {
		    console.log("complete");
		});
}


function displayAptImages(id) {
    var strImg = '';
    $(".carousel-inner").empty();
    $.getJSON(serviceURL + AptImagesURL.replace("{id}", id), function (data) {
        AptImages = data;

        $.each(AptImages, function (index, AptImage) {
            if (index == 0) {
                strImg = strImg + '<div class="item active"><img src = "' + imageURL + AptImage.DisplayName + '"></div>'
            }
            else {
                strImg = strImg + '<div class="item"><img src = "' + imageURL + AptImage.DisplayName + '"></div>'
            }
        });
        $(".carousel-inner").append(strImg);
        hideAll();
        $(".AptDetail").show();
    }).done(function () {
    })
		.fail(function () {
		    console.log("error");
		})
		.always(function () {
		    console.log("complete");
		});
}

function lockProperty(id) {
    var strImg = '';

    $.getJSON(serviceURL + AptSaleURL.replace("{userId}", userId).replace("{apartmentId}", id), function (data) {
        if (data == "success") {
            alert("Property Locked")
            if (isAdmin) {
                $("#LockProp").html('Unlock Property');
            }
            else { $("#LockProp").html('Property Locked'); }
        }
        else {

            alert('No response received from server. Please try again!!');
        }

    }).done(function () {
    })
		.fail(function () {
		    console.log("error");
		})
		.always(function () {
		    console.log("complete");
		});
}

function unlockProperty(id) {
    var strImg = '';

    $.getJSON(serviceURL + AptUnSaleURL.replace("{userId}", userId).replace("{apartmentId}", id), function (data) {
        if (data == "success") {
            alert("Property UnLocked")
            $("#LockProp").html('Lock Property');}
        else {

            alert('No response received from server. Please try again!!');
        }

    }).done(function () {
    })
		.fail(function () {
		    console.log("error");
		})
		.always(function () {
		    console.log("complete");
		});
}

function displayLockProperties(id) {
    $.getJSON(serviceURL + LockPropURL + id, function (data) {
        $('#lockproplist li').remove();
        apartments = data;
        $.each(apartments, function (index, apartment) {
            $('#lockproplist').append('<li>' +
					'<h3>' + apartment.ApartmentDesc + '</h3>' +
					'<p>' + 'Tower - ' + apartment.TowerDesc + '<br>' + ' Property - ' + apartment.PropertyDesc  + '<br>' + ' Lock Start Date - ' + apartment.StartDate +
					'</li>'); //'<span class="ui-li-count">' + apartment.Id + '</span></a></li>');
        });
        $('#lockproplist').refresh;
        hideAll();
        $(".lockpropcontent").show();

    }).done(function () {
    })
     .fail(function () {
         console.log("error");
     })
     .always(function () {
         console.log("complete");
     });
}

$("#employeeList").on("click", "li", function () {
    var propindex = $(this).attr('id');
    //displayTowers(propindex);
    displayPropImages(propindex);
    displayPropertyDetails(propindex);
})

$("#actionList").on("click", "li", function () {
    var towindex = $(this).attr('id');
    //displayApts(towindex);
    displayTowerImages(towindex);
    displayTowerDetails(towindex);
})

$("#apartmentlist").on("click", "li", function () {
    var aptindex = $(this).attr('id');
    //displayApts(towindex);
    displayAptImages(aptindex);
    displayAptDetails(aptindex);
})

$("#viewTower").click(function () {
    var towindex = $(this).attr('itemid');
    displayTowers(towindex);
})

$("#viewApts").click(function () {
    var aptindex = $(this).attr('itemid');
    displayApts(aptindex);
})

$("#btnLockProp").click(function () {
    displayLockProperties(userId);
})

$("#btnViewAll").click(function () {
    dataLoad();
})

$("#propLink").click(function () {
    hideAll();
    dataLoad();
    $("#navbarbutton").click();
})

$("#LockPropLink").click(function () {
    hideAll();
    displayLockProperties(userId);
    $("#navbarbutton").click();
})

$("#profLink").click(function () {
    hideAll();
    $(".UserProfile").show();
    $("#navbarbutton").click();
})

$("#LockProp").click(function () {
    var aptindex = $(this).attr('itemid');
    var title = $(this).text();
    if (title == 'Unlock Property') {
        unlockProperty(aptindex)
    }
    else if (title == 'Lock Property') {
        lockProperty(aptindex);
    }
})


function hideAll() {
    $(".content").hide();
    $(".towcontent").hide();
    $(".aptcontent").hide();
    $(".loginCnt").hide();
    $(".bookingCnt").hide();
    $(".TowerDetail").hide();
    $(".AptDetail").hide();
    $(".UserProfile").hide();
    $(".lockpropcontent").hide();
}

$("#signIn").click(function () {
    var Uname = $("#userName").val();
    var pass = $("#pass").val();

    $.getJSON(serviceURL + AuthURL.replace("{user}", Uname).replace("{pwd}", pass), function (data) {
        if (data.length == 0) {
            alert("No response received from server. Please try again!!")
        }
        else {
            if (data.Id == "null")
            { alert('Invalid Username or Password'); }
            else
            {
                userId = data.Id;
                isAdmin = data.IsOwner;
                displayUserProfile(data);
                //dataLoad();
            }
        }
    });
});
