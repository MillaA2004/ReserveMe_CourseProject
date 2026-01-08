window.venueMap = (function () {
    let primaryLoadPromise;
    let legacyLoadPromise;

    function injectScript(src) {
        return new Promise((resolve, reject) => {
            const exists = Array.from(document.scripts).some(s => s.src === src);
            if (exists) {
                resolve();
                return;
            }

            const s = document.createElement("script");
            s.src = src;
            s.async = true;
            s.defer = true;
            s.onload = () => resolve();
            s.onerror = () => reject(new Error("Failed to load Google Maps JavaScript API."));
            document.head.appendChild(s);
        });
    }

    function loadMapsModern(apiKey) {
        if (window.google && window.google.maps && typeof google.maps.importLibrary === "function") {
            return Promise.resolve();
        }
        if (!primaryLoadPromise) {
            if (!apiKey) return Promise.reject(new Error("Google Maps API key is missing."));
            const src = `https://maps.googleapis.com/maps/api/js?key=${encodeURIComponent(apiKey)}&v=quarterly&loading=async`;
            primaryLoadPromise = injectScript(src);
        }
        return primaryLoadPromise;
    }

    function loadMapsLegacy(apiKey) {
        if (window.google && window.google.maps && typeof google.maps.Map === "function") {
            return Promise.resolve();
        }
        if (!legacyLoadPromise) {
            if (!apiKey) return Promise.reject(new Error("Google Maps API key is missing."));
            const src = `https://maps.googleapis.com/maps/api/js?key=${encodeURIComponent(apiKey)}&v=quarterly`;
            legacyLoadPromise = injectScript(src);
        }
        return legacyLoadPromise;
    }

    function parseCenter(lat, lng) {
        const parsedLat = typeof lat === "string" ? parseFloat(lat) : lat;
        const parsedLng = typeof lng === "string" ? parseFloat(lng) : lng;
        return { lat: parsedLat, lng: parsedLng };
    }

    async function init(apiKey, elementId, lat, lng, title) {
        if (!apiKey || typeof apiKey !== "string" || apiKey.trim() === "") {
            console.error("[venueMap] Missing Google Maps API key.");
            return;
        }
        const el = document.getElementById(elementId);
        if (!el) {
            console.warn("[venueMap] Map container not found:", elementId);
            return;
        }

        const center = parseCenter(lat, lng);
        if (!Number.isFinite(center.lat) || !Number.isFinite(center.lng)) {
            console.error("[venueMap] Invalid coordinates:", lat, lng);
            return;
        }

        try {
            await loadMapsModern(apiKey);
            if (google.maps && typeof google.maps.importLibrary === "function") {
                const { Map } = await google.maps.importLibrary("maps");

                const map = new Map(el, {
                    center,
                    zoom: 15,
                    mapTypeControl: false,
                    streetViewControl: false,
                    fullscreenControl: false
                });

                console.log("[venueMap] Creating default marker (modern) at", center);
                const marker = new google.maps.Marker({
                    map,
                    position: center,
                    title: title || "Venue"
                });
                console.log("[venueMap] Marker created (modern):", marker);

                el.__gm_instance__ = map;
                return; // success
            }
        } catch (e) {
            console.warn("[venueMap] Modern load failed, will try legacy. Reason:", e);
        }

        try {
            await loadMapsLegacy(apiKey);

            if (!(google.maps && typeof google.maps.Map === "function")) {
                console.error("[venueMap] Legacy API loaded but google.maps.Map is unavailable.");
                return;
            }

            const map = new google.maps.Map(el, {
                center,
                zoom: 15,
                mapTypeControl: false,
                streetViewControl: false,
                fullscreenControl: false
            });

            console.log("[venueMap] Creating default marker (legacy) at", center);
            const marker = new google.maps.Marker({
                position: center,
                map,
                title: title || "Venue"
            });
            console.log("[venueMap] Marker created (legacy):", marker);

            el.__gm_instance__ = map;
        } catch (err) {
            console.error("[venueMap] Legacy load also failed:", err);
        }
    }

    return { init };
})();
