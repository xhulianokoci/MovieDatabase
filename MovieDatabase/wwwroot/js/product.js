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
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="w-150 btn-group" role="group">
                            <a href="/Product/Upsert?id=${data}" class="btn btn-primary">
                              <i class="bi bi-pencil-square"></i> &nbsp; Edit
                            </a>
                             <a onclick=Delete('/Product/Delete/+${data}') class="btn btn-danger">
                               <i class="bi bi-trash"></i> &nbsp; Delete
                            </a>
                        </div>
                    `

                    
                }
            }
           

        ]
    });
}

function Delete(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    if (data.success) {
                        dataTable.ajax.reload();
                        toastr.success(data.message);
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            })
        }
    })
}