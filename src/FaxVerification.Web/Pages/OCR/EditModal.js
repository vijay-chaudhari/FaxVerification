abp.modals.ProductInfo = function () {
    function initModal(modalManager, args) {

        //$(document).ready(function () {
        //    debugger;
        //    var Configuration = document.querySelector('#Data_FormConfiguration').value.split('~');
        //    var NameValue = Configuration.length;
        //    for (var i = 1; i < 9; i++) {

        //        if ((i-1) < NameValue) {

        //            document.querySelector('#lbl_' + i).innerHTML = Configuration[(i - 1)];
        //            document.querySelector('#lbl_' + i).classList.remove("Hide_attr");
        //            //document.querySelector('#Data_PersonDetails_Attribute_1').classList.remove("Hide_attr")
        //            document.querySelector('#Data_PersonDetails_Attribute_' + i).classList.remove("Hide_attr");
        //        }
        //        else {
        //            document.querySelector('#lbl_' + i).classList.add("Hide_attr");
        //            document.querySelector('#Data_PersonDetails_Attribute_' + i).classList.add("Hide_attr");
        //        }

        //    }


        //});

        let fileName = "./Pdf/" + document.querySelector("#Data_FilePath").value;

        WebViewer({
            path: "./libs/pdfjs-express/lib",
            disabledElements: ['ribbons', 'menuButton', 'toggleNotesButton'],
            preloadWorker: WebViewer.WorkerTypes.PDF,
            initialDoc: fileName,
        }, document.getElementById('viewer')).then(instance => {

            var Feature = instance.Feature;
            instance.disableFeatures([Feature.Search]);
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
                            getImage(rect, pageIndex);
                            setTimeout(() => {
                                annotationManager.deleteAnnotation(rectangleAnnotation);
                            }, 1000);
                            //doc.getTextByPageAndRect(pageIndex, rect).then(text => {
                            //    if (!text) {
                            //        getImage(rect, pageIndex);
                            //    }
                            //    else {
                            //        setText(text);
                            //    }
                            //    setTimeout(() => {
                            //        annotationManager.deleteAnnotation(rectangleAnnotation);
                            //    }, 1000);
                            //});
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
                getText(imageData, rect, pageIndex);
            };

            const getText = (imageData, rect, pageIndex) => {
                Tesseract.recognize(imageData, 'eng').then(function (result) {
                    if (result.data.text) {
                        setText(result.data.text, rect, pageIndex);
                    }

                });
            };

            const setText = (text, rect, pageIndex) => {
                activeElement.value = text;
                const cords = `${rect.x1},${rect.y1},${rect.x2},${rect.y2}`;
                conversionRequired = false;
                debugger;

                if (activeElement.id === 'Data_PersonDetails_Patient_Name_Text') {
                    document.querySelector('#Data_PersonDetails_Patient_Name_Rectangle').value = cords;
                    document.querySelector('#Data_PersonDetails_Patient_Name_PageNumber').value = pageIndex;
                    hightLightAnnnotation(cords.split(','), pageIndex);
                }
                if (activeElement.id === 'Data_PersonDetails_Patient_BirthDate_Text') {
                    document.querySelector('#Data_PersonDetails_Patient_BirthDate_Rectangle').value = cords;
                    document.querySelector('#Data_PersonDetails_Patient_BirthDate_PageNumber').value = pageIndex;
                    hightLightAnnnotation(cords.split(','), pageIndex);
                }

                if (activeElement.id === 'Data_PersonDetails_Invoice_InvNum_Text') {
                    document.querySelector('#Data_PersonDetails_Invoice_InvNum_Rectangle').value = cords;
                    document.querySelector('#Data_PersonDetails_Invoice_InvNum_PageNumber').value = pageIndex;
                    hightLightAnnnotation(cords.split(','), pageIndex);
                }
                if (activeElement.id === 'Data_PersonDetails_Invoice_InvDate_Text') {
                    document.querySelector('#Data_PersonDetails_Invoice_InvDate_Rectangle').value = cords;
                    document.querySelector('#Data_PersonDetails_Invoice_InvDate_PageNumber').value = pageIndex;
                    hightLightAnnnotation(cords.split(','), pageIndex);
                }
                if (activeElement.id === 'Data_PersonDetails_Invoice_OrderNum_Text') {
                    document.querySelector('#Data_PersonDetails_Invoice_OrderNum_Rectangle').value = cords;
                    document.querySelector('#Data_PersonDetails_Invoice_OrderNum_PageNumber').value = pageIndex;
                    hightLightAnnnotation(cords.split(','), pageIndex);
                }
                if (activeElement.id === 'Data_PersonDetails_Invoice_OrderDate_Text') {
                    document.querySelector('#Data_PersonDetails_Invoice_OrderDate_Rectangle').value = cords;
                    document.querySelector('#Data_PersonDetails_Invoice_OrderDate_PageNumber').value = pageIndex;
                    hightLightAnnnotation(cords.split(','), pageIndex);
                }
                if (activeElement.id === 'Data_PersonDetails_Invoice_VendorName_Text') {
                    document.querySelector('#Data_PersonDetails_Invoice_VendorName_Rectangle').value = cords;
                    document.querySelector('#Data_PersonDetails_Invoice_VendorName_PageNumber').value = pageIndex;
                    hightLightAnnnotation(cords.split(','), pageIndex);
                }
                if (activeElement.id === 'Data_PersonDetails_Invoice_Tax_Text') {
                    document.querySelector('#Data_PersonDetails_Invoice_Tax_Rectangle').value = cords;
                    document.querySelector('#Data_PersonDetails_Invoice_Tax_PageNumber').value = pageIndex;
                    hightLightAnnnotation(cords.split(','), pageIndex);
                }
                if (activeElement.id === 'Data_PersonDetails_Invoice_Total_Text') {
                    document.querySelector('#Data_PersonDetails_Invoice_Total_Rectangle').value = cords;
                    document.querySelector('#Data_PersonDetails_Invoice_Total_PageNumber').value = pageIndex;
                    hightLightAnnnotation(cords.split(','), pageIndex);
                }

                if (activeElement.id.includes("Attribute_")) {
                    //alert(activeElement.id);
                    var elecmetID = activeElement.id.split('_');
                    var elemid = elecmetID[3];

                    var chords_pageNo = cords + "," + pageIndex;

                    document.querySelector('#Data_PersonDetails_AttributeCords_' + elemid).value = chords_pageNo;
                    //document.querySelector('#Data_PersonDetails_Invoice_Total_PageNumber').value = pageIndex;
                    hightLightAnnnotation(cords.split(','), pageIndex);

                }


            };

            inputs.forEach(input => {
                input.addEventListener('click', () => {
                    activeElement = input;
                    if (input.id === 'Data_PersonDetails_Patient_Name_Text') {
                        let cords = document.querySelector('#Data_PersonDetails_Patient_Name_Rectangle').value.split(',');
                        let pageNum = document.querySelector('#Data_PersonDetails_Patient_Name_PageNumber').value;
                        if (cords.every(IsNotZero)) {
                            hightLightAnnnotation(cords, pageNum);
                            scrollToPage(pageNum);
                        }
                    }
                    if (input.id === 'Data_PersonDetails_Patient_BirthDate_Text') {
                        let cords = document.querySelector('#Data_PersonDetails_Patient_BirthDate_Rectangle').value.split(',');
                        let pageNum = document.querySelector('#Data_PersonDetails_Patient_BirthDate_PageNumber').value;
                        if (cords.every(IsNotZero)) {
                            hightLightAnnnotation(cords, pageNum);
                            scrollToPage(pageNum);
                        }
                    }
                    if (input.id === 'Data_PersonDetails_Invoice_InvNum_Text') {
                        let cords = document.querySelector('#Data_PersonDetails_Invoice_InvNum_Rectangle').value.split(',');
                        let pageNum = document.querySelector('#Data_PersonDetails_Invoice_InvNum_PageNumber').value;
                        if (cords.every(IsNotZero)) {
                            hightLightAnnnotation(cords, pageNum);
                            scrollToPage(pageNum);
                        }

                    }
                    if (input.id === 'Data_PersonDetails_Invoice_InvDate_Text') {
                        let cords = document.querySelector('#Data_PersonDetails_Invoice_InvDate_Rectangle').value.split(',');
                        let pageNum = document.querySelector('#Data_PersonDetails_Invoice_InvDate_PageNumber').value;
                        if (cords.every(IsNotZero)) {
                            hightLightAnnnotation(cords, pageNum);
                            scrollToPage(pageNum);
                        }
                        else {
                            removeAnnotation();
                        }
                    }
                    if (input.id === 'Data_PersonDetails_Invoice_OrderNum_Text') {
                        let cords = document.querySelector('#Data_PersonDetails_Invoice_OrderNum_Rectangle').value.split(',');
                        let pageNum = document.querySelector('#Data_PersonDetails_Invoice_OrderNum_PageNumber').value;
                        if (cords.every(IsNotZero)) {
                            hightLightAnnnotation(cords, pageNum);
                            scrollToPage(pageNum);
                        }
                        else {
                            removeAnnotation();
                        }
                    }
                    if (input.id === 'Data_PersonDetails_Invoice_OrderDate_Text') {
                        let cords = document.querySelector('#Data_PersonDetails_Invoice_OrderDate_Rectangle').value.split(',');
                        let pageNum = document.querySelector('#Data_PersonDetails_Invoice_OrderDate_PageNumber').value;
                        if (cords.every(IsNotZero)) {
                            hightLightAnnnotation(cords, pageNum);
                            scrollToPage(pageNum);
                        }
                        else {
                            removeAnnotation();
                        }
                    }
                    if (input.id === 'Data_PersonDetails_Invoice_VendorName_Text') {
                        let cords = document.querySelector('#Data_PersonDetails_Invoice_VendorName_Rectangle').value.split(',');
                        let pageNum = document.querySelector('#Data_PersonDetails_Invoice_VendorName_PageNumber').value;
                        if (cords.every(IsNotZero)) {
                            hightLightAnnnotation(cords, pageNum);
                            scrollToPage(pageNum);
                        }
                        else {
                            removeAnnotation();
                        }
                    }
                    if (input.id === 'Data_PersonDetails_Invoice_Tax_Text') {
                        let cords = document.querySelector('#Data_PersonDetails_Invoice_Tax_Rectangle').value.split(',');
                        let pageNum = document.querySelector('#Data_PersonDetails_Invoice_Tax_PageNumber').value;
                        if (cords.every(IsNotZero)) {
                            hightLightAnnnotation(cords, pageNum);
                            scrollToPage(pageNum);
                        }
                        else {
                            removeAnnotation();
                        }
                    }
                    if (input.id === 'Data_PersonDetails_Invoice_Total_Text') {
                        let cords = document.querySelector('#Data_PersonDetails_Invoice_Total_Rectangle').value.split(',');
                        let pageNum = document.querySelector('#Data_PersonDetails_Invoice_Total_PageNumber').value;
                        if (cords.every(IsNotZero)) {
                            hightLightAnnnotation(cords, pageNum);
                            scrollToPage(pageNum);
                        }
                        else {
                            removeAnnotation();
                        }
                    }

                    if (input.id.includes("Attribute_")) {
                        //debugger;
                        //alert(activeElement.id);
                        var elecmetID = activeElement.id.split('_');
                        var elemid = elecmetID[3];

                        var cordinates = document.querySelector('#Data_PersonDetails_AttributeCords_' + elemid).value.split(',');
                        var page = cordinates[4];
                        cordinates.pop();


                        let cords = cordinates;
                        let pageNum = page;// document.querySelector('#Data_PersonDetails_Patient_Name_PageNumber').value;
                        if (cords.every(IsNotZero) && cordinates.length >0) {
                            hightLightAnnnotation(cords, pageNum);
                            scrollToPage(pageNum);
                        }
                    }
                    //firstResultFound = false;
                    //documentViewer.textSearchInit(input.value, searchMode, searchOptions);
                });
            });

            let firstResultFound = false;

            let previousHighlightAnnot = null;

            const getPageCanvas = pageIndex => {
                return instance.iframeWindow.document.querySelector('#pageContainer' + pageIndex + ' .canvas' + pageIndex);
            };

            const searchMode = instance.Core.Search.Mode.PAGE_STOP | instance.Core.Search.Mode.HIGHLIGHT;

            const searchOptions = {
                fullSearch: true,
                onResult: (result) => {
                    if (result.resultCode === instance.Core.Search.ResultCode.FOUND) {

                        if (firstResultFound) {
                            return;
                        }

                        if (!result.quads.length) {
                            return;
                        };

                        const textQuad = result.quads[0].getPoints();

                        hightLightAnnnotation(textQuad);

                        firstResultFound = true;
                    }
                }
            }

            const hightLightAnnnotation = (cords, pageNum) => {

                removeAnnotation();

                let quad = ConversionUtil(cords);

                const annot = new Annotations.TextHighlightAnnotation();
                annot.X = quad.x1;
                annot.Width = quad.x2 - quad.x1;
                annot.PageNumber = pageNum;
                annot.Y = quad.y3;
                annot.Height = quad.y1 - quad.y3;

                annot.FillColor = new Annotations.Color(0, 0, 255, 0.5);
                annot.StrokeColor = new Annotations.Color(0, 0, 255, 0.5);

                annot.Quads = [quad];
                annotationManager.addAnnotation(annot);
                annotationManager.drawAnnotationsFromList(annot)
                previousHighlightAnnot = annot;
            };

            const scrollToPage = (pageNum) => {
                documentViewer.setCurrentPage(pageNum, true);
            };

            const ConversionUtil = (cords) => {

                const rect = {
                    X1: cords[0],
                    Y1: cords[1],
                    X2: cords[2],
                    Y2: cords[3]
                };

                const quad = new Annotations.Quad(
                    rect.X1, rect.Y1,
                    rect.X2, rect.Y1,
                    rect.X2, rect.Y2,
                    rect.X1, rect.Y2
                );
                return quad;
            };

            const IsNotZero = (num) => {
                return num > 0;
            }

            const removeAnnotation = () => {
                if (previousHighlightAnnot) {
                    annotationManager.deleteAnnotation(previousHighlightAnnot);
                }
            }
        });
    };
    return {
        initModal: initModal
    };
};


//document.onload = function () {


//};


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

