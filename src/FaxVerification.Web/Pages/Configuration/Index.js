$(document).ready(function () {
    $('#saveConfig').click(function () {
        $.ajax({
            type: 'POST',
            url: '/api/app/config',
            data: JSON.stringify(data),
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