window.logout = function () {
    localStorage.removeItem("cart");
    $.ajax({
        url: '/User/Logout', // Replace with your API URL
        method: 'Get',
        success: function (data) {
            toastr.success(data, "Success");
            window.location = "/";
        },
        error: function (xhr, status, error) {
            toastr.error(error, "Error");
        }
    });
}