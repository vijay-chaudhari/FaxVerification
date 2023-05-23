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
            var obj = {
                //"TemplateId":"",
                "fieldName": FieldName,
                "regExpression": Regex,
                "coOrdinates": ""
            }
            data.fields.push(obj);

        }

       

        $.ajax({
            type: 'POST',
            url: '/api/app/config',
            data: JSON.stringify(data),
            contentType: "application/json; charset=utf-8",
            success: function (result) {
                // Handle the success response
                console.log(result);
            },
            error: function (xhr, status, error) {
                // Handle the error
                console.log(error);
            }
        });
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