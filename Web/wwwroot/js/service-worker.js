self.addEventListener("install", function (event) {
	console.log("[ServiceWorker] Install");

	self.skipWaiting();
});

self.addEventListener("activate", (event) => {
	console.log("[ServiceWorker] Activate");
	event.waitUntil(
		(async () => {
			if ("navigationPreload" in self.registration) {
				await self.registration.navigationPreload.enable();
			}
		})()
	);

	// Tell the active service worker to take control of the page immediately.
	self.clients.claim();
});

self.addEventListener("fetch", function (event) {
	if (event.request.mode === "navigate") {
		event.respondWith(
			(async () => {
				try {
					const preloadResponse = await event.preloadResponse;
					if (preloadResponse) {
						return preloadResponse;
					}

					const networkResponse = await fetch(event.request);
					return networkResponse;
				} catch (error) {
					console.log(
						"[Service Worker] Fetch failed; returning offline page instead.",
						error
					);

					const cache = await caches.open(CACHE_NAME);
					const cachedResponse = await cache.match(OFFLINE_URL);
					return cachedResponse;
				}
			})()
		);
	}
});
self.addEventListener('push', function (e) {
	let payload;
	try {
		const { body, title } = JSON.parse(e.data?.text());
		payload = { body, title };
	} catch {
		return;
	}


	var options = {
		body: payload.body,
        icon: "assets/icon-512.png",
        data: {
            dateOfArrival: Date.now()
        },
        actions: [
            {
                action: "explore", title: "Open",
                icon: "asstes/contact.svg"
            },
            {
                action: "close", title: "Close",
                icon: "assets/gallery.svg"
            },
        ]
    };
	e.waitUntil(
		self.registration.showNotification(payload.title, options)
    );
});

self.addEventListener('notificationclick', function (e) {
	e.notification.close();
	if (e.action !== "close") {
		clients.openWindow(e.request.url);
	}
});