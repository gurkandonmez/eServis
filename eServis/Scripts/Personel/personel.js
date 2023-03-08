
$(document).on("click", "#btnArsiv", function () {
    var formValues = $("#ListeForm").serialize();

    var btn = $(this);
    var id = $(btn).data("sil-id");
    $.ajax({
        url: '/Personel/Arsiv/' + id,
        type: 'POST',
        dataType: 'json',
        data: formValues,
        success: function (data) {
            if (data != null && data != '0') {
                var Id = data.Result.KimlikNo;
                var Ad = "<td>" + data.Result.Ad + "</td>";
                var Soyad = "<td>" + data.Result.Soyad + "</td>";
                var Arsiv = "<td>  <button id='btnArsivCikar' data-sil-id='" + data.Result.KimlikNo + "' type='button' style='background-color:#000 !important; color:#ffffff' class='btn btn-sm'>Arşivden Çıkar</button> </td>"

                var Sil = "<td>  <button id='Sil' data-sil-id='" + data.Result.KimlikNo + "' type='button' style='background-color:#f12b0a !important; color:#ffffff' class='btn btn-sm'>Sil</button> </td>"
                $("#arsiv ").append('<tr id="' + data.Result.KimlikNo + '">' + Ad + Soyad + Arsiv + Sil + '</tr>').fadeIn();
                Swal.fire({
                    icon: 'success',
                    title: 'Personel  Arşive Eklendi!',
                    text: 'İşlem başarıyla gerçekleştirildi.'

                })
                window.location = "/Personel/Index/", 7000;
            }


        }
    });
});

$(document).on("click", "#btnArsivCikar", function () {
    var formValues = $("#ListeForm").serialize();
    var btn = $(this);
    var id = $(btn).data("sil-id");
    $.ajax({
        url: '/Personel/ArsivCikar/' + id,
        type: 'POST',
        dataType: 'json',
        data: formValues,
        success: function (data) {
            if (data != null && data != '0') {
                $("#" + id).fadeOut(600);
                Swal.fire({
                    icon: 'success',
                    title: 'Personel Başarıyla Arşivden Çıkarıldı!',
                    text: 'İşlem başarıyla gerçekleştirildi.'

                })
                window.location = "/Personel/Index/", 7000;
            }


        }
    });
})
$(document).on("click", "#btnSil", function () {
    var formValues = $("#ListeForm").serialize();

    var btn = $(this);
    var id = $(btn).data("sil-id");
    $.ajax({
        url: '/Personel/PersonelSil/' + id,
        type: 'POST',
        dataType: 'json',
        data: formValues,
        success: function (data) {
            if (data != null && data != '0') {
                Swal.fire({
                    icon: 'success',
                    title: 'Tebrikler!',
                    text: 'İşlem başarıyla gerçekleştirildi.'

                })
                window.location = "/Personel/Index", 7000;
            }
        }
    })
})