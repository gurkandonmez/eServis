//Aksesuar Ekle
$(document).on("click", "#btnAksesuarEkle", async function () {

    const { value: formValues } = await Swal.fire({
        title: 'Aksesuar Ekle',
        showCancelButton: true,
        cancelButtonText: 'Kapat',
        confirmButtonText: 'Kaydet',
        width: 450,
        height: 200,
        returnFocus: true,
        backdrop: true,
        html:
            "<div class='form-group'><input id='Kod' placeholder='Aksesuar Kodu' class='form-control'></div>" +
            "<div class='form-group'><input id='Ad' placeholder='Aksesuar Adı' class='form-control'></div>" +
            "<div class='form-group'><input id='Fiyat' placeholder=' Aksesuar Fiyatı' class='form-control'></div>" +
            "<div class='form-group'><input id='TknPrim' placeholder='Tkn Prim' class='form-control'></div>" +
            "<div class='form-group'><input id='Aciklama' placeholder='Aciklama' class='form-control'></div>",

        preConfirm: () => {

            return [

                document.getElementById('Kod').value,
                document.getElementById('Ad').value,
                document.getElementById('Fiyat').value,
                document.getElementById('TknPrim').value,
                document.getElementById('Aciklama').value,

            ]
        }
    })

    $.ajax({
        url: '/Aksesuar/AksesuarEkle/',
        type: 'POST',
        dataType: 'json',
        data: { 'Kod': formValues[0], 'Ad': formValues[1], 'Fiyat': formValues[2], 'TknPrim': formValues[3], 'Aciklama': formValues[4], 'KodId': formValues[5], 'Sira':formValues[6] },
        success: function (data) {
            if (formValues[0] != null && formValues[1] != null && formValues[2] != 0) {
                var Id = data.Result.Id;
                var Sira = "<td><input type='text' name='Sira' value=1 id='" + data.Result.KodId + "' class='form-control font-weight-bold sira' /></td>"
                var Kod = "<td><a class='btn btn-block btn-sm btn-dark'>" + data.Result.Kod + " </a></td>";
                var Ad = "<td><a style ='height:30px' data-kod-id='" + data.Result.KodId + "' data-item-kod='" + data.Result.Kod + "' data-item-ad='" + data.Result.Ad + "' data-item-fiyat='" + data.Result.Fiyat + "' data-item-tkn='" + data.Result.TknPrim + "'  class=' guncelle btn btn-block btn-dark  text-white btn-sm font-weight-bold'>" + data.Result.Ad + "</a ></td>";
                var Fiyat = "<td><a href='' class='btn btn-dark btn-sm btn-block'> " + data.Result.Fiyat + " TL </td>";
                var TknPrim = "<td><a href='' class='btn btn-dark btn-sm btn-block'> % " + data.Result.TknPrim + " </td>";
                var Sil = "<td><a id='BtnDelete' data-kod-id='" + data.Result.KodId + "' data-target='#ModalSil' data-delete-id='" + data.Result.Id + "' data-item-kod='" + data.Result.Kod + "' data-item-ad='" + data.Result.Ad + "' data-item-fiyat='" + data.Result.Fiyat + "' data-item-tkn='" + data.Result.TknPrim + "' data-toggle='modal' class='btn btn-danger btn-sm full text-center font-weigt-bold' style='height:30px'>SİL</a></td>"
                $("#Aksesuar tbody ").append('<tr id="' + data.Result.KodId + '">' + Sira + Kod + Ad + Fiyat + TknPrim + Sil + '</tr>').fadeIn();
                Swal.fire({
                    icon: 'success',
                    title: 'Aksesuar Eklendi!',
                    text: 'İşlem başarıyla gerçekleştirildi.'

                })
            }
            else {
                Swal.fire({
                    icon: 'error',
                    title: 'Hata oluştu!',
                    text: 'Alanlar boş olamaz!'

                })
            }

        },
        error: function (data) {

            Swal.fire({
                icon: 'error',
                title: 'Hata oluştu!',
                text: 'Bir hata oluştu.'

            })



        }

    });
});

//Aksesuar Sil
$(document).on("click", "#BtnDelete", async function () {
    var btn = $(this);
    var id = $(btn).data("kod-id");

    var kod = $(this).data("item-kod");
    var ad = $(this).data("item-ad");
    var fiyat = $(this).data("item-fiyat");
    var tknprim = $(this).data("item-tkn");

    const { value: formValues } = await Swal.fire({
        title: 'Aksesuar Sil',
        showCancelButton: true,
        cancelButtonText: 'Kapat',
        confirmButtonText: 'Sil',
        width: 450,
        height: 200,
        returnFocus: true,
        backdrop: true,
        //html:
        html:

            "<dl class='dl-horizontal'>" +
            "<dt><b>Kod</b></dt>" +
            "<dd>" + kod + "</dd>" +
            "<dt><b>Ad</b></dt>" +
            "<dd>" + ad + "</dd>" +
            "<dt><b>Fiyat</b></dt>" +
            "<dd>" + fiyat + "</dd>" +
            "<dt><b>Tkn Prim</b></dt>" +
            "<dd>" + tknprim + "</dd>" +

            "</dl>"




    });

    $.ajax({

        url: "/Aksesuar/AksesuarSil/ " + id,
        type: "POST",
        dataType: "json",
        data: { 'Kod': formValues[0], 'Ad': formValues[1], 'Fiyat': formValues[2], 'TknPrim': formValues[3], 'Aciklama': formValues[4] },
        success: function (data) {
            if (data == 1) {


                $("#" + id).fadeOut(600);
            }
            else {

                Swal.fire({
                    icon: 'error',
                    title: 'Hata oluştu!',
                    text: 'Bir hata oluştu.'

                })

            }
        },
        error: function (data) {


        }


    })
});



//Aksesuar Guncelle
$(document).on("click", ".guncelle", async function () {
    var trid = $(this).data('kod-id');
    //var kod2 = $(this).parent('td.kod').parent('tr#' + trid).find('input#btnKod:first');

    var id = $(this).data('kod-id');
    var kod = $(this).data("item-kod");
    var ad = $(this).data("item-ad");
    var fiyat = $(this).data("item-fiyat");
    var tknprim = $(this).data("item-tkn");
    const { value: formValues } = await Swal.fire({
        title: 'Aksesuar Guncelle',
        showCancelButton: true,
        cancelButtonText: 'Kapat',
        confirmButtonText: 'Kaydet',
        width: 450,
        height: 200,
        returnFocus: true,
        backdrop: true,
        html:
            "<div class='form-group'><input id='Kod' value=" + kod + " placeholder='Aksesuar Kodu' class='form-control'></div>" +
            "<div class='form-group'><input id='Ad' value=" + ad + "  placeholder='Aksesuar Adı' class='form-control'></div>" +
            "<div class='form-group'><input id='Fiyat' value=" + fiyat + "  placeholder=' Aksesuar Fiyatı' class='form-control'></div>" +
            "<div class='form-group'><input id='TknPrim' value=" + tknprim + "  placeholder='Tkn Prim' class='form-control'></div>" +
            "<div class='form-group'><input id='Aciklama' placeholder='Aciklama' class='form-control'></div>",

        preConfirm: () => {

            return [

                document.getElementById('Kod').value,
                document.getElementById('Ad').value,
                document.getElementById('Fiyat').value,
                document.getElementById('TknPrim').value,
                document.getElementById('Aciklama').value,

            ]
        }
    })
    $.ajax({

        url: "/Aksesuar/AksesuarGuncelle/ " + id,
        type: "POST",
        dataType: "json",
        data: { 'Kod': formValues[0], 'Ad': formValues[1], 'Fiyat': formValues[2], 'TknPrim': formValues[3], 'Aciklama': formValues[4], 'Sira': formValues[5] },
        success: function (data) {


            $("#" + id).remove();
            var Id = id;
            var Sira = "<td><input type='text' name='Sira' value=" + data.Result.Sira + " class='form-control font-weight-bold sira' /></td>"
            var Kod = "<td><a class='btn btn-block btn-sm btn-dark'>" + data.Result.Kod + " </a></td>";
            var Ad = "<td><a style ='height:30px' data-kod-id='" + data.Result.KodId + "'   data-item-kod='" + data.Result.Kod + "' data-item-ad='" + data.Result.Ad + "' data-item-fiyat='" + data.Result.Fiyat + "' data-item-tkn='" + data.Result.TknPrim + "' class='guncelle btn btn-block btn-dark  text-white btn-sm font-weight-bold'>" + data.Result.Ad + "</a ></td>";
            var Fiyat = "<td><a href='' class='btn btn-dark btn-sm btn-block'> " + data.Result.Fiyat + " TL </td>";
            var TknPrim = "<td><a href='' class='btn btn-dark btn-sm btn-block'> % " + data.Result.TknPrim + " </td>";
            var Sil = "<td><a id='BtnDelete' data-kod-id='" + data.Result.KodId + "' data-target='#ModalSil' data-delete-id='" + data.Result.Id + "' data-item-kod='" + data.Result.Kod + "' data-item-ad='" + data.Result.Ad + "' data-item-fiyat='" + data.Result.Fiyat + "' data-item-tkn='" + data.Result.TknPrim + "' data-toggle='modal' class='btn btn-danger btn-sm full text-center font-weigt-bold' style='height:30px'>SİL</a></td>"
            $("#Aksesuar tbody ").append('<tr id="' + data.Result.KodId + '">' + Sira + Kod + Ad + Fiyat + TknPrim + Sil + '</tr>').fadeIn();

            Swal.fire({
                icon: 'success',
                title: 'Bilgiler Guncellendi!',
                text: 'İşlem başarıyla gerçekleştirildi'

            })




            //  $("#" + id).fadeOut(600);


        },
        error: function (data) {


            Swal.fire({
                icon: 'error',
                title: 'Hata oluştu!',
                text: 'Bir hata oluştu.'

            })



        }


    })



});


$(document).on("click","#Sirala", async function () {
    window.location.reload();

})