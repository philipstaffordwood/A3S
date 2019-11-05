// Authenticator registration click event
var btnAuthenticator = document.getElementById("btnAuthenticator")
if (btnAuthenticator) {
    btnAuthenticator.addEventListener("click", function () {
        window.location.href = document.getElementById("RegisterAuthenticatorUrl").value;
    });
}

// Authenticator de-register click event
var btnDeregisterAuthenticator = document.getElementById("btnDeregisterAuthenticator")
if (btnDeregisterAuthenticator) {
    btnDeregisterAuthenticator.addEventListener("click", function () {
        window.location.href = document.getElementById("DeregisterAuthenticatorUrl").value;
    });
}

// Reset Recovery Codes click event
var btnResetRecoveryCodes = document.getElementById("btnResetRecoveryCodes")
if (btnResetRecoveryCodes) {
    btnResetRecoveryCodes.addEventListener("click", function () {
        window.location.href = document.getElementById("ResetRecoveryCodesUrl").value;
    });
}

// Email registration click event
var btnAltEmail = document.getElementById("btnAltEmail")
if (btnAltEmail) {
    btnAltEmail.addEventListener("click", function () {
        window.location.href = document.getElementById("RegisterEmailUrl").value;
    });
}

// Email Deregistration click event
var btnDeregisterEmail = document.getElementById("btnDeregisterEmail")
if (btnDeregisterEmail) {
    btnDeregisterEmail.addEventListener("click", function () {
        window.location.href = document.getElementById("DeregisterEmailUrl").value;
    });
}

// Redirect click event
var btnContinueWithout = document.getElementById("btnContinueWithout")
if (btnContinueWithout) {
    btnContinueWithout.addEventListener("click", function () {
        window.location.href = document.getElementById("RedirectUrl").value;
    });
}

// Cancel click event
var btnCancel = document.getElementById("btnCancel")
if (btnCancel) {
    btnCancel.addEventListener("click", function () {
        window.location.href = document.getElementById("Cancel2FARegistrationUrl").value;
    });
}
