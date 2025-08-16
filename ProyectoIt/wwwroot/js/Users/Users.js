$(function () {
    $('#userTable').DataTable({
        language: {
            url: '//cdn.datatables.net/plug-ins/2.3.2/i18n/es-ES.json',
        },
    });
});

$('.addUserBtn').click(function () {
    $.ajax({
        url: '/Users/UsersPartial',
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
                    url: 'Users/UsersPartial',
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
                        } else {
                            modal.hide();
                            Swal.fire({
                                title: "Error!",
                                text: data.message,
                                icon: "error"
                            });
                        }
                    },
                    error: function (xhr, status, error) {
                        modal.hide();
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
        url: '/Users/UsersPartial?id=' + userId,
        type: 'GET',
        success: function (data) {

            $('#UserContent').html(data);
            var modal = new bootstrap.Modal($('#editUserModal'));
            modal.show();

            $('#userForm').submit(function (e) {
                e.preventDefault();

                var formData = new FormData(this);
                $.ajax({
                    url: 'Users/UsersPartial',
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
                        } else {
                            modal.hide();
                            Swal.fire({
                                title: "Error!",
                                text: data.message,
                                icon: "error"
                            });
                        }
                    },
                    error: function (xhr, status, error) {
                        modal.hide();
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
                        location.reload();
                    } else {
                        modal.hide();
                        Swal.fire({
                            title: "Error!",
                            text: data.message,
                            icon: "error"
                        });
                    }
                },
                error: function (xhr, status, error) {
                    modal.hide();
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
