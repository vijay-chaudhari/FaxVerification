abp.modals.ProductInfo = function () {
    function initModal(modalManager, args) {
        let fileName = "./Pdf/" + document.querySelector("#Data_FilePath").value;

        WebViewer({
            path: "./libs/pdfjs-express",
            disabledElements: ['ribbons', 'menuButton', 'toggleNotesButton'],
            preloadWorker: WebViewer.WorkerTypes.PDF,
            initialDoc: fileName,
        }, document.getElementById('viewer')).then(instance => {
            var Feature = instance.Feature;
            instance.disableFeatures([Feature.Search]);
            //instance.UI.disableElements(['ribbons']);
            //instance.UI.disableElements(['menuButton']);
            //instance.UI.disableElements(['toggleNotesButton']);
            //instance.UI.setToolbarGroup(instance.UI.ToolbarGroup.VIEW);
            instance.UI.setToolMode(instance.Core.Tools.ToolNames.RECTANGLE);
            instance.UI.setTheme('dark');

            const inputs = document.querySelectorAll('input');
            let activeElement;
            const {
                documentViewer,
                annotationManager,
                Annotations,
            } = instance.Core;


            documentViewer.one('documentLoaded', async () => {

                var FitMode = instance.UI.FitMode;
                instance.UI.setFitMode(FitMode.FitWidth);

                annotationManager.on('annotationChanged', async (annotations, action) => {
                    if (action === 'add') {
                        if (annotations[0] instanceof Annotations.RectangleAnnotation) {

                            const rectangleAnnotation = annotations[0];
                            const doc = documentViewer.getDocument();
                            const pageIndex = documentViewer.getCurrentPage();
                            const rect = rectangleAnnotation.getRect();

                            doc.getTextByPageAndRect(pageIndex, rect).then(text => {
                                if (!text) {
                                    getImage(rect, pageIndex);
                                }
                                else {
                                    setText(text);
                                }
                                setTimeout(() => {
                                    annotationManager.deleteAnnotation(rectangleAnnotation);
                                }, 1000);
                            });
                        }
                    }


                });
            });

            const getImage = (rect, pageIndex) => {

                var pageCanvas = getPageCanvas(pageIndex);
                const scale = window.devicePixelRatio
                var topOffset = parseFloat(pageCanvas.style.top) || 0 * scale;
                var leftOffset = parseFloat(pageCanvas.style.left) || 0 * scale;
                var zoom = documentViewer.getZoomLevel() * scale;

                var W = rect.x2 - rect.x1;
                var H = rect.y2 - rect.y1;

                var x = rect.x1 * zoom - leftOffset;
                var y = rect.y1 * zoom - topOffset;
                var width = W * zoom;
                var height = H * zoom;

                var copyCanvas = document.createElement('canvas');
                copyCanvas.width = width;
                copyCanvas.height = height;
                var ctx = copyCanvas.getContext('2d');
                ctx.drawImage(pageCanvas, x, y, width, height, 0, 0, width, height);

                var imageData = copyCanvas.toDataURL('image/tiff');
                getText(imageData);
            };

            const getText = imageData => {
                Tesseract.recognize(imageData, 'eng').then(function (result) {
                    setText(result.data.text);
                });
            };

            const setText = text => {
                activeElement.value = text;
            };

            inputs.forEach(input => {
                input.addEventListener('blur', () => {
                    activeElement = input;
                    
                });
                input.addEventListener('click', () => {
                    documentViewer.textSearchInit(input.value, searchMode, searchOptions);
                });

            });

            const getPageCanvas = pageIndex => {
                return instance.iframeWindow.document.querySelector('#pageContainer' + pageIndex + ' .canvas' + pageIndex);
            };

            const searchMode = instance.Core.Search.Mode.PAGE_STOP | instance.Core.Search.Mode.HIGHLIGHT;
            const searchOptions = {
                fullSearch: true, // search the full document,
                onResult: (result) => {
                    if (result.resultCode === instance.Core.Search.ResultCode.FOUND) {

                        if (!result.quads.length) return;

                        const textQuad = result.quads[0].getPoints();
                        const annot = new Annotations.TextHighlightAnnotation();


                        annot.X = textQuad.x1;
                        annot.Width = textQuad.x2 - textQuad.x1;
                        annot.PageNumber = result.pageNum;
                        annot.Y = textQuad.y3;
                        annot.Height = textQuad.y1 - textQuad.y3;

                        annot.FillColor = new Annotations.Color(0, 0, 255, 0.5);
                        annot.StrokeColor = new Annotations.Color(0, 0, 255, 0.5);

                        annot.Quads = [textQuad];

                        annotationManager.addAnnotation(annot);
                        annotationManager.drawAnnotationsFromList([annot])
                    }
                }
            }

        });
    };
    return {
        initModal: initModal
    };
};


//This code works but we have short alternative for this
//const doc = documentViewer.getDocument();
//const pageIndex = documentViewer.getCurrentPage();
//const rect = rectangleAnnotation.getRect();
//console.log(`Coordinates: (${rect.x1}, ${rect.y1}, ${rect.x2}, ${rect.y2})`);
//const text = await extractText(doc, pageIndex, rect.x1, rect.y1, rect.x2, rect.y2);
//const extractText = (doc, pageIndex, top_x, top_y, bottom_x, bottom_y) => {
//    return new Promise(resolve => {
//        doc.loadPageText(pageIndex, text => {
//            debugger;
//            doc.getTextPosition(pageIndex, 0, text.length, (arr) => {

//                // temp array to store the position of characters
//                var indies = []

//                // filter out array with given x, y coordinates
//                arr = arr.filter((item, index) => {
//                    // replace this if statement from the previous message
//                    // if (item.x4 >= top_x && item.y4 >= top_y && tem.x2 <= (top_x + bottom_x) && item.y2 <= (top_y + bottom_y)) {

//                    // with:
//                    if (item.x4 >= top_x && item.y4 >= top_y && item.x2 <= bottom_x && item.y2 <= bottom_y) {
//                        indies.push(index)
//                        return true;
//                    }
//                    return false;
//                })

//                // concatenate chars into string
//                let str = '';
//                for (let i = 0, len = indies.length; i < len; i++) {
//                    str += text[indies[i]];
//                }

//                // filtered arr can be used for other purposes, e.g. debugging

//                // return/resolve concatenated string
//                resolve(str)
//            });
//        });
//    });
//}
//***************************************************************************************************************** */








//*************************************************************************************************************** */
//documentViewer.one('documentLoaded', async () => {
//    //const doc = documentViewer.getDocument();
//    //const pageNumber = 1;
//    //const zoom = 2; // render at twice the resolution

//    //doc.loadCanvasAsync(({
//    //    pageNumber,
//    //    zoom,
//    //    drawComplete: async (thumbnail) => {
//    //        //const corePageRotation = (doc.getPageRotation(pageNumber) / 90) % 4;
//    //        //annotationManager.setAnnotationCanvasTransform(thumbnail.getContext('2d'), zoom, corePageRotation);

//    //        // optionally comment out "drawAnnotations" below to exclude annotations
//    //        //await instance.Core.documentViewer.getAnnotationManager().drawAnnotations(pageNumber, thumbnail);
//    //        // thumbnail is a HTMLCanvasElement or HTMLImageElement
//    //        var imageData = thumbnail.toDataURL('image/tiff');
//    //        console.log(imageData);
//    //    }

//    //}));




//    //await PDFNet.initialize();
//    //const doc = await documentViewer.getDocument().getPDFDoc();
//    //const pdfdraw = await PDFNet.PDFDraw.create(92);
//    //const itr = await doc.getPageIterator(1);
//    //const currPage = await itr.current();

//    //const pngBuffer = await pdfdraw.exportBuffer(currPage, 'PNG');
//    //const tifBuffer = await pdfdraw.exportBuffer(currPage, 'TIFF');
//    //console.log(tifBuffer);

//    annotationManager.on('annotationAdded', async (annotations, action) => {

//        if (annotations[0] instanceof Annotations.RectangleAnnotation) {

//            const rectangleAnnotation = annotations[0];
//            const doc = documentViewer.getDocument();
//            const pageIndex = documentViewer.getCurrentPage();
//            const rect = rectangleAnnotation.getRect();

//            doc.getTextByPageAndRect(pageIndex, rect).then(text => {

//                if (!text) {
//                    getImage(rectangleAnnotation, pageIndex);
//                }
//                else {
//                    setText(text);
//                }
//                annotationManager.deleteAnnotation(annotation);

//            });
//        }
//    });
//});
//*************************************************************************************************************** */


//*************************************************************************************************************** */

//const getImage = (rectAnnotaion, pageIndex) => {

//    var pageCanvas = getPageCanvas(pageIndex);
//    const scale = window.devicePixelRatio
//    var topOffset = parseFloat(pageCanvas.style.top) || 0 * scale;
//    var leftOffset = parseFloat(pageCanvas.style.left) || 0 * scale;
//    var zoom = documentViewer.getZoomLevel() * scale;

//    var x = rectAnnotaion.X * zoom - leftOffset;
//    var y = rectAnnotaion.Y * zoom - topOffset;
//    var width = rectAnnotaion.width * zoom;
//    var height = rectAnnotaion.Height * zoom;


//    var copyCanvas = document.createElement('canvas');
//    copyCanvas.width = width;
//    copyCanvas.height = height;
//    var ctx = copyCanvas.getContext('2d');
//    ctx.drawImage(pageCanvas, x, y, width, height, 0, 0, width, height);

//    var imageData = copyCanvas.toDataURL('image/tiff');
//    getText(imageData);

//    //const rect = rectAnnotaion.getRect();
//    //console.log(`Coordinates: (${rect.x1}, ${rect.y1}, ${rect.x2}, ${rect.y2})`);

//    //working for searchable pdf
//    //const page = documentViewer.getDocument();
//    //console.log(page.getPDFDoc())

//    //page.getTextByPageAndRect(pageNumber, rect).then(text => {
//    //    console.log(text);
//    //});

//    //console.log('pageCanvas', pageCanvas);
//    //const canvas = document.createElement('canvas');
//    //const ctx = canvas.getContext('2d');
//    //canvas.width = pageElement.clientWidth;
//    //canvas.height = pageElement.clientHeight;
//    //ctx.drawImage(pageElement, 0, 0);
//    //const imageData = ctx.getImageData(rect.x1, rect.y1, canvas.width, canvas.height);

//    //console.log(`Content as image data: ${imageData}`);

//    //// Convert image data to an ImageBitmap
//    //createImageBitmap(imageData).then(bitmap => {
//    //    // Call the getText function to perform OCR on the image
//    //    getText(bitmap);
//    //});
//};
//*************************************************************************************************************** */

