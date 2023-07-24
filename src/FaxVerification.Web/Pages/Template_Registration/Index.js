

$(function () {
    var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + 'Template_Registration/Registration',
        /* scriptUrl: '/Pages/OCR/EditModal.js',*/
        modalClass: 'ProductInfo'
    });

    //function ISAssign(data) {
    //    debugger;
    //    if (data.record.assignDocument) {

    //    }

    //}
    $('#cancelModal').click(function () {

        editModal.close();
    });

    var dataTable = $('#BooksTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            searching: false,
            scrollX: false,
            ajax: abp.libs.datatables.createAjax(faxVerification.templateRegistration.registration.getList),
            columnDefs: [
                {
                    title: "Actions",
                    rowAction: {
                        items:
                            [

                              
                                {
                                    text: "Register",
                                    action: function (data) {


                                       
                                                editModal.open({ id: data.record.id });
                                    
                                    }
                                }
                            ]
                    }
                },
                {
                    title: "FileName",
                    data: "filePath",
                    render: function (data) {
                        return data.replace(/^.*(\\|\/|\:)/, '');
                    }
                },
                {
                    title: "Vendor Name",
                    data: "vendorName",
                    render: function (data) {
                        return data == null ? "" : data;
                    }
                },
                //{
                //    title: "Output",
                //    data: "output"
                //},
                //{
                //    title: "Name",
                //    data: "output",
                //    render: function (data) {
                //        var obj = JSON.parse(data);
                //        return obj.Name
                //    }
                //},
                //{
                //    title: "BirthDate",
                //    data: "output",
                //    render: function (data) {
                //        var obj = JSON.parse(data);
                //        return obj.BirthDate
                //    }
                //}
            ]
        })
    );

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });


});


