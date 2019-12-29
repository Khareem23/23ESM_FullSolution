




//var ServerRoot = "http://localhost:5000/";

var colorTbl = $('#colorTbl');


console.log("Got to color ");

$(document).ready(function () {

  

    GetAllColors();

    //save Color event listener
    $('#btnSaveColor').click(saveColor)

    //edit event listener
    $(document).on('click', '#editColor', editColor)

    //delete event listener
    $(document).on('click', '#deleteColor', deleteColor)

    //clear inputs
    $('#btnClose_color').click(clearColorInputs)

});



var GetAllColors = function () {

    $('#colorSpinner').addClass('kt-spinner kt-spinner--v2 kt-spinner--sm kt-spinner--success')

    $.ajax(ServerRoot + "api/color", { method: "get" })
        .then(function (resp) {

            $("#colorTbl").dataTable({
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
                                var update = `<button id="editColor" data-id="${data}" class="btn btn-primary btn-sm mr-2" data-toggle="modal" data-target="#modal_color"><i class="fa fa-2 fa-edit"></i></button>`


                                return update;
                            }

                        },

                        {
                            "targets": 3,
                            "render": function (data) {
                                var deleter = `<button id="deleteColor" data-id="${data}" class="btn btn-danger btn-sm"> <i class="fa fa-2 fa-trash-alt"></i></button>`
                                return deleter;
                            }

                        }
                    ]
            })


            $('#colorSpinner').removeClass('kt-spinner kt-spinner--v2 kt-spinner--sm kt-spinner--success')
        })
        .catch(err => {
            console.log(err);
            $('#colorSpinner').removeClass('kt-spinner kt-spinner--v2 kt-spinner--sm kt-spinner--success')
        })


    //fetch(ServerRoot + "api/color", {
    //    method: "GET",
    //    headers: {
    //        "Content-Type": "application/json"
    //    }
    //}).then(res => res.json())
    //    .then(data => {
    //        console.log(data); 
    //        bindAllColorsToTable(data);
    //        $('#colorSpinner').removeClass('kt-spinner kt-spinner--v2 kt-spinner--sm kt-spinner--success')
    //    })
    //    .catch(err => {
    //        console.log(err);
    //        $('#colorSpinner"').removeClass('kt-spinner kt-spinner--v2 kt-spinner--sm kt-spinner--success')
    //    })
}

var saveColor = function () {
    var prodName = $('#productColorName').val();
    var statusParent = $('#statusParent_Color');
    var status = $('#status_Color');
    var ColorId = $('#colorId').val();

    if (ColorId == "")
        ColorId = 0;
    else
        ColorId = Number(ColorId)

    var btn = $(this);
    var payload = {
        Id: ColorId,
        Name: prodName
    };

    btn.addClass('kt-spinner kt-spinner--v2 kt-spinner--sm kt-spinner--primary');

    $.ajax(`${ServerRoot}api/color`,
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

var deleteColor = function () {

    var btnDelete = $(this);
    btnDelete.addClass('kt-spinner kt-spinner--v2 kt-spinner--sm kt-spinner--success');

    var colorId = btnDelete.attr('data-id');
    // var idx = btnDelete.attr('data-idx');

    var retVal = confirm("Are you sure you want to delete this Item !?");

    if (retVal == true) {
        $.ajax({
            url: `${ServerRoot}api/color/${colorId}`,
            type: "DELETE"
        }).then(result => {

            btnDelete.removeClass('kt-spinner kt-spinner--v2 kt-spinner--sm kt-spinner--success');
            alert(`Item with ID : ${colorId} has been deleted !`);
            window.location.reload();
        })
    }
    else {
        btnDelete.removeClass('kt-spinner kt-spinner--v2 kt-spinner--sm kt-spinner--success');
        return false;

    }

}

var editColor = function () {
    var btnEdit = $(this);

    var colorId = btnEdit.attr('data-id');


    $.ajax(`${ServerRoot}api/color/${colorId}`, { method: "get" })
        .then(resp => {
            $('#productColorName').val(resp.name);
            $('#colorId').val(resp.id);
        }).catch(err => {
            console.log(err);
        })
}




/* Utility Functions Below */

var bindAllColorsToTable = function (arrayData) {
    var colorTbl = document.getElementById("colorTbl");
    arrayData.forEach((data, idx) => {
        buildColorRow(data, idx);
    })
}


var InsertColorToTable = function (data) {
    var rowCount = $('#colorTbl tr').length;    // This get the number of the last row in the array 
    buildColorRow(data, rowCount)
}


var buildColorRow = function (data, idx_color) {
    
    colorTbl.append(`
                <tr id="colorrow_${idx_color + 1}">
                    <th scope="row">${idx_color + 1}</th>
                    <td id=colorTd_${idx_color + 1}>${data.name}</td>
                    <td><button type="button" data-toggle="modal" data-target="#modal_color" data-idx="${idx_color + 1}" data-id="${data.id}" data-name="${data.name}" id="editColor" class="btn btn-success btn-sm my-btn-sm mr-4"> <i class="fa fa-edit"></i> </button> 
                    <button type="button" class="btn btn-danger btn-sm my-btn-sm ml-4" data-id="${data.id}" data-idx="${idx_color + 1}" id="deleteColor"> <i class="fas fa-trash-alt"></i> </button></td>
                            
                </tr>
            `)
}

var clearColorInputs = function () {
    $('#productColorName').val('');
    $('#colorId').val('');
    $('#idx_color').val('');
    $('#statusParent_Color').hide('slow');
    $('#status_Color').text('');
}



var deleteColorFunction = function (colorId, idx_color) {
    fetch(`${ServerRoot}/api/color/${colorId}`, {
        method: "DELETE",
    })
        .then(res => {
            var rowToRemove = $(`#colorrow_${idx_color}`);
            rowToRemove.remove();
        })
        .catch(err => {
            alert(err);
        })
}