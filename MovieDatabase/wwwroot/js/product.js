var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#myTable').DataTable({
        "ajax": {
            "url":"/Product/GetAll"
        },
        "columns": [
            { "data": "title", "width": "15%" },
            { "data": "description", "width": "60%" },
            { "data": "year", "width": "15%" },
            { "data": "rating", "width": "15%" },
            { "data": "category.description", "width": "15%" },
            { "data": "listPrice", "width": "15%" },
            { "data": "price", "width": "15%" },
            { "data": "price50", "width": "15%" },
            { "data": "price100", "width": "15%" },
           

        ]
    });
}