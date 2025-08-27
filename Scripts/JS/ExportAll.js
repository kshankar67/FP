$(document).ready(function () {
   

})
function ExportExcel(btnname,filename) {
    $("#" + btnname).click(function (e) {
        let file = new Blob([$('.divclass').html()], { type: "application/vnd.ms-excel" });
        let url = URL.createObjectURL(file);
        let a = $("<a />", {
            href: url,
            download: filename + ".xls"
        }).appendTo("body").get(0).click();
        e.preventDefault();
    });
}
function printDiv(divId, title) {
    var divContent = document.getElementById(divId).innerHTML;

    // Build full CSS URL dynamically
    var cssPath = window.location.origin + "/Content/css/bootstrap.min.css";

    // Full HTML content for print window
    var printContents = `
        <html>
        <head>
            <title>${title ? title : "Print"}</title>
            <link rel="stylesheet" href="${cssPath}">
            <style>
                body {
                    font-family: Arial, sans-serif;
                    padding: 20px;
                    
                }
                @media print {
                * {
                    -webkit-print-color-adjust: exact;
                    print-color-adjust: exact;
                }
                 .col-sm-12 { width: 100%; }
                .col-xl-4 { width: 33.33%; }
                .col-xl-3 { width: 25%; }
                .col-xl-5 { width: 41.67%; }
                }
            </style>
        </head>
        <body>
            ${divContent}
        </body>
        </html>
    `;

    // Open new window
    var printWindow = window.open('', '_blank', 'width=1024,height=650');

    if (printWindow) {
        printWindow.document.open();
        printWindow.document.write(printContents);
        printWindow.document.close();

        // Wait for content to load before printing
        printWindow.onload = function () {
            printWindow.focus();
            printWindow.print();

            // Auto-close after short delay
            setTimeout(function () {
                printWindow.close();
            }, 100);
        };
    } else {
        alert("Popup blocked. Please allow popups for this site.");
    }
}

