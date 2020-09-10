// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
fetch('./home/AsyncGood?seconds=6')
    .then(function (response) {
        handleResponse(response);
    });

for (var i = 0; i < 200; i++) {
    fetch('./home/AsyncGood?seconds=1')
        .then(function (response) {
            handleResponse(response);
        });
}

fetch('./home/AsyncBad?seconds=9')
    .then(function (response) {
        handleResponse(response);
    });

fetch('./home/AsyncBad?seconds=2')
    .then(function (response) {
        handleResponse(response);
    });

function handleResponse(response, msg) {
    response.json().then(function (data) {
        console.info(`got async response (${msg}) ${JSON.stringify(data)}`);
    });
}