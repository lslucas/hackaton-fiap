// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.getElementById("logout").addEventListener("click", function(event) {
    event.preventDefault(); // Prevent default link behavior (navigation)

    // Create a form dynamically
    var form = document.createElement("form");
    form.method = "POST";
    form.action = "/patient/logout"; // Specify your controller and action method

    
    // Append the form to the body and submit it
    document.body.appendChild(form);
    form.submit();
});
