$(document).ready(function () {

    // Login form validation
    $('#lgnForm').submit(function (event) {
        event.preventDefault();

        var username = $('#lgnUsername').val().trim();
        var password = $('#lgnPassword').val();

        if (username.length > 255) {
            toastr.error("Invalid Username", "Error");
            return;
        }

        if (password.length < 6) {
            toastr.error("Password must be at least 6 characters long", "Error");
            return;
        }
        UserLogin();
    });

    // Sign up form validation
    $('#regForm').submit(function (event) {
        event.preventDefault();

        var username = $('#regUsername').val().trim();
        var email = $('#regEmail').val().trim();
        var password = $('#regPassword').val();
        var confirmPassword = $('#regConfirmPassword').val();

        if (username.length > 255) {
            toastr.error("Username length cannot exceed 255 characters.", "Error");
            return;
        }

        if (email.length > 255) {
            toastr.error("Email length cannot exceed 255 characters.", "Error");
            return;
        }

        if (password.length < 6) {
            toastr.error("Password must be at least 6 characters long.", "Error");
            return;
        }

        if (password !== confirmPassword) {
            toastr.error("Password and Confirm Password must match.", "Error");
            return;
        }
        UserRegister();
    });

});
function toggleForm() {
    var lgnCard = document.getElementById('lgnCard');
    var regCard = document.getElementById('regCard');

    lgnCard.classList.toggle('hidden');
    regCard.classList.toggle('hidden');
    clearForm();
}

function UserLogin() {
    var data = {
        Username: $("#lgnUsername").val().trim(),
        Password: $("#lgnPassword").val()
    }
	$(".loader").show();
    $.ajax({
        type: "POST",
        url: "/User/Login",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify(data),
        success: function (result) {
			$(".loader").hide();
            localStorage.removeItem('cart');
            if (result.isSuccess) {
                toastr.success(result.message, "Success");
                window.location.href = "/Product";
            } else {
                toastr.error(result.message, "Error");
            }
        },
        error: function (result) {
			$(".loader").hide();
        }
    });
}
function UserRegister() {
    var data = {
        Username: $('#regUsername').val().trim(),
        Email: $('#regEmail').val().trim(),
        Password: $('#regPassword').val()
    }
	$(".loader").show();
    $.ajax({
        type: "POST",
        url: "/User/Register",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify(data),
        success: function (result) {
			$(".loader").hide();
            if (result.isSuccess) {
                toastr.success(result.message, "Success");
                toggleForm();
            } else {
                toastr.error(result.message, "Error");
            }

        },
        error: function (result) {
			$(".loader").hide();
        }
    });
}
function clearForm() {
    $("#lgnPassword").val("");
    $("#lgnUsername").val("");
    $('#regUsername').val("");
    $('#regEmail').val("");
    $('#regPassword').val("");
    $('#regConfirmPassword').val("");
}