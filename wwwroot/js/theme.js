// Set the initial theme before Bootstrap renders to prevent a color flash.
(function () {
    let theme;
    try { theme = localStorage.getItem('detailing-theme'); } catch (_) { /* Storage may be disabled. */ }
    if (theme !== 'light' && theme !== 'dark') {
        theme = window.matchMedia('(prefers-color-scheme: dark)').matches ? 'dark' : 'light';
    }
    document.documentElement.dataset.bsTheme = theme;
})();
