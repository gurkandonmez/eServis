$(function () {
    var ListeFormData = $("#ListeForm").serialize();

    $("#ModalEkle").on("show.bs.modal", function (e) {
       

        $("#ModalEkle_body").load("/AdresTanim/Ekle/" + id);

    })
    $("#ModalDuzenle").on("show.bs.modal", function (e) {

        var btn = $(e.relatedTarget);
        var id = btn.data("duzenle-id")
        $("#ModalDuzenle_body").load("/AdresTanim/Duzenle/" + id);

    })
    $("#ModalSil").on("show.bs.modal", function (e) {
        var btn = $(e.relatedTarget);
        var id = btn.data("delete-id")

        $("#ModalSil_body").load("/AdresTanim/Delete/" + id);

    })
})

$(function () {

    $("#btnSirala").click(function () {
        var id = @ViewBag.Id
        $.ajax({
            type: "POST",
            url: "/AdresTanim/Sirala/" + id,
            data: ListeFormData,
            success: function () {
                //  window.location.reload();

            }
        })
    })

    $("#btnSave").click(function () {
        var ListeFormData = $("#ListeForm").serialize();
        $.ajax({
            type: "POST",
            url: "/AdresTanim/Ekle/" + id,
            data: ListeFormData,
            success: function () {
                window.location.reload();

            }
        })

    })



})