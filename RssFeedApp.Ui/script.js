// Theme switcher logic
const themeToggle = document.getElementById('themeToggle');
const body = document.body;
const header = document.querySelector('header');

themeToggle.addEventListener('change', () => {
    console.log('Theme toggled:', themeToggle.checked ? 'Light' : 'Dark');
    body.classList.toggle('light', themeToggle.checked);
    body.classList.toggle('dark', !themeToggle.checked);
    header.classList.toggle('light', themeToggle.checked);
    header.classList.toggle('dark', !themeToggle.checked);
});

// RSS feed fetcher logic
document.getElementById('fetchBtn').addEventListener('click', async () => {
    const url = document.getElementById('rssUrl').value;
    console.log('Fetch button clicked. URL:', url);
    const feedContainer = document.getElementById('feed');
    feedContainer.innerHTML = '';

    try {
        console.log('Fetching RSS feed...');
        await fetch(url).then(response => {
            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }

            var json = JSON.parse(JSON.stringify(response));
            console.log('JSON:', json);

            
            
            return json;
        });

        // console.log('Response status:', response.status);

        // if (!response.ok) throw new Error(`HTTP error! status: ${response.status}`);

        // const json = await response.json();
        // console.log('RSS feed fetched successfully.');


        // console.log('JSON:', json);
        // const parser = new DOMParser();
        // const json = parser.parseFromString(text, 'application/json');

        // const items = json.querySelectorAll('item');
        // console.log(`Number of items found: ${items.length}`);

        // items.forEach(item => {
        //     const title = item.querySelector('title')?.textContent;
        //     const link = item.querySelector('link')?.textContent;
        //     console.log('Processing item:', { title, link });

        //     if (title && link) {
        //         const li = document.createElement('li');
        //         const a = document.createElement('a');
        //         a.href = link;
        //         a.textContent = title;
        //         a.target = '_blank';
        //         li.appendChild(a);
        //         feedContainer.appendChild(li);
        //     }
        // });

        // if (items.length === 0) {
        //     console.warn('No items found in the feed.');
        //     feedContainer.innerHTML = '<li>No items found in the feed.</li>';
        // }
    } catch (error) {
        console.error('Error fetching RSS feed:', error);
        feedContainer.innerHTML = `<li>Error: ${error.message}</li>`;
    }
});