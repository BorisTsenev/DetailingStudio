// Presentation only: persisted theme and accessible navbar toggle.
(function () {
    const toggle = document.getElementById('themeToggle');
    function updateToggle() {
        const dark = document.documentElement.dataset.bsTheme === 'dark';
        toggle.setAttribute('aria-label', dark ? 'Switch to light theme' : 'Switch to dark theme');
        toggle.title = dark ? 'Switch to light theme' : 'Switch to dark theme';
        toggle.querySelector('[aria-hidden]').textContent = dark ? '\u2600' : '\u263e';
    }
    toggle.addEventListener('click', function () {
        const theme = document.documentElement.dataset.bsTheme === 'dark' ? 'light' : 'dark';
        document.documentElement.dataset.bsTheme = theme;
        try { localStorage.setItem('detailing-theme', theme); } catch (_) { /* Theme still works without storage. */ }
        updateToggle();
        document.dispatchEvent(new Event('studio:theme'));
    });
    updateToggle();
})();
