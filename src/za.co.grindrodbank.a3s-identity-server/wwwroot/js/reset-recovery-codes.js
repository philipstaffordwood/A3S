// Return click event
var btnReturn = document.getElementById("btnReturn")
if (btnReturn) {
    btnReturn.addEventListener("click", function () {
        window.location.href = document.getElementById("ReturnTo2FAUrl").value;
    });
}
