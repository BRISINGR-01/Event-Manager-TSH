if ('serviceWorker' in navigator) {
    window.addEventListener("DOMContentLoaded", () => {
        navigator.serviceWorker.register('/js/service-worker.js')
            .then((registration) => {
                if (Notification.permission === "blocked") return;

                return registration.pushManager.getSubscription()
                    .then(async (subscription) => {
                        //if (subscription) return subscription;

                        return registration.pushManager.subscribe({
                            userVisibleOnly: true,
                            applicationServerKey: document.getElementById("applicationServerKey").innerText
                        }).then((sub) => {
                            fetch("/subscribe-to-notifications", {
                                method: "POST",
                                body: JSON.stringify({
                                    userId: window.userId,
                                    endpoint: sub.endpoint,
                                    auth: arrayBufferToBase64(sub.getKey("auth")),
                                    p256dh: arrayBufferToBase64(sub.getKey("p256dh")),
                                })
                            });
                        })
                    });
            }).then(() => {
                window.location.pathname = "/Pages/Events/List";
            })
    });
}

function arrayBufferToBase64(buffer) {
    var binary = '';
    var bytes = new Uint8Array(buffer);
    var len = bytes.byteLength;
    for (var i = 0; i < len; i++) {
        binary += String.fromCharCode(bytes[i]);
    }
    return window.btoa(binary);
}