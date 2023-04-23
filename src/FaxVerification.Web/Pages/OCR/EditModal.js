abp.modals.ProductInfo = function () {
    function initModal(modalManager, args) {
        let fileName = "./Pdf/" + document.querySelector("#Data_FilePath").value;
        WebViewer({
            path: "./libs/pdfjs-express",
            initialDoc: fileName,
        }, document.getElementById('viewer'))
            .then((instance) => {
                instance.UI.loadDocument(initialDoc);
            });

        console.log(WebViewer);
    };
    return {
        initModal: initModal
    };
};




