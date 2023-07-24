$(document).ready(function () {
    $('#saveConfig').click(function () {

        debugger;
        //alert(JSON.stringify('@Model.Data'));

        var DocumnetNumber = document.getElementById("FieldsCount").value;
        var TemplateName = document.getElementById("TemplateName").value;

        data = {
            "TemplateName": "Invoice",
            "Fields": []
        }

        for (var i = 1; i <= DocumnetNumber; i++) {
           
            var FieldName = document.getElementById("FieldName_" + i).value;
            var Regex = document.getElementById("Regex_" + i).value;

            if (FieldName != "") {
                var obj = {
                    //"TemplateId":"",
                    "FieldName": FieldName,
                    "RegExpression": Regex,
                    "CoOrdinates": ""
                }
                data.Fields.push(obj);
            }
        

        }


        //const fs = require('fs');

        ////let data = "Learning how to write in a file."

        //var file = window.location.href + "/Configuration/Config.json";


        //// Write data in 'Output.txt' .
        //fs.writeFile(file, data, (err) => {

        //    // In case of a error throw err.
        //    if (err) throw err;
        //})

        var req = { "userID": JSON.stringify(data), "documentID": TemplateName }

        $.ajax({
            type: 'POST',
            url: '/api/app/config/save-config-file',
            data: JSON.stringify(req),
            contentType: "application/json; charset=utf-8",
            success: function (result) {
                // Handle the success response
                console.log(result);
                abp.notify.success('Configuration Saved');
            },
            error: function (xhr, status, error) {
                // Handle the error
                console.log(error);
                abp.notify.warn('Something went worng');
            }
        });





        var USerID = "181003CD-F98A-B44A-17B5-3A0BACE2050C";


        $.ajax({
            type: 'POST',
            url: '/api/app/config',
            data: JSON.stringify(data),
            contentType: "application/json; charset=utf-8",
            success: function (result) {
                // Handle the success response
                console.log(result);
                abp.notify.success('Configuration Saved');
            },
            error: function (xhr, status, error) {
                // Handle the error
                console.log(error);
                abp.notify.warn('Something went worng');
            }
        });

        debugger;

        $.ajax({
            type: 'PUT',
            url: '/api/app/config/' + USerID,
            data: JSON.stringify(data),
            contentType: "application/json; charset=utf-8",
            success: function (result) {
                // Handle the success response
                console.log(result);
                //abp.notify.success('Configuration Saved');
            },
            error: function (xhr, status, error) {
                // Handle the error
                console.log(error);
                //abp.notify.warn('Something went worng');
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





    function CreateDynamicTable(rows, columns, headerLabels) {

        // Create the table element
        var table = document.createElement('table');

        // Create table head
        var thead = document.createElement('thead');

        // Create header row
        var headerRow = document.createElement('tr');

        // Create header columns
        for (var i = 0; i < columns; i++) {
            var headerCell = document.createElement('th');
            headerCell.textContent = headerLabels[i] || '';

            headerCell.classList.add("TableBorder");


            headerRow.appendChild(headerCell);
        }

        thead.style.backgroundColor = "#d9d5cc";
        // Append header row to the table head
        thead.appendChild(headerRow);

        // Append table head to the table
        table.appendChild(thead);

        // Create table body
        var tbody = document.createElement('tbody');

        // Create rows
        for (var j = 0; j < rows; j++) {
            var row = document.createElement('tr');

            // Create columns
            for (var k = 0; k < columns; k++) {
                var cell = document.createElement('td');
                cell.innerHTML = ' <input id="R_' + j + '_C_' + k + ' " class="form-control" />';
                cell.classList.add("TableBorder");
                row.appendChild(cell);
            }

            // Append row to the table body
            tbody.appendChild(row);
        }

        // Append table body to the table
        table.appendChild(tbody);


        table.classList.add("TableBorder");

        // Return the generated table
        return table;








    }




    var headerLabels = ['Item', 'Quantity', 'Rate', 'Amount'];
    var dynamicTable = CreateDynamicTable(3, 4, headerLabels);

    // Add the table to the document body
    document.getElementById("DynamicTable").appendChild(dynamicTable);

    $("#addcolumn").click(function () {

        var columncount = $('#DynamicTable th').length;
        var rowCount = $('#DynamicTable tr').length;

        nextCoumn = parseInt(columncount) + 1;
        headerLabels.push("Header " + nextCoumn);
        var dynamicTable = CreateDynamicTable(rowCount, nextCoumn, headerLabels);
        $('#DynamicTable').empty();
        document.getElementById("DynamicTable").appendChild(dynamicTable);
    });

    $("#addrow").click(function () {

        debugger
        var columncount = $('#DynamicTable th').length;
        var rowCount = $('#DynamicTable tr').length;
        nextRow = parseInt(rowCount);
        var dynamicTable = CreateDynamicTable(nextRow, columncount, headerLabels);
        $('#DynamicTable').empty();
        document.getElementById("DynamicTable").appendChild(dynamicTable);
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