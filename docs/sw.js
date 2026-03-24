// Boom Chain Service Worker for Push Notifications
const CACHE_NAME = 'boomchain-v2';

self.addEventListener('install', function(event) {
  self.skipWaiting();
});

self.addEventListener('activate', function(event) {
  event.waitUntil(clients.claim());
});

self.addEventListener('push', function(event) {
  var data = event.data ? event.data.json() : {};
  var title = data.title || 'Boom Chain';
  var options = {
    body: data.body || 'Your daily rewards are waiting!',
    icon: data.icon || 'data:image/svg+xml,<svg xmlns=%22http://www.w3.org/2000/svg%22 viewBox=%220 0 100 100%22><text y=%22.9em%22 font-size=%2290%22>\u{1F52C}</text></svg>',
    badge: data.badge || 'data:image/svg+xml,<svg xmlns=%22http://www.w3.org/2000/svg%22 viewBox=%220 0 100 100%22><text y=%22.9em%22 font-size=%2290%22>\u{1F4A5}</text></svg>',
    vibrate: [200, 100, 200],
    data: { url: data.url || './' },
    actions: [
      { action: 'play', title: 'Play Now' },
      { action: 'dismiss', title: 'Later' }
    ]
  };
  event.waitUntil(self.registration.showNotification(title, options));
});

self.addEventListener('notificationclick', function(event) {
  event.notification.close();
  if (event.action === 'dismiss') return;
  event.waitUntil(
    clients.matchAll({ type: 'window' }).then(function(clientList) {
      for (var i = 0; i < clientList.length; i++) {
        if (clientList[i].url.includes('BoomChain') && 'focus' in clientList[i]) {
          return clientList[i].focus();
        }
      }
      if (clients.openWindow) {
        return clients.openWindow(event.notification.data.url || './');
      }
    })
  );
});

// Periodic background sync for daily reminders
self.addEventListener('periodicsync', function(event) {
  if (event.tag === 'daily-reminder') {
    event.waitUntil(
      self.registration.showNotification('Boom Chain', {
        body: '\u{1F3B0} Your daily spin is ready! Collect free coins!',
        icon: 'data:image/svg+xml,<svg xmlns=%22http://www.w3.org/2000/svg%22 viewBox=%220 0 100 100%22><text y=%22.9em%22 font-size=%2290%22>\u{1F52C}</text></svg>',
        vibrate: [200, 100, 200],
        data: { url: './' }
      })
    );
  }
});
