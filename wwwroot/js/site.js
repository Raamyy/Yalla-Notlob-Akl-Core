// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

async function AjaxCall(url, data) {
    debugger
    let result;

    try {

        result = await $.ajax({
            url: url,
            type: 'POST',
            data: data,
            contentType: 'application/json',
        });
        return result;
    } 
    
    catch (error) {
        console.error(error);
    }
}
