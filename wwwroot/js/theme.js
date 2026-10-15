(() => {
    "use strict";

    const storageKey = "hts-theme";
    const motionStorageKey = "hts-motion";
    const root = document.documentElement;
    const reducedMotion = window.matchMedia("(prefers-reduced-motion: reduce)");
    let motionChoice = "full";

    function syncThemeControl(button) {
        const dark = root.dataset.theme === "dark";
        const label = dark ? button.dataset.lightLabel : button.dataset.darkLabel;
        if (!label) return;
        button.setAttribute("aria-label", label);
        button.setAttribute("title", label);
        const visibleLabel = button.querySelector("[data-theme-label]");
        if (visibleLabel && visibleLabel.textContent !== label) visibleLabel.textContent = label;
    }

    function syncMotionControl(button) {
        const paused = root.dataset.motion === "paused";
        const label = reducedMotion.matches ? button.dataset.reducedLabel
            : paused ? button.dataset.resumeLabel : button.dataset.pauseLabel;
        button.disabled = reducedMotion.matches;
        button.setAttribute("aria-pressed", String(paused));
        if (label) {
            button.setAttribute("aria-label", label);
            button.setAttribute("title", label);
            const visibleLabel = button.querySelector("[data-motion-label]");
            if (visibleLabel && visibleLabel.textContent !== label) visibleLabel.textContent = label;
        }
    }

    function syncControls() {
        document.querySelectorAll("[data-theme-toggle]").forEach(syncThemeControl);
        document.querySelectorAll("[data-motion-toggle]").forEach(syncMotionControl);
    }

    function applyTheme(theme) {
        root.dataset.theme = theme === "dark" ? "dark" : "light";
        const meta = document.querySelector('meta[name="theme-color"]');
        if (meta) meta.content = root.dataset.theme === "dark" ? "#131512" : "#f5f3ee";
        syncControls();
    }

    function applyMotion() {
        root.dataset.motion = reducedMotion.matches || motionChoice === "paused" ? "paused" : "full";
        document.querySelectorAll("[data-motion-toggle]").forEach(syncMotionControl);
        document.dispatchEvent(new CustomEvent("hts:motionchange", {
            detail: { motion: root.dataset.motion, reduced: reducedMotion.matches }
        }));
    }

    let initialTheme = "light";
    try {
        initialTheme = localStorage.getItem(storageKey) || "light";
        motionChoice = localStorage.getItem(motionStorageKey) === "paused" ? "paused" : "full";
    } catch { /* The controls still work when storage is unavailable. */ }
    // Set preferences before the stylesheet loads to avoid a motion/theme flash.
    root.dataset.pageHidden = String(document.hidden);
    applyMotion();
    applyTheme(initialTheme);

    document.addEventListener("click", event => {
        if (!(event.target instanceof Element)) return;
        const motionButton = event.target.closest("[data-motion-toggle]");
        if (motionButton) {
            if (motionButton.disabled || reducedMotion.matches) return;
            motionChoice = root.dataset.motion === "paused" ? "full" : "paused";
            applyMotion();
            try { localStorage.setItem(motionStorageKey, motionChoice); } catch { /* Keep the in-memory choice. */ }
            return;
        }
        if (!event.target.closest("[data-theme-toggle]")) return;
        const theme = root.dataset.theme === "dark" ? "light" : "dark";
        applyTheme(theme);
        try { localStorage.setItem(storageKey, theme); } catch { /* The switch still works without persistence. */ }
    });

    window.addEventListener("storage", event => {
        if (event.key === storageKey || event.key === null) applyTheme(event.newValue);
        if (event.key === motionStorageKey || event.key === null) {
            motionChoice = event.newValue === "paused" ? "paused" : "full";
            applyMotion();
        }
    });

    reducedMotion.addEventListener("change", applyMotion);
    document.addEventListener("visibilitychange", () => {
        root.dataset.pageHidden = String(document.hidden);
    });

    function observeControls() {
        syncControls();
        // Only synchronize added controls or their labels. Blazor text updates
        // elsewhere should not scan the page or restart an animation.
        new MutationObserver(records => {
            const controls = new Set();
            function collect(node) {
                if (!(node instanceof Element)) return;
                if (node.matches("[data-theme-toggle], [data-motion-toggle]")) controls.add(node);
                node.querySelectorAll("[data-theme-toggle], [data-motion-toggle]").forEach(button => controls.add(button));
            }
            for (const record of records) {
                if (record.type === "attributes") collect(record.target);
                else for (const node of record.addedNodes) collect(node);
            }
            for (const button of controls) {
                if (button.matches("[data-theme-toggle]")) syncThemeControl(button);
                if (button.matches("[data-motion-toggle]")) syncMotionControl(button);
            }
        }).observe(document.body, {
            childList: true, subtree: true, attributes: true,
            attributeFilter: ["data-light-label", "data-dark-label", "data-pause-label", "data-resume-label", "data-reduced-label"]
        });
    }

    if (document.readyState === "loading") document.addEventListener("DOMContentLoaded", observeControls, { once: true });
    else observeControls();
})();
