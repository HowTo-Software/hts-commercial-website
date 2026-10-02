(() => {
    "use strict";

    const storageKey = "hts-theme";
    const root = document.documentElement;

    function syncControls() {
        const dark = root.dataset.theme === "dark";
        for (const button of document.querySelectorAll("[data-theme-toggle]")) {
            const label = dark ? button.dataset.lightLabel : button.dataset.darkLabel;
            button.setAttribute("aria-label", label);
            button.setAttribute("title", label);
        }
    }

    function applyTheme(theme) {
        root.dataset.theme = theme === "dark" ? "dark" : "light";
        const meta = document.querySelector('meta[name="theme-color"]');
        if (meta) meta.content = root.dataset.theme === "dark" ? "#131512" : "#f5f3ee";
        syncControls();
    }

    let initialTheme = "light";
    try { initialTheme = localStorage.getItem(storageKey) || "light"; } catch { /* Storage may be unavailable. */ }
    applyTheme(initialTheme);

    document.addEventListener("click", event => {
        if (!(event.target instanceof Element) || !event.target.closest("[data-theme-toggle]")) return;
        const theme = root.dataset.theme === "dark" ? "light" : "dark";
        applyTheme(theme);
        try { localStorage.setItem(storageKey, theme); } catch { /* The switch still works without persistence. */ }
    });

    window.addEventListener("storage", event => {
        if (event.key === storageKey) applyTheme(event.newValue);
    });

    function observeControls() {
        syncControls();
        new MutationObserver(syncControls).observe(document.body, { childList: true, subtree: true });
    }

    if (document.readyState === "loading") document.addEventListener("DOMContentLoaded", observeControls, { once: true });
    else observeControls();
})();
