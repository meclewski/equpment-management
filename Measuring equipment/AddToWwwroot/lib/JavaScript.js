//DataTables

$(document).ready(function () {
    $("#dt_device").DataTable({
        "responsive": true,
        "processing": true, // for show progress bar
        "serverSide": true, // for process server side

        "orderMulti": false, // for disable multiple column at once
        "ajax": {
            "url": "/Admin/LoadData",
            "type": "POST",
            "datatype": "json"
        },
        columnDefs: [{
            targets: [4, 5, 7], render: function (data) {
                if (data != null)
                    return moment(data).format('YYYY-MM-DD');
                else return ('-');
            }
        },
        {
            targets: [1], render: function (data) {
                var len = data.toString().length;
                if (len == 1) return ('0000' + data);
                if (len == 2) return ('000' + data);
                if (len == 3) return ('00' + data);
                else return ('0' + data);
            }
        }


        ],

        "columns": [
            { "data": "DeviceId", "className": "never", "autoWidth": true },
            { "data": "RegistrationNo", "name": "RegistrationNo", "autoWidth": true },
            { "data": "InventoryNo", "name": "InventoryNo", "autoWidth": true },
            { "data": "SerialNo", "className": "none", "autoWidth": true },
            { "data": "VerificationDate", "name": "VerificationDate", "autoWidth": true },
            { "data": "TimeToVerification", "name": "TimeToVerification", "autoWidth": true },
            { "data": "VerificationResult", "name": "VerificationResult", "autoWidth": true },
            { "data": "ProductionDate", "name": "ProductionDate", "autoWidth": true },
            { "data": "DeviceDesc", "className": "never", "autoWidth": true },
            { "data": "DeviceName", "name": "DeviceName", "autoWidth": true },
            { "data": "TypeName", "name": "TypeName", "autoWidth": true },

            {
                "render": function (data, type, full, meta) { return '<a class="btn btn-sm btn-info" href="/Admin/Edit?deviceId=' + full.DeviceId + '">Edycja</a>'; }
            }
        ],
        "language": {
            "lengthMenu": "Wyświetl _MENU_ rekordów na stronę",
            "zeroRecords": "Przepraszamy, nic nie znaleziono",
            "info": "Wyświetlana strona _PAGE_ z _PAGES_",
            "infoEmpty": "Brak dostępnych rekordów",
            "infoFiltered": "(Odfiltrowane z _MAX_ wszystkich rekordów)",
            "search": "Wyszukaj",
            "paginate" : {
                "previous": "Poprzednia",
                "next": "Następna"
            }
        }
    });

});

$(document).ready(function () {
    $("#dt_type").DataTable({
        "responsive": true,
        "processing": true, // for show progress bar
        "serverSide": true, // for process server side

        "orderMulti": false, // for disable multiple column at once
        "ajax": {
            "url": "/Type/LoadData",
            "type": "POST",
            "datatype": "json"
        },

        "columns": [
            { "data": "TypeId", "className": "never", "autoWidth": true },
            { "data": "TypeName", "name": "TypeName", "autoWidth": true },
            { "data": "DeviceName", "name": "DeviceName", "autoWidth": true },
            { "data": "ValidityPierod", "name": "ValidityPierod", "autoWidth": true },
            { "data": "Price", "name": "Price", "autoWidth": true },
            { "data": "Image", "className": "never", "autoWidth": true },
            { "data": "TypeDesc", "name": "TypeDesc", "autoWidth": true },
            { "data": "ProducerId", "className": "never", "autoWidth": true },
            { "data": "LaboratoryId", "className": "never", "autoWidth": true },
            { "data": "VerificationId", "className": "never", "autoWidth": true },
            {
                "render": function (data, type, full, meta) { return '<a class="btn btn-sm btn-info" href="/Type/Edit?typeId=' + full.TypeId + '">Edycja</a>'; }
            }
        ],
        "language": {
            "lengthMenu": "Wyświetl _MENU_ rekordów na stronę",
            "zeroRecords": "Przepraszamy, nic nie znaleziono",
            "info": "Wyświetlana strona _PAGE_ z _PAGES_",
            "infoEmpty": "Brak dostępnych rekordów",
            "infoFiltered": "(Odfiltrowane z _MAX_ wszystkich rekordów)",
            "search": "Wyszukaj",
            "paginate": {
                "previous": "Poprzednia",
                "next": "Następna"
            }
        }
    });

});

$(document).ready(function () {
    $("#dt_users").DataTable({
        "responsive": true,
        "language": {
            "lengthMenu": "Wyświetl _MENU_ rekordów na stronę",
            "zeroRecords": "Przepraszamy, nic nie znaleziono",
            "info": "Wyświetlana strona _PAGE_ z _PAGES_",
            "infoEmpty": "Brak dostępnych rekordów",
            "infoFiltered": "(Odfiltrowane z _MAX_ wszystkich rekordów)",
            "search": "Wyszukaj",
            "paginate": {
                "previous": "Poprzednia",
                "next": "Następna"
            }
        }
        
    });
}); 

$(document).ready(function () {
    $("#dt_list").DataTable({
        "responsive": true,
        "order": [[7, "asc"]],
        "language": {
            "lengthMenu": "Wyświetl _MENU_ rekordów na stronę",
            "zeroRecords": "Przepraszamy, nic nie znaleziono",
            "info": "Wyświetlana strona _PAGE_ z _PAGES_",
            "infoEmpty": "Brak dostępnych rekordów",
            "infoFiltered": "(Odfiltrowane z _MAX_ wszystkich rekordów)",
            "search": "Wyszukaj",
            "paginate": {
                "previous": "Poprzednia",
                "next": "Następna"
            }
        }

    });
});

$(document).ready(function () {
    $("#dt_rest").DataTable({
        "responsive": true,
        "language": {
            "lengthMenu": "Wyświetl _MENU_ rekordów na stronę",
            "zeroRecords": "Przepraszamy, nic nie znaleziono",
            "info": "Wyświetlana strona _PAGE_ z _PAGES_",
            "infoEmpty": "Brak dostępnych rekordów",
            "infoFiltered": "(Odfiltrowane z _MAX_ wszystkich rekordów)",
            "search": "Wyszukaj",
            "paginate": {
                "previous": "Poprzednia",
                "next": "Następna"
            }
        }

    });
});

//Data loading by ajax

$(document).ready(function () {
    $("#TypeId").change(function () {
        var typeId = $("#TypeId").val();
        var now = new Date($("#dtp3").find("input").val());

        $.ajax({
            data: { typeId: typeId },
            type: 'GET',
            url: '/Admin/GetData/',
            dataType: 'json',
            success: function (result) {
                var time = result.ValidityPierod;
                var d = result.DeviceName;
                var v = result.ValidityPierod;
                now.setMonth(now.getMonth() + time);
                now.setDate(now.getDate() - 1);
                $("#DeviceName").val(d);
                $("#ValidityPierod").val(v);
                $("#TimeToVerification").val(now.getFullYear() + "-" + ("0" + (now.getMonth() + 1)).slice(-2) + "-" + ("0" + now.getDate()).slice(-2));
            }
        }),
            $.ajax({
                data: { typeId: typeId },
                type: 'GET',
                url: '/Admin/GetDataProd/',
                dataType: 'json',
                success: function (result) {
                    var s = result.ProducerName;
                    $("#ProducerName").val(s);
                }
            }),
            $.ajax({
                data: { typeId: typeId },
                type: 'GET',
                url: '/Admin/GetDataVer/',
                dataType: 'json',
                success: function (result) {
                    var s = result.VerificationName;
                    $("#VerificationName").val(s);
                }
            }),
            $.ajax({
                data: { typeId: typeId },
                type: 'GET',
                url: '/Admin/GetDataLab/',
                dataType: 'json',
                success: function (result) {
                    var s = result.LaboratoryName;
                    $("#LaboratoryName").val(s);
                }
            });

    });

    
    $("#PlaceId").change(function () {
        var placeId = $("#PlaceId").val();

        $.ajax({
            data: { placeId: placeId },
            type: 'GET',
            url: '/Admin/GetDataPlace/',
            dataType: 'json',
            success: function (result) {
                var s = result.DepartmentName;
                $("#DepartmentName").val(s);
            }
        });
    });   
    

    $("#dtp3").on("change.datetimepicker", function () {
        var typeId = $("#TypeId").val();
        var now = new Date($("#dtp3").find("input").val());
        if (typeId > 0) {
            $.ajax({
                data: { typeId: typeId },
                type: 'GET',
                url: '/Admin/GetData/',
                dataType: 'json',
                success: function (result) {
                    var time = result.ValidityPierod;
                    var d = result.DeviceName;
                    now.setMonth(now.getMonth() + time);
                    now.setDate(now.getDate() - 1);
                    $("#DeviceName").val(d);
                    $("#TimeToVerification").val(now.getFullYear() + "-" + ("0" + (now.getMonth() + 1)).slice(-2) + "-" + ("0" + now.getDate()).slice(-2));
                }
            });
        }
    });
});

//DateTimePicker

$(document).ready(function () {
    $('.dtp').datetimepicker({
        format: 'YYYY-MM-DD',
        ignoreReadonly: true
    });


});
