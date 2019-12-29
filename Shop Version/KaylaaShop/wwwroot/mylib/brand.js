var brandTbl = $('#brandTbl');


$(document).ready(function () {

 


        GetAllBrands();

        //save brand event listener
        $('#btnSaveBrand').click(saveBrand)

        //edit event listener
        $(document).on('click', '#editBrand', editFunction)

        //delete event listener
        $(document).on('click', '#deleteBrand', deleteBrand)

        //clear inputs
        $('#btnCloseBrand').click(clearBrandInputs)

});



var GetAllBrands = function () {
       $('#mySpinner').addClass('kt-spinner kt-spinner--v2 kt-spinner--sm kt-spinner--success')


       $.ajax(ServerRoot + "api/brand", { method: "get" })
           .then(function (resp) {

               $("#brandTbl").dataTable({
                   "scrollX": true,
                   "pagingType": "simple",
                   "data": resp,
                   "columns": [
                       { "title": "Id", "data": "id", "className": "text-white"},
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
                                   var update = `<button id="editBrand" data-id="${data}" class="btn btn-primary btn-sm mr-2" data-toggle="modal" data-target="#modal_brand"><i class="fa fa-2 fa-edit"></i></button>`


                                   return update;
                               }

                           },

                           {
                               "targets": 3,
                               "render": function (data) {
                                   var deleter = `<button id="deleteBrand" data-id="${data}" class="btn btn-danger btn-sm"> <i class="fa fa-2 fa-trash-alt"></i></button>`
                                   return deleter;
                               }

                           }
                       ]
               })


               $('#mySpinner').removeClass('kt-spinner kt-spinner--v2 kt-spinner--sm kt-spinner--success')
           })
           .catch(err => {
                        console.log(err);
                    $('#mySpinner').removeClass('kt-spinner kt-spinner--v2 kt-spinner--sm kt-spinner--success')
         })
}

var saveBrand = function () {

   
                var prodName = $('#productBrandName').val();
                var statusParent = $('#statusParent');
                var status = $('#status');
                var brandId = $('#brandId').val();

                if (brandId == "")
                    brandId = 0;
                else
                    brandId = Number(brandId)

                 var btn = $(this);

                var payload = {
                    Id: brandId,
                    Name: prodName
                };

     // debugger

                btn.addClass('kt-spinner kt-spinner--v2 kt-spinner--sm kt-spinner--primary');
            
     $.ajax(`${ServerRoot}api/brand`,
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

 var deleteBrand = function () {

    var btnDelete = $(this);
    btnDelete.addClass('kt-spinner kt-spinner--v2 kt-spinner--sm kt-spinner--success');

    var brandId = btnDelete.attr('data-id');


     var retVal = confirm("Are you sure you want to delete this Item !?");


    if (retVal == true) {
        $.ajax({
            url: `${ServerRoot}api/brand/${brandId}`,
            type: "DELETE"
        }).then(result => {

            btnDelete.removeClass('kt-spinner kt-spinner--v2 kt-spinner--sm kt-spinner--success');
            alert(`Item with ID : ${brandId} has been deleted !`);
            window.location.reload();
        })
    }
    else {
        btnDelete.removeClass('kt-spinner kt-spinner--v2 kt-spinner--sm kt-spinner--success');
        return false;

    }

}




var editFunction = function () {

    var btnEdit = $(this);
    var brandId = btnEdit.attr('data-id');

    $.ajax(`${ServerRoot}api/brand/${brandId}`, { method: "get" })
        .then(resp => {
            $('#productBrandName').val(resp.name);
            $('#brandId').val(resp.id);
            console.log(resp);
        }).catch(err => {

            console.log(err);
        })

}





        /* Utility Functions Below */


var bindAllBrandsToTable = function(arrayData) {
            var brandTbl = document.getElementById("brandTbl");
            arrayData.forEach((data, idx) => {
        buildBrandRow(data, idx);
    })
}



        /*  Both Insert and Get All uses this function : Handles appending and building row  */
        var buildBrandRow = function (data, idx) {
        brandTbl.append(`
                <tr id="brandrow_${idx + 1}">
                    <th scope="row">${idx + 1}</th>
                    <td id=${idx + 1}>${data.name}</td>
                    <td><button type="button" data-toggle="modal" data-target="#modal_brand" data-idx="${idx + 1}" data-id="${data.id}" data-name="${data.name}" id="editBrand" class="btn btn-success btn-sm my-btn-sm mr-4"> <i class="fa fa-edit"></i> </button> 
                    <button type="button" class="btn btn-danger btn-sm my-btn-sm ml-4" data-id="${data.id}" data-idx="${idx + 1}" id="deleteBrand"> <i class="fas fa-trash-alt"></i> </button></td>
                            
                </tr>
            `)
    }

       

        var clearBrandInputs = function () {
        $('#productBrandName').val('');
    $('#brandId').val('');
    $('#idx').val('');
    $('#statusParent').hide('slow');
    $('#statusText').text('');
}

 
       

        var deleteBrandFunction = function (brandId, idx) {
        fetch(`${ServerRoot}/api/brand/${brandId}`, {
            method: "DELETE",
        })
            .then(res => {
                var rowToRemove = $(`#brandrow_${idx}`);
                rowToRemove.remove();
            })
            .catch(err => {
                alert(err);
            })
    }



        /* For Insert */ 
        var InsertBrandToTable = function (data) {
            var rowCount = $('#brandTbl tr').length;
    buildBrandRow(data, rowCount)
}



