


//var ServerRoot = "http://localhost:5000/";

var countryTbl = $('#countryTbl');


console.log("Got to country ");

$(document).ready(function () {

   

    GetAllCountries();

    //save country event listener
    $('#btnSavecountry').click(savecountry)

    //edit event listener
    $(document).on('click', '#editCountry', editcountry)

    //delete event listener
    $(document).on('click', '#deleteCountry', deletecountry)

    //clear inputs
    $('#btnClose_country').click(clearcountryInputs)

});



var GetAllCountries = function () {

    $('#countrySpinner').addClass('kt-spinner kt-spinner--v2 kt-spinner--sm kt-spinner--success')

    $.ajax(ServerRoot + "api/country", { method: "get" })
        .then(function (resp) {

            $("#countryTbl").dataTable({
                "scrollX": true,
                "pagingType": "simple",
                "data": resp,
                "columns": [
                    { "title": "Id", "data": "id", "className": "text-white" },
                    { "title": "Name", "data": "name", "className": "text-white" },
                    {
                        "title": "Edit",
                        "data": "id",
                        "className": "text-white"
                    },
                    {
                        "title": "Delete",
                        "data": "id",
                        "className": "text-white"

                    }


                ],

                "columnDefs":
                    [
                        {
                            "targets": 2,

                            "render": function (data) {
                                var update = `<button id="editCountry" data-id="${data}" class="btn btn-primary btn-sm mr-2" data-toggle="modal" data-target="#modal_country"><i class="fa fa-2 fa-edit"></i></button>`


                                return update;
                            }

                        },

                        {
                            "targets": 3,
                            "render": function (data) {
                                var deleter = `<button id="deleteCountry" data-id="${data}" class="btn btn-danger btn-sm"> <i class="fa fa-2 fa-trash-alt"></i></button>`
                                return deleter;
                            }

                        }
                    ]
            })


            $('#countrySpinner').removeClass('kt-spinner kt-spinner--v2 kt-spinner--sm kt-spinner--success')
        })
        .catch(err => {
            console.log(err);
            $('#countrySpinner').removeClass('kt-spinner kt-spinner--v2 kt-spinner--sm kt-spinner--success')
        })
}

var savecountry = function () {
    var prodName = $('#productcountryName').val();
    var statusParent = $('#statusParent_country');
    var status = $('#status_country');
    var countryId = $('#countryId').val();

    console.log(prodName, countryId);

    

    if (countryId == "")
        countryId = 0;
    else
        countryId = Number(countryId)

    var btn = $(this);

    var payload = {
        Id: countryId,
        Name: prodName
    };

    btn.addClass('kt-spinner kt-spinner--v2 kt-spinner--sm kt-spinner--primary');

    $.ajax(`${ServerRoot}api/country`,
        {
            method: "post",
            data: JSON.stringify(payload),
            headers: {
                "Content-Type": "application/json"
            }
        })
        .done(data => {

            btn.removeClass('kt-spinner kt-spinner--v2 kt-spinner--sm kt-spinner--primary')
            status.text(`${data.name}  ${data.status}`);
            statusParent.show();
            window.location.reload();
        });

}

var deletecountry = function () {

    var btnDelete = $(this);
    btnDelete.addClass('kt-spinner kt-spinner--v2 kt-spinner--sm kt-spinner--success');

    var countryId = btnDelete.attr('data-id');
    

    var retVal = confirm("Are you sure you want to delete this Item !?");

    if (retVal == true) {
        $.ajax({
            url: `${ServerRoot}api/country/${countryId}`,
            type: "DELETE"
        }).then(result => {

            btnDelete.removeClass('kt-spinner kt-spinner--v2 kt-spinner--sm kt-spinner--success');
            alert(`Item with ID : ${countryId} has been deleted !`);
            window.location.reload();
        })
    }
    else {
        btnDelete.removeClass('kt-spinner kt-spinner--v2 kt-spinner--sm kt-spinner--success');
        return false;

    }

}

var editcountry = function () {
    var btnEdit = $(this);

    var countryId = btnEdit.attr('data-id');

    $.ajax(`${ServerRoot}api/country/${countryId}`, { method: "get" })
        .then(resp => {
            $('#productcountryName').val(resp.name);
            $('#countryId').val(resp.id);
        }).catch(err => {

            console.log(err);
        })
}




/* Utility Functions Below */

var bindAllcountrysToTable = function (arrayData) {
    var countryTbl = document.getElementById("countryTbl");
    arrayData.forEach((data, idx) => {
        buildcountryRow(data, idx);
    })
}


var InsertcountryToTable = function (data) {
    var rowCount = $('#countryTbl tr').length;    // This get the number of the last row in the array 
    buildcountryRow(data, rowCount)
}


var buildcountryRow = function (data, idx_country) {

    countryTbl.append(`
                <tr id="countryrow_${idx_country + 1}">
                    <th scope="row">${idx_country + 1}</th>
                    <td id=countryTd_${idx_country + 1}>${data.name}</td>
                    <td><button type="button" data-toggle="modal" data-target="#modal_country" data-idx="${idx_country + 1}" data-id="${data.id}" data-name="${data.name}" id="editcountry" class="btn btn-success btn-sm my-btn-sm mr-4"> <i class="fa fa-edit"></i> </button> 
                    <button type="button" class="btn btn-danger btn-sm my-btn-sm ml-4" data-id="${data.id}" data-idx="${idx_country + 1}" id="deletecountry"> <i class="fas fa-trash-alt"></i> </button></td>
                            
                </tr>
            `)
}

var clearcountryInputs = function () {
    $('#productcountryName').val('');
    $('#countryId').val('');
    $('#idx_country').val('');
    $('#statusParent_country').hide('slow');
    $('#status_country').text('');
}



var deletecountryFunction = function (countryId, idx_country) {
    fetch(`${ServerRoot}/api/country/${countryId}`, {
        method: "DELETE",
    })
        .then(res => {
            var rowToRemove = $(`#countryrow_${idx_country}`);
            rowToRemove.remove();
        })
        .catch(err => {
            alert(err);
        })
}