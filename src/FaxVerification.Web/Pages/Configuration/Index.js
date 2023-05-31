$(document).ready(function () {
    $('#saveConfig').click(function () {

        debugger;
        //alert(JSON.stringify('@Model.Data'));

        var DocumnetNumber = document.getElementById("FieldsCount").value;

        data = {
            "templateName": "Invoice",
            "fields": []
        }

        for (var i = 1; i <= DocumnetNumber; i++) {
           
            var FieldName = document.getElementById("FieldName_" + i).value;
            var Regex = document.getElementById("Regex_" + i).value;

            if (FieldName != "" && Regex != "") {
                var obj = {
                    //"TemplateId":"",
                    "fieldName": FieldName,
                    "regExpression": Regex,
                    "coOrdinates": ""
                }
                data.fields.push(obj);
            }
        

        }

       

        $.ajax({
            type: 'POST',
            url: '/api/app/config',
            data: JSON.stringify(data),
            contentType: "application/json; charset=utf-8",
            success: function (result) {
                // Handle the success response
                console.log(result);
                abp.notify.success(l('Configuration Saved'));
            },
            error: function (xhr, status, error) {
                // Handle the error
                console.log(error);
                abp.notify.warn(l('Something went worng'));
            }
        });
    });


    $("#addconfig").on("click", function () {

        var DocumnetNumber = parseInt(document.getElementById("FieldsCount").value);

        var totaldiv = '<div class="d-md-flex  col-md-12" style="margin-bottom:1%">'

        var fieldname = 'FieldName_' + (DocumnetNumber + 1)
        var Regex = 'Regex_' + (DocumnetNumber + 1)

        totaldiv += ' <label id="lbl_1" asp-for="item_FieldName" class="control-label col-md-2">Attribute-' +( DocumnetNumber +1) +'</label>'

        totaldiv += ' <input asp-for="item_FieldName" id="' + fieldname +'" class="form-control col-md-2" style="width:25%" />'
         
        totaldiv += '<input asp-for="item_RegExpression" id="' + Regex +'" class="form-control col-md-2" style="width:25%;margin-left:2%" /> </div>'
        debugger;
     
        $("#dynamicHtmlList").append(totaldiv);

        document.getElementById("FieldsCount").value = (DocumnetNumber + 1);
    });
});


//Pass this kind of data to  /api/app/config


//{
//    "templateName": "Invoice",
//        "fields": [{
//            "templateId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
//            "fieldName": "Payment Reference",
//            "regExpression": "strasddfaing",
//            "coOrdinates": "12,13,14,15"
//        },
//        {
//            "templateId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
//            "fieldName": "Payment Date",
//            "regExpression": "strasddfaing",
//            "coOrdinates": "12,13,14,15"
//        }
//        ]
//}