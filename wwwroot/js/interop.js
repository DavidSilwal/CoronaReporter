function foo()
{
    window.alert("test");
}

var notificationShowing = false;

function notifyNewPositiveLabReport()
{
    if (!("Notification" in window) || notificationShowing)
        return;

    var showNotification = false;
    
    if (Notification.permission === "granted")
        showNotification = true;
    else if (Notification.permission !== "denied") {
        Notification.requestPermission().then(function (permission) {
            // If the user accepts, let's create a notification
            if (permission === "granted")
                showNotification = true;
        });
    }

    if (showNotification) {
        notificationShowing = true;
        var notification = new Notification("Hi there!");
        notification.onclose = function() {
            notificationShowing = false;
        };
    }
}