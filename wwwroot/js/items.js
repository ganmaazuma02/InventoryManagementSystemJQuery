let rowData = [];
let gridOptions = {
    defaultColDef: {
        sortable: true,
        filter: true,
        resizable: true
    },
    columnDefs: [
        {
            headerName: 'Actions',
            cellRenderer: function (params) {
                var actionDiv = document.createElement('div');
                actionDiv.innerHTML = '<button class="btn btn-sm btn-success">Update</button><button class="btn btn-sm btn-danger">Delete</button>';
                var updateButton = actionDiv.querySelector('.btn-success');
                updateButton.addEventListener('click', function () {
                    $('#addItemModal').modal('show');
                    $('#addItemModal').find('.modal-title').text('Update Item');
                    $('#btnAddItem').text('Update Item');
                    $('#itemId').val(params.data.itemId);
                    $('#itemName').val(params.data.itemName);
                    $('#itemQuantity').val(params.data.quantity);
                    $('#itemDesc').val(params.data.itemDesc);
                });

                var delButton = actionDiv.querySelector('.btn-danger');

                delButton.addEventListener('click', function () {
                    $('#deleteItemModal').modal('show');
                    $('#deleteItemId').val(params.data.itemId);
                });
                return actionDiv;
            }
        },
        {
            headerName: 'Item Name', field: 'itemName',
            cellRenderer: function (params) {
                return '<a href="https://www.google.com">' + params.data.itemName + '</a>'
            }
        },
        { headerName: 'Item Description', field: 'itemDesc'},
        { headerName: 'Quantity', field: 'quantity' },
        {
            headerName: 'Date Time Added', field: 'dateTimeAdded', width: 300, 
            valueFormatter: params => {
                return params.value ? moment(new Date(params.value)).format('DD-MM-YYYY HH:mm:ss') : ''
            }
        }
    ]
}

document.addEventListener('DOMContentLoaded', function () {
    let gridDiv = document.querySelector('#myGrid');
    new agGrid.Grid(gridDiv, gridOptions);
    loadItemData();
});

function loadItemData() {
    debugger;
    $.ajax({
        url: '/api/items/getall',
        type: 'GET',
        success: function (res) {
            // set the grid data from API
            gridOptions.api.setRowData(res)
        },
        error: function (err) {
            console.log(err)
        }
    })
}

$('#btnOpenAddItemModal').on('click', function () {
    $('#addItemModal').modal('show');
    $('#addItemModal').find('.modal-title').text('New Item')
    $('#btnAddItem').text('Add Item')
    $('#locationSelect').empty()
    $('#locationSelect').append('<option selected="selected">Select Location</option>')
    $.ajax({
        url: '/api/locations',
        type: 'GET',
        success: function (res) {
            res.forEach(l => {
                $('#locationSelect').append(new Option(l.locationName, l.locationId))
            });
        },
        error: function (err) {
            console.log(err)
        }
    });

});

$('#btnDeleteItem').on('click', function () {
    $.ajax({
        url: 'api/items/del?itemId=' + $('#deleteItemId').val(),
        type: 'DELETE',
        contentType: 'application/json',
        success: function (res) {
            loadItemData();
            finishDeleteModal('Successfully deleted an item', 'success');
        },
        error: function (err) {
            finishDeleteModal(err, 'danger');
        }
    })
});

$('#btnAddItem').on('click', function () {
    let itemName = $('#itemName').val()
    let itemDesc = $('#itemDesc').val()
    let itemQuantity = $('#itemQuantity').val()
    let itemLocation = $('#locationSelect').val()
    $('#itemNameErr').text('')
    $('#itemDescErr').text('')
    $('#itemQuantityErr').text('')
    $('#itemLocationErr').text('')

    if (!validateInput(itemName, itemDesc, itemQuantity, itemLocation)) {
        return;
    }

    if ($('#btnAddItem').text() === 'Add Item') {
        $.ajax({
            url: '/api/items/add?locationId=' + itemLocation,
            type: 'POST',
            data: JSON.stringify({
                itemName: itemName,
                itemDesc: itemDesc,
                itemQuantity: parseInt(itemQuantity)
            }),
            contentType: 'application/json',
            success: function (res) {
                loadItemData();
                finishModal('Add Item', 'Successfully added an item', 'success');
            },
            error: function (err) {
                finishModal('Add Item', err, 'danger');
            }
        })
    } else if ($('#btnAddItem').text() === 'Update Item') {
        $.ajax({
            url: '/api/items/update?itemId=' + $('#itemId').val(),
            type: 'PUT',
            data: JSON.stringify({
                itemName: itemName,
                itemDesc: itemDesc,
                itemQuantity: parseInt(itemQuantity)
            }),
            contentType: 'application/json',
            success: function (res) {
                loadItemData();
                finishModal('Update Item', 'Successfully updated an item', 'success');
            },
            error: function (err) {
                finishModal('Update Item', err, 'danger');
            }
        })
    }


})

function validateInput(itemName, itemDesc, itemQuantity, itemLocation) {
    let isValidated = true;
    if (!itemName) {
        $('#itemNameErr').text('Item Name is required');
        isValidated = false;
    }
    if (!itemDesc) {
        $('#itemDescErr').text('Item Description is required');
        isValidated = false;
    }
    if (!itemQuantity) {
        $('#itemQuantityErr').text('Item Quantity is required');
        isValidated = false;
    }
    if (itemLocation === 'Select Location') {
        $('#itemLocationErr').text('Item Location is required');
        isValidated = false;
    }
    return isValidated;
}

function finishModal(title, content, status) {
    $('#notiToast').toast('show');
    $('#notiToast').addClass('bg-' + status);
    $('#toastTitle').text(title);
    $('#toastContent').text(content);

    if (status != 'danger') {
        $('#itemName').val('')
        $('#itemDesc').val('')
        $('#itemQuantity').val('')  
        $('#addItemModal').modal('hide')
    }
}

function finishDeleteModal(content, status) {
    $('#notiToast').toast('show');
    $('#notiToast').addClass('bg-' + status);
    $('#toastTitle').text('Delete Item');
    $('#toastContent').text(content);
    $('#deleteItemModal').modal('hide');
}