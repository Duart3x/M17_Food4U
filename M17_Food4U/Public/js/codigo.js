function returnMain(url) {
    window.setTimeout(
        function () {
            window.location.href = url;
        }, 3000
    );
}

function ShowCartNotification(title,message,type) {
    $.toast({
        text: message,
        heading: title,
        icon: type,
        showHideTransition: 'fade',
        allowToastClose: false,
        hideAfter: 1000,
        stack: 10,
        position: 'bottom-left',
        textAlign: 'left',
        loader: false,
        loaderBg: '#9EC600',
        beforeHide: function () {
            window.location.href = window.location.href
        }
    });
}

function ShowNotification(title, message, type) {
    $.toast({
        text: message,
        heading: title,
        icon: type,
        showHideTransition: 'fade',
        allowToastClose: false,
        hideAfter: 3000,
        stack: 10,
        position: 'top-right',
        textAlign: 'left',
        loader: false,
        loaderBg: '#9EC600'
    });
}