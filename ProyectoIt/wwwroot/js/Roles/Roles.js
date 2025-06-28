$(function () {
    $('#rolTable').DataTable({
        language: {
            url: '//cdn.datatables.net/plug-ins/2.3.2/i18n/es-ES.json',
        },
    });
})

$('.addRolBtn').click(function () {
    $.ajax({
        url: '/Roles/RolPartial',
        type: 'GET',
        success: function (data) {
            $('#RolContent').html(data);
            var modal = new bootstrap.Modal($('#editRolModal'));
            modal.show();

            $('#rolForm').submit(function (e) {
                e.preventDefault();
                debugger
                var formData = new FormData(this);
                $.ajax({
                    url: 'Roles/RolPartial',
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
                            text: "No se pudo eliminar el rol.",
                            icon: "error"
                        });
                    }
                });
            })

        }
    });
});

$('.editRolBtn').click(function () {
    var rolId = $(this).data("rol-id");
    $.ajax({
        url: '/Roles/RolPartial?id=' + rolId,
        type: 'GET',
        success: function (data) {

            $('#RolContent').html(data);
            var modal = new bootstrap.Modal($('#editRolModal'));
            modal.show();

            $('#rolForm').submit(function (e) {
                e.preventDefault();

                var formData = new FormData(this);
                $.ajax({
                    url: 'Roles/RolPartial',
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
                            text: "No se pudo eliminar el rol.",
                            icon: "error"
                        });
                    }
                });
            })
        }
    });
});

$('.deleteRolBtn').click(function () {
    Swal.fire({
        title: "Estas Seguro?",
        text: "Vas a eliminar al rol",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Eliminar",
        cancelButtonText: "Cancelar"
    }).then((result) => {
        debugger
        if (result.isConfirmed) {
            var rolId = $(this).data("rol-id");
            $.ajax({
                url: deleteRolUrl,
                type: 'DELETE',
                headers: {
                    "RequestVerificationToken": antiForgeryToken
                },
                contentType: "application/json",
                data: JSON.stringify({ id: rolId }),
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
                        text: "No se pudo eliminar el rol.",
                        icon: "error"
                    });
                }
            });
        }
    });
});
