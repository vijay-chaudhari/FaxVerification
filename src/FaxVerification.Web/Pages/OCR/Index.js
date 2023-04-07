//$(function () {

//    abp.ajax({
//        type: 'GET',
//        url: '/api/app/ocr'
//    }).then(function (result) {
//        var fileName = "./Pdf/" + result.items[2].inputPath.replace(/^.*(\\|\/|\:)/, '');
//        //var fileName = "./Pdf/20212202212158227.pdf";

//        var pdfDoc = null,
//            pageNum = 1,
//            pageRendering = false,
//            pageNumPending = null,
//            scale = 1,
//            canvas1 = document.getElementById('the-canvas'),
//            canvas = document.getElementById('rect-canvas'),
//            ctx1 = canvas1.getContext('2d');
//            ctx = canvas.getContext('2d');
//        //canvas.style.height = "800px";
//        //canvas.style.width = "600px";

//        pdfjsLib.GlobalWorkerOptions.workerSrc = "https://cdn.jsdelivr.net/npm/pdfjs-dist@2.10.377/build/pdf.worker.min.js";
//        pdfjsLib.disableWorker = true;

//        function renderPage(num) {
//            pageRendering = true;
//            // Using promise to fetch the page
//            pdfDoc.getPage(num).then(function (page) {
//                var viewport = page.getViewport({ scale: scale });

//                //var scaleX = canvas.clientWidth / viewport.clientWidth
//                canvas1.height = viewport.height;
//                canvas1.width = viewport.width;
//                canvas.height = viewport.height;
//                canvas.width = viewport.width;
//                // Render PDF page into canvas context
//                var renderContext = {
//                    canvasContext: ctx1,
//                    viewport: viewport
//                };
//                var renderTask = page.render(renderContext);

//                // Wait for rendering to finish
//                renderTask.promise.then(function () {
//                    pageRendering = false;
//                    if (pageNumPending !== null) {
//                        // New page rendering is pending
//                        renderPage(pageNumPending);
//                        pageNumPending = null;
//                    }
//                });
//            });

//            // Update page counters
//            document.getElementById('page_num').textContent = num;
//        }

//        function queueRenderPage(num) {
//            if (pageRendering) {
//                pageNumPending = num;
//            } else {
//                renderPage(num);
//            }
//        }

//        function onPrevPage() {
//            if (pageNum <= 1) {
//                return;
//            }
//            pageNum--;
//            queueRenderPage(pageNum);
//        }

//        document.getElementById('prev').addEventListener('click', onPrevPage);

//        function onNextPage() {
//            if (pageNum >= pdfDoc.numPages) {
//                return;
//            }
//            pageNum++;
//            queueRenderPage(pageNum);
//        }

//        document.getElementById('next').addEventListener('click', onNextPage);

//        document.getElementById('next').addEventListener('click', onNextPage);

//        pdfjsLib.getDocument(fileName).promise.then(function (pdfDoc_) {
//            pdfDoc = pdfDoc_;
//            document.getElementById('page_count').textContent = pdfDoc.numPages;

//            // Initial/first page rendering
//            renderPage(pageNum);
//        });



//        /////working for single page///////////////////////////////////////////////////////////////////////////////

//        //const loadingTask = pdfjsLib.getDocument("./Pdf/" + fileName);
//        //loadingTask.promise.then(pdf =>
//        //{
//        //    pdf.getPage(1).then(page =>
//        //    {
//        //        const canvas = document.getElementById('the-canvas');
//        //        const ctx = canvas.getContext('2d');
//        //        const viewport = page.getViewport({ scale: 1 });
//        //        canvas.height = viewport.height;
//        //        canvas.width = viewport.width;

//        //        page.render({
//        //            canvasContext: ctx,
//        //            viewport: viewport
//        //        });

//        document.getElementById('rect-canvas').addEventListener('mousedown', function (e) {
//            var canvasx = $(canvas).offset().left;
//            var canvasy = $(canvas).offset().top;
//            var last_mousex = last_mousey = 0;
//            var mousex = mousey = 0;
//            var mousedown = false;

//            //Mousedown
//            $(canvas).on('mousedown', function (e) {
//                last_mousex = parseInt(e.clientX - canvasx);
//                last_mousey = parseInt(e.clientY - canvasy);
//                mousedown = true;
//            });

//            //Mouseup
//            $(canvas).on('mouseup', function (e) {
//                mousedown = false;
//            });

//            //Mousemove
//            $(canvas).on('mousemove', function (e) {
//                mousex = parseInt(e.clientX - canvasx);
//                mousey = parseInt(e.clientY - canvasy);
//                if (mousedown) {
//                    ctx.clearRect(0, 0, canvas.width, canvas.height);
//                    ctx.beginPath();
//                    var width = mousex - last_mousex;
//                    var height = mousey - last_mousey;
//                    ctx.rect(last_mousex, last_mousey, width, height);
//                    ctx.strokeStyle = 'red';
//                    ctx.lineWidth = 1;
//                    ctx.stroke();
//                }
//            });
//        });
//        //    });
//        //}).catch(ex => {
//        //    console.log(ex)
//        //});
//        /////////////////////////////////////////////////////////////////////////////////////////////////////////

//    });
//});



$(function () {
    var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + 'OCR/EditModal',
       /* scriptUrl: '/Pages/OCR/EditModal.js',*/
        modalClass: 'ProductInfo'
    });

    var dataTable = $('#BooksTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            searching: false,
            scrollX: false,
            ajax: abp.libs.datatables.createAjax(faxVerification.records.ocr.getList),
            columnDefs: [
                {
                    title: "Actions",
                    rowAction: {
                        items:
                            [
                                {
                                    text: "Edit",
                                    action: function (data) {
                                        editModal.open({ id: data.record.id });
                                    }
                                },
                                {
                                    text: "Delete",
                                    confirmMessage: function (data) {
                                        return ("BookDeletionConfirmationMessage", data.record.name);
                                    },
                                    action: function (data) {
                                        //acme.bookStore.books.book
                                        //    .delete(data.record.id)
                                        //    .then(function () {
                                        //        abp.notify.info(l('SuccessfullyDeleted'));
                                        //        dataTable.ajax.reload();
                                        //    });
                                    }
                                }
                            ]
                    }
                },
                {
                    title: "FileName",
                    data: "inputPath",
                    render: function (data) {
                        return data.replace(/^.*(\\|\/|\:)/, '');
                    }
                },
                {
                    title: "Confidence",
                    data: "confidence"
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



