(() => {
    "use strict";

    const motionPreference = window.matchMedia("(prefers-reduced-motion: reduce)");
    const finePointer = window.matchMedia("(hover: hover) and (pointer: fine)");
    const registered = new WeakSet();
    let scrollFrame = 0;
    let revealObserver;

    if ("IntersectionObserver" in window) {
        revealObserver = new IntersectionObserver(entries => {
            for (const entry of entries) {
                if (!entry.isIntersecting) continue;
                entry.target.classList.add("is-visible");
                revealObserver.unobserve(entry.target);
            }
        }, { threshold: 0.08, rootMargin: "0px 0px -20px 0px" });
    }

    function registerReveal(element) {
        if (registered.has(element)) return;
        registered.add(element);
        if (motionPreference.matches || !revealObserver) {
            element.classList.add("is-visible");
            return;
        }
        element.classList.add("reveal-pending");
        revealObserver.observe(element);
    }

    function registerTree(root) {
        if (!(root instanceof Element)) return;
        if (root.matches("[data-reveal]")) registerReveal(root);
        root.querySelectorAll("[data-reveal]").forEach(registerReveal);
    }

    function updateScroll() {
        scrollFrame = 0;
        const header = document.querySelector(".site-header");
        if (!header) return;
        const maximum = document.documentElement.scrollHeight - window.innerHeight;
        const progress = maximum > 0 ? Math.min(1, Math.max(0, window.scrollY / maximum)) : 0;
        let active = "home";
        for (const section of document.querySelectorAll("main > section[id]")) {
            if (section.getBoundingClientRect().top <= 165) active = section.id;
        }
        header.style.setProperty("--reading-progress", progress.toFixed(4));
        for (const link of header.querySelectorAll(".desktop-nav a")) {
            if (link.hash === "#" + active) link.setAttribute("aria-current", "location");
            else link.removeAttribute("aria-current");
        }
    }

    function scheduleScroll() {
        if (!scrollFrame) scrollFrame = window.requestAnimationFrame(updateScroll);
    }

    const mutationObserver = new MutationObserver(records => {
        for (const record of records) {
            for (const node of record.addedNodes) registerTree(node);
            for (const node of record.removedNodes) {
                if (!(node instanceof Element) || !revealObserver) continue;
                if (node.matches("[data-reveal]")) revealObserver.unobserve(node);
                node.querySelectorAll("[data-reveal]").forEach(element => revealObserver.unobserve(element));
            }
        }
        scheduleScroll();
    });

    document.addEventListener("click", event => {
        if (!(event.target instanceof Element)) return;
        const openButton = event.target.closest("[data-open-dialog]");
        if (openButton) {
            const dialog = document.getElementById(openButton.dataset.openDialog);
            if (dialog instanceof HTMLDialogElement && !dialog.open) dialog.showModal();
        }
        const closeButton = event.target.closest("[data-close-dialog]");
        if (closeButton) closeButton.closest("dialog")?.close();
        if (event.target instanceof HTMLDialogElement && event.target.open) {
            const rect = event.target.getBoundingClientRect();
            if (event.clientX < rect.left || event.clientX > rect.right ||
                event.clientY < rect.top || event.clientY > rect.bottom) event.target.close();
        }
        const menu = document.querySelector(".mobile-menu[open]");
        if (menu && (!menu.contains(event.target) || event.target.closest(".mobile-menu-panel a"))) {
            menu.removeAttribute("open");
        }
    });

    document.addEventListener("change", event => {
        if (!(event.target instanceof HTMLSelectElement) || !event.target.matches("[data-language-select]")) return;
        if (!["pt-BR", "en", "es"].includes(event.target.value)) return;
        const returnUrl = window.location.pathname + window.location.search + window.location.hash;
        window.location.assign("/culture?culture=" + encodeURIComponent(event.target.value) + "&returnUrl=" + encodeURIComponent(returnUrl));
    });

    document.addEventListener("keydown", event => {
        if (event.key !== "Escape") return;
        const menu = document.querySelector(".mobile-menu[open]");
        if (menu) {
            menu.removeAttribute("open");
            menu.querySelector("summary")?.focus();
        }
    });

    let tiltFrame = 0;
    let tiltTarget;
    document.addEventListener("pointermove", event => {
        if (motionPreference.matches || !finePointer.matches || !(event.target instanceof Element)) return;
        const canvas = event.target.closest("[data-tilt]");
        if (!canvas) return;
        const rect = canvas.getBoundingClientRect();
        const x = Math.max(-1, Math.min(1, ((event.clientX - rect.left) / rect.width - 0.5) * 2));
        const y = Math.max(-1, Math.min(1, ((event.clientY - rect.top) / rect.height - 0.5) * 2));
        if (tiltFrame) cancelAnimationFrame(tiltFrame);
        tiltTarget = canvas;
        tiltFrame = requestAnimationFrame(() => {
            if (motionPreference.matches || !canvas.isConnected) { tiltFrame = 0; return; }
            canvas.style.setProperty("--tilt-x", (-y * 2).toFixed(2) + "deg");
            canvas.style.setProperty("--tilt-y", (x * 3).toFixed(2) + "deg");
            tiltFrame = 0;
        });
    }, { passive: true });

    document.addEventListener("pointerout", event => {
        if (!tiltTarget || !(event.target instanceof Element)) return;
        const canvas = event.target.closest("[data-tilt]");
        if (canvas !== tiltTarget || (event.relatedTarget instanceof Node && canvas.contains(event.relatedTarget))) return;
        if (tiltFrame) cancelAnimationFrame(tiltFrame);
        tiltFrame = 0;
        canvas.style.removeProperty("--tilt-x");
        canvas.style.removeProperty("--tilt-y");
        tiltTarget = undefined;
    }, { passive: true });

    motionPreference.addEventListener("change", () => {
        if (!motionPreference.matches) return;
        document.querySelectorAll("[data-reveal]").forEach(element => {
            element.classList.add("is-visible");
            revealObserver?.unobserve(element);
        });
        document.querySelectorAll("[data-tilt]").forEach(element => {
            element.style.removeProperty("--tilt-x");
            element.style.removeProperty("--tilt-y");
        });
    });

    window.hts = {
        focusInvalidForm() {
            const invalid = document.querySelector("#quote .invalid, #quote [aria-invalid='true']");
            if (invalid) {
                const field = invalid.matches("input, select, textarea") ? invalid : invalid.querySelector("input, select, textarea");
                (field || invalid).focus();
                return;
            }
            // Checkbox sets use a fieldset, rather than a Blazor InputBase.
            const group = Array.from(document.querySelectorAll("#quote .wizard-choice-group"))
                .find(element => element.querySelector(".validation-message"));
            group?.querySelector("input")?.focus();
        },
        focusWizardStep() {
            document.querySelector("#wizard-step-heading")?.focus({ preventScroll: true });
            document.querySelector("#quote .quote-panel")?.scrollIntoView({ behavior: "instant", block: "start" });
        },
        downloadBrief(text) {
            const url = URL.createObjectURL(new Blob(["\ufeff", text], { type: "text/plain;charset=utf-8" }));
            const link = document.createElement("a");
            link.href = url;
            link.download = "briefing-hts.txt";
            document.body.appendChild(link);
            link.click();
            link.remove();
            window.setTimeout(() => URL.revokeObjectURL(url), 1000);
        }
    };

    registerTree(document.body);
    mutationObserver.observe(document.body, { childList: true, subtree: true });
    window.addEventListener("scroll", scheduleScroll, { passive: true });
    window.addEventListener("resize", scheduleScroll, { passive: true });
    if ("ResizeObserver" in window) new ResizeObserver(scheduleScroll).observe(document.body);
    scheduleScroll();
})();
