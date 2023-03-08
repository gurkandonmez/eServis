$(function () {

    $("#ModalAdres").on("show.bs.modal", function (e) {


        $("#ModalAdres_body").load("/Adres/Ekle/");

    })

    $("#ModalAdresDuzenle").on("show.bs.modal", function (e) {

        var btn = $(e.relatedTarget);
        var id = btn.data("modal-id")

        $("#ModalAdresDuzenle_body").load("/Adres/Details/" + id);

    })
    $("#ModalAdresSil").on("show.bs.modal", function (e) {

        var btn = $(e.relatedTarget);
        var id = btn.data("delete-id")

        $("#ModalAdresSil_body").load("/Adres/Delete/" + id);

    })


})

$(function () {

    $("#btnSave").click(function () {
        var ListeFormData = $("#ListeForm").serialize();

        $.ajax({
            type: "POST",
            url: "/Adres/Ekle/",
            data: ListeFormData,
            success: function (response) {
                //   window.location.reload();
                if (response == false) {
                    alert("Alanlari bos birakmayiniz!");
                }
            }
        })

    })


    $("#btnUpdate").click(function () {
        var ListeFormData = $("#ListeForm").serialize();
        var btn = $(e.relatedTarget);
        var id = btn.data("modal-id")

        $.ajax({
            type: "POST",
            url: "/Adres/Details/" + id,
            data: ListeFormData,
            success: function () {
                window.location.reload();

            }
        })

    })
})