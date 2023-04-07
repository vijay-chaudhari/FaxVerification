abp.modals.ProductInfo = function () {
    function initModal(modalManager, args) {
        //var $modal = modalManager.getModal();
        //var $form = modalManager.getForm();
        pdfjsLib.GlobalWorkerOptions.workerSrc = "https://cdn.jsdelivr.net/npm/pdfjs-dist@2.10.377/build/pdf.worker.min.js";
        pdfjsLib.disableWorker = true;

        let fileName = "./Pdf/" + document.querySelector("#Data_FilePath").value;
        //let fileName = "./Pdf/Invoice_153494.pdf"

        let pdfDoc = null,
            pageNum = 1,
            pageRendering = false,
            pageNumPending = null,
            scale = 1,
            pdfViewerCanvas = document.getElementById('pdfViewer'),
            drawingCanvas = document.getElementById('drawCanvas'),
            pdfViewerCanvasContext = pdfViewerCanvas.getContext('2d'),
            drawingCanvasContext = drawingCanvas.getContext('2d');

        let startX, startY, endX, endY;
        let activeElement;
        const pdfViewerCanvasWidth = pdfViewerCanvas.width;
        const pdfViewerCanvasHeight = pdfViewerCanvas.height;
        let pdfFactory = undefined;
        


        pdfjsLib.getDocument(fileName).promise.then((pdfDoc_) => {
            pdfDoc = pdfDoc_;
            document.getElementById('page_count').textContent = pdfDoc.numPages;

            // Initial/first page rendering
            renderPage(pageNum);
        });

        function renderPage(num) {
            pageRendering = true;
            // Using promise to fetch the page
            pdfDoc.getPage(num).then(function (page) {
                let vp = page.getViewport({ scale });
                let scaleRatio = Math.min(pdfViewerCanvasWidth / vp.width, pdfViewerCanvasHeight / vp.height);
                var viewport = page.getViewport({ scale: scaleRatio  });

                pdfViewerCanvasContext.imageSmoothingEnabled = false;

                pdfViewerCanvas.width = vp.width * scaleRatio;
                pdfViewerCanvas.height = vp.height * scaleRatio;

                pdfViewerCanvasContext.scale(scale, scale)

                //pdfViewerCanvas.height = viewport.height
                //pdfViewerCanvas.width = viewport.width;
                //drawingCanvas.height = viewport.height;
                //drawingCanvas.width = viewport.width;


                // Render PDF page into canvas context
                var renderContext = {
                    canvasContext: pdfViewerCanvasContext,
                    viewport: viewport,
                    enableHighResolution: true
                };
                var renderTask = page.render(renderContext);

                // Wait for rendering to finish
                renderTask.promise.then(function () {
                    pageRendering = false;
                    if (pageNumPending !== null) {
                        // New page rendering is pending
                        renderPage(pageNumPending);
                        pageNumPending = null;
                    }
                });
            });

            // Update page counters
            document.getElementById('page_num').textContent = num;
        }

        function queueRenderPage(num) {
            if (pageRendering) {
                pageNumPending = num;
            } else {
                renderPage(num);
            }
        }

        function onPrevPage() {
            if (event.target.tagName === "BUTTON") {
                event.preventDefault();
                if (pageNum <= 1) {
                    return;
                }
                pageNum--;
                queueRenderPage(pageNum);
            }
        }

        document.getElementById('prev').addEventListener('click', onPrevPage);

        function onNextPage() {

            if (event.target.tagName === "BUTTON") {
                event.preventDefault();
                if (pageNum >= pdfDoc.numPages) {
                    return;
                }
                pageNum++;
                queueRenderPage(pageNum);
            }

        }

        document.getElementById('next').addEventListener('click', onNextPage);

        drawingCanvas.addEventListener('mousedown', function (e) {
            var canvasx = $(pdfViewerCanvas).offset().left;
            var canvasy = $(pdfViewerCanvas).offset().top;
            var last_mousex = last_mousey = 0;
            var mousex = mousey = 0;
            var mousedown = false;

            //Mousedown
            // $(drawingCanvas).on('mousedown', function (e) {
            last_mousex = startX = parseInt(e.clientX - canvasx);
            last_mousey = startY = parseInt(e.clientY - canvasy);
            mousedown = true;

            //});

            //Mouseup
            $(drawingCanvas).on('mouseup', function (e) {

                mousedown = false;
                getImage();
                //pdfDoc.getData().then((data) => {

                //    pdfFactory = new pdfAnnotationFactory(data)
                //    pdfFactory.createHighlightAnnotation(pageNum,)
                //})
            });

            //Mousemove
            $(drawingCanvas).on('mousemove', function (e) {
                mousex = parseInt(e.clientX - canvasx);
                mousey = parseInt(e.clientY - canvasy);
                if (mousedown) {
                    drawingCanvasContext.clearRect(0, 0, drawingCanvas.width, drawingCanvas.height);
                    drawingCanvasContext.beginPath();
                    var width = mousex - last_mousex;
                    var height = mousey - last_mousey;
                    drawingCanvasContext.rect(last_mousex, last_mousey, width, height);
                    drawingCanvasContext.strokeStyle = 'red';
                    drawingCanvasContext.lineWidth = 1;
                    drawingCanvasContext.stroke();
                    endX = mousex;
                    endY = mousey;
                }
            });



        });
        //let selectionCoordinates = function () {
        //    let rec = window.getSelection().getRangeAt(0).getBoundingClientRect()
        //    let ost = computePageOffset()
        //    let x_1 = rec.x - ost.left
        //    let y_1 = rec.y - ost.top
        //    let x_2 = x_1 + rec.width
        //    let y_2 = y_1 + rec.height

        //    let x_1_y_1 = pdfViewer._pages[pageNum].viewport.convertToPdfPoint(x_1, y_1)
        //    x_1 = x_1_y_1[0]
        //    y_1 = x_1_y_1[1]
        //    let x_2_y_2 = pdfViewer._pages[pageNum].viewport.convertToPdfPoint(x_2, y_2)
        //    x_2 = x_2_y_2[0]
        //    y_2 = x_2_y_2[1]
        //    return [x_1, y_1, x_2, y_2]
        //}
        //let computePageOffset = function () {
        //    var rect = pg.getBoundingClientRect(), bodyElt = document.body;
        //    return {
        //        top: rect.top + bodyElt.scrollTop,
        //        left: rect.left + bodyElt.scrollLeft
        //    }
        //}


        function getImage() {
            const rectangleWidth = Math.abs(endX - startX);
            const rectangleHeight = Math.abs(endY - startY);
            const rectangleX = Math.min(startX, endX);
            const rectangleY = Math.min(startY, endY);


            var newCanvas = document.createElement('canvas');
            newCanvas.width = rectangleWidth;
            newCanvas.height = rectangleHeight;
            var newCtx = newCanvas.getContext('2d');
            newCtx.drawImage(pdfViewerCanvas, rectangleX, rectangleY, rectangleWidth, rectangleHeight, 0, 0, rectangleWidth, rectangleHeight);
            var imageData = newCanvas.toDataURL('image/tiff');
            getText(imageData);

            //newCtx.putImageData(imageData, 0, 0);
            //var base64Data = newCanvas.toDataURL('image/tiff');

            //var rect = pdfViewerCanvas.getBoundingClientRect();
            //var x = rect.left;
            //var y = rect.top;
            //var width = rect.width;
            //var height = rect.height;

            //console.log(rect);

            //var newCanvas = document.createElement('canvas');
            //newCanvas.width = width;
            //newCanvas.height = height;
            //var newCtx = newCanvas.getContext('2d');


            //newCtx.drawImage(drawingCanvas, x, y, width, height, 0, 0, width, height);
            //var imageData = newCanvas.toDataURL('image/tiff');

            //// Convert the base64-encoded string to a binary TIFF file
            //console.log(imageData);
            //var arrayBuffer = base64ToArrayBuffer(imageData.replace(/^data:image\/(tiff|png);base64,/, ''));
            //var tiff = new Tiff({ buffer: arrayBuffer });

            //// Save the TIFF file to disk
            //var blob = new Blob([tiff.toBuffer()], { type: 'image/tiff' });
            //saveAs(blob, 'selected-area.tiff');

        };

        function getText(image) {
            Tesseract.recognize(image, 'eng').then(function (result) {
                setText(result.data.text);
            });
        };

        function setText(text) {
            activeElement.value = text;
        };

        const inputs = document.querySelectorAll('input');

        inputs.forEach(input => {
            input.addEventListener('blur', () => {
                activeElement = input;
            });
        });

        //document.getElementById('form-content').addEventListener('onblur', function (e) {
        //    console.log('in abpFormContentOnblur');
        //    console.log(activeElement);
        //    var element = document.activeElement;
        //    if (element.tagName === 'INPUT' || element.tagName === 'TEXTAREA') {
        //        console.log(activeElement);
        //        activeElement = element;
        //    }
        //});
    };
    return {
        initModal: initModal
    };
};



//function getImage() {
//    var rect = pdfViewerCanvas.getBoundingClientRect();
//    var x = rect.left;
//    var y = rect.top;
//    var width = rect.width;
//    var height = rect.height;

//    console.log(rect);

//    var newCanvas = document.createElement('canvas');
//    newCanvas.width = width;
//    newCanvas.height = height;
//    var newCtx = newCanvas.getContext('2d');


//    newCtx.drawImage(drawingCanvas, x, y, width, height, 0, 0, width, height);
//    var imageData = newCanvas.toDataURL('image/tiff');

//    // Convert the base64-encoded string to a binary TIFF file
//    console.log(imageData);
//    //var arrayBuffer = base64ToArrayBuffer(imageData.replace(/^data:image\/(tiff|png);base64,/, ''));
//    //var tiff = new Tiff({ buffer: arrayBuffer });

//    //// Save the TIFF file to disk
//    //var blob = new Blob([tiff.toBuffer()], { type: 'image/tiff' });
//    //saveAs(blob, 'selected-area.tiff');

//}

//function base64ToArrayBuffer(base64) {
//    var binaryString = atob(base64);
//    var len = binaryString.length;
//    var bytes = new Uint8Array(len);
//    for (var i = 0; i < len; i++) {
//        bytes[i] = binaryString.charCodeAt(i);
//    }
//    return bytes.buffer;
//}