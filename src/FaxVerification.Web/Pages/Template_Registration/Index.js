

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
                                    },
                                    visible: function (data) {
                                        
                                        if (data.vendorNo != null && data.vendorNo != undefined && data.vendorNo != "") {
                                            
                                            return false;
                                            }
                                        else {
                                            
                                            return true;
                                        }
                                    }
                                },
                                {
                                    text: "Edit",
                                    action: function (data) {
                                        editModal.open({ id: data.record.id });
                                    },
                                    visible: function (data) {
                                        
                                        if (data.vendorNo != null && data.vendorNo != undefined && data.vendorNo != "") {
                                            
                                            return true;
                                        }
                                        else {
                                            
                                            return false;
                                        }
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
                    title: "Vendor No",
                    data: "vendorNo",
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


