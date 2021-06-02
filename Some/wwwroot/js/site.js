// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function UnHide(elem) {
    if (elem.innerHTML.charCodeAt(0) === 9658) {
        elem.innerHTML = '&#9660;'
        elem.parentNode.parentNode.parentNode.className = '';
    } else {
        elem.innerHTML = '&#9658;'
        elem.parentNode.parentNode.parentNode.className = 'cl';
    }
    return false;
}