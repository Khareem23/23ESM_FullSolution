
//var ServerRoot = "http://localhost:5000/";
var CategoryTbl = $('#categoryTbl');


console.log("Got to Category ");

$(document).ready(function () {

  

    GetAllCategories();

    //save Category event listener
    $('#btnSaveCategory').click(saveCategory)

    //edit event listener
    $(document).on('click', '#editCategory', editCategory)

    //delete event listener
    $(document).on('click', '#deleteCategory', deleteCategory)

    //clear inputs
    $('#btnClose_category').click(clearCategoryInputs)

});



var GetAllCategories = function () {

    $('#CategorySpinner').addClass('kt-spinner kt-spinner--v2 kt-spinner--sm kt-spinner--success')

    fetch(ServerRoot + "api/category", {
        method: "GET",
        headers: {
            "Content-Type": "application/json"
        }
    }).then(res => res.json())
        .then(data => {
            console.log(data);
            bindAllCategorysToTable(data);
            $('#CategorySpinner').removeClass('kt-spinner kt-spinner--v2 kt-spinner--sm kt-spinner--success')
        })
        .catch(err => {
            console.log(err);
            $('#CategorySpinner"').removeClass('kt-spinner kt-spinner--v2 kt-spinner--sm kt-spinner--success')
        })
}

var saveCategory = function () {
    var prodName = $('#productCategoryName').val();
    var statusParent = $('#statusParent_Category');
    var status = $('#status_Category');
    var CategoryId = $('#categoryId').val();

    if (CategoryId == "")
        CategoryId = 0;
    else
        CategoryId = Number(CategoryId)

    var btn = $(this);

    var payload = {
        Id: CategoryId,
        Name: prodName
    };

    btn.addClass('kt-spinner kt-spinner--v2 kt-spinner--sm kt-spinner--primary');

    fetch(ServerRoot + 'api/category',
        {
            method: "POST",
            body: JSON.stringify(payload),
            headers: {
                "Content-Type": "application/json"
            }
        })
        .then(res => res.json())
        .then(data => {
            btn.removeClass('kt-spinner kt-spinner--v2 kt-spinner--sm kt-spinner--primary')
            status.text(`${data.name}  ${data.status}`);
            statusParent.show();
            //debugger
            if (CategoryId == 0)
                InsertCategoryToTable(data)
            else {

                var idx_Category = $('#idx_Category').val();
                var tdCategoryToEdit = $(`#categoryTd_${idx_Category}`);
                tdCategoryToEdit.text(data.name)
            }

        })
        .catch(err => {
            btn.removeClass('kt-spinner kt-spinner--v2 kt-spinner--sm kt-spinner--primary')
            status.text(`Category fail to add`);
            statusParent.show();
        })

}

var deleteCategory = function () {

    var btnDelete = $(this);
    btnDelete.addClass('kt-spinner kt-spinner--v2 kt-spinner--sm kt-spinner--success');

    var CategoryId = btnDelete.attr('data-id');
    var idx = btnDelete.attr('data-idx');

    var retVal = confirm("Are you sure you want to delete this Item !?");
    if (retVal == true) {
        deleteCategoryFunction(CategoryId, idx);
    }
    else {
        btnDelete.removeClass('kt-spinner kt-spinner--v2 kt-spinner--sm kt-spinner--success');
        return false;

    }

}

var editCategory = function () {
    var btnEdit = $(this);

    var CategoryId = btnEdit.attr('data-id');
    var prodName = btnEdit.attr('data-name');
    var idx = btnEdit.attr('data-idx');


    $('#productCategoryName').val(prodName);
    $('#categoryId').val(CategoryId);
    $('#idx_Category').val(idx);
}




/* Utility Functions Below */

var bindAllCategorysToTable = function (arrayData) {
    var CategoryTbl = document.getElementById("categoryTbl");
    arrayData.forEach((data, idx) => {
        buildCategoryRow(data, idx);
    })
}


var InsertCategoryToTable = function (data) {
    var rowCount = $('#CategoryTbl tr').length;    // This get the number of the last row in the array 
    buildCategoryRow(data, rowCount)
}


var buildCategoryRow = function (data, idx_Category) {

    CategoryTbl.append(`
                <tr id="Categoryrow_${idx_Category + 1}">
                    <th scope="row">${idx_Category + 1}</th>
                    <td id=categoryTd_${idx_Category + 1}>${data.name}</td>
                    <td><button type="button" data-toggle="modal" data-target="#modal_category" data-idx="${idx_Category + 1}" data-id="${data.id}" data-name="${data.name}" id="editCategory" class="btn btn-success btn-sm my-btn-sm mr-4"> <i class="fa fa-edit"></i> </button> 
                    <button type="button" class="btn btn-danger btn-sm my-btn-sm ml-4" data-id="${data.id}" data-idx="${idx_Category + 1}" id="deleteCategory"> <i class="fas fa-trash-alt"></i> </button></td>
                            
                </tr>
            `)
}

var clearCategoryInputs = function () {
    $('#productCategoryName').val('');
    $('#categoryId').val('');
    $('#idx_Category').val('');
    $('#statusParent_Category').hide('slow');
    $('#status_Category').text('');
}



var deleteCategoryFunction = function (CategoryId, idx_Category) {
    fetch(`${ServerRoot}/api/category/${CategoryId}`, {
        method: "DELETE",
    })
        .then(res => {
            var rowToRemove = $(`#Categoryrow_${idx_Category}`);
            rowToRemove.remove();
        })
        .catch(err => {
            alert(err);
        })
}