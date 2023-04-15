abp.modals.ProductInfo = function () {
    function initModal(modalManager, args) {
        //var $modal = modalManager.getModal();
        //var $form = modalManager.getForm();
        pdfjsLib.GlobalWorkerOptions.workerSrc = "https://cdn.jsdelivr.net/npm/pdfjs-dist@2.10.377/build/pdf.worker.min.js";
        pdfjsLib.disableWorker = true;
        let fileName = "./Pdf/" + document.querySelector("#Data_FilePath").value;
        let pdfDoc = null,
            pdfFactory = null,
            viewp = null,
            pageNum = 1,
            pageRendering = false,
            isBestFit = true,
            pageNumPending = null,
            scale = 1,
            pdfViewerCanvas = document.getElementById('pdfViewer'),
            drawingCanvas = document.getElementById('drawCanvas'),
            pdfViewerCanvasContext = pdfViewerCanvas.getContext('2d'),
            drawingCanvasContext = drawingCanvas.getContext('2d');

        let startX, startY, endX, endY;
        let activeElement;
        var desiredWidth = 800;
        let x_1, y_1, x_2, y_2;

        pdfjsLib.getDocument(fileName).promise.then((pdfDoc_) => {
            pdfDoc = pdfDoc_;
            document.getElementById('page_count').textContent = pdfDoc.numPages;
            // Initial/first page rendering
            loadPdfFactory();
            renderPage(pageNum);
        });
        const loadPdfFactory = () => {
            pdfDoc.getData().then((data) => {
                pdfFactory = new pdfAnnotate.AnnotationFactory(data)
                console.log(pdfFactory.data);
            })
        };

        const renderPage = (num) => {
            pageRendering = true;
            pdfDoc.getPage(num).then(function (page) {
                let viewport = page.getViewport({ scale: 1 });
                let scaleRatio = Math.max(desiredWidth / viewport.width);

                if (isBestFit === true) {
                    viewport = page.getViewport({ scale: scaleRatio });
                } else {
                    viewport = page.getViewport({ scale: scale });
                }

                pdfViewerCanvas.width = viewport.width;
                pdfViewerCanvas.height = viewport.height;
                drawingCanvas.height = viewport.height;
                drawingCanvas.width = viewport.width;
                //pdfViewerCanvasContext.scale(scale, scale)
                //pdfViewerCanvasContext.imageSmoothingEnabled = true;
                viewp = viewport;

                // Wait for rendering to finish
                var renderContext = {
                    canvasContext: pdfViewerCanvasContext,
                    viewport: viewport,
                    enableHighResolution: true
                };
                var renderTask = page.render(renderContext);
                //pdfjsLib.AnnotationLayer.render(page, viewport, { annotation });
                //console.log(pdfjsLib.AnnotationLayer);
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

        function getImage() {
            const rectangleWidth = Math.abs(endX - startX);
            const rectangleHeight = Math.abs(endY - startY);
            const rectangleX = Math.min(startX, endX);
            const rectangleY = Math.min(startY, endY);
            //console.log(rectangleX, rectangleY, rectangleWidth, rectangleHeight);

            var newCanvas = document.createElement('canvas');
            newCanvas.width = rectangleWidth;
            newCanvas.height = rectangleHeight;
            var newCtx = newCanvas.getContext('2d');
            newCtx.drawImage(pdfViewerCanvas, rectangleX, rectangleY, rectangleWidth, rectangleHeight, 0, 0, rectangleWidth, rectangleHeight);
            var imageData = newCanvas.toDataURL('image/tiff');
            getText(imageData);

            var parent = document.getElementById('content-wrapper');
            const canvas = document.createElement("canvas");

            // Set the width and height of the canvas
            canvas.width = rectangleWidth;
            canvas.height = rectangleHeight;

            // Get the 2D context of the canvas
            const ctx = canvas.getContext("2d");
            var width = endX - startX;
            var height = endY - startY;
            // Create a rectangle using the context
            ctx.rect(startX, startY, width, height);
            ctx.strokeStyle = 'black';
            ctx.lineWidth = 2;
            ctx.stroke();
            
            //canvas.style.zIndex = 1;
            //canvas.style.border = "2px solid black";
            //canvas.style.position = "fixed";

            // Append the canvas to the parent element
            parent.appendChild(canvas);

        };

        let selectionCoordinates = function (box) {
            
            
            return [x_1, y_1, x_2, y_2]
        }

        function getText(image) {
            Tesseract.recognize(image, 'eng').then(function (result) {
                setText(result.data.text);
                if (result.data.blocks[0] != undefined) {
                    x_1 = result.data.blocks[0].bbox.x0;
                    y_1 = result.data.blocks[0].bbox.y0;
                    x_2 = result.data.blocks[0].bbox.x1;
                    y_2 = result.data.blocks[0].bbox.y1;

                    

                    //pdfFactory.createSquareAnnotation(pageNum, selectionCoordinates(),"","");
                    //renderPage(pageNum);
                }
            });
        };

        function setText(text) {
            activeElement.value = text;
        };

        function handleResponse(response) {
            // Do something with the response, such as updating the UI
            console.log(response);
            renderPage(pageNum);
        }

        function callMyAction(num, x1, y1, width, height) {
            let name = document.querySelector("#Data_OutputPath").value;

            var data = {
                fileName: name,
                x1: x1,
                y1: y1,
                width: width,
                height: height,
                pageNumber: num
            };

            $.ajax({
                url: "/api/app/ocr/highlight",
                type: "POST",
                dataType: 'json',
                contentType: 'application/json',
                data: JSON.stringify(data),
                success: handleResponse
            });
        }


        const inputs = document.querySelectorAll('input');
        //document.querySelector('select.scale').value = RENDER_OPTIONS.scale;
        function setScaleRotate(scaleSelection) {
            if (scaleSelection === '3') {
                isBestFit = true;
            }
            else {
                isBestFit = false;
                scaleSelection = parseFloat(scaleSelection, 10);
                if (scaleSelection !== scale) {
                    scale = scaleSelection;
                    ///localStorage.setItem(RENDER_OPTIONS.documentId + '/scale', RENDER_OPTIONS.scale);
                }
            }
            renderPage(pageNum);
        }

        function handleScaleChange(e) {
            setScaleRotate(e.target.value);
        }
        document.getElementById('scaling').addEventListener('change', handleScaleChange);

        inputs.forEach(input => {
            input.addEventListener('blur', () => {
                activeElement = input;
            });
        });
    };
    return {
        initModal: initModal
    };
};




