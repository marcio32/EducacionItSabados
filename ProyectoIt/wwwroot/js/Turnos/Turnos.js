$(function () {
    $('#turnoTable').DataTable({
        language: {
            url: '//cdn.datatables.net/plug-ins/2.3.2/i18n/es-ES.json',
        },
    });
});

$('.addTurnoBtn').click(function () {
    $.ajax({
        url: '/Turnos/TurnosPartial',
        type: 'GET',
        success: function (data) {
            $('#TurnoContent').html(data);
            var modal = new bootstrap.Modal($('#editTurnoModal'));
            modal.show();

            $('#turnoForm').submit(function (e) {
                e.preventDefault();
                debugger
                var formData = new FormData(this);
                $.ajax({
                    url: 'Turnos/TurnosPartial',
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

$('.editTurnoBtn').click(function () {
    var turnoId = $(this).data("turno-id");
    $.ajax({
        url: '/Turnos/TurnoPartial?id=' + turnoId,
        type: 'GET',
        success: function (data) {

            $('#TurnoContent').html(data);
            var modal = new bootstrap.Modal($('#editTurnoModal'));
            modal.show();

            $('#turnoForm').submit(function (e) {
                e.preventDefault();

                var formData = new FormData(this);
                $.ajax({
                    url: 'Turnos/TurnosPartial',
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

$('.deleteTurnoBtn').click(function () {
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
            var turnoId = $(this).data("turno-id");
            $.ajax({
                url: deleteTurnoUrl,
                type: 'DELETE',
                headers: {
                    "RequestVerificationToken": antiForgeryToken
                },
                contentType: "application/json",
                data: JSON.stringify({ id: turnoId }),
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
