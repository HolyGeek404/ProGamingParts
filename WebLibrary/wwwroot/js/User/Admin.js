function LoadUsersTable() {
    $.get("/UserApi/LoadUsersTable", function (view) {
        $("#content-container").html(view);
    });
}
function LoadUser() {
    var userId = $(this).attr("user-id");
    $.get("/UserApi/LoadUser",
        {
            userId: userId
        }, function (view) {
            $("#content-container").html(view);
        });
}
function LoadProductToAdd() {
    var type = $(this).attr("product-type");

    $.get(`/ProductApi/LoadProductToAdd/${type}`,
        function (view) {
            $("#producToAdd").html(view);
        });
}
function LoadProductToEdit() {
    var productId = $(this).attr("product-id");
    var type = $(this).attr("product-type");

    $.get(`/ProductApi/LoadProductToEdit/${type}/${productId}`,
        function (view) {
            $("#producToEdit").html(view);
        });
}
function LoadProductsManagmentTable() {
    $.get("/ProductApi/LoadProducts", function (view) {
        $("#content-container").html(view);
    });
}
function LoadUserOrders() {
    var row = $(this).closest("tr");
    const tr = $("#usersTable").DataTable().row(row);

    if (row.hasClass("dt-hasChild")) {
        row.next("tr").remove();
        row.removeClass("dt-hasChild");
    } else {
        var userId = $(this).attr("user-id");
        $.get("/UserApi/LoadUserOrders",
            {
                userId: userId
            }, function (data) {
                tr.child(ReturnUserOrdersTable(userId)).show();
                $("#user-id-" + userId + "").DataTable({
                    data: data,
                    paging: false,
                    search: false,
                    columns: [
                        { data: "orderId" },
                        { data: "productName" },
                        { data: "price" }
                    ]
                });
            });
    }

}

function CreateUsersTable(users) {
    $("#usersTable").DataTable({
        data: users,
        columns: [
            { data: "userId" },
            { data: "name" },
            { data: "email" },
            {
                data: "userId",
                render: function (data) {
                    return `<button user-id="${data}" id=\"editUser\" class=\"btn font-weight-bold mr-1 btn-sm btn-outline-primary\">Edytuj <i class=\"fa-solid fa-user-pen\"></i></button>
                            <button user-id="${data}" id=\"loadUserOrders\" class=\"btn font-weight-bold mr-1 btn-sm btn-outline-success\">Zamówienia <i class=\"fa-solid fa-cart-flatbed\"></i></button>
                            <button user-id="${data}" id=\"deleteUser\" class=\"btn font-weight-bold mr-1 btn-sm btn-outline-danger\">Usuń <i class=\"fa-solid fa-user-xmark\"></i></button>`;
                }
            }
        ],
        createdRow: function (row, data) {
            $(row).attr("user-id", data.userId);
        }
    });
}
function CreateProductsTable(products) {
    var cpus = products.cpuList;
    var gpus = products.gpuList;
    var coolers = products.coolerList;

    $("#cpuProductsTable").DataTable({
        data: cpus,
        columns: [
            { data: "id" },
            { data: "name" },
            { data: "price" },
            { data: "type" },
            {
                data: null,
                render: function (data) {
                    return `<button product-id="${data.id}" product-type="${data.type}" id=\"editProduct\" class=\"btn font-weight-bold mr-1 btn-sm btn-outline-primary\">Edytuj <i class=\"fa-solid fa-user-pen\"></i></button>
                            <button product-id="${data.id}" product-type="${data.type}" id=\"deleteProduct\" class=\"btn font-weight-bold mr-1 btn-sm btn-outline-danger\">Usuń <i class=\"fa-solid fa-user-xmark\"></i></button>`;
                }
            }
        ],
        createdRow: function (row, data) {
            $(row).attr("product-id", data.id);
            $(row).attr("product-type", data.type);
        }
    });

    $("#gpuProductsTable").DataTable({
        data: gpus,
        columns: [
            { data: "id" },
            { data: "name" },
            { data: "price" },
            { data: "type" },
            {
                data: null,
                render: function (data) {
                    return `<button product-id="${data.id}" product-type="${data.type}" id=\"editProduct\" class=\"btn font-weight-bold mr-1 btn-sm btn-outline-primary\">Edytuj <i class=\"fa-solid fa-user-pen\"></i></button>
                            <button product-id="${data.id}" product-type="${data.type}" id=\"deleteProduct\" class=\"btn font-weight-bold mr-1 btn-sm btn-outline-danger\">Usuń <i class=\"fa-solid fa-user-xmark\"></i></button>`;
                }
            }
        ],
        createdRow: function (row, data) {
            $(row).attr("product-id", data.id);
            $(row).attr("product-type", data.type);
        }
    });

    $("#coolerProductsTable").DataTable({
        data: coolers,
        columns: [
            { data: "id" },
            { data: "name" },
            { data: "price" },
            { data: "type" },
            {
                data: null,
                render: function (data) {
                    return `<button product-id="${data.id}" product-type="${data.type}" id=\"editProduct\" class=\"btn font-weight-bold mr-1 btn-sm btn-outline-primary\">Edytuj <i class=\"fa-solid fa-user-pen\"></i></button>
                            <button product-id="${data.id}" product-type="${data.type}" id=\"deleteProduct\" class=\"btn font-weight-bold mr-1 btn-sm btn-outline-danger\">Usuń <i class=\"fa-solid fa-user-xmark\"></i></button>`;
                }
            }
        ],
        createdRow: function (row, data) {
            $(row).attr("product-id", data.id);
            $(row).attr("product-type", data.type);
        }
    });
}

function SaveProduct() {
    var formData = $("#productForm").serializeArray();
    var type = $(this).attr("product-type");

    if (!type) {
        alert('Product type is required.');
        return;
    }

    var dataObject = {};
    formData.forEach(function (item) {
        dataObject[item.name] = item.value;
    });

    $.get("/ProductApi/SaveProduct", {
            type: type,
            model: JSON.stringify(dataObject)
        })
        .done(function (response) {
            // Handle success
            console.log("Success:", response);
        })
        .fail(function (error) {
            // Handle error
            console.error("Error:", error);
        });
}

function AddProduct() {
    var formData = $("#addProductForm").serializeArray();
    var type = $(this).attr("product-type");

    if (!type) {
        alert('Product type is required.');
        return;
    }

    var dataObject = {};
    formData.forEach(function (item) {
        dataObject[item.name] = item.value;
    });


    $.ajax({
        url: `/ProductApi/AddNew`,
        type: "POST",
        data: JSON.stringify({
            type: type,
            newModel: JSON.stringify(dataObject)
        }),
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: (result) => {
            var resultParsed = JSON.parse(result);
            if (resultParsed.success) {
                $("#addingFailMsg").attr("hidden");
                $("#addingSuccessMsg").removeAttr("hidden");
            }
            else {
                $("#addingSuccessMsg").attr("hidden");
                $("#addingFailMsg").removeAttr("hidden");
            }
        }
    });
}
function ReturnUserOrdersTable(userId) {
    var userOrderTableTemplate = `
    <table class="table table-dark" id="user-id-${userId}">
        <thead>
            <tr>
                <th>Order Id</th>
                <th>Products</th>
                <th>Price</th>
                <!-- <th>Actions</th> -->
            </tr>
        </thead>
        <tbody></tbody>
    </table>`;

    return userOrderTableTemplate;
}
function DeleteUser() {
    var userId = $(this).attr("user-id");

    $.ajax({
        url: `/UserApi/DeleteUser/${userId}`,
        type: "DELETE",
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: () => {
            $('tr[user-id="' + userId + '"]').closest("tr").remove();
        }
    });
}
function DeleteProduct() {
    var productId = $(this).attr("product-id");
    var type = $(this).attr("product-type");

    $.ajax({
        url: `/ProductApi/Delete/${type}/${productId}`,
        type: "DELETE",
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: () => {
            $(this).closest("tr").remove();
        }
    });
}