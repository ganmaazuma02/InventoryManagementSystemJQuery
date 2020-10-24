$(document).ready(function () {

    $.ajax({
        url: '/api/items/getall',
        type: 'GET',
        success: function (res) {
            for (let i = 0; i < res.length; i++) {
                let itemTr = document.createElement("tr");

                let itemNameTd = document.createElement("td");
                $(itemNameTd).text(res[i].itemName);

                let itemDescTd = document.createElement("td");
                $(itemDescTd).text(res[i].itemDesc);

                let quantityTd = document.createElement("td");
                $(quantityTd).text(res[i].quantity);

                let dateTimeAddedTd = document.createElement("td");
                $(dateTimeAddedTd).text(res[i].dateTimeAdded);

                itemTr.append(itemNameTd);
                itemTr.append(itemDescTd);
                itemTr.append(quantityTd);
                itemTr.append(dateTimeAddedTd);

                $("#view-all-item-table").append(itemTr);
            }      
        },
        error: function (err) {
            console.log(err);
        }
    })

});