const themeToggle = document.getElementById('themeToggle');
const themeLabel = document.getElementById('themeLabel');
const body = document.body;
const header = document.querySelector('header');

themeToggle.addEventListener('change', () => {
    themeLabel.textContent = themeToggle.checked ? 'Light' : 'Dark';
    body.classList.toggle('light', themeToggle.checked);
    body.classList.toggle('dark', !themeToggle.checked);
    header.classList.toggle('light', themeToggle.checked);
    header.classList.toggle('dark', !themeToggle.checked);
});
