$(function () {
    $('#userTable').DataTable();
});

$('.addUserBtn').click(function () {
    $.ajax({
        url: '/Users/UserPartial',
        type: 'GET',
        success: function (data) {
            $('#UserContent').html(data);
            var modal = new bootstrap.Modal($('#editUserModal'));
            modal.show();

            $('#userForm').submit(function (e) {
                e.preventDefault();
                debugger
                var formData = new FormData(this);
                $.ajax({
                    url: 'Users/UserPartial',
                    type: 'POST',
                    headers: {
                        "RequestVerificationToken": antiForgeryToken
                    },
                    data: formData,
                    contentType: false,
                    processData: false,
                    success: function (data) {
                        if (data.success) {
                            modal.hide();
                            location.reload();
                        }
                    },
                    error: function (xhr, status, error) {
                        Swal.fire({
                            title: "Error!",
                            text: "No se pudo eliminar el usuario.",
                            icon: "error"
                        });
                    }
                });
            })

        }
    });
});

$('.editUserBtn').click(function () {
    var userId = $(this).data("user-id");
    $.ajax({
        url: '/Users/UserPartial?id=' + userId,
        type: 'GET',
        success: function (data) {

            $('#UserContent').html(data);
            var modal = new bootstrap.Modal($('#editUserModal'));
            modal.show();

            $('#userForm').submit(function (e) {
                e.preventDefault();

                var formData = new FormData(this);
                $.ajax({
                    url: 'Users/UserPartial',
                    type: 'PUT',
                    headers: {
                        "RequestVerificationToken": antiForgeryToken
                    },
                    data: formData,
                    contentType: false,
                    processData: false,
                    success: function (data) {
                        if (data.success) {
                            modal.hide();
                            location.reload();
                        }
                    },
                    error: function (xhr, status, error) {
                        Swal.fire({
                            title: "Error!",
                            text: "No se pudo eliminar el usuario.",
                            icon: "error"
                        });
                    }
                });
            })
        }
    });
});

$('.deleteUserBtn').click(function () {
    Swal.fire({
        title: "Estas Seguro?",
        text: "Vas a eliminar al usuario",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Eliminar",
        cancelButtonText: "Cancelar"
    }).then((result) => {
        debugger
        if (result.isConfirmed) {
            var userId = $(this).data("user-id");
            $.ajax({
                url: deleteUserUrl,
                type: 'DELETE',
                headers: {
                    "RequestVerificationToken": antiForgeryToken
                },
                contentType: "application/json",
                data: JSON.stringify({ id: userId }),
                success: function (data) {
                    if (data.success) {
                        Swal.fire({
                            title: "Eliminado!",
                            text: "Haz eliminado el usuario.",
                            icon: "success"
                        });
                        location.reload();
                    }
                },
                error: function (xhr, status, error) {
                    Swal.fire({
                        title: "Error!",
                        text: "No se pudo eliminar el usuario.",
                        icon: "error"
                    });
                }
            });
        }
    });
});
