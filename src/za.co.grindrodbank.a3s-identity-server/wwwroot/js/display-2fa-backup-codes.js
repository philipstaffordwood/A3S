function retrieveRecoveryCodesFromList() {
    var recoveryCodesNodeList = document.getElementById("recoveryCodeList").getElementsByTagName("li");

    var recoveryCodesMsg = "Recovery Codes:\n\n";
    if (recoveryCodesNodeList) {
        for (var i = 0; i < recoveryCodesNodeList.length; i++) {
            var codeNode = recoveryCodesNodeList[i];
            recoveryCodesMsg += codeNode.innerText + "\n";
        }
    }

    return recoveryCodesMsg;
}

// Copy click event
var btnCopy = document.getElementById("btnCopy")
if (btnCopy) {
    btnCopy.addEventListener("click", function () {
        var recoveryCodesMsg = retrieveRecoveryCodesFromList();
        copyToClipboard(recoveryCodesMsg);
        this.innerHTML = "Copied to clipboard...";
    });

    btnCopy.addEventListener("mouseout", function () {
        this.innerHTML = "Copy";
    });
}

// Save click event
var btnSave = document.getElementById("btnSave")
if (btnSave) {
    btnSave.addEventListener("click", function () {
        var recoveryCodesMsg = retrieveRecoveryCodesFromList();
        downloadInMemoryTextFile("recovery-codes.txt", recoveryCodesMsg);
    });
}

// Print click event
var btnPrint = document.getElementById("btnPrint")
if (btnPrint) {
    btnPrint.addEventListener("click", function () {
        window.print();
    });
}